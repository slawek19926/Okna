using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Okna.print
{
    public partial class wycena_prt : Form
    {
        public wycena_prt()
        {
            InitializeComponent();
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
        }
        private void wycena_prt_Load(object sender, EventArgs e)
        {
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'wyc_tmpDataSet1.wyc_temp' . Możesz go przenieść lub usunąć.
            this.wyc_tempTableAdapter1.Fill(this.wyc_tmpDataSet1.wyc_temp);
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'wyc_tmpDataSet.wyc_temp' . Możesz go przenieść lub usunąć.
            this.wyc_tempTableAdapter.Fill(this.wyc_tmpDataSet.wyc_temp);
            reportViewer1.RefreshReport();
        }
    }
}
