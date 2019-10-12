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
using System.Net.Mail;

namespace BilliardWindowsApplication
{
    public partial class frmClubLogin : Form
    {
        public frmClubLogin()
        {
            InitializeComponent();
        }
        biliardService.BilliardScoreboard API = new biliardService.BilliardScoreboard();
        biliardService.clubDetails clubDetails = new biliardService.clubDetails();
        private void frmStart_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4 && e.Alt)
                {
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no L1"); }
        }
        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Show();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no L3"); }
        }
        private void btnLogin_ClickButtonArea(object sender, EventArgs e)
        {
            try
            {
                if (BilliardWindowsApplication.Properties.Settings.Default.emailid == textBox1.Text || BilliardWindowsApplication.Properties.Settings.Default.emailid.Trim() == "")
                {
                    clubDetails = API.getClubDetails(textBox1.Text.Trim(), textBox2.Text.Trim());
                    if (clubDetails.Status == "Active")
                    {
                        label6.Text = "";
                        if (BilliardWindowsApplication.Properties.Settings.Default.CID != clubDetails.clubId)
                            BilliardWindowsApplication.Properties.Settings.Default.billiardno = 0;
                        BilliardWindowsApplication.Properties.Settings.Default.emailid = textBox1.Text;
                        BilliardWindowsApplication.Properties.Settings.Default.CID = clubDetails.clubId;
                        BilliardWindowsApplication.Properties.Settings.Default.clubname = clubDetails.ClubName;
                        BilliardWindowsApplication.Properties.Settings.Default.Save();
                        new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
                        frmClubWelcome frm = new frmClubWelcome(clubDetails);
                        frm.FormClosed += frm_FormClosed;
                        this.Hide();
                        frm.ShowDialog();
                    }
                    else
                    {
                        label6.Text = "Enter Correct Details of this club";
                    }
                }
                else
                {
                    label6.Text = "Enter Correct Details of this club";
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no L4"); }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
                    string pass = API.getClubPassword(textBox1.Text);
                    string body = htmlcode(pass);
                    List<string> repmail = new List<string>();
                    repmail.Add(textBox1.Text.Trim());
                    SendHtmlFormattedEmail(repmail, "Biliard Credentials", body);
                    label6.Text = "mail is send to your id";
                }
                else
                {
                    label6.Text = "Enter UserId";
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no L5"); }
        }
        string htmlcode(string Password)
        {
            string home = "";
            try
            {
                home = "<!DOCTYPE html> <html xmlns=" + '"' + "http://www.w3.org/1999/xhtml" + '"' + ">" +
                "<head> <title> </title> </head> <body>    <img src = " + '"' + "http://score.biliardoprofessionale.it/img/logoTop.png" + '"' + " /><br /><br />" +
                " <div style =" + '"' + "border-top:3px solid #22BCE5; border-top-width: 1px;" + '"' + "></div>" +
                "<span style = " + '"' + "font-family:Arial;font-size:10pt" + '"' + ">      <h1>  <strong>Password Recovery</strong><br /></h1>" +
                "Dear Candidate &nbsp;<br /><br /> " +
                "As requested by you, Your Password is: " + Password + "<br /><br /> " +
                "</strong>" +
                "    Thanks<br />" +
                "      Biliardo Professionale" +
                "</body>" +
                "</html>";
                return home;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ex no L6");
                return home;
            }
        }
        private void SendHtmlFormattedEmail(List<string> recepientEmail, string subject, string body)
        {
            try
            {
                for (int i = 0; i < recepientEmail.Count; i++)
                    API.getmail("", "info@biliardoprofessionale.it", recepientEmail[i], subject, body);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ex no L7");
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try { 
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ex no L8");
            }
        }
        private void frmClubLogin_Load(object sender, EventArgs e)
        {
            try { 
            if (!string.IsNullOrEmpty(BilliardWindowsApplication.Properties.Settings.Default.emailid))
            {

                textBox1.Text = BilliardWindowsApplication.Properties.Settings.Default.emailid;
                textBox2.Focus();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ex no L9");
            }
        }
    }
}
