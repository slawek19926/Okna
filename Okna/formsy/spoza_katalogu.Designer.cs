namespace Okna.formsy
{
    partial class spoza_katalogu
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
            this.dodajBTN = new System.Windows.Forms.Button();
            this.anulujBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.indeksTXT = new System.Windows.Forms.TextBox();
            this.nazwaTXT = new System.Windows.Forms.RichTextBox();
            this.nettoTXT = new System.Windows.Forms.TextBox();
            this.bruttoTXT = new System.Windows.Forms.TextBox();
            this.iloscTXT = new System.Windows.Forms.TextBox();
            this.stawkiVAT = new System.Windows.Forms.ComboBox();
            this.jednostki = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dodajBTN
            // 
            this.dodajBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dodajBTN.Location = new System.Drawing.Point(29, 254);
            this.dodajBTN.Name = "dodajBTN";
            this.dodajBTN.Size = new System.Drawing.Size(129, 51);
            this.dodajBTN.TabIndex = 7;
            this.dodajBTN.Text = "Dodaj";
            this.dodajBTN.UseVisualStyleBackColor = true;
            // 
            // anulujBTN
            // 
            this.anulujBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.anulujBTN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.anulujBTN.Location = new System.Drawing.Point(285, 254);
            this.anulujBTN.Name = "anulujBTN";
            this.anulujBTN.Size = new System.Drawing.Size(129, 51);
            this.anulujBTN.TabIndex = 8;
            this.anulujBTN.Text = "Anuluj";
            this.anulujBTN.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Indeks:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nazwa:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cena netto:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Cena brutto:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Ilość:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(55, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "J.M.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Stawka VAT:";
            // 
            // indeksTXT
            // 
            this.indeksTXT.Location = new System.Drawing.Point(93, 27);
            this.indeksTXT.Name = "indeksTXT";
            this.indeksTXT.Size = new System.Drawing.Size(258, 20);
            this.indeksTXT.TabIndex = 0;
            // 
            // nazwaTXT
            // 
            this.nazwaTXT.Location = new System.Drawing.Point(93, 53);
            this.nazwaTXT.Name = "nazwaTXT";
            this.nazwaTXT.Size = new System.Drawing.Size(258, 40);
            this.nazwaTXT.TabIndex = 1;
            this.nazwaTXT.Text = "";
            // 
            // nettoTXT
            // 
            this.nettoTXT.Location = new System.Drawing.Point(93, 99);
            this.nettoTXT.Name = "nettoTXT";
            this.nettoTXT.Size = new System.Drawing.Size(126, 20);
            this.nettoTXT.TabIndex = 2;
            this.nettoTXT.TextChanged += new System.EventHandler(this.nettoTXT_TextChanged);
            // 
            // bruttoTXT
            // 
            this.bruttoTXT.Location = new System.Drawing.Point(93, 152);
            this.bruttoTXT.Name = "bruttoTXT";
            this.bruttoTXT.Size = new System.Drawing.Size(126, 20);
            this.bruttoTXT.TabIndex = 4;
            // 
            // iloscTXT
            // 
            this.iloscTXT.Location = new System.Drawing.Point(93, 178);
            this.iloscTXT.Name = "iloscTXT";
            this.iloscTXT.Size = new System.Drawing.Size(126, 20);
            this.iloscTXT.TabIndex = 5;
            // 
            // stawkiVAT
            // 
            this.stawkiVAT.FormattingEnabled = true;
            this.stawkiVAT.Location = new System.Drawing.Point(93, 125);
            this.stawkiVAT.Name = "stawkiVAT";
            this.stawkiVAT.Size = new System.Drawing.Size(126, 21);
            this.stawkiVAT.TabIndex = 3;
            this.stawkiVAT.SelectedIndexChanged += new System.EventHandler(this.stawkiVAT_SelectedIndexChanged);
            // 
            // jednostki
            // 
            this.jednostki.FormattingEnabled = true;
            this.jednostki.Location = new System.Drawing.Point(93, 204);
            this.jednostki.Name = "jednostki";
            this.jednostki.Size = new System.Drawing.Size(126, 21);
            this.jednostki.TabIndex = 6;
            // 
            // spoza_katalogu
            // 
            this.AcceptButton = this.dodajBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.anulujBTN;
            this.ClientSize = new System.Drawing.Size(448, 329);
            this.Controls.Add(this.jednostki);
            this.Controls.Add(this.stawkiVAT);
            this.Controls.Add(this.iloscTXT);
            this.Controls.Add(this.bruttoTXT);
            this.Controls.Add(this.nettoTXT);
            this.Controls.Add(this.nazwaTXT);
            this.Controls.Add(this.indeksTXT);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.anulujBTN);
            this.Controls.Add(this.dodajBTN);
            this.Name = "spoza_katalogu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "spoza_katalogu";
            this.Load += new System.EventHandler(this.spoza_katalogu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dodajBTN;
        private System.Windows.Forms.Button anulujBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox indeksTXT;
        private System.Windows.Forms.RichTextBox nazwaTXT;
        private System.Windows.Forms.TextBox nettoTXT;
        private System.Windows.Forms.TextBox bruttoTXT;
        private System.Windows.Forms.TextBox iloscTXT;
        private System.Windows.Forms.ComboBox stawkiVAT;
        private System.Windows.Forms.ComboBox jednostki;
    }
}