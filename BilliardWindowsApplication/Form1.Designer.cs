namespace BilliardWindowsApplication
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.l0 = new System.Windows.Forms.Label();
            this.l1 = new System.Windows.Forms.Label();
            this.l2 = new System.Windows.Forms.Label();
            this.l3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter player Password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(132, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "start tracking";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(10, 70);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(162, 96);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(101, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "calibration";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // l0
            // 
            this.l0.BackColor = System.Drawing.Color.Blue;
            this.l0.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.l0.Location = new System.Drawing.Point(116, 219);
            this.l0.Name = "l0";
            this.l0.Size = new System.Drawing.Size(10, 10);
            this.l0.TabIndex = 11;
            this.l0.MouseClick += new System.Windows.Forms.MouseEventHandler(this.l1_MouseClick);
            this.l0.MouseMove += new System.Windows.Forms.MouseEventHandler(this.l1_MouseMove);
            // 
            // l1
            // 
            this.l1.BackColor = System.Drawing.Color.MidnightBlue;
            this.l1.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            this.l1.Location = new System.Drawing.Point(96, 219);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(10, 10);
            this.l1.TabIndex = 12;
            this.l1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.l1_MouseClick);
            this.l1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.l1_MouseMove);
            // 
            // l2
            // 
            this.l2.BackColor = System.Drawing.Color.Blue;
            this.l2.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.l2.Location = new System.Drawing.Point(155, 224);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(10, 10);
            this.l2.TabIndex = 13;
            this.l2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.l1_MouseClick);
            this.l2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.l1_MouseMove);
            // 
            // l3
            // 
            this.l3.BackColor = System.Drawing.Color.Blue;
            this.l3.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            this.l3.Location = new System.Drawing.Point(139, 220);
            this.l3.Name = "l3";
            this.l3.Size = new System.Drawing.Size(10, 10);
            this.l3.TabIndex = 14;
            this.l3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.l1_MouseClick);
            this.l3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.l1_MouseMove);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(182, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "clear graphics";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::BilliardWindowsApplication.Properties.Resources.biil1;
            this.pictureBox3.Location = new System.Drawing.Point(10, 292);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(802, 402);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::BilliardWindowsApplication.Properties.Resources.biil;
            this.pictureBox2.Location = new System.Drawing.Point(818, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(367, 684);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(10, 185);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 81);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::BilliardWindowsApplication.Properties.Resources.biil;
            this.pictureBox4.Location = new System.Drawing.Point(1208, 12);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(367, 684);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 16;
            this.pictureBox4.TabStop = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(270, 41);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "draw last";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dg1
            // 
            this.dg1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dg1.Location = new System.Drawing.Point(182, 97);
            this.dg1.Name = "dg1";
            this.dg1.Size = new System.Drawing.Size(240, 150);
            this.dg1.TabIndex = 18;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(361, 41);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 19;
            this.button5.Text = "replay";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1752, 719);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.l3);
            this.Controls.Add(this.l2);
            this.Controls.Add(this.l1);
            this.Controls.Add(this.l0);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label l0;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.Label l3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button button5;
    }
}