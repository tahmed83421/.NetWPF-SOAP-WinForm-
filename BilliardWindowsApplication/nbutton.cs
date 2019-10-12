using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace BilliardWindowsApplication
{
    public partial class nbutton :Button
    {
        public nbutton()
        {
            InitializeComponent();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            using (var gp = new GraphicsPath())
            {
                gp.AddRectangle(new Rectangle(0, 10, this.Width, this.Height - 20));
                gp.AddRectangle(new Rectangle(10, 0, this.Width - 20, 10));
                gp.AddRectangle(new Rectangle(10, this.Height - 10, this.Width - 20, 10));

                //gp.AddArc(0, 0, 20, 20, 180, 90);
              //  gp.AddArc(this.Width - 20, 0, 20, 20, 270, 90);
              //  gp.AddArc(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
              //  gp.AddArc(0, this.Height - 20, 20, 20, 90, 90);

                gp.AddPie(0, 0, 20, 20, 180, 90);
                gp.AddPie(this.Width - 20, 0, 20, 20, 270, 90);
                gp.AddPie(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
                gp.AddPie(0, this.Height - 20, 20, 20, 90, 90);
                
                

                //gp.AddRectangle(new Rectangle(0, 13, this.Width, this.Height - 25));
                //gp.AddRectangle(new Rectangle(13, 0, this.Width - 25, 13));
                //gp.AddRectangle(new Rectangle(13, this.Height - 12, this.Width - 25, 13));
                //gp.AddPie(0, 0, 25, 25, 180, 90);
                //gp.AddPie(this.Width - 25, 0, 25, 25, 270, 90);
                //gp.AddPie(this.Width - 25, this.Height - 25, 25, 25, 0, 90);
                //gp.AddPie(0, this.Height - 25, 25,25, 90, 90);
                
                this.Region = new Region(gp);

            }
        }
    }
}
