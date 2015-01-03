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
    public partial class Singer : Form
    {
        private List<clsSinger> lst_singer;
        private List<clsProduction> lst_pro;
        private BindingSource bs_singer;
        private BindingSource bs_pro;
        private SqlConnection connection;
        private clsSinger current;
        private ConfigurationData data;
        public Singer()
        {
            InitializeComponent();
        }

        private void Singer_Load(object sender, EventArgs e)
        {
            data = ConfigurationData.Instance();
            connection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
            InitializeForm();
        }
        private void InitializeForm()
        {
            EnableControl(false);
            lst_pro = new List<clsProduction>();
            lst_singer = new List<clsSinger>();
            bs_pro = new BindingSource(lst_pro, null);
            bs_singer = new BindingSource(lst_singer, null);
            cboProduction.DataSource = bs_pro;
            dgvDisplay.DataSource = bs_singer;
            LoadDataToDataGridView();
            dgvDisplay.Columns["P_Photo"].Visible = false;
            bs_singer.ResetBindings(false);
            bs_pro.ResetBindings(false);
        }
        private void EnableControl(bool enabled)
        {
            btnSave.Enabled = enabled;
            btnNew.Enabled = !enabled;
            btnClear.Enabled = enabled;
            pbSinger.Enabled = enabled;
            txtSingerName.Enabled = enabled;
            txtPinYin.Enabled = enabled;
            rdbMale.Enabled = rdbFemale.Enabled = enabled;
            cboProduction.Enabled = enabled;
        }
        private void LoadDataToDataGridView()
        {
            try
            {
                LoadDataToComboBox();
                //lst = null;
                lst_singer.Clear();
                // MessageBox.Show(connection.ToString());
                using (SqlDataReader reader = SqlControl.SelectData("SELECT * FROM Singer", connection))
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
                            lst_singer.Add(new clsSinger(reader.GetInt32(0) + "", reader.GetString(1), reader.GetString(2), GetProduction(reader.GetInt32(3)), reader.GetInt32(4), reader.GetString(5), img));
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
        private clsProduction GetProduction(int id)
        {
            foreach (clsProduction c in lst_pro)
            {
                if (int.Parse(c.P_ProductionID) == id)
                {
                    return c;
                }
            }
            return null;
        }
        private void LoadDataToComboBox()
        {
            try
            {
                lst_pro.Clear();
               
                using (SqlDataReader reader = SqlControl.SelectData("SELECT * FROM Production", connection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst_pro.Add(new clsProduction(reader.GetInt32(0) + "", reader.GetString(1), null, null));
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

        private void pbSinger_Click(object sender, EventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            //img.Filter = "Image File (*.jpg|(*JPG)|*.png|(*.PNG)|*.gif|(*.GIF))";
            if (img.ShowDialog() == DialogResult.OK)
            {
                pbSinger.Image = Image.FromFile(img.FileName);
            }   
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            btnSave.Text = "Save";
        }
        private void SaveData()
        {
            try
            {
                string proname = txtSingerName.Text;
                string gender = "";
                if (rdbMale.Checked)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                string pinyin = txtPinYin.Text;
                clsProduction coun = (clsProduction)bs_pro.Current;
                //MessageBox.Show(pbCountry.ImageLocation);
                byte[] img = ConvertImage(pbSinger.Image);

                SqlControl.InsertData("Singer", connection,new string[]{"Singer_name","Sex","Production","PopularCount","Pinyin","Photo"}, new SqlControl.CommandParameter("@singername", SqlDbType.NChar, proname)
                                                          ,new SqlControl.CommandParameter("@gender",SqlDbType.NChar,gender)
                                                          , new SqlControl.CommandParameter("@production", SqlDbType.Int, int.Parse(coun.P_ProductionID))
                                                          ,new SqlControl.CommandParameter("@count",SqlDbType.Int,1)
                                                          , new SqlControl.CommandParameter("@pinyin", SqlDbType.NChar, pinyin)
                                                          , new SqlControl.CommandParameter("@img", SqlDbType.Image, img
                                                           ));

                LoadDataToDataGridView();
                bs_singer.ResetBindings(false);
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
                string proname = txtSingerName.Text;
                string gender = "";
                if (rdbMale.Checked)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                string pinyin = txtPinYin.Text;
                clsProduction coun = (clsProduction)bs_pro.Current;
                //MessageBox.Show(pbCountry.ImageLocation);
                byte[] img = ConvertImage(pbSinger.Image);

                SqlControl.UpdateData("Singer", connection, new string[] { "Singer_Name", "Sex", "Production", "Pinyin", "Photo" }, "WHERE SGID=" + long.Parse(current.P_SingerID)
                                                          , new SqlControl.CommandParameter("@singername", SqlDbType.NChar, proname)
                                                          , new SqlControl.CommandParameter("@gender", SqlDbType.NChar, gender)
                                                          , new SqlControl.CommandParameter("@production", SqlDbType.Int, int.Parse(coun.P_ProductionID))
                                                          , new SqlControl.CommandParameter("@pinyin", SqlDbType.NChar, pinyin)
                                                          , new SqlControl.CommandParameter("@img", SqlDbType.Image, img));

                LoadDataToDataGridView();
                bs_singer.ResetBindings(false);
                btnClear_Click(this, new EventArgs());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                SaveData();
            }
            else
            {
                EditData();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSingerName.Text = "";
            txtPinYin.Text = "";
            pbSinger.Image = null;
            cboProduction.Text = "";
            rdbMale.Checked = true;
            EnableControl(false);
        }

        private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EnableControl(true);
                current = (clsSinger)bs_singer.Current;
                //MessageBox.Show(temp.P_CountryId);
                txtSingerName.Text = current.P_SingerName;
                txtPinYin.Text = current.P_Pinyin + "";
                cboProduction.SelectedItem = current.P_Pro;
                pbSinger.Image = current.P_Photo;
                btnSave.Text = "Update";
                //MessageBox.Show(current.P_Gender);
                if (current.P_Gender.Trim() == "Male")
                {
                    rdbMale.Checked = true;
                }
                else
                {
                    rdbFemale.Checked = true;
                }
            }
        }
       
    }

}
