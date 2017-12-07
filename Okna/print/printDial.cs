using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Okna.print
{
    public partial class printDial : Form
    {
        wycena wycena;
        public printDial(wycena wycena)
        {
            InitializeComponent();
            this.wycena = wycena;
        }

        private void printDial_Load(object sender, EventArgs e)
        {
            ReportParameter param_Name = new ReportParameter("wycenaNR", wycena.wycena_nr.Text, false);
            reportViewer1.LocalReport.SetParameters(param_Name);
            reportViewer1.RefreshReport();
        }
    }
}
