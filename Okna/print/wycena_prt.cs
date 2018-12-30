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
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace Okna.print
{
    public partial class wycena_prt : Form
    {
        private wycena wycena;
        public wycena_prt(wycena wycena)
        {
            this.wycena = wycena;
            InitializeComponent();
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
        }
        private void wycena_prt_Load(object sender, EventArgs e)
        {
            GetData();
            Parameters();
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        void GetData()
        {
            string fblocation = AppDomain.CurrentDomain.BaseDirectory + "DBAPP.fdb";
            wyc_tmp tmp = new wyc_tmp();
            string cs = @"UserID=SYSDBA;Password=masterkey;Database=" + fblocation + "";
            FbConnection cn = new FbConnection(cs);
            FbDataAdapter da = new FbDataAdapter("SELECT * FROM wyc_temp", cn);
            da.Fill(tmp, tmp.Tables[0].TableName);

            ReportDataSource rds = new ReportDataSource("wyc_temp", tmp.Tables[0]);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.Refresh();
        }

        void Parameters()
        {
            ReportParameter[] param = new ReportParameter[]
            {
                new ReportParameter("kwotaSlownie", wycena.slownie.Text),
            };
            reportViewer1.LocalReport.SetParameters(param);
        }
    }
}
