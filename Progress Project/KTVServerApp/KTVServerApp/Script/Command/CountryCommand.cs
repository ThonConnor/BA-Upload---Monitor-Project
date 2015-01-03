using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KTVServerApp.Script.Command
{
    internal class CountryCommand:IControl
    {
       
        public void Execute(Panel parent)
        {
            parent.Controls.Clear();
            Country s = new Country();
            s.TopLevel = false;
            s.Show();
            parent.Controls.Add(s);
        }
    }
}
