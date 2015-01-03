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
using System.Collections;

namespace KTVServerApp
{
    public partial class TestClient : Form
    {
        private DataSongClient client;
        public TestClient()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            client.SendDataLoad("11.11.11.55", 10000, "Select SongName,Production,FilePath from Song");
        }
        void client_OnRecieveData(ArrayList data)
        {
            //textBox1.Text=data.SongName+"    "+data.FilePath;
        }
        private void TestCient_Load(object sender, EventArgs e)
        {
            client = new DataSongClient();
            client.Init();
            client.OnRecieveData += client_OnRecieveData;
            S_NetworkCommunication.ListenPort();
        }
    }
}
