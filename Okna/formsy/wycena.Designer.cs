namespace Okna
{
    partial class wycena
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
            this.addBTN = new System.Windows.Forms.Button();
            this.editBTN = new System.Windows.Forms.Button();
            this.deleteBTN = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sumaTXT = new System.Windows.Forms.Label();
            this.resztaTXT = new System.Windows.Forms.Label();
            this.zaplaconoTXT = new System.Windows.Forms.TextBox();
            this.slownie = new System.Windows.Forms.Label();
            this.saveBTN = new System.Windows.Forms.Button();
            this.userBTN = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.klientTXT = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.wycena_nr = new System.Windows.Forms.Label();
            this.print_btn = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.numerek = new System.Windows.Forms.Label();
            this.indeks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.po_r = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ilosc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wart_n = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wart_b = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Narzut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addBTN
            // 
            this.addBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addBTN.Location = new System.Drawing.Point(13, 331);
            this.addBTN.Name = "addBTN";
            this.addBTN.Size = new System.Drawing.Size(111, 42);
            this.addBTN.TabIndex = 0;
            this.addBTN.Text = "Dodaj";
            this.addBTN.UseVisualStyleBackColor = true;
            this.addBTN.Click += new System.EventHandler(this.addBTN_Click);
            // 
            // editBTN
            // 
            this.editBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editBTN.Location = new System.Drawing.Point(130, 331);
            this.editBTN.Name = "editBTN";
            this.editBTN.Size = new System.Drawing.Size(111, 42);
            this.editBTN.TabIndex = 0;
            this.editBTN.Text = "Edytuj";
            this.editBTN.UseVisualStyleBackColor = true;
            this.editBTN.Click += new System.EventHandler(this.editBTN_Click);
            // 
            // deleteBTN
            // 
            this.deleteBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteBTN.Location = new System.Drawing.Point(247, 331);
            this.deleteBTN.Name = "deleteBTN";
            this.deleteBTN.Size = new System.Drawing.Size(111, 42);
            this.deleteBTN.TabIndex = 0;
            this.deleteBTN.Text = "Usuń";
            this.deleteBTN.UseVisualStyleBackColor = true;
            this.deleteBTN.Click += new System.EventHandler(this.deleteBTN_Click);
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
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indeks,
            this.nazwa,
            this.cena,
            this.rabat,
            this.po_r,
            this.ilosc,
            this.wart_n,
            this.wart_b,
            this.Narzut});
            this.dataGridView1.Location = new System.Drawing.Point(13, 54);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(918, 271);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DynList_CellValueChanged);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DynList_RowValidated);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 394);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Zpłacono:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 418);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Reszta:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(13, 440);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Do zpałaty:";
            // 
            // sumaTXT
            // 
            this.sumaTXT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sumaTXT.AutoSize = true;
            this.sumaTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sumaTXT.ForeColor = System.Drawing.Color.Red;
            this.sumaTXT.Location = new System.Drawing.Point(108, 440);
            this.sumaTXT.Name = "sumaTXT";
            this.sumaTXT.Size = new System.Drawing.Size(36, 16);
            this.sumaTXT.TabIndex = 4;
            this.sumaTXT.Text = "0,00";
            // 
            // resztaTXT
            // 
            this.resztaTXT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resztaTXT.AutoSize = true;
            this.resztaTXT.Location = new System.Drawing.Point(108, 418);
            this.resztaTXT.Name = "resztaTXT";
            this.resztaTXT.Size = new System.Drawing.Size(28, 13);
            this.resztaTXT.TabIndex = 3;
            this.resztaTXT.Text = "0,00";
            // 
            // zaplaconoTXT
            // 
            this.zaplaconoTXT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.zaplaconoTXT.Location = new System.Drawing.Point(111, 389);
            this.zaplaconoTXT.Name = "zaplaconoTXT";
            this.zaplaconoTXT.Size = new System.Drawing.Size(100, 20);
            this.zaplaconoTXT.TabIndex = 5;
            this.zaplaconoTXT.TextChanged += new System.EventHandler(this.zaplaconoTXT_TextChanged);
            this.zaplaconoTXT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.zaplaconoTXT_KeyDown);
            this.zaplaconoTXT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.zaplaconoTXT_KeyPress);
            // 
            // slownie
            // 
            this.slownie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.slownie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.slownie.Location = new System.Drawing.Point(13, 465);
            this.slownie.Name = "slownie";
            this.slownie.Size = new System.Drawing.Size(1000, 15);
            this.slownie.TabIndex = 3;
            this.slownie.Text = "slownie";
            this.slownie.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saveBTN
            // 
            this.saveBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveBTN.Location = new System.Drawing.Point(364, 331);
            this.saveBTN.Name = "saveBTN";
            this.saveBTN.Size = new System.Drawing.Size(111, 42);
            this.saveBTN.TabIndex = 0;
            this.saveBTN.Text = "Zapisz";
            this.saveBTN.UseVisualStyleBackColor = true;
            this.saveBTN.Click += new System.EventHandler(this.saveBTN_Click);
            // 
            // userBTN
            // 
            this.userBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.userBTN.Location = new System.Drawing.Point(247, 389);
            this.userBTN.Name = "userBTN";
            this.userBTN.Size = new System.Drawing.Size(111, 42);
            this.userBTN.TabIndex = 0;
            this.userBTN.UseVisualStyleBackColor = true;
            this.userBTN.Click += new System.EventHandler(this.userBTN_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(366, 404);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Wybrany klient:";
            // 
            // klientTXT
            // 
            this.klientTXT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.klientTXT.AutoSize = true;
            this.klientTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.klientTXT.Location = new System.Drawing.Point(475, 404);
            this.klientTXT.Name = "klientTXT";
            this.klientTXT.Size = new System.Drawing.Size(0, 15);
            this.klientTXT.TabIndex = 7;
            this.klientTXT.TextChanged += new System.EventHandler(this.klients_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.wycena_nr);
            this.panel1.Location = new System.Drawing.Point(13, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(918, 36);
            this.panel1.TabIndex = 8;
            // 
            // wycena_nr
            // 
            this.wycena_nr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wycena_nr.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.wycena_nr.Location = new System.Drawing.Point(0, 0);
            this.wycena_nr.Name = "wycena_nr";
            this.wycena_nr.Size = new System.Drawing.Size(918, 36);
            this.wycena_nr.TabIndex = 10;
            this.wycena_nr.Text = "Wycena nr: wycena_nr";
            this.wycena_nr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // print_btn
            // 
            this.print_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.print_btn.Location = new System.Drawing.Point(481, 331);
            this.print_btn.Name = "print_btn";
            this.print_btn.Size = new System.Drawing.Size(111, 42);
            this.print_btn.TabIndex = 0;
            this.print_btn.Text = "Drukuj";
            this.print_btn.UseVisualStyleBackColor = true;
            this.print_btn.Click += new System.EventHandler(this.print_btn_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(718, 483);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(222, 23);
            this.progressBar.TabIndex = 9;
            // 
            // numerek
            // 
            this.numerek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numerek.AutoSize = true;
            this.numerek.Location = new System.Drawing.Point(896, 465);
            this.numerek.Name = "numerek";
            this.numerek.Size = new System.Drawing.Size(35, 13);
            this.numerek.TabIndex = 10;
            this.numerek.Text = "label5";
            this.numerek.Visible = false;
            // 
            // indeks
            // 
            this.indeks.Frozen = true;
            this.indeks.HeaderText = "Indeks";
            this.indeks.Name = "indeks";
            this.indeks.ReadOnly = true;
            // 
            // nazwa
            // 
            this.nazwa.HeaderText = "Nazwa";
            this.nazwa.Name = "nazwa";
            this.nazwa.ReadOnly = true;
            // 
            // cena
            // 
            this.cena.HeaderText = "Netto";
            this.cena.Name = "cena";
            this.cena.ReadOnly = true;
            // 
            // rabat
            // 
            this.rabat.HeaderText = "Rabat";
            this.rabat.Name = "rabat";
            // 
            // po_r
            // 
            this.po_r.HeaderText = "Po rabacie";
            this.po_r.Name = "po_r";
            this.po_r.ReadOnly = true;
            // 
            // ilosc
            // 
            this.ilosc.HeaderText = "Ilość";
            this.ilosc.Name = "ilosc";
            this.ilosc.ReadOnly = true;
            // 
            // wart_n
            // 
            this.wart_n.HeaderText = "Wartość netto";
            this.wart_n.Name = "wart_n";
            this.wart_n.ReadOnly = true;
            // 
            // wart_b
            // 
            this.wart_b.HeaderText = "Wartość brutto";
            this.wart_b.Name = "wart_b";
            this.wart_b.ReadOnly = true;
            // 
            // Narzut
            // 
            this.Narzut.HeaderText = "Narzut";
            this.Narzut.Name = "Narzut";
            this.Narzut.ReadOnly = true;
            // 
            // wycena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 506);
            this.Controls.Add(this.numerek);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.klientTXT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.zaplaconoTXT);
            this.Controls.Add(this.sumaTXT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.resztaTXT);
            this.Controls.Add(this.slownie);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.userBTN);
            this.Controls.Add(this.print_btn);
            this.Controls.Add(this.saveBTN);
            this.Controls.Add(this.deleteBTN);
            this.Controls.Add(this.editBTN);
            this.Controls.Add(this.addBTN);
            this.Name = "wycena";
            this.ShowIcon = false;
            this.Text = "wycena";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.wycena_FormClosing);
            this.Load += new System.EventHandler(this.wycena_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addBTN;
        private System.Windows.Forms.Button editBTN;
        private System.Windows.Forms.Button deleteBTN;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label sumaTXT;
        private System.Windows.Forms.Label resztaTXT;
        private System.Windows.Forms.TextBox zaplaconoTXT;
        private System.Windows.Forms.Label slownie;
        private System.Windows.Forms.Button saveBTN;
        private System.Windows.Forms.Button userBTN;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label klientTXT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label wycena_nr;
        private System.Windows.Forms.Button print_btn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label numerek;
        private System.Windows.Forms.DataGridViewTextBoxColumn indeks;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn cena;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn po_r;
        private System.Windows.Forms.DataGridViewTextBoxColumn ilosc;
        private System.Windows.Forms.DataGridViewTextBoxColumn wart_n;
        private System.Windows.Forms.DataGridViewTextBoxColumn wart_b;
        private System.Windows.Forms.DataGridViewTextBoxColumn Narzut;
    }
}