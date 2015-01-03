using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataGridProgressBarTest;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using KTVServerApp.Script.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace KTVServerApp
{
    public partial class UploadSongV2 : Form
    {
        private delegate void GUIThread();
        private delegate void SetProgress(long current, long total);
        
        //private List<string> lstheader = null;
        private SqlConnection connection;
        private int totalsong;
        private bool ispause;
        private GUIThread gui;
        private ConfigurationData data;
        private int addcolindex = 0;
        private string addcolname = "";

        public UploadSongV2()
        {
            InitializeComponent();
           
        }
        private int? GetIDColumn(string tablename,string columnname,string value)
        {
            try
            {
                using (SqlDataReader reader = SqlControl.SelectData("Select * From " + tablename + " Where " + columnname + "='" + value + "'", connection))
                {
                    reader.Read();
                    return (int)reader[0];
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private void TransferSong(int nextindex)
        {
            try
            {
                if (nextindex < dgvSong.Rows.Count)
                {
                    string original = dgvSong.Rows[nextindex].Cells["Path"].Value.ToString();
                    string destination = Path.Combine(data.FolderName, Path.GetFileNameWithoutExtension(dgvSong.Rows[nextindex].Cells["Song's Name"].Value.ToString()) + ".DAT");
                    FileStream input = new FileStream(original, FileMode.Open);
                    FileStream output = new FileStream(destination, FileMode.Create);
                    CopyStream(nextindex, input, output);
                }
                else
                {
                    gui = () =>
                    {
                        btnBrowse.Enabled = false;
                        btnPause.Enabled = false;
                        btnUploadSong.Enabled = false;
                        btnClear.Enabled = true;
                    };
                    this.Invoke(gui);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void CopyStream(int rnd,FileStream input, FileStream output)
        {
            SetProgress set = (current, total) =>
            {
                long value = current * 100 / total;
                DataGridViewRow row = dgvSong.Rows[rnd];
                int progress;
                if (row.Cells["Upload Progress"].Value == null)
                {
                    progress = 0;
                }
                else
                {
                    progress = (int)value;
                }
                row.Cells["Upload Progress"].Value = progress;
            };
            new TaskFactory().StartNew(() =>
            {
                byte[] buffer = new byte[1024];
                int len;
                while (true)
                {
                    if (!ispause)
                    {
                        if ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, len);
                            this.Invoke(set, output.Length, input.Length);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                output.Close();
                input.Close();
                output = null;
                input = null;
                GC.Collect();
                //this.Invoke(nextsong);
                //tsInfo.Text = (rnd + 1) + " songs completed of " + totalsong + " songs";
                InsertData(rnd);
                TransferSong(++rnd);
            });

        }
        private int SetSongNumber()
        {
            using (SqlDataReader reader = SqlControl.SelectData("Select * From Song", connection))
            {
                if (!reader.HasRows)
                {
                    return 1;
                }
                else
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        i++;
                    }
                    return i + 1;
                }
            }
        }
        private string GenerateSongNumber(string number,int total)
        {
            int count = number.Length;
            if (count >= total)
            {
                return number;
            }
            else
            {
                string result = "";
                for (int i = 0; i < (total - count); i++)
                {
                    result += "0";
                }
                result += number;
                return result;
            }
        }
        private string GetShortcut(int? id)
        {
            if (id.HasValue)
            {
                using (SqlDataReader reader = SqlControl.SelectData("Select Shortcut From Country Where CID=" + id, connection))
                {
                    reader.Read();
                    return reader.GetString(0);
                }
            }
            else
            {
                return "";
            }
        }
        private void InsertData(int ind)
        {
            DataGridViewRow row = dgvSong.Rows[ind];
            int? language = GetIDColumn("Country", "EnglishName", (string)row.Cells["Language"].Value);
            string songnumber = GetShortcut(language) + GenerateSongNumber(SetSongNumber() + "", 7);
            string songname =(string) row.Cells["Song's Name"].Value;
            string path = "\\\\"+data.IpAddress+@"\"+data.LastPath;
            string pinyin = (string)row.Cells["PinYin"].Value;
            int? num = null;
            if (row.Cells["NumberOfWord"].Value == null) num = (int?)row.Cells["NumberOfWord"].Value;
            else num = int.Parse(row.Cells["NumberOfWord"].Value+"");
            int? vol = null;
            if (row.Cells["Volume"].Value == null) vol = (int?)row.Cells["Volume"].Value;
            else vol = int.Parse(row.Cells["Volume"].Value+"");
            int? category = GetIDColumn("Category", "CategoryName", (string)row.Cells["Category"].Value);
            int? feat = GetIDColumn("Singer", "Singer_name", (string)row.Cells["Feat"].Value);
            //int? language = GetIDColumn("Country", "EnglishName",(string) row.Cells["Language"].Value);
            int? pro = GetIDColumn("Production", "ProductionName",(string) row.Cells["Production"].Value);
            int? album = GetIDColumn("Album", "AlbumName",(string) row.Cells["Album"].Value);
            int? singer = GetIDColumn("Singer", "Singer_name",(string) row.Cells["Singer"].Value);
            int issingle = 0;
            if (feat != null) issingle = 0;
            else issingle = 1;
         
            SqlControl.InsertData("Song", connection, SqlControl.Hash("SongNumber", songnumber
                                                                    ,"SongName", songname
                                                                    ,"Volume",vol
                                                                    ,"Feat",feat
                                                                    ,"Category",category
                                                                    ,"NumberOfWords",num
                                                                    ,"Pinyin",pinyin
                                                                    ,"Language",language
                                                                    ,"FilePath",path
                                                                    ,"Production",pro
                                                                    ,"Album",album
                                                                    , "IsSingle", issingle
                                                                    ,"Singer",singer
                                                                    ,"PopularCount",1));
        }
        private void AddProgressColumn(string headername)
        {
            DataGridViewProgressColumn dvpro = new DataGridViewProgressColumn();
            dvpro.HeaderText = headername;
            dvpro.Name = headername;
            dgvSong.Columns.Add(dvpro);
        }
        private void AddNormalColumn(string headername)
        {
            DataGridViewTextBoxColumn dvtext = new DataGridViewTextBoxColumn();
            dvtext.HeaderText = headername;
            dvtext.Name = headername;
            dgvSong.Columns.Add(dvtext);
        }
        private void AddComboBoxItem(string headername,string tablename="",string display="")
        {
            try
            {
                DataGridViewComboBoxColumn dvcombo = new DataGridViewComboBoxColumn();
                dvcombo.AutoComplete = true;
                dvcombo.HeaderText = headername;
                dvcombo.Name = headername;
                //dvcombo.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                dvcombo.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (tablename != "")
                {
                   GetDataSource(tablename, display,dvcombo);
                   // dvcombo.DisplayMember = display;
                }
                dgvSong.Columns.Add(dvcombo);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetDataSource(string tablename, string display,DataGridViewComboBoxColumn dv)
        {
            dv.Items.Clear();
            using (SqlDataReader reader = SqlControl.SelectData("Select "+display+" From " + tablename, connection))
            {
                while (reader.Read())
                {
                    dv.Items.Add(reader[0].ToString().Trim());
                }
                //dv.DataSource = reader;
            }
        }
        private void AddTestColumn(string headername)
        {
            DataGridViewColumn dvc = new DataGridViewColumn();
        }
        private void OpenAddingForm(string name, int index)
        {
            Form temp = null;
            string tablename = "";
            string displayname = "";
            switch (name)
            {
                case "Category": temp = new Category(); tablename = "Category"; displayname = "CategoryName"; break;
                case "Feat": temp = new Singer(); tablename = "Singer"; displayname = "Singer_name"; break;
                case "Singer": temp = new Singer(); tablename = "Singer"; displayname = "Singer_name"; break;
                case "Production": temp = new Production(); tablename = "Production"; displayname = "ProductionName"; break;
                case "Album": temp = new Album(); tablename = "Album"; displayname = "AlbumName"; break;
                case "Language": temp = new Country(); tablename = "Country"; displayname = "EnglishName"; break;
            }
            if (temp != null)
            {
                temp.ShowDialog();
                GetDataSource(tablename, displayname, (DataGridViewComboBoxColumn)dgvSong.Columns[index]);
            }

        }
        //private void CopySong(int rowind,string originalpath,string destinationpath)
        //{
        //    bool help;
        //    RefreshProgress t1 = (i) =>
        //    {
        //        DataGridViewRow row = dgvSong.Rows[rowind];
        //        int progress;
        //        if (row.Cells["Upload Progress"].Value == null)
        //        {
        //            progress = 0;
        //        }
        //        else
        //        {
        //            progress = (int)row.Cells["Upload Progress"].Value;
        //        }
                
        //        if (progress < 90)
        //        {
        //            progress += Convert.ToInt32(i);
        //        }
        //        else if (i < 0)
        //        {
        //            progress = 100;
        //        }
        //        row.Cells["Upload Progress"].Value = progress;
        //        dgvSong.Refresh();
        //    }; 
        //    Thread t = new Thread(new ThreadStart(() =>
        //    {
        //        float i = 0;
        //        help = false;
        //        while (!help)
        //        {
        //            i++;
        //            Thread.Sleep(1000);
        //            this.Invoke(t1, i);
        //        }
        //        //progressBar1.Value = 100;
        //        this.Invoke(t1, -1);
        //    }));
           
        //    Task task = new TaskFactory().StartNew(() =>
        //    {
        //        File.Copy(originalpath, destinationpath, true);
        //        help = true;
        //    });
        //    t.Start();
        //}
        
       
        #region GUI Event
        private void dgvSong_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 6)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (dgvSong.Rows.Count > 0)
                    {
                        string name = dgvSong.Columns[e.ColumnIndex].Name;
                        string temp = (string)dgvSong.Rows[0].Cells[name].EditedFormattedValue;
                        // MessageBox.Show(temp);
                        foreach (DataGridViewRow row in dgvSong.Rows)
                        {
                            row.Cells[name].Value = temp;
                        }
                    }
                }
                else
                {
                    //OpenAddingForm(dgvSong.Columns[e.ColumnIndex].Name,e.ColumnIndex);
                    addcolname = dgvSong.Columns[e.ColumnIndex].Name;
                    addcolindex = e.ColumnIndex;
                    cmsAdding.Show(Cursor.Position);
                }
            }
        }
      
        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvSong.Rows.Clear();
            btnUploadSong.Enabled = btnClear.Enabled = false;
            btnBrowse.Enabled = true;
            tsInfo.Text = "All Song was clear from the list";
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "Pause")
            {
                ispause = true;
                btnPause.Text = "Start";
            }
            else
            {
                ispause = false;
                btnPause.Text = "Pause";
            }
        }
        private void UploadSongV2_Load(object sender, EventArgs e)
        {
            data = ConfigurationData.Instance();
            connection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord,data.ServerName);
            //lstheader = new List<string> {"Language", "Album", "Category", "Production", "Vol"};
            //DataTable dt = new DataTable();

            AddNormalColumn("Path");
            AddProgressColumn("Upload Progress");
            AddNormalColumn("Song's Name");
            //AddNormalColumn("Upload Path");
            AddNormalColumn("PinYin");
            AddNormalColumn("NumberOfWord");
            AddNormalColumn("Volume");
            AddComboBoxItem("Category","Category","CategoryName");
            AddComboBoxItem("Language", "Country", "EnglishName");
            AddComboBoxItem("Production", "Production", "ProductionName");
            AddComboBoxItem("Album", "Album", "AlbumName");
            AddComboBoxItem("Singer", "Singer", "Singer_name");
            AddComboBoxItem("Feat", "Singer", "Singer_name");
            
           
            dgvSong.Columns["Path"].Visible = false;
            dgvSong.AllowUserToAddRows = false;
            //dgvSong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            btnBrowse.Enabled = true;
            btnClear.Enabled = btnUploadSong.Enabled = btnPause.Enabled = false;
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            //open.Filter = "Media File **.DAT)|*.dat|(*.MPG)|*.mpg|(*.flv)|*.flv|(*.mp4)|*.mp4";
            open.Title = "Select Song";
            if (open.ShowDialog() == DialogResult.OK)
            {
                foreach (string name in open.FileNames)
                {
                    int ind = dgvSong.Rows.Add();
                    DataGridViewRow row = dgvSong.Rows[ind];
                    row.Cells["Path"].Value = name;
                    row.Cells["Song's Name"].Value = System.IO.Path.GetFileName(name);
                }
                tsInfo.Text = open.FileNames.Length + " Songs Selected";
                totalsong = open.FileNames.Length;
                btnUploadSong.Enabled = true;
                btnClear.Enabled = true;
                btnBrowse.Enabled = false;
            }
        }
        private void btnUploadSong_click(object sender, EventArgs e)
        {
            btnClear.Enabled = btnBrowse.Enabled = btnUploadSong.Enabled = false;
            btnPause.Enabled = true;
            tsInfo.Text = "Starting Upload Songs";
            TransferSong(0);
        }
        #endregion

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenAddingForm(addcolname, addcolindex);
        }
    }
}
