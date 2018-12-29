namespace Okna.print
{
    partial class pirnt_wycena
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.wycdetBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.wyc_details = new Okna.wyc_details();
            this.firmadetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.wycdetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.klientdetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wycdetBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyc_details)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firmadetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wycdetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.klientdetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // wycdetBindingSource1
            // 
            this.wycdetBindingSource1.DataMember = "wyc_det";
            this.wycdetBindingSource1.DataSource = this.wyc_details;
            // 
            // wyc_details
            // 
            this.wyc_details.DataSetName = "wyc_details";
            this.wyc_details.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // firmadetBindingSource
            // 
            this.firmadetBindingSource.DataMember = "firma_det";
            this.firmadetBindingSource.DataSource = this.wyc_details;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "wyc_det";
            reportDataSource1.Value = this.wycdetBindingSource1;
            reportDataSource2.Name = "firma_det";
            reportDataSource2.Value = this.firmadetBindingSource;
            reportDataSource3.Name = "klient_det";
            reportDataSource3.Value = this.klientdetBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Okna.print.wyc_det.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // wycdetBindingSource
            // 
            this.wycdetBindingSource.DataMember = "wyc_det";
            this.wycdetBindingSource.DataSource = this.wyc_details;
            // 
            // klientdetBindingSource
            // 
            this.klientdetBindingSource.DataMember = "klient_det";
            this.klientdetBindingSource.DataSource = this.wyc_details;
            // 
            // pirnt_wycena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "pirnt_wycena";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pirnt_wycena";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.pirnt_wycena_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wycdetBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyc_details)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firmadetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wycdetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.klientdetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource wycdetBindingSource;
        private wyc_details wyc_details;
        private System.Windows.Forms.BindingSource firmadetBindingSource;
        private System.Windows.Forms.BindingSource wycdetBindingSource1;
        private System.Windows.Forms.BindingSource klientdetBindingSource;
    }
}