using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkCommsDotNet;
using DPSBase;
using System.Windows.Forms;
namespace KTVServerApp.Script.Network
{
    public static class S_NetworkCommunication
    {
        /// <summary>
        /// start listen to TCP connection
        /// </summary>
        public static void ListenPort()
        {
            TCPConnection.StartListening(true);
        }
        /// <summary>
        /// close connection
        /// </summary>
        public static void CloseConnection()
        {
            NetworkComms.Shutdown();
        }
        /// <summary>
        /// start connection to server
        /// </summary>
        /// <param name="packagetype"> type of package</param>
        /// <param name="serverip"> server's ip</param>
        /// <param name="port">port of server</param>
        public static void StartConnectionToServer(string packagetype,string serverip,int port)
        {
            try
            {
                NetworkComms.SendObject(packagetype, serverip, port, "");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"StartConnectionToServer");
            }
        }
        /// <summary>
        /// send package
        /// </summary>
        /// <typeparam name="T">type of message</typeparam>
        /// <param name="type"> type of package</param>
        /// <param name="destinationip"> ip of destination</param>
        /// <param name="destinationport">port of destination</param>
        /// <param name="message">message to send</param>
        public static void SendMessage<T>(string type, string destinationip, int destinationport, T message)
        {
            try
            {
                NetworkComms.SendObject(type, destinationip, destinationport, message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "SendMessage");
            }
        }
        /// <summary>
        /// send object through network
        /// </summary>
        /// <typeparam name="T">type of class</typeparam>
        /// <param name="type"> type of packet</param>
        /// <param name="destinationip">ip of server</param>
        /// <param name="destinationport">port</param>
        /// <param name="message">object</param>
        public static void SendObjectType<T>(string type, string destinationip, int destinationport, T message)
        {
            try
            {
                DataSerializer ds = BinaryFormaterSerializer.Instance;
                StreamSendWrapper ssw = ds.SerialiseDataObject<T>(message);
                S_NetworkCommunication.SendMessage<StreamSendWrapper>(type, destinationip, destinationport, ssw);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "SendObjectType");
            }
        }

        /// <summary>
        /// return object from byte[] data
        /// </summary>
        /// <typeparam name="T">type of class</typeparam>
        /// <param name="message">data</param>
        /// <returns>object</returns>
        public static T RecieveIncomingObject<T>(byte[] message)
        {
            DataSerializer ds = BinaryFormaterSerializer.Instance;
            return ds.DeserialiseDataObject<T>(message);
        }
        /// <summary>
        /// receive package
        /// </summary>
        /// <typeparam name="T">type of message</typeparam>
        /// <param name="packagetype">type of package</param>
        /// <param name="callback">delegate to call after recieve package paramater (string type,Connection con,T message)</param>
        public static void RecieveIncomingPacket<T>(string packagetype,NetworkComms.PacketHandlerCallBackDelegate<T> callback)
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<T>(packagetype, callback);
        }
        /// <summary>
        /// set port to use connection
        /// </summary>
        /// <param name="port">number of our port</param>
        public static void SetLocalPort(int port)
        {
            foreach (System.Net.IPEndPoint localEndPoint in TCPConnection.ExistingLocalListenEndPoints())
            {
                localEndPoint.Port = port;
            }
        }
        /// <summary>
        /// return port remote connection
        /// </summary>
        /// <param name="con"> connection</param>
        /// <returns>port</returns>
        public static int GetRemotePort(Connection con)
        {
            return con.ConnectionInfo.RemoteEndPoint.Port;
        }
        /// <summary>
        /// return port local connection
        /// </summary>
        /// <param name="con">connection</param>
        /// <returns>port</returns>
        public static int GetLocalPort(Connection con)
        {
            return con.ConnectionInfo.LocalEndPoint.Port;
        }
        /// <summary>
        /// return ip remote connection
        /// </summary>
        /// <param name="con">connection</param>
        /// <returns>ip address</returns>
        public static string GetRemoteIpAddress(Connection con)
        {
            return con.ConnectionInfo.RemoteEndPoint.Address.ToString();
        }
        /// <summary>
        /// return ip local connection
        /// </summary>
        /// <param name="con">connection</param>
        /// <returns>ip address</returns>
        public static string GetLocalIpAddress(Connection con)
        {
            return con.ConnectionInfo.LocalEndPoint.Address.ToString();
        }
        /// <summary>
        /// initialize connection to server 
        /// </summary>
        /// <param name="serverip"></param>
        /// <param name="port"></param>
        /// <returns> object of Connection</returns>
        public static Connection StartConenction(string serverip, int port)
        {
            int numberconnection=3;
            while (numberconnection != 0)
            {
                try
                {
                    ConnectionInfo info = new ConnectionInfo(serverip, port);
                    return TCPConnection.GetConnection(info);
                }
                catch (Exception e)
                {
                    numberconnection--;
                    MessageBox.Show(e.Message,"ConnectionFail");
                }
            }
            return null;
        }
        /// <summary>
        /// send message using object of Connnection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="con"></param>
        /// <param name="message"></param>
        public static void SendMessage<T>(string type,Connection con, T message)
        {
            try
            {
                con.SendObject(type, message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Method send Message Connection");
            }
        }
        /// <summary>
        /// send object serialize using object of Connection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="con"></param>
        /// <param name="message"></param>
        public static void SendObjectType<T>(string type, Connection con, T message)
        {
            try
            {
                DataSerializer ds = BinaryFormaterSerializer.Instance;
                StreamSendWrapper ssw = ds.SerialiseDataObject<T>(message);
                con.SendObject(type, ssw);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "SendObjectType");
            }
        }
    }
}
