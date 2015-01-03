using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KTVServerApp.Script.Network;
using NetworkCommsDotNet;
using KTVServerApp.Script.Synchronize;
namespace KTVServerApp
{
    public partial class Testing : Form
    {
        public Testing()
        {
            InitializeComponent();
        }

        private void Testing_Load(object sender, EventArgs e)
        {
            S_NetworkCommunication.SetLocalPort(10000);
            S_NetworkCommunication.ListenPort();

            S_NetworkCommunication.RecieveIncomingPacket<string>("testingtoday", (type, connection, message) => {

                MessageBox.Show(message);
            });

            S_NetworkCommunication.RecieveIncomingPacket<byte[]>("testingobject", (type, connection, message) => {

                SynSong song = S_NetworkCommunication.RecieveIncomingObject<SynSong>(message);

                MessageBox.Show(song.SongName);

                S_NetworkCommunication.SendMessage<string>("testingstring", connection, "I got it");
            });
        }
    }
}
