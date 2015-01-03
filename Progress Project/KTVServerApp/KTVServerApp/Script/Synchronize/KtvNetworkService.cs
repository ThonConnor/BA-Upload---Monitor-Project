using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using KTVServerApp.Script.Network;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using KTVServerApp.Script.Sql;
using System.IO;
using System.Drawing;
namespace KTVServerApp.Script.Synchronize
{
    public class KtvNetworkService
    {
        private static KtvNetworkService instance;

        private KtvNetworkService()
        {

        }
       
        public SqlConnection ServerConnection { get; set; }

        public void Initailize()
        {
            GetConnection();
            RegisterReciever();
        }

        public static KtvNetworkService Instace()
        {
            if (instance == null) instance = new KtvNetworkService();
            return instance;
        }
        private void GetConnection()
        {
            ConfigurationData data = ConfigurationData.Instance();

            ServerConnection = SqlControl.InitializeConnection(data.DatabaseName, data.UserName, data.PassWord, data.ServerName);
           
        }
        private void RegisterReciever()
        {
            S_NetworkCommunication.RecieveIncomingPacket<string>("LoadSongServer", (type, connection, message) =>
            {
                SendDataLoad(connection, message);
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
            //ServerConnection.Open();
            SqlDataAdapter adapter = SqlControl.SelectData(ServerConnection,sql);
            DataTable table = new DataTable();
            adapter.Fill(table);
            S_NetworkCommunication.SendObjectType<DataTable>("RequestDataClient", con, table);
        }

        private void SendDataLoadImage(Connection con, string sql)
        {
            ServerConnection.Open(); 
            SqlDataReader reader = SqlControl.SelectData(sql, ServerConnection);
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
                S_NetworkCommunication.SendObjectType<ArrayList>("LoadImageClient", con, listimage);
            }
        }
        private void SendDataLoad(Connection con, string sql)
        {
            ServerConnection.Open(); 
            SqlDataReader reader = SqlControl.SelectData(sql, ServerConnection);
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
                        SynSong song = new SynSong((string)reader[0], (byte[])reader["Photo"], (string)reader[2]);
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
