using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
    public partial class frmStart : Form
    {
        private Rectangle rcItalianGameButton = new Rectangle(790, 525, 415, 45);
        private Rectangle rcTeacherButton = new Rectangle(790, 590, 415, 45);
        private Rectangle rcCaromGameButton = new Rectangle(790, 660, 415, 45);

        public frmStart()
        {
            InitializeComponent();

            if (Screen.PrimaryScreen.Bounds.Width == 1366)
            {
                rcItalianGameButton = new Rectangle(790, 525, 415, 45);
                rcTeacherButton = new Rectangle(790, 590, 415, 45);
                rcCaromGameButton = new Rectangle(790, 660, 415, 45);
            }
            else if (Screen.PrimaryScreen.Bounds.Width == 1920)
            {
                rcItalianGameButton = new Rectangle(780, 555, 400, 45);
                rcTeacherButton = new Rectangle(780, 620, 400, 45);
                rcCaromGameButton = new Rectangle(780, 685, 400, 45);
            }
            
        }
        biliardService.BilliardScoreboard API = new biliardService.BilliardScoreboard();
        public static bool usb = false;
        private void label1_Click(object sender, EventArgs e)
        {
            if (usb)
            {
                timer1.Enabled = false;
                new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
                FrmGameSetup frm = new FrmGameSetup();
                frm.FormClosed += frm_FormClosed;
                usb = false;
                this.Hide();
                frm.ShowDialog();
            }
        }
        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (usb)
            {
                usb = false;
                usbrelay.nbreleaseusbrelay();
            }
            timer1.Enabled = true;
            this.Show();
           
        }
        private void pnlSetupClub_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            frmClubLogin frm = new frmClubLogin();
            frm.FormClosed += frm_FormClosed;
            this.Hide();
            frm.ShowDialog();
        }
        private void panel2_Click(object sender, EventArgs e)
        {
            frmclose frm = new frmclose();
            frm.ShowDialog();
        }
        void exit()
        {
            API.deletelastgamecostiffree(BLL_BilliardWindowsApplication.gamecostdetailsStatic);
            wanttoclose = true;
            Application.Exit();
        }
        private void pnlSetupClub_Paint_1(object sender, PaintEventArgs e)
        {

        }
        async void loadcostdetails()
        {
            BLL_BilliardWindowsApplication.costDetailsStataic = await Task.Run(() => API.getCostDetails(BilliardWindowsApplication.Properties.Settings.Default.CID, BilliardWindowsApplication.Properties.Settings.Default.billiardno.ToString()));
        }
        async void insertgamecost()
        {
            BLL_BilliardWindowsApplication.gamecostdetailsStatic.billno = BilliardWindowsApplication.Properties.Settings.Default.billiardno.ToString();
            BLL_BilliardWindowsApplication.gamecostdetailsStatic.clubid = BilliardWindowsApplication.Properties.Settings.Default.CID;

            if (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.gamecostdetailsStatic.id))
            {
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.id = await Task.Run(() => API.checklastgamecost(BLL_BilliardWindowsApplication.gamecostdetailsStatic));
                if (BLL_BilliardWindowsApplication.gamecostdetailsStatic.id == "0")
                {
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.date = DateTime.Now.Date.Day + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year;
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.duration = "";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.fromtime = "";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.p1 = "0";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.p2 = "0";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.p3 = "0";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.p4 = "0";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.totcost = "";

                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer = "";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.totime = "";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.noplayers = "0";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.gameover = false.ToString();

                    string respond = await Task.Run(() => API.insertgamecost(BLL_BilliardWindowsApplication.gamecostdetailsStatic));
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.id = respond;
                }
            }
            else
            {
                if (BLL_BilliardWindowsApplication.gamecostdetailsStatic.gameover == true.ToString())
                {
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.date = DateTime.Now.Date.Day + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year;
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.duration = "";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.fromtime = "";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.p1 = "0";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.p2 = "0";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.p3 = "0";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.p4 = "0";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.totcost = "";

                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer = "";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.totime = "";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.noplayers = "0";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.gameover = false.ToString();

                    string respond = await Task.Run(() => API.insertgamecost(BLL_BilliardWindowsApplication.gamecostdetailsStatic));
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.id = respond;
                }
            }
            timer1.Enabled = true;
        }
        private void frmStart_Load(object sender, EventArgs e)
        {
            try
            {
                if (Screen.PrimaryScreen.Bounds.Width < 1920)
                {
                    MessageBox.Show(Screen.PrimaryScreen.Bounds.Width.ToString());
                    //
                    Application.Exit();
                }
                loadcostdetails();
                if (BilliardWindowsApplication.Properties.Settings.Default.billiardno > 0)
                {
                    pbClub.ImageLocation = "https://score.biliardoprofessionale.it/" + BilliardWindowsApplication.Properties.Settings.Default.ClubLogo;
                    panel1.Visible = true;
                    pictureBox1.Image = BilliardWindowsApplication.Properties.Resources.billiardno;
                    label3.Text = BilliardWindowsApplication.Properties.Settings.Default.billiardno + "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); Application.Exit();
            }
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + @"\BilliardUpdate.exe");
            exit();
        }
        private void frmStart_Click(object sender, EventArgs e)
        {
            
        }
        private void frmStart_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			if (rcItalianGameButton.Contains(e.Location))
			{
                if (usb)
                {
                    timer1.Enabled = false;
                    new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
                    FrmGameSetup frm = new FrmGameSetup();
                    frm.FormClosed += frm_FormClosed;
                    usb = false;
                    this.Hide();
                    frm.ShowDialog();
                }
			}
			else if (rcTeacherButton.Contains(e.Location))
			{
				new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
				FrmTeaching frmTeaching = new FrmTeaching();
				frmTeaching.FormClosed += frm_FormClosed;
				this.Hide();
				frmTeaching.ShowDialog();
			}
			else if (rcCaromGameButton.Contains(e.Location))
			{

			}
		}
        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                string respond = await Task.Run(() => API.checkusbrelay(BLL_BilliardWindowsApplication.gamecostdetailsStatic.id));
                if (respond == "f")
                {
                    if (usb)
                    {
                        usb = false;
                        usbrelay.nbreleaseusbrelay();
                    }
                }
                else
                {
                    if (!usb)
                    {
                        offrelaytime = 0;
                        usb = true;
                        usbrelay.nbinitusbrelay();
                    }
                    else
                    {
                        offrelaytime++;
                        if (offrelaytime == 36)
                        {
                            usb = false;
                            usbrelay.nbreleaseusbrelay();
                            API.UpdategameusbrelayAsync(BLL_BilliardWindowsApplication.gamecostdetailsStatic.id, "f");
                        }
                    }
                }
                timer1.Enabled = true;
            }
            catch (Exception ex)
            { //MessageBox.Show(ex.ToString()+"hff");
            }
        }
        private async void frmStart_Shown(object sender, EventArgs e)
        {
            try
            {
                string downloadlink = await Task.Run(() => API.checkupdate("ball", 1));
                if (!string.IsNullOrEmpty(downloadlink))
                {
                    List<string> mess = downloadlink.Split('|').ToList();
                    if (DialogResult.OK == MessageBox.Show(mess[1] + System.Environment.NewLine + "Want To Download new?", "New Version " + mess[0] + " Is Available", MessageBoxButtons.OKCancel))
                    {
                        BilliardWindowsApplication.Properties.Settings.Default.emailid = "";
                        BilliardWindowsApplication.Properties.Settings.Default.Save();
                        WebClient webClient2 = new WebClient();
                        webClient2.DownloadFile(new Uri("https://score.biliardoprofessionale.it/setup/BilliardUpdate.exe"), Application.StartupPath + @"\BilliardUpdate.exe");

                        WebClient webClient = new WebClient();
                        progressBar.Visible = true;
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                        webClient.DownloadFileAsync(new Uri("https://score.biliardoprofessionale.it/setup/ball/Release.zip"), @"c:\Release.zip");
                    }
                }
                insertgamecost();
                if (BLL_BilliardWindowsApplication.languague == 0)
                    MessageBox.Show("cost in indian languague");
            }
            catch { MessageBox.Show("Please Connect to internet"); Application.Exit(); }
        }
        private bool wanttoclose = false;
        private void frmStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!wanttoclose)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                    e.Cancel = true;
            }
        }
        int offrelaytime = 0;

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            FrmGameSetup frmGameSetup = new FrmGameSetup();
            this.Hide();
            frmGameSetup.ShowDialog();

        }
    }
}