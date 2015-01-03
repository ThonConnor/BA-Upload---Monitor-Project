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
    public partial class Album : Form
    {
        private List<clsAlbum> lst_album;
        private List<clsProduction> lst_pro;
        private BindingSource bs_album;
        private BindingSource bs_pro;
        private SqlConnection connection;
        private clsAlbum current;
        private ConfigurationData data;
        public Album()
        {
            InitializeComponent();
        }

        private void Album_Load(object sender, EventArgs e)
        {
            data = ConfigurationData.Instance();
            connection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
            InitializeForm();
        }
        private void InitializeForm()
        {
            EnableControl(false);
            lst_pro = new List<clsProduction>();
            lst_album = new List<clsAlbum>();
            bs_pro = new BindingSource(lst_pro, null);
            bs_album = new BindingSource(lst_album, null);
            cboProduction.DataSource = bs_pro;
            dgvDisplay.DataSource = bs_album;
            LoadDataToDataGridView();
            dgvDisplay.Columns["P_Photo"].Visible = false;
            bs_album.ResetBindings(false);
            bs_pro.ResetBindings(false);
        }
        private void EnableControl(bool enabled)
        {
            btnSave.Enabled = enabled;
            btnNew.Enabled = !enabled;
            btnClear.Enabled = enabled;
            pbAlbum.Enabled = enabled;
            txtAlbumName.Enabled = enabled;
            txtVol.Enabled = enabled;
            cboProduction.Enabled = enabled;
        }
        private void SaveData()
        {
            try
            {
                string proname = txtAlbumName.Text;
                int vol = int.Parse(txtVol.Text);
                clsProduction coun = (clsProduction)bs_pro.Current;
                //MessageBox.Show(pbCountry.ImageLocation);
                byte[] img = ConvertImage(pbAlbum.Image);

                SqlControl.InsertData("Album", connection, new string[] { "AlbumName","Photo","Vol","Production"}
                                                            , new SqlControl.CommandParameter("@albumname", SqlDbType.NChar, proname)
                                                          , new SqlControl.CommandParameter("@img", SqlDbType.Image, img)
                                                          , new SqlControl.CommandParameter("@vol", SqlDbType.Int, vol)
                                                          , new SqlControl.CommandParameter("@production", SqlDbType.Int, int.Parse(coun.P_ProductionID)));

                LoadDataToDataGridView();
                bs_album.ResetBindings(false);
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
                string albumname = txtAlbumName.Text;
                clsProduction cou = (clsProduction)bs_pro.Current;
                int vol = int.Parse(txtVol.Text);
                //MessageBox.Show(pbCountry.ImageLocation);
                byte[] img = ConvertImage(pbAlbum.Image);

                SqlControl.UpdateData("Album", connection, new string[] { "AlbumName", "Photo", "Vol","Production" }, "WHERE ABID=" + long.Parse(current.P_AlbumID)
                                        , new SqlControl.CommandParameter("@albumname", SqlDbType.NChar, albumname)
                                        , new SqlControl.CommandParameter("@img", SqlDbType.Image, img)
                                        ,new SqlControl.CommandParameter("@vol",SqlDbType.Int,vol)
                                        , new SqlControl.CommandParameter("@Production", SqlDbType.Int, long.Parse(cou.P_ProductionID)));

                LoadDataToDataGridView();
                bs_album.ResetBindings(false);
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
                            lst_pro.Add(new clsProduction(reader.GetInt32(0)+"",reader.GetString(1),null,null));
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
        private void LoadDataToDataGridView()
        {
            try
            {
                LoadDataToComboBox();
                //lst = null;
                lst_album.Clear();
                // MessageBox.Show(connection.ToString());
                using (SqlDataReader reader = SqlControl.SelectData("SELECT * FROM Album", connection))
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
                            lst_album.Add(new clsAlbum(reader.GetInt32(0) + "", reader.GetString(1), reader.GetInt32(3), img, GetProduction(reader.GetInt32(4))));
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
        private void btnNew_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            btnSave.Text = "Save";
        }
        private bool IsEmptyOrNull(TextBox txt)
        {
            if (txt.Text == "" || txt.Text == null)
            {
                txt.Focus();
                return true;
            }
            return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsEmptyOrNull(txtVol))
            {
                MessageBox.Show("Please input correct data");
                txtVol.Focus();
                return;
            }
            else if (IsEmptyOrNull(txtAlbumName))
            {
                MessageBox.Show("Please input correct data");
                txtAlbumName.Focus();
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAlbumName.Text = "";
            txtVol.Text = "";
            pbAlbum.Image = null;
            cboProduction.Text = "";
            EnableControl(false);
        }
        private void pbAlbum_Click(object sender, EventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            //img.Filter = "Image File (*.jpg|(*JPG)|*.png|(*.PNG)|*.gif|(*.GIF))";
            if (img.ShowDialog() == DialogResult.OK)
            {
                pbAlbum.Image = Image.FromFile(img.FileName);
            }   
        }
        private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EnableControl(true);
                current = (clsAlbum)bs_album.Current;
                //MessageBox.Show(temp.P_CountryId);
                txtAlbumName.Text = current.P_AlbumName;
                txtVol.Text = current.P_Vol+"";
                cboProduction.SelectedItem = current.P_Production;
                pbAlbum.Image = current.P_Photo;
                btnSave.Text = "Update";
            }
        }
    }
}
