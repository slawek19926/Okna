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
            //DataTable dt = new DataTable();
            //var source = wycena.dataGridView1.DataSource;

            //while(source is BindingSource)
            //{
            //    source = ((BindingSource)source).DataSource;
            //}
            //var table = source as DataTable;
            //if (table != null)
            //{
            //    dt = table;
            //    var reportSource = new ReportDataSource("wycenaDruk", dt);
            //    reportViewer1.Reset();
            //    reportViewer1.ProcessingMode = ProcessingMode.Local;
            //    reportViewer1.LocalReport.ReportPath = "printWycena.rdlc";
            //    reportViewer1.LocalReport.DataSources.Clear();
            //    reportViewer1.LocalReport.DataSources.Add(reportSource);
            //    reportViewer1.RefreshReport();
            //}

            //reportViewer1.RefreshReport();
        }
    }
}
