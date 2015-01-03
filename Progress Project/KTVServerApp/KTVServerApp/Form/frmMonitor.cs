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
using System.Threading;
using DataGridProgressBarTest;
using KTVServerApp.Script;

namespace KTVServerApp
{
    public partial class frmMonitor : Form
    {
        private ServerMonitoring monitor;
        private static frmMonitor monitor_instance = null;
        public frmMonitor()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Init();
            
        }
        public static frmMonitor Instance()
        {
            if (monitor_instance == null)
            {
                monitor_instance = new frmMonitor();
            }
            return monitor_instance;
        }
        private void Init()
        {
            monitor = ServerMonitoring.GetInstance(this, dgvMonitor);
            dgvMonitor.DataSource = monitor.bs ;
            dgvMonitor.Columns[0].Visible = false;
            DataGridViewProgressColumn dgpc = new DataGridViewProgressColumn();
            dgvMonitor.Columns.Add(dgpc);
            dgpc.HeaderText = "ProgessBar";
            dgpc.DefaultCellStyle.SelectionForeColor = Color.Green;
            dgpc.DataPropertyName = "ProgressBar";
            monitor.bs.ResetBindings(false);
            monitor.StartConnection();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  monitor.CloseConnection();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //bs.ResetBindings(false);
        }
    }
}
