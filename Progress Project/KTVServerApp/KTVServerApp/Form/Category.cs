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
namespace KTVServerApp
{
    public partial class Category : Form
    {
        private SqlConnection connection;
        ConfigurationData data;
        public Category()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCategoryName.Text))
            {
                MessageBox.Show("Please input data");
                txtCategoryName.Focus();
                return;
            }
            try
            {
                string name = txtCategoryName.Text;
                SqlControl.InsertData("Category", connection, SqlControl.Hash("CategoryName", name,"PopularCount",1));
                txtCategoryName.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Category_Load(object sender, EventArgs e)
        {
            data = ConfigurationData.Instance();
            connection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
        }
    }
}
