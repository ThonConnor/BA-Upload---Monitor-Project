using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KTVServerApp.StoreData;
using System.Data.SqlClient;
using KTVServerApp.Script.Sql;
using System.IO;

namespace KTVServerApp
{
    public partial class Country : Form
    {

        private List<clsCountry> lst;
        private BindingSource bs;
        private SqlConnection connection;
        private clsCountry current;
        private ConfigurationData data;
        public Country()
        {
            InitializeComponent();
        }

        private void Country_Load(object sender, EventArgs e)
        {
            data = ConfigurationData.Instance();
            connection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
            InitializeForm();
        }
        private void InitializeForm()
        {
            EnableControl(false);
            lst = new List<clsCountry>();
            bs = new BindingSource(lst, null);
            dgvDisplay.DataSource = bs;
            
            LoadDataToDataGridView();
            dgvDisplay.Columns["P_Photo"].Visible = false;
            bs.ResetBindings(false);
            
        }
        private void EnableControl(bool enabled)
        {
            btnSave.Enabled = enabled;
            btnNew.Enabled = !enabled;
            btnClear.Enabled = enabled;
            pbCountry.Enabled = enabled;
            txtEnglishName.Enabled = enabled;
            txtRealName.Enabled = enabled;
            txtShortcut.Enabled = enabled;
        }
        private void LoadDataToDataGridView()
        {
            try
            {
                //lst = null;
                lst.Clear();
                
                // MessageBox.Show(connection.ToString());
                using (SqlDataReader reader = SqlControl.SelectData("SELECT * FROM Country", connection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Image img = null;
                            byte[] byteimg = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("Photo")))
                            {
                                byteimg = (byte[])reader["Photo"];
                                using (MemoryStream ms = new MemoryStream(byteimg))
                                {
                                    img = Image.FromStream(ms);
                                }
                            }
                            lst.Add(new clsCountry(reader.GetInt32(0) + "", reader.GetString(1), reader.GetString(2), img, reader.GetString(4)));

                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            SqlControl.TerminateConnection(connection);
        }
        private void SaveData()
        {
            try
            {

                string engname = txtEnglishName.Text;
                string real = txtRealName.Text;
                //MessageBox.Show(pbCountry.ImageLocation);
                byte[] img = ConvertImage(pbCountry.Image);
                string shortcut = txtShortcut.Text;
                SqlControl.InsertData("Country",connection,new string[]{"EnglishName","RealName","Photo","Shortcut"},new SqlControl.CommandParameter("@eng",SqlDbType.NChar,engname)
                                                          ,new SqlControl.CommandParameter("@real",SqlDbType.NChar,real)
                                                          ,new SqlControl.CommandParameter("@img",SqlDbType.Image,img)
                                                          ,new SqlControl.CommandParameter("@shortcut",SqlDbType.NVarChar,shortcut));

                LoadDataToDataGridView();
                bs.ResetBindings(false);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void EditData()
        {
            try
            {

                string engname = txtEnglishName.Text;
                string real = txtRealName.Text;
                //MessageBox.Show(pbCountry.ImageLocation);
                byte[] img = ConvertImage(pbCountry.Image);
                string shortcut = txtShortcut.Text;
                //bool t =SqlControl.InsertData("Country", connection, SqlControl.Hash("EnglishName", engname
                //                                                             ,"RealName",real
                //                                                           ,"Photo",img));
                //MessageBox.Show(t + "");

                SqlControl.UpdateData("Country",connection,new string[]{"EnglishName","RealName","Photo","Shortcut"},"WHERE CID=" + long.Parse(current.P_CountryId)
                                          ,new SqlControl.CommandParameter("@eng",SqlDbType.NChar,engname)
                                          ,new SqlControl.CommandParameter("@real",SqlDbType.NChar,real)
                                          ,new SqlControl.CommandParameter("@img",SqlDbType.Image,img)
                                          ,new SqlControl.CommandParameter("@shortcut",SqlDbType.NVarChar,shortcut));

                //string sql = "UPDATE Country SET EnglishName=@eng,RealName=@real,Photo=@img WHERE CID=" + long.Parse(current.P_CountryId);
                //SqlControl.StartConnection(connection);
                //SqlCommand cmd = new SqlCommand(sql, connection);
                //cmd.Parameters.Add("@eng", SqlDbType.NChar).Value = engname;
                //cmd.Parameters.Add("@real", SqlDbType.NChar).Value = real;
                //cmd.Parameters.Add("@img", SqlDbType.Image).Value = img;
                //cmd.ExecuteNonQuery();

                LoadDataToDataGridView();
                bs.ResetBindings(false);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private byte[] ConvertImage(Image img)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Bitmap bit = new Bitmap(img);
                bit.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            btnSave.Text = "Save";
        }
        private void pbCountry_Click(object sender, EventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            //img.Filter = "Image File (*.jpg|(*JPG)|*.png|(*.PNG)|*.gif|(*.GIF))";
            if (img.ShowDialog() == DialogResult.OK)
            {
                pbCountry.Image = Image.FromFile(img.FileName);
                pbCountry.ImageLocation = img.FileName;
            }   
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEnglishName.Text = "";
            txtRealName.Text = "";
            pbCountry.Image = null;
            txtShortcut.Text = "";
            EnableControl(false);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtEnglishName.Text))
            {
                MessageBox.Show("Please Input Data in English Name");
                return;
            }else if (String.IsNullOrEmpty(txtRealName.Text))
            {
                MessageBox.Show("Please Input Data in Real Name");
                return;
            }
            else if (String.IsNullOrEmpty(txtShortcut.Text))
            {
                MessageBox.Show("Please Input Data in Shortcut");
                return;
            }
            if (btnSave.Text == "Save")
            {
                SaveData();
            }
            else
            {
                EditData();
            }
        }

        private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // MessageBox.Show(e.RowIndex + "");
            if (e.RowIndex >= 0)
            {
                EnableControl(true);
                current = (clsCountry)bs.Current;
                //MessageBox.Show(temp.P_CountryId);
                txtEnglishName.Text = current.P_RepresentName;
                txtRealName.Text = current.P_RealName;
                pbCountry.Image = current.P_Photo;
                txtShortcut.Text = current.P_Shortcut;
                btnSave.Text = "Update";
            }
         }
        
    }
}
