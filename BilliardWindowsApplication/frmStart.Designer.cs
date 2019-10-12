namespace BilliardWindowsApplication
{
    partial class frmStart
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbClub = new BilliardWindowsApplication.pictureboxRound();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlSetupClub = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pbClub);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(408, 186);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1149, 358);
            this.panel1.TabIndex = 150;
            this.panel1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 250F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(721, -25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(508, 408);
            this.label3.TabIndex = 150;
            this.label3.Text = "14";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(469, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 78);
            this.label2.TabIndex = 149;
            this.label2.Text = "Billiard";
            // 
            // pbClub
            // 
            this.pbClub.BackColor = System.Drawing.Color.DarkGray;
            this.pbClub.ErrorImage = null;
            this.pbClub.Location = new System.Drawing.Point(117, 77);
            this.pbClub.Name = "pbClub";
            this.pbClub.Size = new System.Drawing.Size(220, 220);
            this.pbClub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbClub.TabIndex = 148;
            this.pbClub.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImage = global::BilliardWindowsApplication.Properties.Resources.off;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Location = new System.Drawing.Point(1250, 712);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(114, 43);
            this.panel2.TabIndex = 5;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            // 
            // pnlSetupClub
            // 
            this.pnlSetupClub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSetupClub.BackgroundImage = global::BilliardWindowsApplication.Properties.Resources.setup;
            this.pnlSetupClub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlSetupClub.Location = new System.Drawing.Point(12, 712);
            this.pnlSetupClub.Name = "pnlSetupClub";
            this.pnlSetupClub.Size = new System.Drawing.Size(114, 43);
            this.pnlSetupClub.TabIndex = 4;
            this.pnlSetupClub.Click += new System.EventHandler(this.pnlSetupClub_Click);
            this.pnlSetupClub.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSetupClub_Paint_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Image = global::BilliardWindowsApplication.Properties.Resources.start;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1889, 1027);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(754, 779);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(432, 23);
            this.progressBar.TabIndex = 151;
            this.progressBar.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1376, 778);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlSetupClub);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmStart";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStart_FormClosing);
            this.Load += new System.EventHandler(this.frmStart_Load);
            this.Shown += new System.EventHandler(this.frmStart_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlSetupClub;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private pictureboxRound pbClub;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer timer1;
    }
}