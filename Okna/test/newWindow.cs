using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Okna.test
{
    
    public partial class newWindow : Form
    {
        private Form1 Form1;
        private int defX = 100;
        private int defY = 100;
        private int Xpos;
        private int Ypos;
        public newWindow(Form1 Form1)
        {
            InitializeComponent();
            this.Form1 = Form1;
            WindowState = FormWindowState.Maximized;
            Text = "Nowy projekt test 123";
            Cursor = new Cursor(Cursor.Current.Handle);
            int pozX = Cursor.Position.X;
            int pozY = Cursor.Position.Y;
            xPos.Text = pozX.ToString();
            yPos.Text = pozY.ToString();
            
        }
        
        private void newWindow_Load(object sender, EventArgs e)
        {
            osX.Text = defX.ToString();
            osY.Text = defY.ToString();
            pictureBox1.Width = defX;
            pictureBox1.Height = defY;
            Xpos = (panel1.Width / 2) - (pictureBox1.Width / 2);
            Ypos = (panel1.Height / 2) - (pictureBox1.Height / 2);
            pictureBox1.Location = new Point(Xpos, Ypos);
        }

        private void xPos_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = new Cursor(Cursor.Current.Handle);
            int pozX = Cursor.Position.X;
            int pozY = Cursor.Position.Y;
            xPos.Text = pozX.ToString();
            yPos.Text = pozY.ToString();
        }

        private void osX_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrWhiteSpace(osX.Text))
                {
                    pictureBox1.Width = defX;
                    Xpos = (panel1.Width / 2) - (pictureBox1.Width / 2);
                    Ypos = (panel1.Height / 2) - (pictureBox1.Height / 2);
                    pictureBox1.Location = new Point(Xpos, Ypos);
                }
                else
                {
                    pictureBox1.Width = Convert.ToInt32(osX.Text);
                    Xpos = (panel1.Width / 2) - (pictureBox1.Width / 2);
                    Ypos = (panel1.Height / 2) - (pictureBox1.Height / 2);
                    pictureBox1.Location = new Point(Xpos, Ypos);
                    pictureBox1.Refresh();
                }
            }
        }

        private void osY_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrWhiteSpace(osY.Text))
                {
                    pictureBox1.Width = defY;
                    Xpos = (panel1.Width / 2) - (pictureBox1.Width / 2);
                    Ypos = (panel1.Height / 2) - (pictureBox1.Height / 2);
                    pictureBox1.Location = new Point(Xpos, Ypos);
                }
                else
                {
                    pictureBox1.Height = Convert.ToInt32(osY.Text);
                    Xpos = (panel1.Width / 2) - (pictureBox1.Width / 2);
                    Ypos = (panel1.Height / 2) - (pictureBox1.Height / 2);
                    pictureBox1.Location = new Point(Xpos, Ypos);
                    pictureBox1.Refresh();
                }
            }
        }
    }
}
