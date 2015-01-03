using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace KTVServerApp.Network
{
    /*
     * base class of server client
     */ 
    public abstract class clsNetWork
    {
        #region Variables
        protected string v_ipaddress;     // ip of network
        protected string v_port;  // port of network
        protected E_NetworkStatus v_status; // status of network
        protected int v_progress;
        protected string v_currentTitle;
        #endregion

        #region abstract properties
       
        public abstract int Progressbar { get; set; }
        public abstract string P_currentTitle { get; set; }
        /// <summary>
        /// return type ( Client of Server )
        /// </summary>
        public abstract string P_Type { get; }
        /// <summary>
        /// get and set Ip 
        /// </summary>
        public abstract string P_IpAddress { get; set; }
        /// <summary>
        /// get and set  Port
        /// </summary>
        public abstract string P_Port { get; set; }
        /// <summary>
        /// get and set Status
        /// </summary>
        public abstract E_NetworkStatus P_Status { get; set; }
        #endregion

        #region abstract method
        /// <summary>
        /// method to split ip and port
        /// </summary>
        /// <param name="ipport"></param>
        public abstract void M_SetAddress(string ipport);
        #endregion
    }
    /// <summary>
    /// enum ... network status (online or offline)
    /// </summary>
    public enum E_NetworkStatus
    {
        NONE,
        ONLINE,
        OFFLINE
    }
}
