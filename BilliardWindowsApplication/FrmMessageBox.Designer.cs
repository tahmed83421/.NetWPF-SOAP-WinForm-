namespace BilliardWindowsApplication
{
	partial class FrmMessageBox
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
			this.picBackground = new System.Windows.Forms.PictureBox();
			this.picCancel = new System.Windows.Forms.PictureBox();
			this.picYes = new System.Windows.Forms.PictureBox();
			this.picOK = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picCancel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picYes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picOK)).BeginInit();
			this.SuspendLayout();
			// 
			// picBackground
			// 
			this.picBackground.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picBackground.Image = global::BilliardWindowsApplication.Properties.Resources.ask_save;
			this.picBackground.Location = new System.Drawing.Point(0, 0);
			this.picBackground.Name = "picBackground";
			this.picBackground.Size = new System.Drawing.Size(596, 147);
			this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picBackground.TabIndex = 0;
			this.picBackground.TabStop = false;
			// 
			// picCancel
			// 
			this.picCancel.Image = global::BilliardWindowsApplication.Properties.Resources.no_button;
			this.picCancel.Location = new System.Drawing.Point(359, 77);
			this.picCancel.Name = "picCancel";
			this.picCancel.Size = new System.Drawing.Size(169, 47);
			this.picCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picCancel.TabIndex = 3;
			this.picCancel.TabStop = false;
			this.picCancel.Click += new System.EventHandler(this.picCancel_Click);
			// 
			// picYes
			// 
			this.picYes.Image = global::BilliardWindowsApplication.Properties.Resources.yes_button;
			this.picYes.Location = new System.Drawing.Point(69, 77);
			this.picYes.Name = "picYes";
			this.picYes.Size = new System.Drawing.Size(169, 47);
			this.picYes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picYes.TabIndex = 2;
			this.picYes.TabStop = false;
			this.picYes.Click += new System.EventHandler(this.picOK_Click);
			// 
			// picOK
			// 
			this.picOK.Image = global::BilliardWindowsApplication.Properties.Resources.ok_button;
			this.picOK.Location = new System.Drawing.Point(216, 77);
			this.picOK.Name = "picOK";
			this.picOK.Size = new System.Drawing.Size(169, 47);
			this.picOK.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picOK.TabIndex = 4;
			this.picOK.TabStop = false;
			this.picOK.Visible = false;
			this.picOK.Click += new System.EventHandler(this.picOK_Click_1);
			// 
			// FrmMessageBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(596, 147);
			this.Controls.Add(this.picOK);
			this.Controls.Add(this.picCancel);
			this.Controls.Add(this.picYes);
			this.Controls.Add(this.picBackground);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmMessageBox";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picCancel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picYes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picOK)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picBackground;
		private System.Windows.Forms.PictureBox picCancel;
		private System.Windows.Forms.PictureBox picYes;
		private System.Windows.Forms.PictureBox picOK;

	}
}