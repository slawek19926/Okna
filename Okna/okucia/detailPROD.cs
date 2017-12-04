using System;
using System.Drawing;
using System.Windows.Forms;

namespace Okna.okucia
{
    public partial class detailPROD : Form
    {
        private katalog katalog;

        public detailPROD(katalog katalog)
        {
            InitializeComponent();
            this.katalog = katalog;
            Text = "Szczegóły produktu " + katalog.dataGridView1.SelectedCells[1].Value.ToString();         
            imgProd.Location = new Point(0, 0);
        }

        private void detailPROD_Load(object sender, EventArgs e)
        {
            string indeks = katalog.dataGridView1.SelectedCells[1].Value.ToString();
            string nazwa = katalog.dataGridView1.SelectedCells[2].Value.ToString();
            string opis = katalog.dataGridView1.SelectedCells[9].Value.ToString();
            int id = Convert.ToInt32(katalog.dataGridView1.SelectedCells[0].Value.ToString());
            nazaTXT.Text = indeks + " " + nazwa;
            try
            {
                imgProd.ImageLocation = "http://wektor.czest.pl/img/okucia/" + indeks + ".jpg";
                imgProd.MouseWheel += new MouseEventHandler(imgProd_OnMouseWheel);
                imgProd.MouseUp += new MouseEventHandler(imgProd_MouseUp);
                imgProd.MouseDown += new MouseEventHandler(imgProd_MouseDown);
                imgProd.MouseMove += new MouseEventHandler(imgProd_MouseMove);
                imgProd.MouseLeave += new EventHandler(imgProd_MouseLeave);
            }
            catch
            {
                imgProd.Image = imgProd.ErrorImage;
            }
            opisTXT.Text = opis;
            if (id >= 1 && id <= 10)
            {
                imgDet.Visible = true;
                imgDet.ImageLocation = "http://wektor.czest.pl/img/okucia/Tabelka_Z.jpg";
            }
            else
            {
                imgDet.Visible = false;
            }
        }

        void imgProd_OnMouseWheel(object sender, MouseEventArgs mea)
        {

            if (imgProd.Image != null)
            {
                if (mea.Delta > 0)
                {
                    if ((imgProd.Width < (15 * Width)) && (imgProd.Height < (15 * Height)))
                    {
                        imgProd.Width = (int)(imgProd.Width * 1.25);
                        imgProd.Height = (int)(imgProd.Height * 1.25);

                        imgProd.Top = (int)(mea.Y - 1.25 * (mea.Y - imgProd.Top));
                        imgProd.Left = (int)(mea.X - 1.25 * (mea.X - imgProd.Left));
                    }
                }
                else
                {
                    if ((imgProd.Width > (Width / 15)) && (imgProd.Height > (Height / 15)))
                    {
                        imgProd.Width = (int)(imgProd.Width / 1.25);
                        imgProd.Height = (int)(imgProd.Height / 1.25);

                        imgProd.Top = (int)(mea.Y - 0.80 * (mea.Y - imgProd.Top));
                        imgProd.Left = (int)(mea.X - 0.80 * (mea.X - imgProd.Left));
                    }
                }
            }
        }

        private bool Dragging;
        private int xPos;
        private int yPos;
        void imgProd_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }
        private void imgProd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Dragging = true;
                xPos = e.X;
                yPos = e.Y;
            }
        }
        private void imgProd_MouseMove(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            if (Dragging && c != null)
            {
                c.Top = e.Y + c.Top - yPos;
                c.Left = e.X + c.Left - xPos;
            }
        }
        private void imgProd_MouseLeave(object sender, EventArgs e)
        {
            imgProd.Location = new Point(0, 0);
            imgProd.Width = 314;
            imgProd.Height = 490;
        }

        private void rotateM90_Click(object sender, EventArgs e)
        {
            imgProd.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
            imgProd.Image.RotateFlip(RotateFlipType.Rotate180FlipY);
            imgProd.Refresh();
        }
    }
}
