namespace Okna.finanse
{
    partial class rabaty
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
            this.editRabat = new System.Windows.Forms.Button();
            this.addBTN = new System.Windows.Forms.Button();
            this.delBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(629, 411);
            this.dataGridView1.TabIndex = 0;
            // 
            // editRabat
            // 
            this.editRabat.Location = new System.Drawing.Point(506, 429);
            this.editRabat.Name = "editRabat";
            this.editRabat.Size = new System.Drawing.Size(135, 46);
            this.editRabat.TabIndex = 1;
            this.editRabat.Text = "Edytuj";
            this.editRabat.UseVisualStyleBackColor = true;
            this.editRabat.Click += new System.EventHandler(this.editRabat_Click);
            // 
            // addBTN
            // 
            this.addBTN.Location = new System.Drawing.Point(224, 429);
            this.addBTN.Name = "addBTN";
            this.addBTN.Size = new System.Drawing.Size(135, 46);
            this.addBTN.TabIndex = 2;
            this.addBTN.Text = "Dodaj";
            this.addBTN.UseVisualStyleBackColor = true;
            this.addBTN.Click += new System.EventHandler(this.addBTN_Click);
            // 
            // delBTN
            // 
            this.delBTN.Location = new System.Drawing.Point(365, 429);
            this.delBTN.Name = "delBTN";
            this.delBTN.Size = new System.Drawing.Size(135, 46);
            this.delBTN.TabIndex = 3;
            this.delBTN.Text = "Usuń";
            this.delBTN.UseVisualStyleBackColor = true;
            this.delBTN.Click += new System.EventHandler(this.delBTN_Click);
            // 
            // rabaty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 487);
            this.Controls.Add(this.delBTN);
            this.Controls.Add(this.addBTN);
            this.Controls.Add(this.editRabat);
            this.Controls.Add(this.dataGridView1);
            this.Name = "rabaty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "rabaty";
            this.Load += new System.EventHandler(this.rabaty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button editRabat;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button addBTN;
        private System.Windows.Forms.Button delBTN;
    }
}