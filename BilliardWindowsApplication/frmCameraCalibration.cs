using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
    public partial class frmCameraCalibration : Form
    {
        public frmCameraCalibration()
        {
            InitializeComponent();
        }

       

        private void frmStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void frmStart_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F4 && e.Alt)
            //{
            //    e.SuppressKeyPress = true;

            //}
        }
       
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
          //  frmCalibrationSave frm = new frmCalibrationSave();
          //  frm.FormClosed += frm_FormClosed;
          //  frm.ShowDialog();
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
               
        }
    }
}
