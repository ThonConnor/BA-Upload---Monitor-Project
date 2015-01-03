using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KTVServerApp.Script.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
namespace KTVServerApp
{
    public partial class SongModified : Form
    {
        private delegate void SetProgress(long current,long total);
        private delegate void GUIThread();
        private int songid = 0;
        private SqlConnection connection;
        private ConfigurationData data;
        private string originalname = "";
        private bool isreplace = false;
        public SongModified(int songid)
        {
            this.songid = songid;
            InitializeComponent();
            
        }

        private void SongModified_Load(object sender, EventArgs e)
        {
            Initialize();
        }
        private void Initialize()
        {
            data = ConfigurationData.Instance();
            connection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Song Where SID=" + songid, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tbSong");

            txtSongNumber.Text = ds.Tables["tbSong"].Rows[0]["SongNumber"].ToString();
            txtSongName.Text = ds.Tables["tbSong"].Rows[0]["SongName"].ToString();
            originalname = txtSongName.Text;
            txtPinYin.Text = ds.Tables["tbSong"].Rows[0]["Pinyin"].ToString();
            txtNumberWord.Text = ds.Tables["tbSong"].Rows[0]["NumberOfWords"].ToString();
            txtVolume.Text = ds.Tables["tbSong"].Rows[0]["Volume"].ToString();

            InitComboBox(cbCategory, "Category", "CateID", "CategoryName");
            InitComboBox(cboAlbum, "Album", "ABID", "AlbumName");
            InitComboBox(cboLanguage, "Country", "CID", "EnglishName");
            InitComboBox(cboProduction, "Production", "PID", "ProductionName");
            InitComboBox(cboSinger, "Singer", "SGID", "Singer_name");
            InitComboBox(cboFeat, "Singer", "SGID", "Singer_name");

            SetComboBox(cbCategory, ds.Tables["tbSong"].Rows[0]["Category"].ToString());
            SetComboBox(cboAlbum, ds.Tables["tbSong"].Rows[0]["Album"].ToString());
            SetComboBox(cboLanguage, ds.Tables["tbSong"].Rows[0]["Language"].ToString());
            SetComboBox(cboProduction, ds.Tables["tbSong"].Rows[0]["Production"].ToString());
            SetComboBox(cboSinger, ds.Tables["tbSong"].Rows[0]["Singer"].ToString());
            SetComboBox(cboFeat, ds.Tables["tbSong"].Rows[0]["Feat"].ToString());
        }
        private void InitComboBox(ComboBox cbo,string tablename,string value,string display)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From "+tablename, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, tablename);
            cbo.DataSource = ds.Tables[tablename];
            cbo.DisplayMember = display;
            cbo.ValueMember = value;
        }
        private void SetComboBox(ComboBox cbo,object id)
        {
           
            if (id == null || id=="")
            {
                cbo.Text = "";
                //MessageBox.Show(cbo.SelectedValue + "");
                //cbo.SelectedValue = null;
            }
            else
            {
                cbo.SelectedValue = id;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int? GetValue(TextBox txt)
        {
            if (txt.Text == "")
            {
                return null;
            }
            else
            {
                return int.Parse(txt.Text);
            }

        }
        private int? GetValueComboBox(ComboBox cbo)
        {
            if (cbo.Text == "")
            {
                return null;
            }
            else
            {
                return (int)cbo.SelectedValue;
            }
        }
        private void RenameSong()
        {
            string oldpath = Path.Combine(data.FolderName, originalname);
            string newpath = Path.Combine(data.FolderName, txtSongName.Text);
            if (File.Exists(oldpath) && !File.Exists(newpath))
            {
                File.Move(oldpath, newpath);
            }
        }
        private void ReplaceSong()
        {
           string oldpath = Path.Combine(data.FolderName, originalname);
           if (File.Exists(oldpath))
           {
               File.Delete(oldpath);
           }
        }
        
        private void CopyStream( FileStream input, FileStream output)
        {
            SetProgress set = (current, total) =>
            {
                long value = current * 100 / total;
                int progress;
                if (pbCopy.Value==null)
                {
                    progress = 0;
                }
                else
                {
                    progress = (int)value;
                }
                pbCopy.Value = progress;
            };
            GUIThread gui = () => { UpdateSong(); };
            new TaskFactory().StartNew(() =>
            {
                byte[] buffer = new byte[1024];
                int len;
                while (true)
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
                
                output.Close();
                input.Close();
                output = null;
                input = null;
                GC.Collect();
                this.Invoke(gui);
            });

        }
        private void UpdateSong()
        {
            if (isreplace)
            {
                ReplaceSong();
            }
            else
            {
                RenameSong();
            }
            string songnumber = txtSongNumber.Text;
            string songname = txtSongName.Text;
            string pinyin = txtPinYin.Text;
            int? num = GetValue(txtNumberWord);
            int? vol = GetValue(txtVolume);
            int? language = GetValueComboBox(cboLanguage);
            int? singer = GetValueComboBox(cboSinger);
            int? feat = GetValueComboBox(cboFeat);
            int? category = GetValueComboBox(cbCategory);
            int? album = GetValueComboBox(cboAlbum);
            // MessageBox.Show(singer + "");
            int? production = GetValueComboBox(cboProduction);
            int issingle = 0;
            if (feat == null) issingle = 1;
            SqlControl.UpdateData("Song", connection, SqlControl.Hash("SongNumber", songnumber,
                                                                        "SongName", songname,
                                                                        "Pinyin", pinyin,
                                                                        "Language", language,
                                                                        "NumberOfWords", num,
                                                                        "Singer", singer,
                                                                        "Feat", feat,
                                                                        "Category", category,
                                                                        "Volume", vol,
                                                                        "Album", album,
                                                                        "Production", production,
                                                                        "IsSingle", issingle), "SID=" + songid);
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNewSong.Text))
            {
                string original = txtNewSong.Text;
                string destination = Path.Combine(data.FolderName, Path.GetFileNameWithoutExtension(txtSongName.Text) + ".DAT");
                FileStream input = new FileStream(original, FileMode.Open);
                FileStream output = new FileStream(destination, FileMode.Create);
                CopyStream(input, output);
            }
            else
            {
                UpdateSong();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    txtNewSong.Text = file.FileName;
                    txtSongName.Text = Path.GetFileName(file.FileName);
                    isreplace = true;
                }
            }
        }
    }
}
