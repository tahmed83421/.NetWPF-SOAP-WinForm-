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
    public partial class frmClubWelcome : Form
    {
        biliardService.clubDetails clubDetails=new biliardService.clubDetails();
        public frmClubWelcome(biliardService.clubDetails clubDetails)
        {
            InitializeComponent();
            this.clubDetails = clubDetails;
        }
        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
			//if (BallTrackAPI.BTAPI_IsCameraConnected())
			//	BallTrackAPI.BTAPI_DisconnectCamera();
			//BallTrackAPI.BTAPI_Free();
            BallTrackAPI.mbInitialized = false;
            
            this.Show();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmCalibrationSave frms=new frmCalibrationSave();
            frms.FormClosed += frm_FormClosed;
            
            frmCameraCalibration frm = new frmCameraCalibration();
            frm.FormClosed += frm_FormClosed;
            
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
           
           if (BallTrackAPI.m_nInputMethod == 1)
               BallTrackAPI.BTAPI_OpenVideoFile("5balls.avi");
           else
           {
                if(!BallTrackAPI.BTAPI_IsCameraConnected())
                    BallTrackAPI.BTAPI_ConnectCamera(IntPtr.Zero);
           }
           if (BallTrackAPI.BTAPI_GetCalibPoints(ref BallTrackAPI.ptCorners[0], ref BallTrackAPI.ptCenter))
                frms.ShowDialog();
           else frm.ShowDialog();   
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmClubWelcome_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(clubDetails.ContactPerson))
                clubDetails.ContactPerson="--------";
            if (string.IsNullOrEmpty(clubDetails.ClubName))
                clubDetails.ClubName = "------";
            label5.Text="Welcome Mr. "+clubDetails.ContactPerson +" Club "+clubDetails.ClubName;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            frmbilliarno frm = new frmbilliarno(clubDetails);
            frm.FormClosed += frm_FormClosed;
            this.Hide();
            frm.ShowDialog();
            
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            frmCostSetup frm = new frmCostSetup();
            frm.FormClosed += frm_FormClosed;
            this.Hide();
            frm.ShowDialog();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            frmDashboard frm = new frmDashboard();
            frm.FormClosed += frm_FormClosed;
            this.Hide();
            frm.ShowDialog();
        }
    }
}
