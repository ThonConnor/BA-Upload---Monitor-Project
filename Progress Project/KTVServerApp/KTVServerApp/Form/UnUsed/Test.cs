using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KTVServerApp.Script.Network;

using DPSBase;
using NetworkCommsDotNet;

namespace KTVServerApp
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            S_NetworkCommunication.ListenPort();
            TestClass tc = new TestClass();
            tc.test = "Hello";
            List<TestClass> temp = new List<TestClass>();
            temp.Add(tc);
            temp.Add(tc);
            temp.Add(tc);
            temp.Add(tc);
            temp.Add(tc);
            temp.Add(tc);
            temp.Add(tc);
            temp.Add(tc);

            DataSerializer ds = BinaryFormaterSerializer.Instance;
            StreamSendWrapper ssw = ds.SerialiseDataObject<List<TestClass>>(temp);
            S_NetworkCommunication.SendMessage<StreamSendWrapper>("Test", "11.11.11.7", 10000,ssw);
        }
    }
    [Serializable()]
    public class TestClass
    {
        public string test = "";
    }
}
