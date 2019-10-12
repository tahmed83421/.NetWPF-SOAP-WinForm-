using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
    public partial class frmwiner : Form
    {
        bool winner = false; string p1 = ""; string p2 = ""; int s1 = 0; int s2 = 0; string pt1 = "0"; string pt2 = "0";
        public frmwiner(string p1,string p2,int s1,int s2,string pt1, string pt2,bool winner)
        {
            InitializeComponent();
            this.p1 = p1;
            this.p2 = p2;
            this.s1 = s1;
            this.s2 = s2;
            this.pt1 = pt1;
            this.pt2 = pt2;
            this.winner = winner;
        }

        private void frmwiner_Load(object sender, EventArgs e)
        {
            if (winner)
            {
                label1.Text = "Winner of the Game";
                label2.Text = p1;
                label3.Text = p2;
                label4.Text =s1 + " Sets to " + s2 + " Set";
                button1.Text = "Setup Page";
                label5.Text = "Last Set " + pt1 + " to " + pt2 + " Points";
            }
            else
            {
                label1.Text = "Winner of the Set";
                label2.Text = p1;
                label3.Text = p2;
                label4.Text = "";
                //s1 + " Sets to " + s2 + " Set";
                button1.Text = "Next Set";
                label5.Text = "Last Set " + pt1 + " to " + pt2 + " Points";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                this.Close();
        }
    }
}
