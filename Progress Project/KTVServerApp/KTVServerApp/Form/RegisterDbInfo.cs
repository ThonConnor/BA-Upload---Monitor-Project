using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using KTVServerApp.Script.Sql;
using System.Data.SqlClient;
using System.Security.Cryptography;
using KTVServerApp.Script.Encryption;

namespace KTVServerApp
{
    public partial class RegisterDbInfo : Form
    {
        private string path;
        public RegisterDbInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigurationData.SerializeData(new ConfigurationData(txtServerName.Text, txtDbName.Text, txtUserName.Text, txtPassword.Text, txtIp.Text, txtUploadFolder.Text,path), "Configuration.bin");
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fb = new FolderBrowserDialog())
            {
                if (fb.ShowDialog() == DialogResult.OK)
                {
                    txtUploadFolder.Text = fb.SelectedPath;
                    this.path = txtUploadFolder.Text.Substring(txtUploadFolder.Text.LastIndexOf(@"\") + 1);
                    
                }
            }
        }

        private void RegisterDbInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void RegisterDbInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Dispose();
            
            //if (Loading.instance != null)
            //{
            //    Loading.instance.Close();
            //}

            Application.Exit();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(txtUploadFolder.Text))
                {
                    throw new Exception();
                }
                SqlConnection con = SqlControl.InitializeConnection(txtDbName.Text, txtUserName.Text, txtPassword.Text, txtServerName.Text);
                con.Open();
                MessageBox.Show("Connection Successful");
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Connection Fail");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MyEncrytion.GenerateKey();
        }
        
    }
   
    [Serializable]
    public class ConfigurationData
    {
        private byte[] servername;
        private byte[] ipaddress;
        private byte[] databasename;
        private byte[] username;
        private byte[] password;
        private byte[] foldername;
        private byte[] lastpath;

        public ConfigurationData(string servername, string databasename, string username, string password, string ipaddress, string foldername,string last)
        {
            this.servername =MyEncrytion.EncryptString(servername);
            this.databasename = MyEncrytion.EncryptString(databasename);
            this.ipaddress = MyEncrytion.EncryptString(ipaddress);
            this.username = MyEncrytion.EncryptString(username);
            this.foldername = MyEncrytion.EncryptString(foldername);
            this.password = MyEncrytion.EncryptString(password);
            this.lastpath = MyEncrytion.EncryptString(last);
        }

        public string ServerName { get { return MyEncrytion.DecryptByte(servername); } }
        public string DatabaseName { get { return MyEncrytion.DecryptByte(databasename); } }
        public string UserName { get { return MyEncrytion.DecryptByte(username); } }
        public string PassWord { get { return MyEncrytion.DecryptByte(password); } }
        public string IpAddress { get { return MyEncrytion.DecryptByte(ipaddress); } }
        public string FolderName { get { return MyEncrytion.DecryptByte(foldername); } }
        public string LastPath { get { return MyEncrytion.DecryptByte(lastpath); } }
        
        public static ConfigurationData Instance()
        {
            if (File.Exists(Application.StartupPath + "/Configuration.bin"))
            {
                return ConfigurationData.DeserializeData();
            }
            else
            {
               // RegisterDbInfo db = new RegisterDbInfo();
               // db.ShowDialog();
               //// MessageBox.Show(db.IsDisposed + "" + db.Visible);
               // if (!db.IsDisposed)
               // {
               //     return ConfigurationData.Instance();
               // }
                MessageBox.Show("File Configuration.bin is missing","Configuration.bin",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return null;
            }
        }
        public static void SerializeData(ConfigurationData data,string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(Application.StartupPath + "/" + filename, FileMode.Create))
                {
                    //using (var memoryStream = new MemoryStream())
                    //{
                       
                        var formatter = new BinaryFormatter();
                        formatter.Serialize(fs, data);
                    //    memoryStream.Seek(0, SeekOrigin.Begin);
                    //    var bytes = new byte[memoryStream.Length];
                    //    memoryStream.Read(bytes, 0, (int)memoryStream.Length);
                    //    byte[] encryptedData = MyEncrytion.RSAEncrypt(bytes, MyEncrytion.GetKey(), false);
                    //    fs.Write(encryptedData, 0, encryptedData.Length);
                    //}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static ConfigurationData DeserializeData()
        {
            try
            {
                using (FileStream fs = File.OpenRead(Application.StartupPath + "/Configuration.bin"))
                {
                    //byte[] encryptedData = new byte[fs.Length];
                    //int numBytesToRead = (int)fs.Length;
                    //int numBytesRead = 0;
                    //while (numBytesToRead > 0)
                    //{
                    //    // Read may return anything from 0 to numBytesToRead. 
                    //    int n = fs.Read(encryptedData, numBytesRead, numBytesToRead);

                    //    // Break when the end of the file is reached. 
                    //    if (n == 0)
                    //        break;

                    //    numBytesRead += n;
                    //    numBytesToRead -= n;
                    //}
                    //var decryptedData =MyEncrytion.RSADecrypt(encryptedData,MyEncrytion.GetKey(), false);
                    //using (var memory = new MemoryStream(decryptedData))
                    //{
                        BinaryFormatter bf = new BinaryFormatter();
                       // MessageBox.Show((string)bf.Deserialize(memory));
                        ConfigurationData data=(ConfigurationData)bf.Deserialize(fs);
                        return data;
                   // }
                }    
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        #region Encrypt
        
        #endregion
    }
    

}
