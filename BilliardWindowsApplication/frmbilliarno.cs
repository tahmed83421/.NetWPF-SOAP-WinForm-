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
    public partial class frmbilliarno : Form
    {
        biliardService.clubDetails clubDetails=new biliardService.clubDetails();
      
        biliardService.BilliardScoreboard API = new biliardService.BilliardScoreboard();
        public frmbilliarno(biliardService.clubDetails clubDetails)
        {
            InitializeComponent();
            this.clubDetails = clubDetails;
        }
    
      
        private void frmClubWelcome_Load(object sender, EventArgs e)
        {
            textBox1.Text = BilliardWindowsApplication.Properties.Settings.Default.billiardno+"";
            pbClub.ImageLocation = "https://score.biliardoprofessionale.it/" + clubDetails.ClubLogo;
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //int no = 0;
            //try
            //{
            //    no = Convert.ToInt32(textBox1.Text);
            //    BilliardWindowsApplication.Properties.Settings.Default.billiardno = no;
            //    BilliardWindowsApplication.Properties.Settings.Default.ClubLogo = clubDetails.ClubLogo;
            //    BilliardWindowsApplication.Properties.Settings.Default.Save();

            //}
            //catch
            //{
            //    BilliardWindowsApplication.Properties.Settings.Default.billiardno = no;
            //    BilliardWindowsApplication.Properties.Settings.Default.ClubLogo = "";
            //    BilliardWindowsApplication.Properties.Settings.Default.Save();

            //}
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        SOAPService.BilliardScoreboard API2 = new SOAPService.BilliardScoreboard();
        private void label5_Click(object sender, EventArgs e)
        {

            API.deletelastgamecostiffree(BLL_BilliardWindowsApplication.gamecostdetailsStatic);
            API2.AddBiliardNoAsync(textBox1.Text);
            // Added on Neeraj's File - This app is for another use now.

            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play(); 
            int no = 0;
            try
            {
                no = Convert.ToInt32(textBox1.Text);
                
                BilliardWindowsApplication.Properties.Settings.Default.billiardno = no;
                BilliardWindowsApplication.Properties.Settings.Default.ClubLogo = clubDetails.ClubLogo;
                BilliardWindowsApplication.Properties.Settings.Default.Save();

            }
            catch
            {
                BilliardWindowsApplication.Properties.Settings.Default.billiardno = no;
                BilliardWindowsApplication.Properties.Settings.Default.ClubLogo = "";
                BilliardWindowsApplication.Properties.Settings.Default.Save();

            }
            MessageBox.Show("Setting Saved Successfully." + Environment.NewLine + " Application Restart", "Biliardoprofessionale", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FrmGameSetup gameSetup = new FrmGameSetup();
            this.Hide();
            gameSetup.ShowDialog();
           
        }
    }
}
