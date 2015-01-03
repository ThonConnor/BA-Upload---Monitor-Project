using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KTVServerApp.Script.Command
{
    class ControlForm
    {
        private IControl com;
        private Panel p;
        public ControlForm(Panel p)
        {
            this.p = p;
        }
        public void OpenForm(Form form)
        {
            p.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            p.Controls.Add(form);
            //s.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            //| System.Windows.Forms.AnchorStyles.Right)));
            form.Show();
        }
    }
}
