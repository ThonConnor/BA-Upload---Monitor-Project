using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using KTVServerApp.Script.Sql;
using System.Data.SqlClient;
namespace KTVServerApp
{
    public partial class SongDetail : Form
    {
        private int rowtoedit = 0;
        private ConfigurationData data;
        private SqlConnection connection; 
        public SongDetail()
        {
            InitializeComponent();
        }

        private void dgvDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SongDetail_Load(object sender, EventArgs e)
        {
            data=ConfigurationData.Instance();
            connection= SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
            connection.Open();
            LoadData();
            dgvDisplay.AllowUserToAddRows = false;
            dgvDisplay.AllowUserToDeleteRows = false;
        }
        private void LoadData()
        {
            if (connection.State == ConnectionState.Closed) connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Song", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tbSong");
            dgvDisplay.DataSource = ds.Tables["tbSong"];
        }
        private void dgvDisplay_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvDisplay.Rows.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    rowtoedit = (int)dgvDisplay.Rows[e.RowIndex].Cells["SID"].Value;
                    cmsShow.Show(Cursor.Position);
                    
                }
            }
        }
        private void tsEdit_Click(object sender, EventArgs e)
        {
            SongModified f = new SongModified(rowtoedit);
            f.ShowDialog();
            LoadData();
        }
    }
}
