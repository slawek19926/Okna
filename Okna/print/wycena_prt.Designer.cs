namespace Okna.print
{
    partial class wycena_prt
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.wyc_tmpDataSet = new Okna.wyc_tmpDataSet();
            this.wyctempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.wyc_tempTableAdapter = new Okna.wyc_tmpDataSetTableAdapters.wyc_tempTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.wyc_tmpDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyctempBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet2";
            reportDataSource1.Value = this.wyctempBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Okna.print.wyc_prt.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // wyc_tmpDataSet
            // 
            this.wyc_tmpDataSet.DataSetName = "wyc_tmpDataSet";
            this.wyc_tmpDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // wyctempBindingSource
            // 
            this.wyctempBindingSource.DataMember = "wyc_temp";
            this.wyctempBindingSource.DataSource = this.wyc_tmpDataSet;
            // 
            // wyc_tempTableAdapter
            // 
            this.wyc_tempTableAdapter.ClearBeforeFill = true;
            // 
            // wycena_prt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "wycena_prt";
            this.Text = "wycena_prt";
            this.Load += new System.EventHandler(this.wycena_prt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wyc_tmpDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyctempBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private wyc_tmpDataSet wyc_tmpDataSet;
        private System.Windows.Forms.BindingSource wyctempBindingSource;
        private wyc_tmpDataSetTableAdapters.wyc_tempTableAdapter wyc_tempTableAdapter;
    }
}