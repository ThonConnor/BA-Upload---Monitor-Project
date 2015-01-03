using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KTVServerApp.Script.Command
{
    internal class ExitCommand:IControl
    {
        public void Execute(Panel parent)
        {
            Application.Exit();
        }
    }
}
