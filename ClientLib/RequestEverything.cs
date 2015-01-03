using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using KTVServerApp.Script.Network;
using NetworkCommsDotNet;

namespace ClientLib
{
     public class RequestEverything
    {
        public delegate void RecieveHandler(DataTable data);
        public event RecieveHandler OnRecieveData;

        public void Init()
        {

            S_NetworkCommunication.RecieveIncomingPacket<byte[]>("RequestDataClient", (type, connection, message) =>
            {
                //  System.Windows.Forms.MessageBox.Show(message.Length + "");
                DataTable data = S_NetworkCommunication.RecieveIncomingObject<DataTable>(message);
                if (OnRecieveData != null)
                {
                    OnRecieveData(data);
                }
            });

        }
        public void SendRequest(Connection con, string sql)
        {
            S_NetworkCommunication.SendMessage<string>("RequestDataServer", con, sql);
        }
    }
}

