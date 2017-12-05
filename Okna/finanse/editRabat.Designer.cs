namespace Okna.finanse
{
    partial class editRabat
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nazwaTXT = new System.Windows.Forms.TextBox();
            this.symbolTXT = new System.Windows.Forms.TextBox();
            this.wartoscTXT = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.idN = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nazwa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Symbol:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Wartość:";
            // 
            // nazwaTXT
            // 
            this.nazwaTXT.Location = new System.Drawing.Point(84, 32);
            this.nazwaTXT.Name = "nazwaTXT";
            this.nazwaTXT.Size = new System.Drawing.Size(334, 20);
            this.nazwaTXT.TabIndex = 3;
            // 
            // symbolTXT
            // 
            this.symbolTXT.Location = new System.Drawing.Point(84, 63);
            this.symbolTXT.Name = "symbolTXT";
            this.symbolTXT.Size = new System.Drawing.Size(334, 20);
            this.symbolTXT.TabIndex = 4;
            // 
            // wartoscTXT
            // 
            this.wartoscTXT.Location = new System.Drawing.Point(84, 92);
            this.wartoscTXT.Name = "wartoscTXT";
            this.wartoscTXT.Size = new System.Drawing.Size(334, 20);
            this.wartoscTXT.TabIndex = 5;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(326, 130);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(92, 37);
            this.saveBtn.TabIndex = 6;
            this.saveBtn.Text = "Zapisz";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // idN
            // 
            this.idN.AutoSize = true;
            this.idN.Location = new System.Drawing.Point(141, 139);
            this.idN.Name = "idN";
            this.idN.Size = new System.Drawing.Size(35, 13);
            this.idN.TabIndex = 7;
            this.idN.Text = "label4";
            // 
            // editRabat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 179);
            this.Controls.Add(this.idN);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.wartoscTXT);
            this.Controls.Add(this.symbolTXT);
            this.Controls.Add(this.nazwaTXT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "editRabat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "editRabat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nazwaTXT;
        private System.Windows.Forms.TextBox symbolTXT;
        private System.Windows.Forms.TextBox wartoscTXT;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label idN;
    }
}