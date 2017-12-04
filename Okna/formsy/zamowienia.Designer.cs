namespace Okna
{
    partial class zamowienia
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.detailBtn = new System.Windows.Forms.Button();
            this.changeStat = new System.Windows.Forms.Button();
            this.wystawFV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(862, 389);
            this.dataGridView1.TabIndex = 0;
            // 
            // detailBtn
            // 
            this.detailBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.detailBtn.Location = new System.Drawing.Point(13, 423);
            this.detailBtn.Name = "detailBtn";
            this.detailBtn.Size = new System.Drawing.Size(121, 38);
            this.detailBtn.TabIndex = 1;
            this.detailBtn.Text = "Szczegóły";
            this.detailBtn.UseVisualStyleBackColor = true;
            this.detailBtn.Click += new System.EventHandler(this.detailBtn_Click);
            // 
            // changeStat
            // 
            this.changeStat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.changeStat.Location = new System.Drawing.Point(140, 423);
            this.changeStat.Name = "changeStat";
            this.changeStat.Size = new System.Drawing.Size(121, 38);
            this.changeStat.TabIndex = 1;
            this.changeStat.Text = "Zmień status";
            this.changeStat.UseVisualStyleBackColor = true;
            this.changeStat.Click += new System.EventHandler(this.changeStat_Click);
            // 
            // wystawFV
            // 
            this.wystawFV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wystawFV.Location = new System.Drawing.Point(267, 423);
            this.wystawFV.Name = "wystawFV";
            this.wystawFV.Size = new System.Drawing.Size(121, 38);
            this.wystawFV.TabIndex = 2;
            this.wystawFV.Text = "Wystaw fakturę";
            this.wystawFV.UseVisualStyleBackColor = true;
            this.wystawFV.Click += new System.EventHandler(this.wystawFV_Click);
            // 
            // zamowienia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 473);
            this.Controls.Add(this.wystawFV);
            this.Controls.Add(this.changeStat);
            this.Controls.Add(this.detailBtn);
            this.Controls.Add(this.dataGridView1);
            this.Name = "zamowienia";
            this.Text = "zamowienia";
            this.Load += new System.EventHandler(this.zamowienia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button detailBtn;
        private System.Windows.Forms.Button changeStat;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button wystawFV;
    }
}