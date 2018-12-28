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
            this.wyctempBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.wyc_tmpDataSet1 = new Okna.wyc_tmpDataSet1();
            this.wyctempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.wyc_tmpDataSet = new Okna.wyc_tmpDataSet();
            this.wyc_tempTableAdapter = new Okna.wyc_tmpDataSetTableAdapters.wyc_tempTableAdapter();
            this.wyctmpDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.wyc_tempTableAdapter1 = new Okna.wyc_tmpDataSet1TableAdapters.wyc_tempTableAdapter();
            this.wyc_tempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wyctempBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyc_tmpDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyctempBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyc_tmpDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyctmpDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyc_tempBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet2";
            reportDataSource1.Value = this.wyc_tempBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Okna.print.wyc_prt.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1036, 863);
            this.reportViewer1.TabIndex = 0;
            // 
            // wyctempBindingSource1
            // 
            this.wyctempBindingSource1.DataMember = "wyc_temp";
            this.wyctempBindingSource1.DataSource = this.wyc_tmpDataSet1;
            // 
            // wyc_tmpDataSet1
            // 
            this.wyc_tmpDataSet1.DataSetName = "wyc_tmpDataSet1";
            this.wyc_tmpDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // wyctempBindingSource
            // 
            this.wyctempBindingSource.DataMember = "wyc_temp";
            this.wyctempBindingSource.DataSource = this.wyc_tmpDataSet;
            // 
            // wyc_tmpDataSet
            // 
            this.wyc_tmpDataSet.DataSetName = "wyc_tmpDataSet";
            this.wyc_tmpDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // wyc_tempTableAdapter
            // 
            this.wyc_tempTableAdapter.ClearBeforeFill = true;
            // 
            // wyctmpDataSetBindingSource
            // 
            this.wyctmpDataSetBindingSource.DataSource = this.wyc_tmpDataSet;
            this.wyctmpDataSetBindingSource.Position = 0;
            // 
            // wyc_tempTableAdapter1
            // 
            this.wyc_tempTableAdapter1.ClearBeforeFill = true;
            // 
            // wyc_tempBindingSource
            // 
            this.wyc_tempBindingSource.DataMember = "wyc_temp";
            this.wyc_tempBindingSource.DataSource = this.wyc_tmpDataSet1;
            // 
            // wycena_prt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 863);
            this.Controls.Add(this.reportViewer1);
            this.Name = "wycena_prt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "wycena_prt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.wycena_prt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wyctempBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyc_tmpDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyctempBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyc_tmpDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyctmpDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wyc_tempBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private wyc_tmpDataSet wyc_tmpDataSet;
        private System.Windows.Forms.BindingSource wyctempBindingSource;
        private wyc_tmpDataSetTableAdapters.wyc_tempTableAdapter wyc_tempTableAdapter;
        private System.Windows.Forms.BindingSource wyctmpDataSetBindingSource;
        private wyc_tmpDataSet1 wyc_tmpDataSet1;
        private System.Windows.Forms.BindingSource wyctempBindingSource1;
        private wyc_tmpDataSet1TableAdapters.wyc_tempTableAdapter wyc_tempTableAdapter1;
        private System.Windows.Forms.BindingSource wyc_tempBindingSource;
    }
}