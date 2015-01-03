using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KTVServerApp.Script.Command
{
    internal class ProductionCommand:IControl
    {
       
        public void Execute(Panel parent)
        {
            parent.Controls.Clear();
            Production s = new Production();
            s.TopLevel = false;
            s.Show();
            parent.Controls.Add(s);
        }
    }
}
