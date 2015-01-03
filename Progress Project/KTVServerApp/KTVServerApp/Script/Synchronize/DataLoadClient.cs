using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTVServerApp.Script.Network;
using NetworkCommsDotNet;
namespace KTVServerApp.Script.Synchronize
{
    public class DataLoadClient
    {
        public delegate void RecieveHandler(SynDataOnLoad data);
        public event RecieveHandler OnRecieveData;
        public void Init()
        {
            SendDataLoad("", 0);
            S_NetworkCommunication.RecieveIncomingPacket<byte[]>("LoadDataClient", (type, connection, message) => {
                SynDataOnLoad data = S_NetworkCommunication.RecieveIncomingObject<SynDataOnLoad>(message);
                if (OnRecieveData != null)
                {
                    OnRecieveData(data);
                }
            });
        }

        private void SendDataLoad(string ip,int port)
        {
            S_NetworkCommunication.SendMessage<string>("LoadDataServer", ip, port, "");
        }
    }
}
