namespace Okna.formsy
{
    partial class statusForm
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
            this.selectBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.statusBOX = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // selectBTN
            // 
            this.selectBTN.Location = new System.Drawing.Point(65, 106);
            this.selectBTN.Name = "selectBTN";
            this.selectBTN.Size = new System.Drawing.Size(150, 46);
            this.selectBTN.TabIndex = 1;
            this.selectBTN.Text = "Wybierz";
            this.selectBTN.UseVisualStyleBackColor = true;
            this.selectBTN.Click += new System.EventHandler(this.selectBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(84, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Wybierz status";
            // 
            // statusBOX
            // 
            this.statusBOX.FormattingEnabled = true;
            this.statusBOX.Location = new System.Drawing.Point(45, 62);
            this.statusBOX.Name = "statusBOX";
            this.statusBOX.Size = new System.Drawing.Size(191, 21);
            this.statusBOX.TabIndex = 3;
            // 
            // statusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 188);
            this.Controls.Add(this.statusBOX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectBTN);
            this.MaximizeBox = false;
            this.Name = "statusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "statusForm";
            this.Load += new System.EventHandler(this.statusForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox statusBOX;
    }
}