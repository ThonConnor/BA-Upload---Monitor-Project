using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using KTVServerApp.Network;
using KTVServerApp.Script.Network;
using System.Windows.Forms;

namespace KTVServerApp.Script
{
    public class ServerMonitoring
    {
        private delegate void RefreshDel();
        
        private RefreshDel RefreshControl;
        public List<clsNetWork> lst;
        private Form monitor;
        public BindingSource bs;
        private DataGridView dgv;
        private static ServerMonitoring instance=new ServerMonitoring();

        private ServerMonitoring()
        {

        }

        private ServerMonitoring(Form monitor,DataGridView dgv)
        {
            lst = new List<clsNetWork>();
            bs = new BindingSource(lst, null);
            this.monitor = monitor;
            this.dgv = dgv;
        }
        public static ServerMonitoring GetInstance(Form monitor, DataGridView dgv)
        {
            if (instance.lst == null)
            {
                instance.lst = new List<clsNetWork>();
            }
            
            instance.bs = new BindingSource(instance.lst, null);
            instance.monitor = monitor;
            instance.dgv = dgv;
            return instance;
        }
        public  void StartConnection()
        {
            S_NetworkCommunication.RecieveIncomingPacket<string>("Client", (header, connection, message) =>
            {
                RefreshControl = () => { bs.ResetBindings(false); };
                AddItem(connection);
            });
            S_NetworkCommunication.RecieveIncomingPacket<string>("Progressbar", (header,connection,message) =>
            {
                RefreshControl = () => { bs.ResetBindings(false); };
                UpdateProgressBar(GetItem(connection), message);
            });
            S_NetworkCommunication.RecieveIncomingPacket<string>("Title", (header,connection,message) =>
            {
                RefreshControl = () => { bs.ResetBindings(false); };
                UpdateTitle(GetItem(connection), message);
            });
            S_NetworkCommunication.RecieveIncomingPacket<string>("Shutdown", (header,connection,message) =>
            {
                RefreshControl = () => { bs.ResetBindings(false); };
               UpdateShutdown(connection);
            });
            S_NetworkCommunication.ListenPort();
            S_NetworkCommunication.SetLocalPort(10000);
        }
        public void CloseConnection()
        {
            NetworkComms.Shutdown();
        }
        //private void IncomingShutDown(PacketHeader header, Connection connection, string message)
        //{
        //    RefreshControl = () => { bs.ResetBindings(false); };
        //    UpdateShutdown(connection);
        //}
        //private void IncomingProgress(PacketHeader header, Connection connection, string message)
        //{
        //    RefreshControl = () => { bs.ResetBindings(false); };
        //    UpdateProgressBar(GetItem(connection), message);
        //}
        //private void IncomingTitle(PacketHeader header, Connection connection, string message)
        //{
        //    RefreshControl = () => { bs.ResetBindings(false); };
        //    UpdateTitle(GetItem(connection), message);
        //}
        //private void IncomingConnection(PacketHeader header, Connection connection, string message)
        //{

        //    RefreshControl = () => { bs.ResetBindings(false); };
        //    AddItem(connection);
        //}
        private void UpdateShutdown(Connection con)
        {
            lst.Remove(GetItem(con));
            monitor.Invoke(RefreshControl);
        }
       
        private void UpdateProgressBar(clsNetWork current, string message)
        {
            try
            {
                string[] temp = message.Split('-');
                int cur = int.Parse(temp[0]);
                int max = int.Parse(temp[1]);
                if (dgv.InvokeRequired)
                {
                    current.Progressbar = (cur * 100) / max;
                    monitor.Invoke(RefreshControl);
                }
            }
            catch (Exception e)
            {
                //current.Progressbar = 0;
            }
        }
       
        private void UpdateTitle(clsNetWork current, string message)
        {
            if (dgv.InvokeRequired)
            {
                current.P_currentTitle = message;
                monitor.Invoke(RefreshControl);
            }
        }
        private clsNetWork GetItem(Connection con)
        {
            foreach (clsNetWork item in lst)
            {
                if (item.P_IpAddress == con.ConnectionInfo.RemoteEndPoint.Address.ToString())
                {
                    if (item.P_Port == con.ConnectionInfo.RemoteEndPoint.Port.ToString())
                    {
                        return item;
                    }
                }
            }
            return null;
        }
      
        private void AddItem(Connection con)
        {
            if (dgv.InvokeRequired)
            {
                clsNetWork message1 = new clsClient();
                message1.P_IpAddress = con.ConnectionInfo.RemoteEndPoint.Address.ToString();
                message1.P_Port = con.ConnectionInfo.RemoteEndPoint.Port.ToString();
                message1.P_Status = E_NetworkStatus.ONLINE;
                lst.Add(message1);
                MessageBox.Show(message1.P_IpAddress);
                //bs.ResetBindings(false);
                monitor.Invoke(RefreshControl);
            }
        }
    }
}
