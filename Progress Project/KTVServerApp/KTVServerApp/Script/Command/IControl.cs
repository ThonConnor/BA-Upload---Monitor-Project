using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace KTVServerApp.Script.Command
{
   internal interface IControl
    {
        void Execute(Panel p);
    }
}
