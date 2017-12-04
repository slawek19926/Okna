namespace Okna.okucia
{
    partial class detailPROD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(detailPROD));
            this.imgPanel = new System.Windows.Forms.Panel();
            this.rotateM90 = new System.Windows.Forms.Button();
            this.imgProd = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imgDet = new System.Windows.Forms.PictureBox();
            this.opisTXT = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.nazaTXT = new System.Windows.Forms.Label();
            this.imgPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProd)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgDet)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgPanel
            // 
            this.imgPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgPanel.Controls.Add(this.rotateM90);
            this.imgPanel.Controls.Add(this.imgProd);
            this.imgPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.imgPanel.Location = new System.Drawing.Point(0, 0);
            this.imgPanel.Name = "imgPanel";
            this.imgPanel.Size = new System.Drawing.Size(314, 541);
            this.imgPanel.TabIndex = 0;
            // 
            // rotateM90
            // 
            this.rotateM90.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rotateM90.BackgroundImage")));
            this.rotateM90.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rotateM90.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rotateM90.Location = new System.Drawing.Point(10, 494);
            this.rotateM90.Name = "rotateM90";
            this.rotateM90.Size = new System.Drawing.Size(42, 40);
            this.rotateM90.TabIndex = 1;
            this.rotateM90.UseVisualStyleBackColor = true;
            this.rotateM90.Click += new System.EventHandler(this.rotateM90_Click);
            // 
            // imgProd
            // 
            this.imgProd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgProd.ErrorImage = null;
            this.imgProd.Location = new System.Drawing.Point(-2, -2);
            this.imgProd.Name = "imgProd";
            this.imgProd.Size = new System.Drawing.Size(314, 490);
            this.imgProd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgProd.TabIndex = 0;
            this.imgProd.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(314, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(632, 541);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.imgDet);
            this.panel1.Controls.Add(this.opisTXT);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 490);
            this.panel1.TabIndex = 1;
            // 
            // imgDet
            // 
            this.imgDet.Location = new System.Drawing.Point(59, 127);
            this.imgDet.Name = "imgDet";
            this.imgDet.Size = new System.Drawing.Size(516, 312);
            this.imgDet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgDet.TabIndex = 2;
            this.imgDet.TabStop = false;
            // 
            // opisTXT
            // 
            this.opisTXT.Dock = System.Windows.Forms.DockStyle.Top;
            this.opisTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.opisTXT.Location = new System.Drawing.Point(0, 20);
            this.opisTXT.Name = "opisTXT";
            this.opisTXT.Size = new System.Drawing.Size(624, 104);
            this.opisTXT.TabIndex = 1;
            this.opisTXT.Text = "label2";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(624, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zastosowanie:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.nazaTXT);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(628, 47);
            this.panel3.TabIndex = 0;
            // 
            // nazaTXT
            // 
            this.nazaTXT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nazaTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nazaTXT.Location = new System.Drawing.Point(0, 0);
            this.nazaTXT.Name = "nazaTXT";
            this.nazaTXT.Size = new System.Drawing.Size(624, 43);
            this.nazaTXT.TabIndex = 0;
            this.nazaTXT.Text = "label1";
            this.nazaTXT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // detailPROD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 541);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.imgPanel);
            this.Name = "detailPROD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "detailPROD";
            this.Load += new System.EventHandler(this.detailPROD_Load);
            this.imgPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgProd)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgDet)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel imgPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox imgProd;
        private System.Windows.Forms.Label nazaTXT;
        private System.Windows.Forms.Button rotateM90;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label opisTXT;
        private System.Windows.Forms.PictureBox imgDet;
    }
}