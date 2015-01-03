using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KTVServerApp.Script.Command;
using KTVServerApp.Script.Network;
using DPSBase;
using NetworkCommsDotNet;
using KTVServerApp.Script.Sql;
namespace KTVServerApp
{
    public partial class MainForm : Form
    {
        private ControlForm control;
        public MainForm()
        {
            InitializeComponent();
            control = new ControlForm(panel1);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //S_NetworkCommunication.ListenPort();
            //S_NetworkCommunication.SetLocalPort(10000);
            //S_NetworkCommunication.RecieveIncomingPacket<byte[]>("Test", (header,connection,message) => {
            //    DataSerializer ds = BinaryFormaterSerializer.Instance;
            //    List<TestClass> tc = ds.DeserialiseDataObject<List<TestClass>>(message);
            //    MessageBox.Show(tc.Count+"");
                
            //});
            ////control.ShowSinger(panel1);
        }
        #region GUI Event
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            control.OpenForm(new Menu());
        }
        private void uploadSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control.OpenForm(new UploadSongV2());  
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to close?", "Exit", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                S_NetworkCommunication.CloseConnection();
                Application.Exit();
            }
        }
        private void uploadSingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control.OpenForm(new Singer());
        }
        private void uploadCountryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control.OpenForm(new Country());
        }
        private void uploadProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control.OpenForm(new Production());
        }
        private void uploadAlbumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control.OpenForm(new Album());
        }
        private void uploadCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control.OpenForm(new Category());
        }
        private void toolMonitoring_Click(object sender, EventArgs e)
        {
            control.OpenForm(new frmMonitor());
        }
        #endregion

        private void uploadLocalSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control.OpenForm(new UploadSongV2());
        }
        private void uploadExcelSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //control.ShowUploadExcel();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            control.OpenForm(new SongDetail());
        }

    }
}
