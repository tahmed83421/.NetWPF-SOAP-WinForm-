namespace BilliardWindowsApplication
{
    partial class frmMemoryDetails
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMemoryDetails));
			this.label1 = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.pb1 = new System.Windows.Forms.PictureBox();
			this.pb2 = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.pictureboxRound1 = new BilliardWindowsApplication.pictureboxRound();
			this.colscore = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.turn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colpoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colset = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colplayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colshot = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colisplayrecord = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colreplayset = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pb2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureboxRound1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Times New Roman", 25F);
			this.label1.ForeColor = System.Drawing.Color.LawnGreen;
			this.label1.Location = new System.Drawing.Point(0, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(1090, 39);
			this.label1.TabIndex = 0;
			this.label1.Text = "Memory Details";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.ColumnHeadersVisible = false;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colscore,
            this.turn,
            this.colpoint,
            this.colset,
            this.colplayer,
            this.colshot,
            this.colisplayrecord,
            this.colreplayset});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.Location = new System.Drawing.Point(408, 183);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(262, 435);
			this.dataGridView1.TabIndex = 2;
			this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
			// 
			// pb1
			// 
			this.pb1.Image = global::BilliardWindowsApplication.Properties.Resources.billplay;
			this.pb1.Location = new System.Drawing.Point(12, 57);
			this.pb1.Name = "pb1";
			this.pb1.Size = new System.Drawing.Size(404, 768);
			this.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pb1.TabIndex = 164;
			this.pb1.TabStop = false;
			this.pb1.Visible = false;
			this.pb1.Paint += new System.Windows.Forms.PaintEventHandler(this.WhiteRePlayer_Paint);
			// 
			// pb2
			// 
			this.pb2.Image = global::BilliardWindowsApplication.Properties.Resources.billplay;
			this.pb2.Location = new System.Drawing.Point(676, 57);
			this.pb2.Name = "pb2";
			this.pb2.Size = new System.Drawing.Size(404, 768);
			this.pb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pb2.TabIndex = 165;
			this.pb2.TabStop = false;
			this.pb2.Visible = false;
			this.pb2.Paint += new System.Windows.Forms.PaintEventHandler(this.YellowReplayer_Paint);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// pictureboxRound1
			// 
			this.pictureboxRound1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureboxRound1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pictureboxRound1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureboxRound1.Image = global::BilliardWindowsApplication.Properties.Resources.close;
			this.pictureboxRound1.Location = new System.Drawing.Point(1052, 0);
			this.pictureboxRound1.Name = "pictureboxRound1";
			this.pictureboxRound1.Size = new System.Drawing.Size(38, 39);
			this.pictureboxRound1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureboxRound1.TabIndex = 1;
			this.pictureboxRound1.TabStop = false;
			this.pictureboxRound1.Click += new System.EventHandler(this.pictureboxRound1_Click);
			// 
			// colscore
			// 
			this.colscore.FillWeight = 100.8403F;
			this.colscore.HeaderText = "Column1";
			this.colscore.Name = "colscore";
			this.colscore.ReadOnly = true;
			this.colscore.Width = 80;
			// 
			// turn
			// 
			this.turn.FillWeight = 101.8757F;
			this.turn.HeaderText = "Column1";
			this.turn.Name = "turn";
			this.turn.ReadOnly = true;
			this.turn.Width = 50;
			// 
			// colpoint
			// 
			this.colpoint.FillWeight = 101.82F;
			this.colpoint.HeaderText = "Column1";
			this.colpoint.Name = "colpoint";
			this.colpoint.ReadOnly = true;
			this.colpoint.Width = 50;
			// 
			// colset
			// 
			this.colset.FillWeight = 95.46397F;
			this.colset.HeaderText = "Column1";
			this.colset.Name = "colset";
			this.colset.ReadOnly = true;
			this.colset.Width = 50;
			// 
			// colplayer
			// 
			this.colplayer.HeaderText = "colplayer";
			this.colplayer.Name = "colplayer";
			this.colplayer.ReadOnly = true;
			this.colplayer.Visible = false;
			// 
			// colshot
			// 
			this.colshot.HeaderText = "colshot";
			this.colshot.Name = "colshot";
			this.colshot.ReadOnly = true;
			this.colshot.Visible = false;
			// 
			// colisplayrecord
			// 
			this.colisplayrecord.HeaderText = "colisplayrecord";
			this.colisplayrecord.Name = "colisplayrecord";
			this.colisplayrecord.ReadOnly = true;
			this.colisplayrecord.Visible = false;
			// 
			// colreplayset
			// 
			this.colreplayset.HeaderText = "playColor";
			this.colreplayset.Name = "colreplayset";
			this.colreplayset.ReadOnly = true;
			this.colreplayset.Visible = false;
			// 
			// frmMemoryDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(1088, 831);
			this.ControlBox = false;
			this.Controls.Add(this.pb2);
			this.Controls.Add(this.pb1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.pictureboxRound1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMemoryDetails";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmMemoryDetails_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pb2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureboxRound1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private pictureboxRound pictureboxRound1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pb1;
		private System.Windows.Forms.PictureBox pb2;
        private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.DataGridViewTextBoxColumn colscore;
		private System.Windows.Forms.DataGridViewTextBoxColumn turn;
		private System.Windows.Forms.DataGridViewTextBoxColumn colpoint;
		private System.Windows.Forms.DataGridViewTextBoxColumn colset;
		private System.Windows.Forms.DataGridViewTextBoxColumn colplayer;
		private System.Windows.Forms.DataGridViewTextBoxColumn colshot;
		private System.Windows.Forms.DataGridViewTextBoxColumn colisplayrecord;
		private System.Windows.Forms.DataGridViewTextBoxColumn colreplayset;
    }
}