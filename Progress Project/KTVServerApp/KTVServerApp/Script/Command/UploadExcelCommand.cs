using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace KTVServerApp.Script.Command
{
    internal class UploadExcelCommand : IControl
    {
       
        public void Execute(Panel parent)
        {
            parent.Controls.Clear();
            UploadExcel us = new UploadExcel();
            us.TopLevel = false;
            us.Show();
            parent.Controls.Add(us);
        }
    }
}
