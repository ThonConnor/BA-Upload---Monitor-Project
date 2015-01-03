using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTVServerApp.Script.Network;
using NetworkCommsDotNet;
namespace KTVServerApp.Script.Synchronize
{
    public class DataLoadServer
    {
        public void Init()
        {
            S_NetworkCommunication.RecieveIncomingPacket<string>("LoadDataServer", (type, connection, message) => {
                SendDataLoad(connection);
            });

        }

        private void SendDataLoad(Connection con)
        {
            // load sql
            SynDataOnLoad data = null;
            string ip=con.ConnectionInfo.RemoteEndPoint.Address.ToString();
            int port = con.ConnectionInfo.RemoteEndPoint.Port;
            S_NetworkCommunication.SendObjectType<SynDataOnLoad>("LoadDataClient", ip, port, data);
        }
    }
}
