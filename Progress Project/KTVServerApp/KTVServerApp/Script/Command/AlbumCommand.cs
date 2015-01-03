using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace KTVServerApp.Script.Command
{
    internal class AlbumCommand:IControl
    {
      
        public void Execute(Panel parent)
        {
            parent.Controls.Clear();
            Album a = new Album();
            a.TopLevel = false;
            a.Show();
            parent.Controls.Add(a);
        }
    }
}
