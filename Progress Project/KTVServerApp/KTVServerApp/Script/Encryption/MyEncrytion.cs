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
using Microsoft.Win32;
using System.Net.NetworkInformation;
namespace KTVServerApp.Script.Encryption
{
    static class MyEncrytion
    {
        public static RSAParameters GetKey()
        {
            RSAParameters param = new RSAParameters() ;
            if (File.Exists(Path.Combine(Application.StartupPath, "Key")))
            {
                using (var file = new FileStream(Path.Combine(Application.StartupPath, "Key"), FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    MyParam p = (MyParam)bf.Deserialize(file);
                    param = p.GetValue();
                }
            }
            else
            {
                MessageBox.Show("File Key is missing", "Key", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return param;
        }
        public static void GenerateKey()
        {
            try
            {
                RSAParameters param;
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                using (var file = new FileStream(Path.Combine(Application.StartupPath, "Key"), FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    param = RSA.ExportParameters(true);
                    bf.Serialize(file, new MyParam(param));
                }
                MessageBox.Show("Create Key Successful");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //foreach(byte t in RSAKeyInfo.Modulus){
                //   MessageBox.Show(RSAKeyInfo.Modulus.Length+"");
                //}
                //Create a new instance of RSACryptoServiceProvider. 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);

                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }

            catch (CryptographicException e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider. 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    RSA.ImportParameters(RSAKeyInfo);

                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        public static byte[] EncryptString(string data)
        {
            return RSAEncrypt(Encoding.UTF8.GetBytes(data), GetKey(), false);
        }
        public static string DecryptByte(byte[] data)
        {
            return Encoding.UTF8.GetString(RSADecrypt(data, GetKey(), false));
        }

        public static bool HasLicense()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Ktv");
            if (key != null)
            {
                byte[] value =(byte[]) key.GetValue("License");
                if (value != null)
                {
                    string licensekey = DecryptByte(value);
                    if (licensekey == NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString())
                    {
                        return true;
                    }
                    //MessageBox.Show(licensekey);
                    //compare key
                }
            }
            return false;
        }
        public static void WriteLicense()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software",true);
            if (key != null)
            {
                key=key.CreateSubKey("Ktv");
                byte[] encrypt = EncryptString(NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString());
                key.SetValue("License", encrypt, RegistryValueKind.Binary);
            }
            else
            {
                MessageBox.Show("Wrong!!!");
            }
        }
    }
}
