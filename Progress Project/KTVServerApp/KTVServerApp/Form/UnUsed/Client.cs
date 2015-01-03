using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetworkCommsDotNet;
using KTVServerApp.Network;
namespace KTVServerApp
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            NetworkComms.SendObject("Client", "11.11.11.36", 10000, "");
            NetworkComms.SendObject("Progressbar", "11.11.11.36", 10000, 10);
            NetworkComms.SendObject("Title", "11.11.11.36", 1000, "");
        }
        private void Client_Load(object sender, EventArgs e)
        {
            TCPConnection.StartListening(true);  
        }

        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            NetworkComms.Shutdown();
        }
    }
}
