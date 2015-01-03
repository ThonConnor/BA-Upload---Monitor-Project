using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTVServerApp.Script.Network;
using NetworkCommsDotNet;
using System.Collections;

namespace KTVServerApp.Script.Synchronize
{
    public class DataSongClient
    {
        public delegate void RecieveHandler(ArrayList data);
        public event RecieveHandler OnRecieveData;

        public void Init()
        {
            
            S_NetworkCommunication.RecieveIncomingPacket<byte[]>("LoadSongClient", (type, connection, message) =>
            {
              //  System.Windows.Forms.MessageBox.Show(message.Length + "");
                ArrayList data = S_NetworkCommunication.RecieveIncomingObject<ArrayList>(message);
                if (OnRecieveData != null)
                {
                    OnRecieveData(data);
                }
            });
           
        }

        public void SendDataLoad(string ip, int port,string sql)
        {
            S_NetworkCommunication.SendMessage<string>("LoadSongServer", ip, port, sql);
        }
        public void SendDataLoad(Connection con, string sql)
        {
            S_NetworkCommunication.SendMessage<string>("LoadSongServer", con, sql);
        }
    }
}
