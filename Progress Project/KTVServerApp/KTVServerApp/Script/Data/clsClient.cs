using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace KTVServerApp.Network
{
    public class clsClient:clsNetWork
    {
        public clsClient(string ip, string port, E_NetworkStatus status)
        {
            v_ipaddress = ip;
            v_port = port;
            v_status = status;
        }
        public clsClient()
        {

        }
        public override int Progressbar
        {
            get
            {
                return v_progress;
            }
            set
            {
                v_progress = value;
            }
        }
        public override string P_IpAddress
        {
            get
            {
                return v_ipaddress;
            }
            set
            {
                v_ipaddress = value;
            }
        }
        public override string P_Type
        {
            get { return "Client"; }
        }
        public override string P_Port
        {
            get
            {
                return v_port;
            }
            set
            {
                v_port = value;
            }
        }
        public override E_NetworkStatus P_Status
        {
            get
            {
                return v_status;
            }
            set
            {
                v_status = value;
            }
        }
        public override string P_currentTitle
        {
            get
            {
                return v_currentTitle;
            }
            set
            {
                v_currentTitle = value;
            }
        }
        public override void M_SetAddress(string ipport)
        {
            try
            {
                string[] info = ipport.Split(':');
                P_IpAddress = info[0];
                P_Port = info[1];
            }
            catch (Exception e)
            {

            }
        }
    }
}
