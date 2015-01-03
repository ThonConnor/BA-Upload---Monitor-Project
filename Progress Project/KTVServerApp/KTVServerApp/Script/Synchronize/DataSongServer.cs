using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using KTVServerApp.Script.Network;
using NetworkCommsDotNet;
using KTVServerApp.Script.Sql;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Data;
namespace KTVServerApp.Script.Synchronize
{
   public class DataSongServer
    {
        public void Init()
        {
            S_NetworkCommunication.RecieveIncomingPacket<string>("LoadSongServer", (type, connection, message) =>
            {
                SendDataLoad(connection,message);
            });
            S_NetworkCommunication.RecieveIncomingPacket<string>("LoadImageServer", (type, connection, message) =>
            {
                SendDataLoadImage(connection, message);
            });
            //
            S_NetworkCommunication.RecieveIncomingPacket<string>("RequestDataServer", (type, connection, message) =>
            {
                SendDataToClient(connection, message);
            });
        }
        private void SendDataToClient(Connection con, string sql)
        {
            ConfigurationData data = ConfigurationData.Instance();
            SqlConnection connection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            S_NetworkCommunication.SendObjectType<DataTable>("RequestDataClient", con, table);
        }

        private void SendDataLoadImage(Connection con, string sql)
        {
            ConfigurationData data = ConfigurationData.Instance();
            SqlConnection connection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
            SqlDataReader reader = SqlControl.SelectData(sql, connection);
            if (reader != null)
            {
                ArrayList listimage = new ArrayList();
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
                        //SynSong song = new SynSong((string)reader[0], null, (string)reader[2]);
                        ClientLib.SynImage.StoreImage image = new ClientLib.SynImage.StoreImage((string)reader[0], img);
                        listimage.Add(image);
                    }
                }
                // MessageBox.Show(listsong.Count+"");
                // load sql
                string ip = con.ConnectionInfo.RemoteEndPoint.Address.ToString();
                int port = con.ConnectionInfo.RemoteEndPoint.Port;
                //S_NetworkCommunication.SendObjectType<ArrayList>("LoadImageClient", ip, port, listsong);
                S_NetworkCommunication.SendObjectType<ArrayList>("LoadImageClient",con,listimage);
            }
        }
        private void SendDataLoad(Connection con,string sql)
        {
            ConfigurationData data = ConfigurationData.Instance();
            SqlConnection connection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
            SqlDataReader reader = SqlControl.SelectData(sql, connection);
            if (reader != null)
            {
                ArrayList listsong = new ArrayList();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Image img = null;
                        //byte[] byteimg = null;
                        //if (!reader.IsDBNull(reader.GetOrdinal("Photo")))
                        //{
                        //    byteimg = (byte[])reader["Photo"];
                        //    using (MemoryStream ms = new MemoryStream(byteimg))
                        //    {
                        //        img = Image.FromStream(ms);
                        //    }
                        //}
                        SynSong song = new SynSong((string)reader[0], (byte[])reader["Photo"] ,(string)reader[2]);
                        listsong.Add(song);
                    }
                }
               // MessageBox.Show(listsong.Count+"");
                // load sql
                //MessageBox.Show(listsong.Count+"");
                string ip = con.ConnectionInfo.RemoteEndPoint.Address.ToString();
                int port = con.ConnectionInfo.RemoteEndPoint.Port;
               // S_NetworkCommunication.SendObjectType<ArrayList>("LoadSongClient", ip, port, listsong);
                S_NetworkCommunication.SendObjectType<ArrayList>("LoadSongClient", con, listsong);
            }
        }
        
   }
}
