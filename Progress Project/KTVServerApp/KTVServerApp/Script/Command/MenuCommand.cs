using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KTVServerApp.Script.Command
{
    internal class MenuCommand:IControl
    {
        
        public void Execute(Panel parent)
        {
            parent.Controls.Clear();
            Menu m = new Menu();
            m.TopLevel = false;
            m.Show();
            parent.Controls.Add(m);
        }
    }
}
