using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KTVServerApp.Script.Synchronize;
using NetworkCommsDotNet;
using KTVServerApp.Script.Network;
using KTVServerApp.Script.Synchronize;

namespace KTVServerApp
{
    public partial class TestServer : Form
    {
        private DataSongServer temp;
        public TestServer()
        {
            InitializeComponent();
        }

        private void TestServer_Load(object sender, EventArgs e)
        {
            temp = new DataSongServer();
            temp.Init();
            S_NetworkCommunication.SetLocalPort(10000);
            S_NetworkCommunication.ListenPort();
        }
    }
}
