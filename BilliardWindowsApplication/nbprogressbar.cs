using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
    public partial class nbprogressbar : UserControl
    {
        public nbprogressbar()
        {
            InitializeComponent();
        }
        int bartotal = 0;
        int barvalue = 0;
        bool bartimer2 =true;
        public void setBar(int total,int value, bool timer2)
        {
            bartotal = total;
            barvalue = value;
            bartimer2 = timer2;
            if (bartimer2 == false)
                bar.BackColor = Color.LawnGreen;
            else bar.BackColor = Color.Red;
            int no = this.Size.Height / bartotal;
            bar.Location = new Point(bar.Location.X, this.Size.Height - no * barvalue);
            bar.Size = new Size(bar.Size.Width, no * barvalue);
            bar.Text = barvalue.ToString() + '"';
        }

        public event EventHandler ButtonClick;
        private void bar_Click(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }

        public void setbarcolor(Color color)
        {
            bar.BackColor = color;
        }
        public Color Getbarcolor()
        {
            return bar.BackColor;
        }
        public void setBar(int value)
        {
            barvalue = value;
            int no = this.Size.Height / bartotal;
            if (barvalue == bartotal)
            {
                bar.Location = new Point(bar.Location.X, 0);
                bar.Size = new Size(bar.Size.Width, this.Height);
            }
            else
            {
                bar.Location = new Point(bar.Location.X, this.Size.Height - no * barvalue);
                bar.Size = new Size(bar.Size.Width, no * barvalue);
            }
            bar.Text = barvalue.ToString() + '"';
        }
        public void setBarFont(Font barfont)
        {
            bar.Font = barfont;
        }
        public int GetBartotal()
        {
            return bartotal;
        }
        public int GetBarValue()
        {
            return barvalue;
        }
        public bool GetBarTimer2()
        {
            return bartimer2;
        }
        private void nbprogressbar_Click(object sender, EventArgs e)
        {
            //bar.Location = new Point(bar.Location.X,bar.Location.Y-10);
            //bar.Size = new Size(bar.Size.Width,bar.Size.Height+10);


        }

     
    }
}
