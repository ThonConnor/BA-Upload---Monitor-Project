using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace KTVServerApp.Script.Command
{
    internal class UploadSongCommand:IControl
    {
       
        public void Execute(Panel parent)
        {
            parent.Controls.Clear();
            UploadSongV2 us = new UploadSongV2();
            us.TopLevel = false;
            us.Show();
            parent.Controls.Add(us);
        }
    }
}
