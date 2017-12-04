namespace Okna.formsy
{
    partial class wyceny_detail
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
            this.klient = new System.Windows.Forms.Label();
            this.wycena_nr = new System.Windows.Forms.Label();
            this.realizuj = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.changeKLI = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(839, 271);
            this.dataGridView1.TabIndex = 0;
            // 
            // klient
            // 
            this.klient.AutoSize = true;
            this.klient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.klient.Location = new System.Drawing.Point(70, 345);
            this.klient.Name = "klient";
            this.klient.Size = new System.Drawing.Size(47, 15);
            this.klient.TabIndex = 1;
            this.klient.Text = "label1";
            // 
            // wycena_nr
            // 
            this.wycena_nr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wycena_nr.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.wycena_nr.Location = new System.Drawing.Point(12, 8);
            this.wycena_nr.Name = "wycena_nr";
            this.wycena_nr.Size = new System.Drawing.Size(840, 36);
            this.wycena_nr.TabIndex = 11;
            this.wycena_nr.Text = "Wycena nr: wycena_nr";
            this.wycena_nr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // realizuj
            // 
            this.realizuj.Location = new System.Drawing.Point(727, 357);
            this.realizuj.Name = "realizuj";
            this.realizuj.Size = new System.Drawing.Size(124, 48);
            this.realizuj.TabIndex = 12;
            this.realizuj.Text = "Realizuj";
            this.realizuj.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(16, 345);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Klient:";
            // 
            // changeKLI
            // 
            this.changeKLI.Location = new System.Drawing.Point(12, 373);
            this.changeKLI.Name = "changeKLI";
            this.changeKLI.Size = new System.Drawing.Size(105, 32);
            this.changeKLI.TabIndex = 14;
            this.changeKLI.Text = "Zmień klienta";
            this.changeKLI.UseVisualStyleBackColor = true;
            this.changeKLI.Click += new System.EventHandler(this.changeKLI_Click);
            // 
            // wyceny_detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 417);
            this.Controls.Add(this.changeKLI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.realizuj);
            this.Controls.Add(this.wycena_nr);
            this.Controls.Add(this.klient);
            this.Controls.Add(this.dataGridView1);
            this.Name = "wyceny_detail";
            this.Text = "wyceny_detail";
            this.Load += new System.EventHandler(this.wyceny_detail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label klient;
        private System.Windows.Forms.Label wycena_nr;
        private System.Windows.Forms.Button realizuj;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button changeKLI;
    }
}