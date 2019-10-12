namespace BilliardWindowsApplication
{
    partial class FrmTeaching
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
            this.lblPw = new System.Windows.Forms.Label();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.lblBallFeature = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTeacherName = new System.Windows.Forms.Label();
            this.txtShotName = new System.Windows.Forms.TextBox();
            this.txtShotDescription = new System.Windows.Forms.TextBox();
            this.cmbShotHistory = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.PictureBox();
            this.btnReplay = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.PictureBox();
            this.picOneShot = new System.Windows.Forms.PictureBox();
            this.picBallFeature = new System.Windows.Forms.PictureBox();
            this.picBilliardTable = new System.Windows.Forms.PictureBox();
            this.picTeacher = new System.Windows.Forms.PictureBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtRailCount = new System.Windows.Forms.TextBox();
            this.timerDraw = new System.Windows.Forms.Timer(this.components);
			this.picDownload = new System.Windows.Forms.PictureBox();
			this.picUpload = new System.Windows.Forms.PictureBox();
			this.lblDownload = new System.Windows.Forms.Label();
			this.lblUpload = new System.Windows.Forms.Label();
            this.picClub = new BilliardWindowsApplication.pictureboxRound();
			this.pbLabel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOneShot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBallFeature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBilliardTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTeacher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picDownload)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClub)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPw
            // 
            this.lblPw.AutoSize = true;
            this.lblPw.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPw.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPw.Location = new System.Drawing.Point(147, 107);
            this.lblPw.Name = "lblPw";
            this.lblPw.Size = new System.Drawing.Size(32, 20);
            this.lblPw.TabIndex = 4;
            this.lblPw.Text = "Pw";
            // 
            // txtPw
            // 
            this.txtPw.BackColor = System.Drawing.SystemColors.Desktop;
            this.txtPw.Font = new System.Drawing.Font("MS UI Gothic", 13F);
            this.txtPw.ForeColor = System.Drawing.SystemColors.Window;
            this.txtPw.Location = new System.Drawing.Point(206, 105);
            this.txtPw.Name = "txtPw";
            this.txtPw.Size = new System.Drawing.Size(259, 25);
            this.txtPw.TabIndex = 5;
            this.txtPw.UseSystemPasswordChar = true;
            this.txtPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPw_KeyDown);
            this.txtPw.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPw_KeyPress);
            // 
            // lblBallFeature
            // 
            this.lblBallFeature.AutoSize = true;
            this.lblBallFeature.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.lblBallFeature.ForeColor = System.Drawing.SystemColors.Control;
            this.lblBallFeature.Location = new System.Drawing.Point(555, 108);
            this.lblBallFeature.Name = "lblBallFeature";
            this.lblBallFeature.Size = new System.Drawing.Size(129, 22);
            this.lblBallFeature.TabIndex = 6;
            this.lblBallFeature.Text = "Balls Feature";
			this.lblBallFeature.Click += new System.EventHandler(this.lblBallFeature_Click);
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(766, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "One shot at a time";
            // 
            // lblTeacherName
            // 
            this.lblTeacherName.AutoSize = true;
            this.lblTeacherName.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.lblTeacherName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTeacherName.Location = new System.Drawing.Point(139, 179);
            this.lblTeacherName.Name = "lblTeacherName";
            this.lblTeacherName.Size = new System.Drawing.Size(79, 20);
            this.lblTeacherName.TabIndex = 148;
			// 
            // txtShotName
            // 
            this.txtShotName.BackColor = System.Drawing.SystemColors.Desktop;
            this.txtShotName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.txtShotName.ForeColor = System.Drawing.SystemColors.Window;
            this.txtShotName.Location = new System.Drawing.Point(143, 315);
            this.txtShotName.Name = "txtShotName";
            this.txtShotName.Size = new System.Drawing.Size(693, 27);
            this.txtShotName.TabIndex = 150;
            this.txtShotName.Text = "Insert New Shot Name";
            this.txtShotName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShotName.Enter += new System.EventHandler(this.txtShotName_Enter);
            this.txtShotName.Leave += new System.EventHandler(this.txtShotName_Leave);
            // 
            // txtShotDescription
            // 
            this.txtShotDescription.BackColor = System.Drawing.SystemColors.Desktop;
            this.txtShotDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.txtShotDescription.ForeColor = System.Drawing.SystemColors.Window;
            this.txtShotDescription.Location = new System.Drawing.Point(144, 379);
            this.txtShotDescription.Multiline = true;
            this.txtShotDescription.Name = "txtShotDescription";
            this.txtShotDescription.Size = new System.Drawing.Size(692, 379);
            this.txtShotDescription.TabIndex = 151;
            this.txtShotDescription.Text = "Description of shot";
            this.txtShotDescription.Enter += new System.EventHandler(this.txtShotDescription_Enter);
            this.txtShotDescription.Leave += new System.EventHandler(this.txtShotDescription_Leave);
            // 
            // cmbShotHistory
            // 
            this.cmbShotHistory.BackColor = System.Drawing.SystemColors.Desktop;
            this.cmbShotHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.cmbShotHistory.ForeColor = System.Drawing.SystemColors.Window;
            this.cmbShotHistory.FormattingEnabled = true;
            this.cmbShotHistory.Location = new System.Drawing.Point(143, 782);
            this.cmbShotHistory.Name = "cmbShotHistory";
            this.cmbShotHistory.Size = new System.Drawing.Size(693, 28);
            this.cmbShotHistory.TabIndex = 152;
            this.cmbShotHistory.SelectedIndexChanged += new System.EventHandler(this.cmbShotHistory_SelectedIndexChanged);
            this.cmbShotHistory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbShotHistory_KeyPress);
            // 
            // btnExit
            // 
            this.btnExit.Image = global::BilliardWindowsApplication.Properties.Resources.exit_button;
            this.btnExit.Location = new System.Drawing.Point(914, 744);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(158, 60);
            this.btnExit.TabIndex = 174;
            this.btnExit.TabStop = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReset
            // 
            this.btnReset.Image = global::BilliardWindowsApplication.Properties.Resources.reset_button;
            this.btnReset.Location = new System.Drawing.Point(914, 657);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(158, 60);
            this.btnReset.TabIndex = 173;
            this.btnReset.TabStop = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
			this.btnSave.Enabled = false;
            this.btnSave.Image = global::BilliardWindowsApplication.Properties.Resources.save_button;
            this.btnSave.Location = new System.Drawing.Point(914, 570);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(158, 60);
            this.btnSave.TabIndex = 172;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReplay
            // 
            this.btnReplay.Enabled = false;
            this.btnReplay.Image = global::BilliardWindowsApplication.Properties.Resources.replay_button;
            this.btnReplay.Location = new System.Drawing.Point(914, 483);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(158, 60);
            this.btnReplay.TabIndex = 171;
            this.btnReplay.TabStop = false;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Image = global::BilliardWindowsApplication.Properties.Resources.cancel_button;
            this.btnCancel.Location = new System.Drawing.Point(914, 395);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(158, 60);
            this.btnCancel.TabIndex = 170;
            this.btnCancel.TabStop = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Image = global::BilliardWindowsApplication.Properties.Resources.start_button;
            this.btnStart.Location = new System.Drawing.Point(914, 309);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(158, 60);
            this.btnStart.TabIndex = 169;
            this.btnStart.TabStop = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // picOneShot
            // 
            this.picOneShot.Image = global::BilliardWindowsApplication.Properties.Resources.white_red_circle;
            this.picOneShot.Location = new System.Drawing.Point(723, 105);
            this.picOneShot.Name = "picOneShot";
            this.picOneShot.Size = new System.Drawing.Size(30, 30);
            this.picOneShot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOneShot.TabIndex = 166;
            this.picOneShot.TabStop = false;
            // 
            // picBallFeature
            // 
            this.picBallFeature.Image = global::BilliardWindowsApplication.Properties.Resources.white_green_circle;
            this.picBallFeature.Location = new System.Drawing.Point(515, 105);
            this.picBallFeature.Name = "picBallFeature";
            this.picBallFeature.Size = new System.Drawing.Size(30, 30);
            this.picBallFeature.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBallFeature.TabIndex = 165;
            this.picBallFeature.TabStop = false;
            this.picBallFeature.Click += new System.EventHandler(this.picBallFeature_Click);
            // 
            // picBilliardTable
            // 
            this.picBilliardTable.Image = global::BilliardWindowsApplication.Properties.Resources.billplay;
            this.picBilliardTable.Location = new System.Drawing.Point(1260, 180);
            this.picBilliardTable.Name = "picBilliardTable";
            this.picBilliardTable.Size = new System.Drawing.Size(440, 800);
            this.picBilliardTable.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBilliardTable.TabIndex = 164;
            this.picBilliardTable.TabStop = false;
            this.picBilliardTable.Tag = "0";
            this.picBilliardTable.Paint += new System.Windows.Forms.PaintEventHandler(this.picBilliardTable_Paint);
            // 
            // picTeacher
            // 
            this.picTeacher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picTeacher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picTeacher.Location = new System.Drawing.Point(144, 207);
            this.picTeacher.Name = "picTeacher";
            this.picTeacher.Size = new System.Drawing.Size(65, 65);
            this.picTeacher.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTeacher.TabIndex = 147;
            this.picTeacher.TabStop = false;
            // 
            // picLogo
            // 
            this.picLogo.Image = global::BilliardWindowsApplication.Properties.Resources.BigLogo;
            this.picLogo.Location = new System.Drawing.Point(590, 18);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(767, 68);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // txtRailCount
            // 
            this.txtRailCount.BackColor = System.Drawing.SystemColors.Desktop;
            this.txtRailCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.txtRailCount.ForeColor = System.Drawing.SystemColors.Window;
            this.txtRailCount.Location = new System.Drawing.Point(982, 191);
            this.txtRailCount.Name = "txtRailCount";
            this.txtRailCount.Size = new System.Drawing.Size(97, 27);
            this.txtRailCount.TabIndex = 175;
            this.txtRailCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timerDraw
            // 
            this.timerDraw.Tick += new System.EventHandler(this.timerDraw_Tick);
            // 
			// picDownload
			// 
			this.picDownload.Image = global::BilliardWindowsApplication.Properties.Resources.white_red_circle;
			this.picDownload.Location = new System.Drawing.Point(723, 150);
			this.picDownload.Name = "picDownload";
			this.picDownload.Size = new System.Drawing.Size(30, 30);
			this.picDownload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picDownload.TabIndex = 179;
			this.picDownload.TabStop = false;
			this.picDownload.Click += new System.EventHandler(this.picDownload_Click);
			// 
			// picUpload
			// 
			this.picUpload.Image = global::BilliardWindowsApplication.Properties.Resources.white_green_circle;
			this.picUpload.Location = new System.Drawing.Point(515, 150);
			this.picUpload.Name = "picUpload";
			this.picUpload.Size = new System.Drawing.Size(30, 30);
			this.picUpload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picUpload.TabIndex = 178;
			this.picUpload.TabStop = false;
			this.picUpload.Click += new System.EventHandler(this.picUpload_Click);
			// 
			// lblDownload
			// 
			this.lblDownload.AutoSize = true;
			this.lblDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
			this.lblDownload.ForeColor = System.Drawing.SystemColors.Control;
			this.lblDownload.Location = new System.Drawing.Point(766, 153);
			this.lblDownload.Name = "lblDownload";
			this.lblDownload.Size = new System.Drawing.Size(98, 22);
			this.lblDownload.TabIndex = 177;
			this.lblDownload.Text = "Download";
			this.lblDownload.Click += new System.EventHandler(this.lblDownload_Click);
			// 
			// lblUpload
			// 
			this.lblUpload.AutoSize = true;
			this.lblUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
			this.lblUpload.ForeColor = System.Drawing.SystemColors.Control;
			this.lblUpload.Location = new System.Drawing.Point(555, 153);
			this.lblUpload.Name = "lblUpload";
			this.lblUpload.Size = new System.Drawing.Size(73, 22);
			this.lblUpload.TabIndex = 176;
			this.lblUpload.Text = "Upload";
			this.lblUpload.Click += new System.EventHandler(this.lblUpload_Click);
			// 
            // picClub
            // 
            this.picClub.BackColor = System.Drawing.Color.DarkGray;
            this.picClub.Location = new System.Drawing.Point(263, 207);
            this.picClub.Name = "picClub";
            this.picClub.Size = new System.Drawing.Size(65, 65);
            this.picClub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picClub.TabIndex = 149;
            this.picClub.TabStop = false;
            // 
			// pbLabel
			// 
			this.pbLabel.BackColor = System.Drawing.Color.Transparent;
			this.pbLabel.Image = global::BilliardWindowsApplication.Properties.Resources.wrong_shot_red;
			this.pbLabel.Location = new System.Drawing.Point(96, 170);
			this.pbLabel.Name = "pbLabel";
			this.pbLabel.Size = new System.Drawing.Size(588, 123);
			this.pbLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbLabel.TabIndex = 180;
			this.pbLabel.TabStop = false;
			this.pbLabel.Visible = false;
			this.pbLabel.Click += new System.EventHandler(this.pbLabel_Click);
			// 
            // FrmTeaching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1904, 1098);
            this.ControlBox = false;
			this.Controls.Add(this.pbLabel);
			this.Controls.Add(this.picDownload);
			this.Controls.Add(this.picUpload);
			this.Controls.Add(this.lblDownload);
			this.Controls.Add(this.lblUpload);
            this.Controls.Add(this.txtRailCount);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReplay);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.picOneShot);
            this.Controls.Add(this.picBallFeature);
            this.Controls.Add(this.picBilliardTable);
            this.Controls.Add(this.cmbShotHistory);
            this.Controls.Add(this.txtShotDescription);
            this.Controls.Add(this.txtShotName);
            this.Controls.Add(this.picTeacher);
            this.Controls.Add(this.picClub);
            this.Controls.Add(this.lblTeacherName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBallFeature);
            this.Controls.Add(this.txtPw);
            this.Controls.Add(this.lblPw);
            this.Controls.Add(this.picLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmTeaching";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmTeaching_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOneShot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBallFeature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBilliardTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTeacher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picDownload)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClub)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.PictureBox picLogo;
		private System.Windows.Forms.Label lblPw;
		private System.Windows.Forms.TextBox txtPw;
		private System.Windows.Forms.Label lblBallFeature;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox picTeacher;
		private pictureboxRound picClub;
		private System.Windows.Forms.Label lblTeacherName;
		private System.Windows.Forms.TextBox txtShotName;
		private System.Windows.Forms.TextBox txtShotDescription;
		private System.Windows.Forms.ComboBox cmbShotHistory;
		private System.Windows.Forms.PictureBox picBilliardTable;
		private System.Windows.Forms.PictureBox picBallFeature;
		private System.Windows.Forms.PictureBox picOneShot;
		private System.Windows.Forms.PictureBox btnStart;
		private System.Windows.Forms.PictureBox btnCancel;
		private System.Windows.Forms.PictureBox btnReplay;
		private System.Windows.Forms.PictureBox btnSave;
		private System.Windows.Forms.PictureBox btnReset;
		private System.Windows.Forms.PictureBox btnExit;
		private System.Windows.Forms.TextBox txtRailCount;
		private System.Windows.Forms.Timer timerDraw;
		private System.Windows.Forms.PictureBox picDownload;
		private System.Windows.Forms.PictureBox picUpload;
		private System.Windows.Forms.Label lblDownload;
		private System.Windows.Forms.Label lblUpload;
		private System.Windows.Forms.PictureBox pbLabel;
    }
}