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
            reportViewer1.LocalReport.ReportPath = "C:/Users/alleg/Documents/Visual Studio 2017/Projects/Okna/Okna/print/wycenaPrint.rdlc";
            ReportParameter param_Name = new ReportParameter("testParam", "customValue");
            reportViewer1.LocalReport.SetParameters(param_Name);
            reportViewer1.RefreshReport();
        }
    }
}
