using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KTVServerApp.Script.Command
{
    internal class CategoryCommand:IControl
    {
      
        public void Execute(Panel parent)
        {
            parent.Controls.Clear();
            Category s = new Category();
            s.TopLevel = false;
            s.Show();
            parent.Controls.Add(s);
        }
    }
}
