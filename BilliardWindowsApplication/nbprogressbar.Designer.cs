namespace BilliardWindowsApplication
{
    partial class nbprogressbar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bar
            // 
            this.bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bar.BackColor = System.Drawing.Color.LimeGreen;
            this.bar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar.ForeColor = System.Drawing.SystemColors.Control;
            this.bar.Location = new System.Drawing.Point(0, 148);
            this.bar.Margin = new System.Windows.Forms.Padding(0);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(148, 0);
            this.bar.TabIndex = 1;
            this.bar.Text = "ggg";
            this.bar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bar.Click += new System.EventHandler(this.bar_Click);
            // 
            // nbprogressbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.bar);
            this.Name = "nbprogressbar";
            this.Size = new System.Drawing.Size(148, 148);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label bar;

    }
}
