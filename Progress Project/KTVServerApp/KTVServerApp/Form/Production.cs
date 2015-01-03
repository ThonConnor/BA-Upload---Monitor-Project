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
    public partial class Production : Form
    {
        private List<clsCountry> lst_country;
        private List<clsProduction> lst_pro;
        private BindingSource bs_country;
        private BindingSource bs_pro;
        private SqlConnection connection;
        private clsProduction current;
        private ConfigurationData data;

        public Production()
        {
            InitializeComponent();
        }

        private void Production_Load(object sender, EventArgs e)
        {
            data = ConfigurationData.Instance();
            connection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
            InitializeForm();
        }
        private void InitializeForm()
        {
            EnableControl(false);
            lst_pro = new List<clsProduction>();
            lst_country = new List<clsCountry>();
            bs_pro = new BindingSource(lst_pro, null);
            bs_country = new BindingSource(lst_country, null);
            cboCountry.DataSource = bs_country;
            dgvDisplay.DataSource = bs_pro;
            LoadDataToDataGridView();
            dgvDisplay.Columns["P_Photo"].Visible = false;
            bs_country.ResetBindings(false);
            bs_pro.ResetBindings(false);

        }
        private void EnableControl(bool enabled)
        {
            btnSave.Enabled = enabled;
            btnNew.Enabled = !enabled;
            btnClear.Enabled = enabled;
            pbProduction.Enabled = enabled;
            txtProductionName.Enabled = enabled;
            cboCountry.Enabled = enabled;
        }
        private void LoadDataToComboBox()
        {
            try
            {
                lst_country.Clear();
               
                using (SqlDataReader reader = SqlControl.SelectData("SELECT * FROM Country", connection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lst_country.Add(new clsCountry(reader.GetInt32(0) + "", reader.GetString(1),null,null,null));
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
        private clsCountry GetCountry(int id)
        {
            foreach (clsCountry c in lst_country)
            {
                if (int.Parse(c.P_CountryId) == id)
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
                lst_pro.Clear();
                // MessageBox.Show(connection.ToString());
                using (SqlDataReader reader = SqlControl.SelectData("SELECT * FROM Production", connection))
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
                            lst_pro.Add(new clsProduction(reader.GetInt32(0) + "", reader.GetString(1), img, GetCountry(reader.GetInt32(2))));
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
                string proname = txtProductionName.Text;
                clsCountry coun =(clsCountry) bs_country.Current;
                int? countryid = null;
                if (coun != null) countryid = int.Parse(coun.P_CountryId);
                //MessageBox.Show(pbCountry.ImageLocation);
                byte[] img = ConvertImage(pbProduction.Image);

                SqlControl.InsertData("Production", connection,new string[]{"ProductionName","Country","Photo"}, new SqlControl.CommandParameter("@proname", SqlDbType.NChar, proname)
                                                          , new SqlControl.CommandParameter("@country", SqlDbType.Int,countryid)
                                                          , new SqlControl.CommandParameter("@img", SqlDbType.Image, img));

                LoadDataToDataGridView();
                bs_pro.ResetBindings(false);
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
                Bitmap bit = new Bitmap(img,300,200);
                bit.Save(ms, img.RawFormat);
                MessageBox.Show(ms.ToArray().Length+"");
                return ms.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private void EditData()
        {
            try
            {
                string proname = txtProductionName.Text;
                clsCountry cou = (clsCountry)bs_country.Current;
                //MessageBox.Show(pbCountry.ImageLocation);
                byte[] img = ConvertImage(pbProduction.Image);

                SqlControl.UpdateData("Production",connection,new string[]{"ProductionName","Country","Photo"},"WHERE PID=" + long.Parse(current.P_ProductionID)
                                        ,new SqlControl.CommandParameter("@proname",SqlDbType.NChar,proname)
                                        ,new SqlControl.CommandParameter("@country",SqlDbType.Int,long.Parse(cou.P_CountryId))
                                        ,new SqlControl.CommandParameter("@img",SqlDbType.Image,img));
                
                //string sql = "UPDATE Production SET ProductionName=@proname,Country=@country,Photo=@img WHERE PID=" + long.Parse(current.P_ProductionID);
                //SqlControl.StartConnection(connection);
                //SqlCommand cmd = new SqlCommand(sql, connection);
                //cmd.Parameters.Add("@proname", SqlDbType.NChar).Value = proname;
                //cmd.Parameters.Add("@country", SqlDbType.Int).Value = long.Parse(cou.P_CountryId);
                //cmd.Parameters.Add("@img", SqlDbType.Image).Value = img;
                //cmd.ExecuteNonQuery();
                LoadDataToDataGridView();
                bs_pro.ResetBindings(false);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }       
        private void btnNew_Click(object sender, EventArgs e)
        {
            EnableControl(true);
            btnSave.Text = "Save";
        }

        private void pbProduction_Click(object sender, EventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            //img.Filter = "Image File (*.jpg|(*JPG)|*.png|(*.PNG)|*.gif|(*.GIF))";
            if (img.ShowDialog() == DialogResult.OK)
            {
                pbProduction.Image = Image.FromFile(img.FileName);
                pbProduction.ImageLocation = img.FileName;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProductionName.Text = "";
            cboCountry.SelectedIndex = 0;
            pbProduction.Image = null;
            EnableControl(false);
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

        private void dgvDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EnableControl(true);
                current = (clsProduction)bs_pro.Current;
                //MessageBox.Show(temp.P_CountryId);
                txtProductionName.Text = current.P_ProductionName;
                cboCountry.SelectedItem = current.P_Country;
                pbProduction.Image = current.P_Photo;
                btnSave.Text = "Update";
            }
        }
    }
}
