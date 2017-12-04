using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okna
{
    class RoundedButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }
}
