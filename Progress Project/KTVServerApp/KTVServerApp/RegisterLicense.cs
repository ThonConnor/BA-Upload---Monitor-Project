using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KTVServerApp.Script.Encryption;
namespace KTVServerApp
{
    public partial class RegisterLicense : Form
    {
        public RegisterLicense()
        {
            InitializeComponent();
        }

        private void RegisterLicense_Load(object sender, EventArgs e)
        {
            try
            {
                MyEncrytion.WriteLicense();
                MessageBox.Show("Register successfully");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
