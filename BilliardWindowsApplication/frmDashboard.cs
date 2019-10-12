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
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label209_Click(object sender, EventArgs e)
        {

        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {

        }

        private void label160_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel41_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {
            new frmCostSetup().ShowDialog();
        }

        private void pbGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
