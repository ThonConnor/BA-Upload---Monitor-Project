using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using KTVServerApp.Script.Encryption;
namespace KTVServerApp
{
    public partial class Loading : Form
    {
       
        private delegate void SetGUI(string text, int value);
        private delegate void OpenForm();

        private TaskFactory task;
        private CancellationTokenSource ts;
        
        public Loading()
        {
            InitializeComponent();
            task = new TaskFactory();
            ts = new CancellationTokenSource();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            try
            {
               // if (MyEncrytion.HasLicense())
                //{
                    label1.Text = "Initialize , Don't close this program!!!";
                    if (ConfigurationData.Instance() == null)
                    {
                        Application.Exit();
                    }
                    // MyEncrytion.WriteLicense();
                    //MyEncrytion.HasLicense();
                    InitializeService();
            //    }
            //    else
            //    {
            //        //MessageBox.Show("U haven't registered yet, please contact awesome developer");
            //        //this.Close();
            //        //Application.Exit();

            //    }
            }
            catch (Exception ex)
            {
                this.Close();
                Application.Exit();
            }
        }
        
        private void InitializeService()
        {
            try
            {
                CancellationToken ct = ts.Token;
                SetGUI gui = (text, value) =>
                {
                    label1.Text = text;
                    pgbLoading.Value = value;
                };
                OpenForm form = () =>
                {
                    MainForm main = new MainForm();
                    main.Show();
                    this.Hide();
                };
                OpenForm thisform = () =>
                {
                    this.Opacity += 0.1f;
                };
                OpenForm thisformdecs = () =>
                {
                    this.Opacity -= .1f;
                };
                //OpenForm register = () =>
                //{
                //    frmMonitor.Instance();
                //};
                task.StartNew(() =>
               {
                   for (int i = 0; i < 10; i++)
                   {
                       Thread.Sleep(100);
                       Invoke(thisform);
                   }
                   Thread.Sleep(300);
                   Invoke(gui, "Check Configuration File", 10);

                   Thread.Sleep(500);
                   Invoke(gui, "Configuraion File Installed", 15);
                   Thread.Sleep(500);
                   Invoke(gui, "Starting Service and Initialize Monitoring", 15);
                   //Invoke(register);
                   frmMonitor.Instance();
                   Thread.Sleep(500);
                   Invoke(gui, "Service Monitring Successfully Starting", 30);

                   Invoke(gui, "Loading Complete!!!", 100);
                   Thread.Sleep(500);
                   for (int i = 0; i < 10; i++)
                   {
                       Thread.Sleep(50);
                       Invoke(thisformdecs);
                   }
                   Invoke(form);
                   // service monitor , search
               }, ct);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    
        private void Loading_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ts != null)
            {
                ts.Cancel();
            }
        }

        private void Loading_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
