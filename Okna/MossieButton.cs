using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Okna
{
    class MossieButton : Button
    {

        private static System.Drawing.Font _normalFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        private static System.Drawing.Color _back = System.Drawing.Color.Gray;
        private static System.Drawing.Color _border = System.Drawing.Color.FromArgb(30,80,245);
        private static System.Drawing.Color _activeBorder = System.Drawing.Color.Red;
        private static System.Drawing.Color _fore = System.Drawing.Color.White;

        private static Padding _margin = new Padding(5, 0, 5, 0);
        private static Padding _padding = new Padding(3, 3, 3, 3);

        private static System.Drawing.Size _minSize = new System.Drawing.Size(100, 30);

        private bool _active;

        public MossieButton()
            : base()
        {
            base.Font = _normalFont;
            base.BackColor = _border;
            base.ForeColor = _fore;
            base.FlatAppearance.BorderColor = _back;
            base.FlatStyle = FlatStyle.Flat;
            base.Margin = _margin;
            base.Padding = _padding;
            base.MinimumSize = _minSize;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            UseVisualStyleBackColor = false;
        }

        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!_active)
                FlatAppearance.BorderColor = _activeBorder;
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!_active)
                FlatAppearance.BorderColor = _border;
        }

        public void SetStateActive()
        {
            _active = true;
            FlatAppearance.BorderColor = _activeBorder;
        }

        public void SetStateNormal()
        {
            _active = false;
            FlatAppearance.BorderColor = _border;
        }
    }
}
