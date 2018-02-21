using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MaterialSkin.Animations;

namespace Okna.boxes
{
    public partial class frmZalogowany : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public frmZalogowany()
        {
            InitializeComponent();
            Text = "Zalogowano pomyślnie";
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            Close();
        }
    }
}
