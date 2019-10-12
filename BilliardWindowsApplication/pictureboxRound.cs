using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
    class pictureboxRound : PictureBox
    {
        public pictureboxRound()
        { this.BackColor = Color.Black;
        
        }
       
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle(0, 0, this.Width-1, this.Height-1));
                //gp.AddEllipse(new Rectangle(0, 0, this.Width - 2, this.Height - 2));

                this.Region = new Region(gp);
                
            }
        }
    }
}