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
            
        }
        private  string rvConnection = @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + "SampleDatabase.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
        private string rvSQL = @"SELECT * FROM dbo.wyc.temp";
        private void wycena_prt_Load(object sender, EventArgs e)
        {
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'wyc_tmpDataSet.wyc_temp' . Możesz go przenieść lub usunąć.
            this.wyc_tempTableAdapter.Fill(this.wyc_tmpDataSet.wyc_temp);
            //using (SqlConnection sqlConn = new SqlConnection(rvConnection))
            //using (SqlDataAdapter da = new SqlDataAdapter(rvSQL, rvConnection))
            //{
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    DataTable dt = ds.Tables[0];

            //    this.reportViewer1.Reset();
            //    this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            //    this.reportViewer1.LocalReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory + "ssrsExport.rdlc";
            //    ReportDataSource reportDataSource = new ReportDataSource();
            //    // Must match the DataSet in the RDLC
            //    reportDataSource.Name = "DataSet3";
            //    reportDataSource.Value = ds.Tables[0];
            //    this.reportViewer1.LocalReport.DataSources.Add("DataSet3",ds.Tables[0]);
            //    this.reportViewer1.RefreshReport();
            //}
            reportViewer1.RefreshReport();
        }
    }
}
