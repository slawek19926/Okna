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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wycena));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.numerek = new System.Windows.Forms.Label();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.numberBAZA = new System.Windows.Forms.Label();
            this.userID = new System.Windows.Forms.Label();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.indeks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.po_r = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ilosc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wart_n = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wart_b = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Narzut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.print_btn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.addBTN = new MaterialSkin.Controls.MaterialRaisedButton();
            this.editBTN = new MaterialSkin.Controls.MaterialRaisedButton();
            this.deleteBTN = new MaterialSkin.Controls.MaterialRaisedButton();
            this.saveBTN = new MaterialSkin.Controls.MaterialRaisedButton();
            this.userBTN = new MaterialSkin.Controls.MaterialRaisedButton();
            this.zaplaconoTXT = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.klientTXT = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.slownie = new MaterialSkin.Controls.MaterialLabel();
            this.sumaTXT = new MaterialSkin.Controls.MaterialLabel();
            this.resztaTXT = new MaterialSkin.Controls.MaterialLabel();
            this.wycena_nr = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // numerek
            // 
            this.numerek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numerek.AutoSize = true;
            this.numerek.Location = new System.Drawing.Point(1104, 465);
            this.numerek.Name = "numerek";
            this.numerek.Size = new System.Drawing.Size(35, 13);
            this.numerek.TabIndex = 10;
            this.numerek.Text = "label5";
            this.numerek.Visible = false;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // numberBAZA
            // 
            this.numberBAZA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numberBAZA.AutoSize = true;
            this.numberBAZA.Location = new System.Drawing.Point(974, 467);
            this.numberBAZA.Name = "numberBAZA";
            this.numberBAZA.Size = new System.Drawing.Size(35, 13);
            this.numberBAZA.TabIndex = 11;
            this.numberBAZA.Text = "label5";
            this.numberBAZA.Visible = false;
            // 
            // userID
            // 
            this.userID.AutoSize = true;
            this.userID.Location = new System.Drawing.Point(827, 467);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(35, 13);
            this.userID.TabIndex = 12;
            this.userID.Text = "label5";
            this.userID.Visible = false;
            // 
            // metroGrid1
            // 
            this.metroGrid1.AllowUserToAddRows = false;
            this.metroGrid1.AllowUserToDeleteRows = false;
            this.metroGrid1.AllowUserToResizeColumns = false;
            this.metroGrid1.AllowUserToResizeRows = false;
            this.metroGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.metroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.metroGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.metroGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indeks,
            this.nazwa,
            this.cena,
            this.rabat,
            this.po_r,
            this.ilosc,
            this.wart_n,
            this.wart_b,
            this.Narzut});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGrid1.DefaultCellStyle = dataGridViewCellStyle2;
            this.metroGrid1.EnableHeadersVisualStyles = false;
            this.metroGrid1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGrid1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.metroGrid1.Location = new System.Drawing.Point(13, 118);
            this.metroGrid1.Name = "metroGrid1";
            this.metroGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.metroGrid1.RowHeadersVisible = false;
            this.metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.Size = new System.Drawing.Size(1129, 207);
            this.metroGrid1.TabIndex = 13;
            this.metroGrid1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroGrid1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.metroGrid1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DynList_CellValueChanged);
            this.metroGrid1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.metroGrid1_RowPrePaint);
            this.metroGrid1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.metroGrid1.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DynList_RowValidated);
            this.metroGrid1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // indeks
            // 
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
            this.rabat.ReadOnly = true;
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
            // print_btn
            // 
            this.print_btn.AutoSize = true;
            this.print_btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.print_btn.Depth = 0;
            this.print_btn.Icon = null;
            this.print_btn.Location = new System.Drawing.Point(302, 334);
            this.print_btn.MouseState = MaterialSkin.MouseState.HOVER;
            this.print_btn.Name = "print_btn";
            this.print_btn.Primary = true;
            this.print_btn.Size = new System.Drawing.Size(73, 36);
            this.print_btn.TabIndex = 14;
            this.print_btn.Text = "Drukuj";
            this.print_btn.UseVisualStyleBackColor = true;
            this.print_btn.Click += new System.EventHandler(this.print_btn_Click_1);
            // 
            // addBTN
            // 
            this.addBTN.AutoSize = true;
            this.addBTN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addBTN.Depth = 0;
            this.addBTN.Icon = null;
            this.addBTN.Location = new System.Drawing.Point(17, 334);
            this.addBTN.MouseState = MaterialSkin.MouseState.HOVER;
            this.addBTN.Name = "addBTN";
            this.addBTN.Primary = true;
            this.addBTN.Size = new System.Drawing.Size(65, 36);
            this.addBTN.TabIndex = 15;
            this.addBTN.Text = "Dodaj";
            this.addBTN.UseVisualStyleBackColor = true;
            this.addBTN.Click += new System.EventHandler(this.addBTN_Click);
            // 
            // editBTN
            // 
            this.editBTN.AutoSize = true;
            this.editBTN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.editBTN.Depth = 0;
            this.editBTN.Icon = null;
            this.editBTN.Location = new System.Drawing.Point(88, 334);
            this.editBTN.MouseState = MaterialSkin.MouseState.HOVER;
            this.editBTN.Name = "editBTN";
            this.editBTN.Primary = true;
            this.editBTN.Size = new System.Drawing.Size(71, 36);
            this.editBTN.TabIndex = 16;
            this.editBTN.Text = "Edytuj";
            this.editBTN.UseVisualStyleBackColor = true;
            this.editBTN.Click += new System.EventHandler(this.editBTN_Click);
            // 
            // deleteBTN
            // 
            this.deleteBTN.AutoSize = true;
            this.deleteBTN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deleteBTN.Depth = 0;
            this.deleteBTN.Icon = null;
            this.deleteBTN.Location = new System.Drawing.Point(165, 334);
            this.deleteBTN.MouseState = MaterialSkin.MouseState.HOVER;
            this.deleteBTN.Name = "deleteBTN";
            this.deleteBTN.Primary = true;
            this.deleteBTN.Size = new System.Drawing.Size(57, 36);
            this.deleteBTN.TabIndex = 17;
            this.deleteBTN.Text = "Usuń";
            this.deleteBTN.UseVisualStyleBackColor = true;
            this.deleteBTN.Click += new System.EventHandler(this.deleteBTN_Click);
            // 
            // saveBTN
            // 
            this.saveBTN.AutoSize = true;
            this.saveBTN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveBTN.Depth = 0;
            this.saveBTN.Icon = null;
            this.saveBTN.Location = new System.Drawing.Point(228, 334);
            this.saveBTN.MouseState = MaterialSkin.MouseState.HOVER;
            this.saveBTN.Name = "saveBTN";
            this.saveBTN.Primary = true;
            this.saveBTN.Size = new System.Drawing.Size(68, 36);
            this.saveBTN.TabIndex = 18;
            this.saveBTN.Text = "Zapisz";
            this.saveBTN.UseVisualStyleBackColor = true;
            this.saveBTN.Click += new System.EventHandler(this.saveBTN_Click);
            // 
            // userBTN
            // 
            this.userBTN.AutoSize = true;
            this.userBTN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.userBTN.Depth = 0;
            this.userBTN.Icon = null;
            this.userBTN.Location = new System.Drawing.Point(228, 392);
            this.userBTN.MouseState = MaterialSkin.MouseState.HOVER;
            this.userBTN.Name = "userBTN";
            this.userBTN.Primary = true;
            this.userBTN.Size = new System.Drawing.Size(137, 36);
            this.userBTN.TabIndex = 19;
            this.userBTN.Text = "Wybierz klienta";
            this.userBTN.UseVisualStyleBackColor = true;
            this.userBTN.Click += new System.EventHandler(this.userBTN_Click);
            // 
            // zaplaconoTXT
            // 
            this.zaplaconoTXT.Depth = 0;
            this.zaplaconoTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.zaplaconoTXT.Hint = "";
            this.zaplaconoTXT.Location = new System.Drawing.Point(111, 384);
            this.zaplaconoTXT.MaxLength = 32767;
            this.zaplaconoTXT.MouseState = MaterialSkin.MouseState.HOVER;
            this.zaplaconoTXT.Name = "zaplaconoTXT";
            this.zaplaconoTXT.PasswordChar = '\0';
            this.zaplaconoTXT.SelectedText = "";
            this.zaplaconoTXT.SelectionLength = 0;
            this.zaplaconoTXT.SelectionStart = 0;
            this.zaplaconoTXT.Size = new System.Drawing.Size(111, 23);
            this.zaplaconoTXT.TabIndex = 20;
            this.zaplaconoTXT.TabStop = false;
            this.zaplaconoTXT.Text = "0";
            this.zaplaconoTXT.UseSystemPasswordChar = false;
            this.zaplaconoTXT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.zaplaconoTXT_KeyDown);
            this.zaplaconoTXT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.zaplaconoTXT_KeyPress);
            this.zaplaconoTXT.TextChanged += new System.EventHandler(this.zaplaconoTXT_TextChanged);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(371, 400);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(110, 19);
            this.materialLabel1.TabIndex = 21;
            this.materialLabel1.Text = "Wybrany klient:";
            // 
            // klientTXT
            // 
            this.klientTXT.AutoSize = true;
            this.klientTXT.Depth = 0;
            this.klientTXT.Font = new System.Drawing.Font("Roboto", 11F);
            this.klientTXT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.klientTXT.Location = new System.Drawing.Point(487, 400);
            this.klientTXT.MouseState = MaterialSkin.MouseState.HOVER;
            this.klientTXT.Name = "klientTXT";
            this.klientTXT.Size = new System.Drawing.Size(0, 19);
            this.klientTXT.TabIndex = 22;
            this.klientTXT.TextChanged += new System.EventHandler(this.klients_TextChanged);
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(10, 394);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(84, 19);
            this.materialLabel2.TabIndex = 23;
            this.materialLabel2.Text = "Zapłacono:";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(10, 418);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(58, 19);
            this.materialLabel3.TabIndex = 24;
            this.materialLabel3.Text = "Reszta:";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(10, 440);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(83, 19);
            this.materialLabel4.TabIndex = 25;
            this.materialLabel4.Text = "Do zapłaty:";
            // 
            // slownie
            // 
            this.slownie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.slownie.Depth = 0;
            this.slownie.Font = new System.Drawing.Font("Roboto", 11F);
            this.slownie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.slownie.Location = new System.Drawing.Point(13, 478);
            this.slownie.MouseState = MaterialSkin.MouseState.HOVER;
            this.slownie.Name = "slownie";
            this.slownie.Size = new System.Drawing.Size(1129, 19);
            this.slownie.TabIndex = 26;
            this.slownie.Text = "slownie";
            // 
            // sumaTXT
            // 
            this.sumaTXT.AutoSize = true;
            this.sumaTXT.Depth = 0;
            this.sumaTXT.Font = new System.Drawing.Font("Roboto", 11F);
            this.sumaTXT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.sumaTXT.Location = new System.Drawing.Point(108, 440);
            this.sumaTXT.MouseState = MaterialSkin.MouseState.HOVER;
            this.sumaTXT.Name = "sumaTXT";
            this.sumaTXT.Size = new System.Drawing.Size(36, 19);
            this.sumaTXT.TabIndex = 27;
            this.sumaTXT.Text = "0,00";
            // 
            // resztaTXT
            // 
            this.resztaTXT.AutoSize = true;
            this.resztaTXT.Depth = 0;
            this.resztaTXT.Font = new System.Drawing.Font("Roboto", 11F);
            this.resztaTXT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.resztaTXT.Location = new System.Drawing.Point(108, 418);
            this.resztaTXT.MouseState = MaterialSkin.MouseState.HOVER;
            this.resztaTXT.Name = "resztaTXT";
            this.resztaTXT.Size = new System.Drawing.Size(36, 19);
            this.resztaTXT.TabIndex = 28;
            this.resztaTXT.Text = "0,00";
            // 
            // wycena_nr
            // 
            this.wycena_nr.Depth = 0;
            this.wycena_nr.Font = new System.Drawing.Font("Roboto", 11F);
            this.wycena_nr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.wycena_nr.Location = new System.Drawing.Point(13, 66);
            this.wycena_nr.MouseState = MaterialSkin.MouseState.HOVER;
            this.wycena_nr.Name = "wycena_nr";
            this.wycena_nr.Size = new System.Drawing.Size(1135, 49);
            this.wycena_nr.TabIndex = 29;
            this.wycena_nr.Text = "Wycena nr";
            this.wycena_nr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wycena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 506);
            this.Controls.Add(this.wycena_nr);
            this.Controls.Add(this.resztaTXT);
            this.Controls.Add(this.sumaTXT);
            this.Controls.Add(this.slownie);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.klientTXT);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.zaplaconoTXT);
            this.Controls.Add(this.userBTN);
            this.Controls.Add(this.saveBTN);
            this.Controls.Add(this.deleteBTN);
            this.Controls.Add(this.editBTN);
            this.Controls.Add(this.addBTN);
            this.Controls.Add(this.print_btn);
            this.Controls.Add(this.userID);
            this.Controls.Add(this.numberBAZA);
            this.Controls.Add(this.numerek);
            this.Controls.Add(this.metroGrid1);
            this.Name = "wycena";
            this.ShowIcon = false;
            this.Text = "wycena";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.wycena_FormClosing);
            this.Load += new System.EventHandler(this.wycena_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        public System.Windows.Forms.Label numerek;
        private System.Windows.Forms.Label numberBAZA;
        private System.Windows.Forms.Label userID;
        private System.Windows.Forms.DataGridViewTextBoxColumn indeks;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn cena;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn po_r;
        private System.Windows.Forms.DataGridViewTextBoxColumn ilosc;
        private System.Windows.Forms.DataGridViewTextBoxColumn wart_n;
        private System.Windows.Forms.DataGridViewTextBoxColumn wart_b;
        private System.Windows.Forms.DataGridViewTextBoxColumn Narzut;
        public MetroFramework.Controls.MetroGrid metroGrid1;
        private MaterialSkin.Controls.MaterialRaisedButton print_btn;
        private MaterialSkin.Controls.MaterialRaisedButton addBTN;
        private MaterialSkin.Controls.MaterialRaisedButton editBTN;
        private MaterialSkin.Controls.MaterialRaisedButton deleteBTN;
        private MaterialSkin.Controls.MaterialRaisedButton saveBTN;
        private MaterialSkin.Controls.MaterialRaisedButton userBTN;
        private MaterialSkin.Controls.MaterialSingleLineTextField zaplaconoTXT;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel slownie;
        private MaterialSkin.Controls.MaterialLabel sumaTXT;
        private MaterialSkin.Controls.MaterialLabel resztaTXT;
        public MaterialSkin.Controls.MaterialLabel klientTXT;
        public MaterialSkin.Controls.MaterialLabel wycena_nr;
    }
}