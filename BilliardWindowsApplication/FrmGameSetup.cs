using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
    public partial class FrmGameSetup : Form
    {
        int MatchId = 0;
        public FrmGameSetup()
        {
            InitializeComponent();
        }

        TimeSpan currentTime;
        biliardService.BilliardScoreboard API = new biliardService.BilliardScoreboard();
        List<string> preplayer = new List<string>();

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
                    //MessageBox.Show(respond);
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

        }

        private void FrmGameSetup_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width < 1920)
            {
                MessageBox.Show(Screen.PrimaryScreen.Bounds.Width.ToString());
                Application.Exit();
            }
            loadcostdetails();
            insertgamecost();
            resetlocation();
            resetGame();
            timer1.Enabled = true;
            
        }
        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //mail cost to player1,player2,club owner;
            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
            { API.UpdatePlayerLogin(BLL_BilliardWindowsApplication.player1.PlayerId, "0"); preplayer.Add(BLL_BilliardWindowsApplication.player1.PlayerId); }
            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
            { API.UpdatePlayerLogin(BLL_BilliardWindowsApplication.player2.PlayerId, "0"); preplayer.Add(BLL_BilliardWindowsApplication.player2.PlayerId); }
            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
            { API.UpdatePlayerLogin(BLL_BilliardWindowsApplication.player3.PlayerId, "0"); preplayer.Add(BLL_BilliardWindowsApplication.player3.PlayerId); }
            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
            { API.UpdatePlayerLogin(BLL_BilliardWindowsApplication.player4.PlayerId, "0"); preplayer.Add(BLL_BilliardWindowsApplication.player4.PlayerId); }

            timer1.Enabled = true;
            resetlocation();
            resetGame();
            this.Show();
            insertgamecost();

            try
            {              
                if (BallTrackAPI.m_bStartTracking)
                {
                    BallTrackAPI.BTAPI_StopTracking();
                    BallTrackAPI. m_bStartTracking = false;
                }
				//if (BallTrackAPI.BTAPI_IsCameraConnected())
				//	BallTrackAPI.BTAPI_DisconnectCamera();

				//BallTrackAPI.BTAPI_Free();
                BallTrackAPI.mbInitialized = false;

                BallTrackAPI.m_WhiteHitPos = new POINT[128];
                BallTrackAPI.m_YellowHitPos = new POINT[128];
                BallTrackAPI.m_RedHitPos = new POINT[128];
                BallTrackAPI.drawInfo = new DRAW_INFO();
                BallTrackAPI.drawInfobackup = new DRAW_INFO();
               
                BallTrackAPI.ptCorners = new POINT[4];
                BallTrackAPI.ptCorner_new = new POINT[4];
                BallTrackAPI.ptCenter = new POINT();
                BallTrackAPI.checkinval = new POINT[3];
				BallTrackAPI.m_playHistory[0] = new List<List<CRASH_ELEMENT>>();
				BallTrackAPI.m_playHistory[1] = new List<List<CRASH_ELEMENT>>();
				BallTrackAPI.m_playHistory[2] = new List<List<CRASH_ELEMENT>>();

                this.Show();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void resetGame()
        {
            cbEmail.Checked = cbPenalty.Checked = false;
            //cbEmail.Checked = cbIdpw.Checked = cbPenalty.Checked = cbZero.Checked = false;
            txtPoint.Text = "0";
            txtSet.Text = "1";
            txtQuills.Text = "5";
            txtTimer.Text = "40";
            txtTimer2.Text = "20";
            rbGameTime.Checked = true;
            pbClub1.Image = pbClub2.Image = pbClub3.Image = pbClub4.Image = pbPlayer1.Image = pbPlayer2.Image = pbPlayer3.Image = pbPlayer4.Image = null;
            lblPlayer1.Text = lblPlayer2.Text = lblPlayer3.Text = lblPlayer4.Text = " - ";
            txtPlayer1.Text = txtPlayer2.Text = txtPlayer3.Text = txtPlayer4.Text = "";
            BLL_BilliardWindowsApplication.player1 = BLL_BilliardWindowsApplication.player2 = BLL_BilliardWindowsApplication.player3 = BLL_BilliardWindowsApplication.player4 = new biliardService.Details();
 
        }
        void resetlocation()
        {
            int x = this.Width;
            int y = x - pnlGame.Width;
            int z;
            try
            { z = y / 2; }
            catch { z = (y + 1) / 2; }
            pnlGame.Location = new Point(z, pnlGame.Location.Y);
        }
        SOAPService.BilliardScoreboard API2 = new SOAPService.BilliardScoreboard();

        private async void pbEnter_Click(object sender, EventArgs e)
        {

            // Insert Data for Mathch Stream 
          
       
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
           
         

            API2.AddPlayerDetailsOnStreamTestAsync("1111", BLL_BilliardWindowsApplication.player1.Name.ToString(), BLL_BilliardWindowsApplication.player2.Name.ToString(), BLL_BilliardWindowsApplication.player3.Name.ToString(), BLL_BilliardWindowsApplication.player4.Name.ToString(), BLL_BilliardWindowsApplication.player1.ClubName.ToString(), BLL_BilliardWindowsApplication.player2.ClubName.ToString(), BLL_BilliardWindowsApplication.player3.ClubName.ToString(), BLL_BilliardWindowsApplication.player4.ClubName.ToString(),BLL_BilliardWindowsApplication.player1.PlayerPicture.ToString(),BLL_BilliardWindowsApplication.player2.PlayerPicture.ToString(),BLL_BilliardWindowsApplication.player3.PlayerPicture.ToString(),BLL_BilliardWindowsApplication.player4.PlayerPicture.ToString(),BLL_BilliardWindowsApplication.player1.ClubPicture.ToString(),BLL_BilliardWindowsApplication.player2.ClubPicture.ToString(),BLL_BilliardWindowsApplication.player3.ClubPicture.ToString(),BLL_BilliardWindowsApplication.player4.ClubPicture.ToString()) ;
        


            if (((PictureBox)sender).Name == "pbGame")
            {
                timer1.Enabled = false;
                wanttoclose = true;
                API.UpdategameusbrelayAsync(BLL_BilliardWindowsApplication.gamecostdetailsStatic.id, "f");
             
                usbrelay.nbreleaseusbrelay();
                this.Close();
            }
            else
            if (((PictureBox)sender).Name == "pbReset")
            {
                resetGame();
            }
            else
            if (((PictureBox)sender).Name == "pbEnter")
            {
                try
                {

                    //if (false)
                    //    MessageBox.Show("you can not check Penalty and Zero at same time");
                    //else
                    if (Convert.ToInt32(txtTimer.Text.Split('"').FirstOrDefault()) <= 0)
                        MessageBox.Show("Timer1 must be more than 0");
                    else
                        if (Convert.ToInt32(txtPoint.Text) <= 0)
                            MessageBox.Show("Point must be more than 0");
                        else
                            if (cbIdpw.Checked == true && (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name) && string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name)))
                                MessageBox.Show("Sorry! Please select both player.");
                            else
                                if (cbIdpw.Checked == true && (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name) && string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name)))
                                    MessageBox.Show("Sorry! Please select atleast one opponent.");
                                else
                                {
                                    string respond = await Task.Run(() => API.checkusbrelay(BLL_BilliardWindowsApplication.gamecostdetailsStatic.id));

                                    if (preplayer.Count == 0 || respond != "f" || preplayer.Contains(BLL_BilliardWindowsApplication.player1.PlayerId) || preplayer.Contains(BLL_BilliardWindowsApplication.player2.PlayerId) || preplayer.Contains(BLL_BilliardWindowsApplication.player3.PlayerId) || preplayer.Contains(BLL_BilliardWindowsApplication.player4.PlayerId))
                                    {
                                        timer1.Enabled = false;

                                        try
                                        {
                                            if (usbrelay.Status == 1)
                                            {
                                                if (usbrelay.relay1on == 0)
                                                {
                                                    int a = usbrelay.usb_relay_device_open_one_relay_channel(usbrelay.hHandle, 01);
                                                    if (a == 0)
                                                        usbrelay.relay1on = 1;
                                                }

                                                if (usbrelay.relay2on == 0)
                                                {
                                                    int a = usbrelay.usb_relay_device_open_one_relay_channel(usbrelay.hHandle, 02);
                                                    if (a == 0) usbrelay.relay2on = 1;
                                                }
                                            }
                                        }
                                        catch { }

                                        BLL_BilliardWindowsApplication.id = cbIdpw.Checked;
                                        BLL_BilliardWindowsApplication.panalty = cbPenalty.Checked;
                                        BLL_BilliardWindowsApplication.email = cbEmail.Checked;
                                        BLL_BilliardWindowsApplication.point = Convert.ToInt32(txtPoint.Text);
                                        BLL_BilliardWindowsApplication.set = Convert.ToInt32(txtSet.Text);
                                        BLL_BilliardWindowsApplication.quills = Convert.ToInt32(txtQuills.Text);
                                        BLL_BilliardWindowsApplication.timer1 = Convert.ToInt32(txtTimer.Text);
                                        BLL_BilliardWindowsApplication.timer2 = Convert.ToInt32(txtTimer2.Text);
                                        BLL_BilliardWindowsApplication.gametime = rbGameTime.Checked;

                                        BLL_BilliardWindowsApplication.p1 = pbPlayer1.Image;
                                        BLL_BilliardWindowsApplication.p2 = pbPlayer2.Image;
                                        BLL_BilliardWindowsApplication.p3 = pbPlayer3.Image;
                                        BLL_BilliardWindowsApplication.p4 = pbPlayer4.Image;
                                        BLL_BilliardWindowsApplication.c1 = pbClub1.Image;
                                        BLL_BilliardWindowsApplication.c2 = pbClub2.Image;
                                        BLL_BilliardWindowsApplication.c3 = pbClub3.Image;
                                        BLL_BilliardWindowsApplication.c4 = pbClub4.Image;

                                        preplayer = new List<string>();

                                        currentTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);



                                        //if (!BallTrackAPI.mbInitialized)
                                        //	BallTrackAPI.mbInitialized = BallTrackAPI.InitSDK(BallTrackAPI.PtrBallPosProc, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                                        BallTrackAPI.BTAPI_SetBallPosCallback(BallTrackAPI.PtrBallPosProc);

                                        bool camwork = false;

                                        if (BallTrackAPI.m_nInputMethod == 1)
                                        {
                                            BallTrackAPI.BTAPI_OpenVideoFile("D:\\opencv\\Outgrowth\\Samples\\x ZS  2L rail B.avi");
                                            //bdll.BTAPI_OpenVideoFile("5balls.avi");
                                            camwork = true;
                                            MessageBox.Show("this setup is for vedio");
                                        }
                                        else
                                        {
                                            if (!BallTrackAPI.BTAPI_IsCameraConnected())
                                                BallTrackAPI.BTAPI_ConnectCamera(IntPtr.Zero);
                                            if (BallTrackAPI.BTAPI_IsCameraConnected())
                                                camwork = true;

                                        }
                                        if (!camwork)
                                        {
                                            frmGameScore3NEW frm = new frmGameScore3NEW(currentTime);
                                            frm.FormClosed += frm_FormClosed;
                                            this.Hide();
                                            try
                                            {
                                                frm.ShowDialog();
                                            }
                                            catch { }
                                        }
                                        else
                                        {
                                            MessageBox.Show("camera not working");
                                          //  Application.Exit();
                                            //frmGameScore3 frm = new frmGameScore3();
                                            //frm.FormClosed += frm_FormClosed;
                                            //this.Hide();
                                            //frm.ShowDialog();
                                        }

                                    }
                                }
                }
                catch (Exception ex) { MessageBox.Show("Sorry! Please enter correct details" + ex.ToString()); }
            }
            else
            {
             
                SendKeys.Send(((PictureBox)sender).Tag.ToString());
            }
        }
        private async void txtPlayer1_Leave(object sender, EventArgs e)
        {
            if (txtPlayer1.Text.Trim() != "")
                if ((txtPlayer1.Text.Trim() == txtPlayer2.Text.Trim() || txtPlayer1.Text.Trim() == txtPlayer3.Text.Trim()) || txtPlayer1.Text.Trim() == txtPlayer4.Text.Trim())
                    MessageBox.Show("You are already logged in");
                else
                {
                    BLL_BilliardWindowsApplication.player1 = await Task.Run(() => API.getPlayerDetails(txtPlayer1.Text.Trim()));
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
                    {
                        if (BLL_BilliardWindowsApplication.player1.ClubId.Trim() == "0")
                        {
                            lblPlayer1.Text = BLL_BilliardWindowsApplication.player1.Name + " " + BLL_BilliardWindowsApplication.player1.FamilyName;
                            pbPlayer1.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player1.PlayerPicture;
                            pbClub1.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player1.ClubPicture;
                        }
                        else MessageBox.Show("You are already logged in");
                    }
                }
        }
        private async void txtPlayer2_Leave(object sender, EventArgs e)
        {
            if (txtPlayer2.Text.Trim() != "")
                if ((txtPlayer2.Text.Trim() == txtPlayer1.Text.Trim() || txtPlayer2.Text.Trim() == txtPlayer3.Text.Trim()) || txtPlayer2.Text.Trim() == txtPlayer4.Text.Trim())
                    MessageBox.Show("You are already logged in");
                else
                {
                    BLL_BilliardWindowsApplication.player2 = await Task.Run(() => API.getPlayerDetails(txtPlayer2.Text.Trim()));
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
                    {
                        if (BLL_BilliardWindowsApplication.player2.ClubId.Trim() == "0")
                        {
                            lblPlayer2.Text = BLL_BilliardWindowsApplication.player2.Name + " " + BLL_BilliardWindowsApplication.player2.FamilyName;
                            pbPlayer2.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player2.PlayerPicture;
                            pbClub2.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player2.ClubPicture;
                        }
                        else MessageBox.Show("You are already logged in");
                    }
                }
        }
        private async void txtPlayer3_Leave(object sender, EventArgs e)
        {
            if (txtPlayer3.Text.Trim() != "")
                if ((txtPlayer3.Text.Trim() == txtPlayer1.Text.Trim() || txtPlayer3.Text.Trim() == txtPlayer2.Text.Trim()) || txtPlayer3.Text.Trim() == txtPlayer4.Text.Trim())
                    MessageBox.Show("You are already logged in");
                else
                {
                    BLL_BilliardWindowsApplication.player3 = await Task.Run(() => API.getPlayerDetails(txtPlayer3.Text.Trim()));
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
                    {
                        if (BLL_BilliardWindowsApplication.player3.ClubId.Trim() == "0")
                        {
                            lblPlayer3.Text = BLL_BilliardWindowsApplication.player3.Name + " " + BLL_BilliardWindowsApplication.player3.FamilyName;
                            pbPlayer3.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player3.PlayerPicture;
                            pbClub3.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player3.ClubPicture;
                        }
                        else MessageBox.Show("You are already logged in");
                    }
                }
        }
        private async void txtPlayer4_Leave(object sender, EventArgs e)
        {
            if (txtPlayer4.Text.Trim() != "")
                if ((txtPlayer4.Text.Trim() == txtPlayer1.Text.Trim() || txtPlayer4.Text.Trim() == txtPlayer2.Text.Trim()) || txtPlayer4.Text.Trim() == txtPlayer3.Text.Trim())
                    MessageBox.Show("You are already logged in");
                else
                {
                    BLL_BilliardWindowsApplication.player4 = await Task.Run(() => API.getPlayerDetails(txtPlayer4.Text.Trim()));
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
                    {
                        if (BLL_BilliardWindowsApplication.player4.ClubId.Trim() == "0")
                        {
                            lblPlayer4.Text = BLL_BilliardWindowsApplication.player4.Name + " " + BLL_BilliardWindowsApplication.player4.FamilyName;
                            pbPlayer4.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player4.PlayerPicture;
                            pbClub4.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player4.ClubPicture;
                        }
                        else MessageBox.Show("You are already logged in");
                    }

                } 

        }
        private void rbGameTime_Click(object sender, EventArgs e)
        {
            if (BLL_BilliardWindowsApplication.gametime)
                BLL_BilliardWindowsApplication.gametime = rbGameTime.Checked = false;
            else BLL_BilliardWindowsApplication.gametime = rbGameTime.Checked = true;
           
        }
        private void txtTimer2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                    e.Handled = true;
            }
            else
            {
                if((TextBox)sender == txtQuills)
                {
                    if(!char.IsControl(e.KeyChar) && char.ToString(e.KeyChar)!="5" && char.ToString(e.KeyChar)!="9")
                    e.Handled = true;
                }
                if((TextBox)sender==txtSet)
                {
                    if (!char.IsControl(e.KeyChar) && char.ToString(e.KeyChar) != "1" && char.ToString(e.KeyChar) != "3" && char.ToString(e.KeyChar) != "5" && char.ToString(e.KeyChar) != "7" && char.ToString(e.KeyChar) != "9")
                        e.Handled = true;
                }
            }
        }

        private async void pnltemp1_Click(object sender, EventArgs e)
        {
            if (txtPlayer1.Text.Trim() != "")
                if ((txtPlayer1.Text.Trim() == txtPlayer2.Text.Trim() || txtPlayer1.Text.Trim() == txtPlayer3.Text.Trim()) || txtPlayer1.Text.Trim() == txtPlayer4.Text.Trim())
                    MessageBox.Show("You are already logged in");
                else
                {
                    BLL_BilliardWindowsApplication.player1 = await Task.Run(() => API.getPlayerDetails(txtPlayer1.Text.Trim()));
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
                    {
                        if (BLL_BilliardWindowsApplication.player1.ClubId.Trim() == "0")
                        {
                            lblPlayer1.Text = BLL_BilliardWindowsApplication.player1.Name + " " + BLL_BilliardWindowsApplication.player1.FamilyName;
                            pbPlayer1.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player1.PlayerPicture;
                            pbClub1.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player1.ClubPicture;
                        }
                        else MessageBox.Show("You are already logged in");
                    }
                }
        }
        private async void pnltemp2_Click(object sender, EventArgs e)
        {
            if (txtPlayer2.Text.Trim() != "")
                if ((txtPlayer2.Text.Trim() == txtPlayer1.Text.Trim() || txtPlayer2.Text.Trim() == txtPlayer3.Text.Trim()) || txtPlayer2.Text.Trim() == txtPlayer4.Text.Trim())
                    MessageBox.Show("You are already logged in");
                else
                {
                    BLL_BilliardWindowsApplication.player2 = await Task.Run(() => API.getPlayerDetails(txtPlayer2.Text.Trim()));
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
                    {
                        if (BLL_BilliardWindowsApplication.player2.ClubId.Trim() == "0")
                        {
                            lblPlayer2.Text = BLL_BilliardWindowsApplication.player2.Name + " " + BLL_BilliardWindowsApplication.player2.FamilyName;
                            pbPlayer2.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player2.PlayerPicture;
                            pbClub2.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player2.ClubPicture;
                        }
                        else MessageBox.Show("You are already logged in");
                    }
                }
        }

        private async void pnltemp3_Click(object sender, EventArgs e)
        {
            if (txtPlayer3.Text.Trim() != "")
                if ((txtPlayer3.Text.Trim() == txtPlayer1.Text.Trim() || txtPlayer3.Text.Trim() == txtPlayer2.Text.Trim()) || txtPlayer3.Text.Trim() == txtPlayer4.Text.Trim())
                    MessageBox.Show("You are already logged in");
                else
                {
                    BLL_BilliardWindowsApplication.player3 = await Task.Run(() => API.getPlayerDetails(txtPlayer3.Text.Trim()));
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
                    {
                        if (BLL_BilliardWindowsApplication.player3.ClubId.Trim() == "0")
                        {
                            lblPlayer3.Text = BLL_BilliardWindowsApplication.player3.Name + " " + BLL_BilliardWindowsApplication.player3.FamilyName;
                            pbPlayer3.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player3.PlayerPicture;
                            pbClub3.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player3.ClubPicture;
                        }
                        else MessageBox.Show("You are already logged in");
                    }
                }
        }

        private async void pnltemp4_Click(object sender, EventArgs e)
        {
            if (txtPlayer4.Text.Trim() != "")
                if ((txtPlayer4.Text.Trim() == txtPlayer1.Text.Trim() || txtPlayer4.Text.Trim() == txtPlayer2.Text.Trim()) || txtPlayer4.Text.Trim() == txtPlayer3.Text.Trim())
                    MessageBox.Show("You are already logged in");
                else
                {
                    BLL_BilliardWindowsApplication.player4 = await Task.Run(() => API.getPlayerDetails(txtPlayer4.Text.Trim()));
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
                    {
                        if (BLL_BilliardWindowsApplication.player4.ClubId.Trim() == "0")
                        {
                            lblPlayer4.Text = BLL_BilliardWindowsApplication.player4.Name + " " + BLL_BilliardWindowsApplication.player4.FamilyName;
                            pbPlayer4.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player4.PlayerPicture;
                            pbClub4.ImageLocation = "https://score.biliardoprofessionale.it/" + BLL_BilliardWindowsApplication.player4.ClubPicture;
                        }
                        else MessageBox.Show("You are already logged in");
                    }
                }
        }

        private void pnlGame_Paint(object sender, PaintEventArgs e)
        {

        }
        private bool wanttoclose = false;
        private void FrmGameSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!wanttoclose)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                    e.Cancel = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            usbrelay.nbreleaseusbrelay();
            API.UpdategameusbrelayAsync(BLL_BilliardWindowsApplication.gamecostdetailsStatic.id, "f");
            wanttoclose = true;
            this.Close();
        }
    }
}
