namespace Okna.formsy
{
    partial class klients
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
            this.useBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.klientBOX = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // useBTN
            // 
            this.useBTN.Location = new System.Drawing.Point(69, 162);
            this.useBTN.Name = "useBTN";
            this.useBTN.Size = new System.Drawing.Size(150, 46);
            this.useBTN.TabIndex = 1;
            this.useBTN.Text = "Wybierz";
            this.useBTN.UseVisualStyleBackColor = true;
            this.useBTN.Click += new System.EventHandler(this.useBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(93, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Wybierz klienta";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // klientBOX
            // 
            this.klientBOX.FormattingEnabled = true;
            this.klientBOX.Location = new System.Drawing.Point(40, 94);
            this.klientBOX.Name = "klientBOX";
            this.klientBOX.Size = new System.Drawing.Size(198, 21);
            this.klientBOX.TabIndex = 3;
            // 
            // klients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 247);
            this.Controls.Add(this.klientBOX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.useBTN);
            this.Name = "klients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "klient";
            this.Load += new System.EventHandler(this.klient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button useBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox klientBOX;
    }
}