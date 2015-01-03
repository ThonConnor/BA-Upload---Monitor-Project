using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KTVServerApp.Script.Command
{
    internal class SingerCommand:IControl
    {
        
        public void Execute(Panel parent)
        {
            parent.Controls.Clear();
            Singer s = new Singer();
            s.TopLevel = false;
            s.Dock = DockStyle.Fill;
            parent.Controls.Add(s);
            //s.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            //| System.Windows.Forms.AnchorStyles.Right)));
            s.Show();
            
           
        }
    }
}
