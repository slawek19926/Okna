namespace Okna.test
{
    partial class newWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(newWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.xPos = new System.Windows.Forms.Label();
            this.yPos = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.osY = new System.Windows.Forms.TextBox();
            this.osX = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            // 
            // xPos
            // 
            this.xPos.AutoSize = true;
            this.xPos.Location = new System.Drawing.Point(61, 8);
            this.xPos.Name = "xPos";
            this.xPos.Size = new System.Drawing.Size(35, 13);
            this.xPos.TabIndex = 3;
            this.xPos.Text = "label3";
            // 
            // yPos
            // 
            this.yPos.AutoSize = true;
            this.yPos.Location = new System.Drawing.Point(61, 21);
            this.yPos.Name = "yPos";
            this.yPos.Size = new System.Drawing.Size(35, 13);
            this.yPos.TabIndex = 4;
            this.yPos.Text = "label4";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.osY);
            this.panel1.Controls.Add(this.osX);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(205, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(732, 419);
            this.panel1.TabIndex = 5;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.xPos_MouseMove);
            // 
            // osY
            // 
            this.osY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.osY.Location = new System.Drawing.Point(625, 186);
            this.osY.Name = "osY";
            this.osY.Size = new System.Drawing.Size(100, 20);
            this.osY.TabIndex = 1;
            this.osY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.osY_Enter);
            // 
            // osX
            // 
            this.osX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.osX.Location = new System.Drawing.Point(311, 392);
            this.osX.Name = "osX";
            this.osX.Size = new System.Drawing.Size(100, 20);
            this.osX.TabIndex = 0;
            this.osX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.osX_Enter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(311, 140);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(800, 800);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(24, 497);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(21, 20);
            this.toolStripButton1.Text = "Otwórz szablon";
            // 
            // newWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 497);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.yPos);
            this.Controls.Add(this.xPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "newWindow";
            this.ShowIcon = false;
            this.Text = "newWindow";
            this.Load += new System.EventHandler(this.newWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label xPos;
        private System.Windows.Forms.Label yPos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox osX;
        private System.Windows.Forms.TextBox osY;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}