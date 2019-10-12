using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
    public partial class frmGameScore3NEW : Form
    {
		protected bool m_bRefereeEnteredPoint = false;
        public frmGameScore3NEW(TimeSpan currentTime)
        {
            this.currentTime = currentTime;
            try
            {
                InitializeComponent();
                
				m_rcDrawRect = new Rectangle(0, 0, pictureBox3.Width, pictureBox3.Height);
				m_fMarginScaleX = (float)m_rcDrawRect.Width / (float)m_szImageBackground.Width;
				m_fMarginScaleY = (float)m_rcDrawRect.Height / (float)m_szImageBackground.Height;

				m_nMarginX = (int)(50 * m_fMarginScaleX + 0.5f);
				m_nMarginY = (int)(50 * m_fMarginScaleY + 0.5f);

                BallTrackAPI.ClearHistory();
				BallTrackAPI.drawProc = new BallTrackAPI.DrawDelegate(DrawPlay);
				BallTrackAPI.stateProc = new BallTrackAPI.StateUpdateDelegate(StateProc);

				BallTrackAPI.ShowTeacherPoint(false);
				BallTrackAPI.ShowBallSpeed(false);
            }
            catch (Exception ex) { MessageBox.Show("exno tgs1" +ex.ToString()); }
        }
        bllBillardLogic bll = new bllBillardLogic();

        public bool CheckAbleToInputScore()
        {
            if (playturn1 && BallTrackAPI.m_nCurrentPlayer != 0)
                return false;

            else if (!playturn1 && BallTrackAPI.m_nCurrentPlayer != 1)
                return false;

            if (BallTrackAPI.m_bTwoBallTooClose)
                return false;

            return true;
        }
		public void ShowRefereePresence()
		{
			try
			{
                if (!BallTrackAPI.m_bStartTracking)
                    return;

                if (BallTrackAPI.m_nInputMethod == 0 && !BLL_BilliardWindowsApplication.panalty)
                    return;

                if (BallTrackAPI.m_bTwoBallTooClose)
                {
                    pbCheckBallColor.Location = new Point(659, 289);
                    pbCheckBallColor.Visible = true;
                    pbCheckBallColor.BringToFront();
                    rafreeaction = true;

					colorwhite(true);
					colorYellow(true);
                }
				else if (playturn1 && BallTrackAPI.m_nCurrentPlayer != 0)
				{
					///20171204. for new spec when wrong ball shot, add custom point by referee
					//int nYScore = 0;
					//try
					//{
					//	nYScore = Convert.ToInt32(lblYscoreboard.Text);
					//}
					//catch (Exception)
					//{

					//}
					//lblYscoreboard.Text = (nYScore + 2).ToString();
					//lblYPoint.Tag = Convert.ToInt32(lblYPoint.Tag) - 2;
					//lblYPoint.Text = "p" + Convert.ToInt32(lblYPoint.Tag);
					//memorydetails m = new memorydetails();
					//m.score = 2;
					//m.extra = 5;
					//m.player1 = true;
					//m.point = lblWPoint.Text;
					//m.turn = lblWTurn.Text;
					//m.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;
					//m.isreplayrecord = true;
					//m.replayset = 1;
					//m.replayrecord = BallTrackAPI.BTAPI_GetYellowHistoryCount();
					//memory.Add(m);
					/////////////////////////////////////////////////////////////////////////////////
					pbRefereePresence.Location = new Point(659, 289);
					pbRefereePresence.Visible = true;
					pbRefereePresence.BringToFront();

					//BallTrackAPI.BTAPI_StopTracking();
                    rafreeaction = true;
					BallTrackAPI.m_bWrongBallShotted = true;
					colorwhite(true);
					colorYellow(true);

				}
				else if (!playturn1 && BallTrackAPI.m_nCurrentPlayer != 1)
				{
					
					pbRefereePresence.Location = new Point(659, 289);
					pbRefereePresence.Visible = true;
					pbRefereePresence.BringToFront();

					//BallTrackAPI.BTAPI_StopTracking();
                    rafreeaction = true;
					BallTrackAPI.m_bWrongBallShotted = true;
					colorwhite(true);
					colorYellow(true);
				}
				else
					BallTrackAPI.m_bWrongBallShotted = false;

			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);

			}
		}
		public void StateProc(int nStep)
		{
			try
			{

				//add unnamed short temporarily

                System.Threading.Thread.Sleep(1000);
                memorydetails m = new memorydetails();
                m.score = 0;
                m.extra = 6;
                m.player1 = playturn1;
				m.recordplayer = playturn1 ? 0 : 1;
                m.point = playturn1 ? lblWPoint.Text : lblYPoint.Text;
                m.turn = "t" + (turntag + 1);
                m.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;
                m.replayset = BallTrackAPI.m_nCurrentPlayer;
                m.isreplayrecord = true;
                if (BallTrackAPI.m_nCurrentPlayer == 0)
                    m.replayrecord = BallTrackAPI.BTAPI_GetWhiteHistoryCount() - 1;
                else
                    m.replayrecord = BallTrackAPI.BTAPI_GetYellowHistoryCount() - 1;

                memory.Add(m);
				System.Threading.Thread.Sleep(2000);
				BallTrackAPI.m_nCurrentStep = 2;
				BallTrackAPI.drawProc(2);
				this.BeginInvoke(new Action(() => ShowRefereePresence()));

				System.Threading.Thread.Sleep(3000);
				BallTrackAPI.StorePreviousTrajectory();
				//BallTrackAPI.ResetCurrentTrajectory();
				BallTrackAPI.drawProc(BallTrackAPI.m_nCurrentStep);
				BallTrackAPI.StopTracking();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}
        //Graphics m_gCanvas;

        TimeSpan currentTime;
        double dcost = 0.00;
        int slottime1 = 0;
        int slottime2 = 0;
        int slottime3 = 0;
        int slottime4 = 0;
        int noofplayers = 0;
        double totcost = 0.00;

        TimeSpan starttime = new TimeSpan(0, 0, 0);
        bool startset = true;
        bool playturn1 = true;
        bool gamepause = true;
        int timertick = 0;
        bool extratime1 = true;
        bool extratime2 = true;
        int set1 = 0;
        int set2 = 0;
        bool handicap = false;
        bool rightscore = false;
        bool UpScore = false;
        Timer tmstart = new Timer();
        Timer tmhandicap = new Timer();
        Timer tmextratime = new Timer();
        bool rafreeaction = false;
        bool timeoutPenalty = false;
        bool sendbackScore = false;
        bool addm1 = false;
        bool addm2 = false;
        bool colorw = false;
        bool colory = false;
		
        int m_nMarginX;
		int m_nMarginY;
		float m_fMarginScaleX;
		float m_fMarginScaleY;
		float m_fScaleX;
		float m_fScaleY;
		Rectangle m_rcDrawRect;
		Size m_szImageBackground = new Size(523, 950);
        bool m_bRepositionBall = false;
        
        int turntag = 0;
        
        biliardService.BilliardScoreboard API = new biliardService.BilliardScoreboard();
        List<List<memorydetails>> memorylist = new List<List<memorydetails>>();
        List<memorydetails> memory = new List<memorydetails>();
        void playclicksound()
        {
            try
            {
                new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            }
            catch (Exception ex) { MessageBox.Show("exno tgs2" + ex.ToString()); }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                int t1 = starttime.Minutes;
                starttime = starttime.Add(new TimeSpan(0, 0, 1));
                int t2 = starttime.Minutes;
                try
                {
                    if (t1 != t2)
                        calculatecostlbl();
                }
                catch (Exception ex) { MessageBox.Show("ex costcalutate:- " + ex.ToString()); }

                if ((BilliardWindowsApplication.Properties.Settings.Default.billiardno > 0 && int.Parse(BilliardWindowsApplication.Properties.Settings.Default.CID) > 0) && (BLL_BilliardWindowsApplication.costDetailsStataic.coston == "t" && BLL_BilliardWindowsApplication.costDetailsStataic.costvisible == "t"))
                    lbltimer.Text = starttime.ToString() + " - " + lblCost.Text;
                else lbltimer.Text = starttime.ToString();

                //starttime = starttime.Add(new TimeSpan(0, 0, 1));
                //lbltimer.Text = starttime.ToString();
            }
            catch (Exception ex) { MessageBox.Show("exno tgs3" + ex.ToString()); }
        }

        string htmlgamecostcode(string playername, string costplayer)
        {
            string gamecostbody = "<img src = " + '"' + "http://score.biliardoprofessionale.it/img/logoTop.png" + '"' + " /><br /><br />" +
                " <div style =" + '"' + "border-top:3px solid #22BCE5; border-top-width: 1px;" + '"' + "></div>" +
                "<span style = " + '"' + "font-family:Arial;font-size:10pt" + '"' + ">      <h1>  <strong>Cost of Billiard</strong><br /></h1>" +
                "Hello <b>" + playername + "</b><br /><br />" +
                "Billiard Number : " + BLL_BilliardWindowsApplication.costDetailsStataic.bilino + "<br>" +
                "Date : " + DateTime.Now.Date.ToShortDateString() + "  <br>";
            if (currentTime.Hours > 12)
                gamecostbody = gamecostbody + "From " + (currentTime.Hours - 12) / 10 + (currentTime.Hours - 12) % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "PM";
            else if (currentTime.Hours == 12)
                gamecostbody = gamecostbody + "From " + currentTime.Hours / 10 + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "PM";
            else gamecostbody = gamecostbody + "From " + currentTime.Hours / 10 + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "AM";
            if (DateTime.Now.Hour > 12)
                gamecostbody = gamecostbody + " / To " + (DateTime.Now.Hour - 12) / 10 + (DateTime.Now.Hour - 12) % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "PM  <br>";
            else if (DateTime.Now.Hour == 12)
                gamecostbody = gamecostbody + " / To " + DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "PM  <br>";
            else gamecostbody = gamecostbody + " / To " + DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "AM  <br>";
            if (slottime1 > 0)
                gamecostbody = gamecostbody + "Slot Time n<sup>o</sup> " + "1" + " / n<sup>o</sup> " + slottime1 + " minutes <br>";
            if (slottime2 > 0)
                gamecostbody = gamecostbody + "Slot Time n<sup>o</sup> " + "2" + " / n<sup>o</sup> " + slottime2 + " minutes <br>";
            if (slottime3 > 0)
                gamecostbody = gamecostbody + "Slot Time n<sup>o</sup> " + "3" + " / n<sup>o</sup> " + slottime3 + " minutes <br>";
            if (slottime4 > 0)
                gamecostbody = gamecostbody + "Slot Time n<sup>o</sup> " + "4" + " / n<sup>o</sup> " + slottime4 + " minutes <br>";
            gamecostbody = gamecostbody +
                "Total time n<sup>o</sup> " + (starttime.Hours * 60 + starttime.Minutes) + " Minutes <br>";


            if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                gamecostbody = gamecostbody + "Total cost to be payed : <b> Cost+Special " + costplayer + " Euro </b><br><br />";
            else gamecostbody = gamecostbody + "Total cost to be payed : <b> Cost " + costplayer + " Euro </b><br><br />";
            gamecostbody = gamecostbody + "Thanks<br />" +
               "Biliardo Professionale<br>" +
               "<b>" + "Club " + BilliardWindowsApplication.Properties.Settings.Default.clubname + "</b>" + "</span>";
            return gamecostbody;
        }
        async void insertgamecost(biliardService.gamecostdetails gcdetails)
        {
            await Task.Run(() => API.Updategamecost(gcdetails));
            //MessageBox.Show(respond);
        }
        void costmail()
        {
            try
            {

                if (usbrelay.relay1on == 1)
                {
                    int a = usbrelay.usb_relay_device_close_one_relay_channel(usbrelay.hHandle, 01);
                    if (a == 0) usbrelay.relay1on = 0;
                    //MessageBox.Show("" + a);
                }


                if (usbrelay.relay2on == 1)
                {
                    int a = usbrelay.usb_relay_device_close_one_relay_channel(usbrelay.hHandle, 02);
                    if (a == 0) usbrelay.relay2on = 0;
                    //MessageBox.Show("" + a);
                }

                if (BilliardWindowsApplication.Properties.Settings.Default.billiardno > 0)
                {

                    string cost = lblCost.Text.Replace("€", "").Replace(",", ".");

                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.billno = BilliardWindowsApplication.Properties.Settings.Default.billiardno.ToString();
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.clubid = BilliardWindowsApplication.Properties.Settings.Default.CID;
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.date = DateTime.Now.Date.Day + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year;
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.duration = (starttime.Hours * 60) / 10 + (starttime.Hours * 60) % 10 + starttime.Minutes / 10 + starttime.Minutes % 10 + "";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.fromtime = currentTime.Hours / 10 + "" + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + "" + currentTime.Minutes % 10 + "";
                    noofplayers = 0;
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p1 = BLL_BilliardWindowsApplication.player1.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p1 = "0";
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p2 = BLL_BilliardWindowsApplication.player2.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p2 = "0";
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p3 = BLL_BilliardWindowsApplication.player3.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p3 = "0";
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p4 = BLL_BilliardWindowsApplication.player4.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p4 = "0";

                    if (BLL_BilliardWindowsApplication.costDetailsStataic.coston == "t")
                    {
                        BLL_BilliardWindowsApplication.gamecostdetailsStatic.totcost = cost.Replace("€", "").Replace(",", ".");

                        if (BLL_BilliardWindowsApplication.languague == 0)
                            BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer = (double.Parse(lblCost.Text.Replace("€", "").Replace(",", ".")) / noofplayers).ToString().Replace(",", ".");
                        else if (BLL_BilliardWindowsApplication.languague == 1)
                            BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer = (double.Parse(lblCost.Text.Replace("€", "")) / noofplayers).ToString().Replace(",", ".");
                    }
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.totime = DateTime.Now.Hour / 10 + "" + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + "" + DateTime.Now.Minute % 10 + "";
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.noplayers = noofplayers.ToString();
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.gameover = true.ToString();
                    insertgamecost(BLL_BilliardWindowsApplication.gamecostdetailsStatic);

                    BLL_BilliardWindowsApplication.costDetailsStataic.d1 = int.Parse(BLL_BilliardWindowsApplication.costDetailsStataic.d1) + slottime1 + "";
                    BLL_BilliardWindowsApplication.costDetailsStataic.d2 = int.Parse(BLL_BilliardWindowsApplication.costDetailsStataic.d2) + slottime2 + "";
                    BLL_BilliardWindowsApplication.costDetailsStataic.d3 = int.Parse(BLL_BilliardWindowsApplication.costDetailsStataic.d3) + slottime3 + "";
                    BLL_BilliardWindowsApplication.costDetailsStataic.d4 = int.Parse(BLL_BilliardWindowsApplication.costDetailsStataic.d4) + slottime4 + "";
                    API.updatecostdaysAsync(BLL_BilliardWindowsApplication.costDetailsStataic);

                    // cost of game mail
                    string gamecostclub = "<img src = " + '"' + "http://score.biliardoprofessionale.it/img/logoTop.png" + '"' + " /><br /><br />" +
                    " <div style =" + '"' + "border-top:3px solid #22BCE5; border-top-width: 1px;" + '"' + "></div>" +
                    "<span style = " + '"' + "font-family:Arial;font-size:10pt" + '"' + ">      <h1>  <strong>Cost of Billiard</strong><br /></h1>";

                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
                    {
                        string gamecostbody = htmlgamecostcode(BLL_BilliardWindowsApplication.player1.Name + " " + BLL_BilliardWindowsApplication.player1.FamilyName, BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ","));
                        if (BLL_BilliardWindowsApplication.costDetailsStataic.emailcostPlayer == "t")
                            SendHtmlFormattedEmail(BLL_BilliardWindowsApplication.player1.PlayerId, "Cost of Billiard", gamecostbody);

                        if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                            gamecostclub += "<br><b>TOTAL TO BE CASHED : Cost+Special " + BLL_BilliardWindowsApplication.gamecostdetailsStatic.totcost.Replace(".", ",") + " Euro</b><br><br>-----<br><b>" + BLL_BilliardWindowsApplication.player1.Name + " " + BLL_BilliardWindowsApplication.player1.FamilyName + "</b><br /><br />";
                        else gamecostclub += "<br><b>TOTAL TO BE CASHED : Cost " + BLL_BilliardWindowsApplication.gamecostdetailsStatic.totcost.Replace(".", ",") + " Euro</b><br><br>-----<br><b>" + BLL_BilliardWindowsApplication.player1.Name + " " + BLL_BilliardWindowsApplication.player1.FamilyName + "</b><br /><br />";
                        gamecostclub += "Billiard Number : " + BLL_BilliardWindowsApplication.costDetailsStataic.bilino + "<br>" +
                        "Date : " + DateTime.Now.Date.ToShortDateString() + "  <br>";
                        if (currentTime.Hours > 12)
                            gamecostbody = gamecostbody + "From " + (currentTime.Hours - 12) / 10 + (currentTime.Hours - 12) % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "PM";
                        else if (currentTime.Hours == 12)
                            gamecostbody = gamecostbody + "From " + currentTime.Hours / 10 + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "PM";
                        else gamecostbody = gamecostbody + "From " + currentTime.Hours / 10 + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "AM";
                        if (DateTime.Now.Hour > 12)
                            gamecostbody = gamecostbody + " / To " + (DateTime.Now.Hour - 12) / 10 + (DateTime.Now.Hour - 12) % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "PM  <br>";
                        else if (DateTime.Now.Hour == 12)
                            gamecostbody = gamecostbody + " / To " + DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "PM  <br>";
                        else gamecostbody = gamecostbody + " / To " + DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "AM  <br>";
                        if (slottime1 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "1" + " / n<sup>o</sup> " + slottime1 + " minutes <br>";
                        if (slottime2 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "2" + " / n<sup>o</sup> " + slottime2 + " minutes <br>";
                        if (slottime3 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "3" + " / n<sup>o</sup> " + slottime3 + " minutes <br>";
                        if (slottime4 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "4" + " / n<sup>o</sup> " + slottime4 + " minutes <br>";
                        gamecostclub = gamecostclub +

                    "Total time n<sup>o</sup> " + (starttime.Hours * 60 + starttime.Minutes) + " Minutes <br>";
                        if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                            gamecostclub = gamecostclub + "Total cost to be payed : <b> Cost+Special " + BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ",") + " Euro </b><br><br />";
                        else gamecostclub = gamecostclub + "Total cost to be payed : <b> Cost " + BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ",") + " Euro </b><br><br />";


                    }
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
                    {
                        string gamecostbody = htmlgamecostcode(BLL_BilliardWindowsApplication.player2.Name + " " + BLL_BilliardWindowsApplication.player2.FamilyName, BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ","));
                        if (BLL_BilliardWindowsApplication.costDetailsStataic.emailcostPlayer == "t")
                            SendHtmlFormattedEmail(BLL_BilliardWindowsApplication.player2.PlayerId, "Cost of Billiard", gamecostbody);

                        gamecostclub += "-----<br><b>" + BLL_BilliardWindowsApplication.player2.Name + " " + BLL_BilliardWindowsApplication.player2.FamilyName + "</b><br /><br />" +
                    "Billiard Number : " + BLL_BilliardWindowsApplication.costDetailsStataic.bilino + "<br>" +
                    "Date : " + DateTime.Now.Date.ToShortDateString() + "  <br>";
                        if (currentTime.Hours > 12)
                            gamecostbody = gamecostbody + "From " + (currentTime.Hours - 12) / 10 + (currentTime.Hours - 12) % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "PM";
                        else if (currentTime.Hours == 12)
                            gamecostbody = gamecostbody + "From " + currentTime.Hours / 10 + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "PM";
                        else gamecostbody = gamecostbody + "From " + currentTime.Hours / 10 + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "AM";
                        if (DateTime.Now.Hour > 12)
                            gamecostbody = gamecostbody + " / To " + (DateTime.Now.Hour - 12) / 10 + (DateTime.Now.Hour - 12) % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "PM  <br>";
                        else if (DateTime.Now.Hour == 12)
                            gamecostbody = gamecostbody + " / To " + DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "PM  <br>";
                        else gamecostbody = gamecostbody + " / To " + DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "AM  <br>";
                        if (slottime1 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "1" + " / n<sup>o</sup> " + slottime1 + " minutes <br>";
                        if (slottime2 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "2" + " / n<sup>o</sup> " + slottime2 + " minutes <br>";
                        if (slottime3 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "3" + " / n<sup>o</sup> " + slottime3 + " minutes <br>";
                        if (slottime4 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "4" + " / n<sup>o</sup> " + slottime4 + " minutes <br>";
                        gamecostclub = gamecostclub +
                    "Total time n<sup>o</sup> " + (starttime.Hours * 60 + starttime.Minutes) + " Minutes <br>";
                        if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                            gamecostclub = gamecostclub + "Total cost to be payed : <b> Cost+Special " + BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ",") + " Euro </b><br><br />";
                        else gamecostclub = gamecostclub + "Total cost to be payed : <b> Cost " + BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ",") + " Euro </b><br><br />";

                    }
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
                    {
                        string gamecostbody = htmlgamecostcode(BLL_BilliardWindowsApplication.player3.Name + " " + BLL_BilliardWindowsApplication.player3.FamilyName, BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ","));
                        if (BLL_BilliardWindowsApplication.costDetailsStataic.emailcostPlayer == "t")
                            SendHtmlFormattedEmail(BLL_BilliardWindowsApplication.player3.PlayerId, "Cost of Billiard", gamecostbody);

                        gamecostclub += "-----<br><b>" + BLL_BilliardWindowsApplication.player3.Name + " " + BLL_BilliardWindowsApplication.player3.FamilyName + "</b><br /><br />" +
                   "Billiard Number : " + BLL_BilliardWindowsApplication.costDetailsStataic.bilino + "<br>" +
                   "Date : " + DateTime.Now.Date.ToShortDateString() + "  <br>";

                        if (currentTime.Hours > 12)
                            gamecostbody = gamecostbody + "From " + (currentTime.Hours - 12) / 10 + (currentTime.Hours - 12) % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "PM";
                        else if (currentTime.Hours == 12)
                            gamecostbody = gamecostbody + "From " + currentTime.Hours / 10 + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "PM";
                        else gamecostbody = gamecostbody + "From " + currentTime.Hours / 10 + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "AM";
                        if (DateTime.Now.Hour > 12)
                            gamecostbody = gamecostbody + " / To " + (DateTime.Now.Hour - 12) / 10 + (DateTime.Now.Hour - 12) % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "PM  <br>";
                        else if (DateTime.Now.Hour == 12)
                            gamecostbody = gamecostbody + " / To " + DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "PM  <br>";
                        else gamecostbody = gamecostbody + " / To " + DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "AM  <br>";
                        if (slottime1 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "1" + " / n<sup>o</sup> " + slottime1 + " minutes <br>";
                        if (slottime2 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "2" + " / n<sup>o</sup> " + slottime2 + " minutes <br>";
                        if (slottime3 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "3" + " / n<sup>o</sup> " + slottime3 + " minutes <br>";
                        if (slottime4 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "4" + " / n<sup>o</sup> " + slottime4 + " minutes <br>";
                        gamecostclub = gamecostclub +
                         "Total time n<sup>o</sup> " + (starttime.Hours * 60 + starttime.Minutes) + " Minutes <br>";
                        if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                            gamecostclub = gamecostclub + "Total cost to be payed : <b> Cost+Special " + BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ",") + " Euro </b><br><br />";
                        else gamecostclub = gamecostclub + "Total cost to be payed : <b> Cost " + BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ",") + " Euro </b><br><br />";

                    }
                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
                    {
                        string gamecostbody = htmlgamecostcode(BLL_BilliardWindowsApplication.player4.Name + " " + BLL_BilliardWindowsApplication.player4.FamilyName, BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ","));
                        if (BLL_BilliardWindowsApplication.costDetailsStataic.emailcostPlayer == "t")
                            SendHtmlFormattedEmail(BLL_BilliardWindowsApplication.player4.PlayerId, "Cost of Billiard", gamecostbody);

                        gamecostclub += "-----<br><b>" + BLL_BilliardWindowsApplication.player4.Name + " " + BLL_BilliardWindowsApplication.player4.FamilyName + "</b><br /><br />" +
                   "Billiard Number : " + BLL_BilliardWindowsApplication.costDetailsStataic.bilino + "<br>" +
                   "Date : " + DateTime.Now.Date.ToShortDateString() + "  <br>";

                        if (currentTime.Hours > 12)
                            gamecostbody = gamecostbody + "From " + (currentTime.Hours - 12) / 10 + (currentTime.Hours - 12) % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "PM";
                        else if (currentTime.Hours == 12)
                            gamecostbody = gamecostbody + "From " + currentTime.Hours / 10 + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "PM";
                        else gamecostbody = gamecostbody + "From " + currentTime.Hours / 10 + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + currentTime.Minutes % 10 + "AM";
                        if (DateTime.Now.Hour > 12)
                            gamecostbody = gamecostbody + " / To " + (DateTime.Now.Hour - 12) / 10 + (DateTime.Now.Hour - 12) % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "PM  <br>";
                        else if (DateTime.Now.Hour == 12)
                            gamecostbody = gamecostbody + " / To " + DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "PM  <br>";
                        else gamecostbody = gamecostbody + " / To " + DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "AM  <br>";
                        if (slottime1 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "1" + " / n<sup>o</sup> " + slottime1 + " minutes <br>";
                        if (slottime2 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "2" + " / n<sup>o</sup> " + slottime2 + " minutes <br>";
                        if (slottime3 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "3" + " / n<sup>o</sup> " + slottime3 + " minutes <br>";
                        if (slottime4 > 0)
                            gamecostclub = gamecostclub + "Slot Time n<sup>o</sup> " + "4" + " / n<sup>o</sup> " + slottime4 + " minutes <br>";
                        gamecostclub = gamecostclub +
                           "Total time n<sup>o</sup> " + (starttime.Hours * 60 + starttime.Minutes) + " Minutes <br>";
                        if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                            gamecostclub = gamecostclub + "Total cost to be payed : <b> Cost+Special " + BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ",") + " Euro </b><br><br />";
                        else gamecostclub = gamecostclub + "Total cost to be payed : <b> Cost " + BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer.Replace(".", ",") + " Euro </b><br><br />";

                    }

                    gamecostclub += "Thanks<br />" +
                    "Biliardo Professionale<br>" +
                    "<b>" + "Club " + BilliardWindowsApplication.Properties.Settings.Default.clubname + "</b>" + "</span>";

                    if (BLL_BilliardWindowsApplication.costDetailsStataic.emailcostowner == "t")
                        SendHtmlFormattedEmail(BilliardWindowsApplication.Properties.Settings.Default.emailid, "Cost of Billiard", gamecostclub);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }


        }
        string htmlcode(string TimeFinish, bool firstwin, int p1, int p2, List<List<memorydetails>> tabledetails)
        {
            string TEMPSCORE = "";
            string table = "";
            table = "<table> <tr>";

            //stats var
            int pl12set, pl34set, pl12shot, pl34shot, pl12score, pl34score, pl12zero, pl34zero, pl12gain, pl34gain, pl12pal, pl34pal, pl12han, pl34han;

            pl12set = pl34set = pl12shot = pl34shot = pl12score = pl34score = pl12zero = pl34zero = pl12gain = pl34gain = pl12pal = pl34pal = pl12han = pl34han = 0;

            string pl12mediapoints = "", pl34mediapoints = "";

            
            for (int i = 0; i < tabledetails.Count; i++)
            {
                int p1turn = -1, turn = 0; ;   
                table += "<td style=" + '"' + "vertical-align:top" + '"' + ">Result  Set " + (i + 1) + "<br>";
                table += "<table>";
   
                for (int j = 0; j < tabledetails[i].Count; j++)
                {

                    if (tabledetails[i][j].score >= 0)
                        TEMPSCORE = "+" + tabledetails[i][j].score;
                    else TEMPSCORE = tabledetails[i][j].score + "";
                    if (tabledetails[i][j].extra == 1)
                    {
                        TEMPSCORE = "@2T1" + TEMPSCORE;
                        if (tabledetails[i][j].player1)
                        {
                            pl34pal += tabledetails[i][j].score;
                        }
                        else
                        {
                            pl12pal += tabledetails[i][j].score;
                        }
                    }
                    if (tabledetails[i][j].extra == 2)
                    {
                        TEMPSCORE = "@2T2" + TEMPSCORE;

                        if (tabledetails[i][j].player1)
                        {
                            pl34pal += tabledetails[i][j].score;
                        }
                        else
                        {
                            pl12pal += tabledetails[i][j].score;
                        }
                    }
                    if (tabledetails[i][j].extra == 3)
                    {
                        TEMPSCORE = "@" + TEMPSCORE;
                        if (tabledetails[i][j].player1)
                            pl34gain += tabledetails[i][j].score;
                        else pl12gain += tabledetails[i][j].score;
                    }
                    if (tabledetails[i][j].extra == 4)
                    {
                        TEMPSCORE = "H" + TEMPSCORE;
                        if (tabledetails[i][j].player1)
                            pl12han += tabledetails[i][j].score;
                        else pl12han += tabledetails[i][j].score;
                    }
                    if (tabledetails[i][j].player1)
                    {
                        if (turn < Convert.ToInt32(tabledetails[i][j].turn.Split('t')[1]))
                        { pl12shot++; turn++; }
                        p1turn = 1;
                        if (tabledetails[i][j].extra == 0)
                        {
                            if (tabledetails[i][j].score > 0)
                                pl12mediapoints += tabledetails[i][j].score + ",";
                             pl12score += tabledetails[i][j].score;
                            if (tabledetails[i][j].score == 0)
                                pl12zero++;

                        }
                        table += "<font style=" + '"' + "color:black" + '"' + " ><tr style=" + '"' + "color:#black" + '"' + "> <td>" + TEMPSCORE + "&nbsp;&nbsp;</td><td>" + tabledetails[i][j].point + "&nbsp;&nbsp;</td><td>" + tabledetails[i][j].turn + "&nbsp;&nbsp;</td><td>" + tabledetails[i][j].set + "&nbsp;&nbsp;</td>" + "</tr></font>";
                    }
                    else
                    {
                        if (turn < Convert.ToInt32(tabledetails[i][j].turn.Split('t')[1]))
                        { pl34shot++; turn++; }
                        p1turn = 2;
                        if (tabledetails[i][j].extra == 0)
                        {
                            if (tabledetails[i][j].score > 0)
                                pl34mediapoints += tabledetails[i][j].score + ",";
                             pl34score += tabledetails[i][j].score;
                            if (tabledetails[i][j].score == 0)
                                pl34zero++;
                        } table += "<font style=" + '"' + "color:#D2691E" + '"' + " ><tr style=" + '"' + "color:#D2691E" + '"' + "><td>" + TEMPSCORE + "&nbsp;&nbsp;</td><td>" + tabledetails[i][j].point + "&nbsp;&nbsp;</td><td>" + tabledetails[i][j].turn + "&nbsp;&nbsp;</td><td>" + tabledetails[i][j].set + "&nbsp;&nbsp;</td>" + " </tr></font>";
                    }
                }
                if (p1turn == 1)
                    pl12set++;
                else pl34set++;
                table += "</table>";
               table+= "</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>";
            }
            table = table + "</tr></table>";

            string teamAname = "";
            string teamBname = "";
            string teamAclub = "";
            string teamBclub = "";

            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
            { teamAname += BLL_BilliardWindowsApplication.player1.Name + " " + BLL_BilliardWindowsApplication.player1.FamilyName; teamAclub += BLL_BilliardWindowsApplication.player1.ClubName; }
            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
            { teamAname += " & " + BLL_BilliardWindowsApplication.player3.Name + " " + BLL_BilliardWindowsApplication.player3.FamilyName; teamAclub += " & " + BLL_BilliardWindowsApplication.player3.ClubName; }
            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
            { teamBname += BLL_BilliardWindowsApplication.player2.Name + " " + BLL_BilliardWindowsApplication.player2.FamilyName; teamBclub += BLL_BilliardWindowsApplication.player2.ClubName; }
            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
            { teamBname += " & " + BLL_BilliardWindowsApplication.player4.Name + " " + BLL_BilliardWindowsApplication.player4.FamilyName; teamBclub += " & " + BLL_BilliardWindowsApplication.player4.ClubName; }



            float a = (float)(pl12score - pl34pal - pl34gain) / (float)(pl12shot - pl12zero);
            float b = (float)(pl34score - pl12pal - pl12gain) / (float)(pl34shot - pl34zero);
            

            table +=  "<br> " + teamAname+ "--statistic :</strong>";
            table += "<br> [Set Wins " + pl12set + "]--[Total Shots " + pl12shot + "/media points " + (pl12score - pl34pal - pl34gain) + "]--[Shot with points " + (pl12shot - pl12zero) + "/media points " + a.ToString("0.000").Replace('.',',')+ "]--[shot with zeroPoint " + pl12zero + "]--[points granted @" + pl34gain + "]--[panalty point @" + pl34pal + "]--[handicap point " + pl12han + "]";
            table +=  "<br> <br>";
            table +=  "<br> <font style=" + '"' + "color:#D2691E" + '"' + "><strong>" +teamBname+ "--statistic :</strong>";
            table += "<br>[Set Wins " + pl34set + "]--[Total Shots " + pl34shot + "/media points " + (pl34score - pl12pal - pl12gain) + "]--[Shot with points " + (pl34shot - pl34zero) + "/media points " + b.ToString("0.000").Replace('.', ',') + "]--[shot with zeroPoint " + pl34zero + "]--[points granted @" + pl12gain + "]--[panalty point @" + pl12pal + "]--[handicap point " + pl34han + "]";
            table +=  "<strong></font>";


            biliardService.ScoreData scdata = new biliardService.ScoreData();

            scdata.gameid = Int32.Parse(BLL_BilliardWindowsApplication.gamecostdetailsStatic.id);
            scdata.setdata = false;
            scdata.setid = 0;
            scdata.status = "set";
            scdata.tlavail = false;

            scdata.text = " [Set Wins " + pl12set + "]--[Total Shots " + pl12shot + "/media points " + (pl12score - pl34pal - pl34gain) + "]--[Shot with points " + (pl12shot - pl12zero) + "/media points " + a.ToString("0.000").Replace('.', ',') + "]--[shot with zeroPoint " + pl12zero + "]--[points granted @" + pl34gain + "]--[panalty point @" + pl34pal + "]--[handicap point " + pl12han + "]";
            scdata.color = "White";
            API.setScoreData(scdata);

            scdata.text = "[Set Wins " + pl34set + "]--[Total Shots " + pl34shot + "/media points " + (pl34score - pl12pal - pl12gain) + "]--[Shot with points " + (pl34shot - pl34zero) + "/media points " + b.ToString("0.000").Replace('.', ',') + "]--[shot with zeroPoint " + pl34zero + "]--[points granted @" + pl12gain + "]--[panalty point @" + pl12pal + "]--[handicap point " + pl34han + "]";
            scdata.color = "#D2691E";
            API.setScoreData(scdata);

            string home = "<!DOCTYPE html> <html xmlns=" + '"' + "http://www.w3.org/1999/xhtml" + '"' + ">" +
            "<head> <style type="+'"'+"text/css"+'"'+">.boxed { border: 1px solid green; } </style><title> </title> </head> <body>    <img src = " + '"' + "http://score.biliardoprofessionale.it/img/logoTop.png" + '"' + " /><br /><br />" +
            " <div style =" + '"' + "border-top:3px solid #22BCE5; border-top-width: 1px;" + '"' + "></div>" +
            "<span style = " + '"' + "font-family:Arial;font-size:10pt" + '"' + ">      <h1>  <strong>Game Summary</strong><br /></h1>" +
            "<font style=" + '"' + "color:blue" + '"' + " ><u><strong>Game settled at : </strong></u></font>  &nbsp;   Point : &nbsp; " + BLL_BilliardWindowsApplication.point + " --- Set n. : " + BLL_BilliardWindowsApplication.set + " ---- Quills n. : " + BLL_BilliardWindowsApplication.quills + " ---  Timer 1 : " + BLL_BilliardWindowsApplication.timer1 + "’’ --- Time 2  : " + BLL_BilliardWindowsApplication.timer2 + "’’ " +
            "<br /><br/>" +
            "<font style=" + '"' + "color:blue" + '"' + " ><u><strong>Time Info :  </strong></u></font> &nbsp;  Start time : 00.00.00 --- Finish Time : " + TimeFinish + "   --- Total Duration : " + TimeFinish +
            " <br /><br />" +
            " <strong> <font style=" + '"' + "color:blue" + '"' + " ><u>Game Held Between : </u></font></strong>&nbsp;" +
            "    <strong>" + teamAname + " / " + teamAclub + "   <font style=" + '"' + "color:red" + '"' + " > < vs > </font><font style=" + '"' + "color:#D2691E" + '"' + " >  " + teamBname + " / " + teamBclub + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong/></font>" +
            "	<br/><br/>" +
            "      <font style=" + '"' + "color:blue" + '"' + " ><u> Winner of the match : </u></font> &nbsp; ";

            if (firstwin)
                home += teamAname;
            else home += teamBname;
          
          home+=" <br/><br/>  " +"<font style=" + '"' + "color:blue" + '"' + " ><u>Loser of the match : </u></font> &nbsp; ";
            if (firstwin)
                home += teamBname;
            else home += teamAname;
           
            home +=
              "        </strong>" +
              "        <br />" +
              "<h1>  <strong>  E-mail Score Report</strong><br /></h1>" +
              "</span>";
            home += "<div class=" + '"' + "boxed" + '"' + "> <br><strong> " + table + " </strong><br><br></div>"
            + "   <br> <br><strong> Thanks<br />" +
              "      Biliardo Professionale" +
              "</strong>" +
              "</body>" +
              "</html>";
            return home;
        }
        private void SendHtmlFormattedEmail(List<string> recepientEmail, string subject, string body)
        {
            try
            {
                for (int i = 0; i < recepientEmail.Count; i++)
                API.getmail("", "info@biliardoprofessionale.it", recepientEmail[i], subject, body);
                
            }
          
            catch (Exception ex) { MessageBox.Show("exno tgs5" + ex.ToString()); }
        }
        private async void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            try
            {
                await Task.Run(() => API.getmail("", "info@biliardoprofessionale.it", recepientEmail, subject, body));
            }
            catch { }
        }

        void calculatecostlbl()
        {
            TimeSpan t1, t2, t;

            int hrs = DateTime.Now.Hour;
            int min = DateTime.Now.Minute;

            t = new TimeSpan(hrs, min, 0);
            t1 = bll.convertDbToTime(BLL_BilliardWindowsApplication.costDetailsStataic.f1);
            t2 = bll.convertDbToTime(BLL_BilliardWindowsApplication.costDetailsStataic.t1);
            if (t1 < t2)
            {
                if (t >= t1 && t <= t2)
                {
                    slottime1++;

                    double newcost = Convert.ToDouble(BLL_BilliardWindowsApplication.costDetailsStataic.h1.Replace("€", "").Replace(",", ".")) / 60;
                    if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                        dcost = dcost + (newcost * Convert.ToInt32(BLL_BilliardWindowsApplication.costDetailsStataic.SpecialCharge)) / 100 + newcost;
                    else dcost = dcost + newcost;

                }
            }
            else
            {
                if (t >= t1 || t <= t2)
                {
                    slottime1++;
                    double newcost = Convert.ToDouble(BLL_BilliardWindowsApplication.costDetailsStataic.h1.Replace("€", "").Replace(",", ".")) / 60;
                    if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                        dcost = dcost + (newcost * Convert.ToInt32(BLL_BilliardWindowsApplication.costDetailsStataic.SpecialCharge)) / 100 + newcost;
                    else dcost = dcost + newcost;
                }
            }


            t1 = bll.convertDbToTime(BLL_BilliardWindowsApplication.costDetailsStataic.f2);
            t2 = bll.convertDbToTime(BLL_BilliardWindowsApplication.costDetailsStataic.t2);
            if (t1 < t2)
            {
                if (t >= t1 && t <= t2)
                {
                    slottime2++;
                    double newcost = Convert.ToDouble(BLL_BilliardWindowsApplication.costDetailsStataic.h2.Replace("€", "").Replace(",", ".")) / 60;
                    if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                        dcost = dcost + (newcost * Convert.ToInt32(BLL_BilliardWindowsApplication.costDetailsStataic.SpecialCharge)) / 100 + newcost;
                    else dcost = dcost + newcost;
                }
            }
            else
            {
                if (t >= t1 || t <= t2)
                {
                    slottime2++;
                    double newcost = Convert.ToDouble(BLL_BilliardWindowsApplication.costDetailsStataic.h2.Replace("€", "").Replace(",", ".")) / 60;
                    if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                        dcost = dcost + (newcost * Convert.ToInt32(BLL_BilliardWindowsApplication.costDetailsStataic.SpecialCharge)) / 100 + newcost;
                    else dcost = dcost + newcost;
                }
            }


            t1 = bll.convertDbToTime(BLL_BilliardWindowsApplication.costDetailsStataic.f3);
            t2 = bll.convertDbToTime(BLL_BilliardWindowsApplication.costDetailsStataic.t3);
            if (t1 < t2)
            {
                if (t >= t1 && t <= t2)
                {
                    slottime3++;
                    double newcost = Convert.ToDouble(BLL_BilliardWindowsApplication.costDetailsStataic.h3.Replace("€", "").Replace(",", ".")) / 60;
                    if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                        dcost = dcost + (newcost * Convert.ToInt32(BLL_BilliardWindowsApplication.costDetailsStataic.SpecialCharge)) / 100 + newcost;
                    else dcost = dcost + newcost;
                }
            }
            else
            {
                if (t >= t1 || t <= t2)
                {
                    slottime3++;
                    double newcost = Convert.ToDouble(BLL_BilliardWindowsApplication.costDetailsStataic.h3.Replace("€", "").Replace(",", ".")) / 60;
                    if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                        dcost = dcost + (newcost * Convert.ToInt32(BLL_BilliardWindowsApplication.costDetailsStataic.SpecialCharge)) / 100 + newcost;
                    else dcost = dcost + newcost;
                }
            }


            t1 = bll.convertDbToTime(BLL_BilliardWindowsApplication.costDetailsStataic.f4);
            t2 = bll.convertDbToTime(BLL_BilliardWindowsApplication.costDetailsStataic.t4);
            if (t1 < t2)
            {
                if (t >= t1 && t <= t2)
                {
                    slottime4++;
                    double newcost = Convert.ToDouble(BLL_BilliardWindowsApplication.costDetailsStataic.h4.Replace("€", "").Replace(",", ".")) / 60;
                    if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                        dcost = dcost + (newcost * Convert.ToInt32(BLL_BilliardWindowsApplication.costDetailsStataic.SpecialCharge)) / 100 + newcost;
                    else dcost = dcost + newcost;
                }
            }
            else
            {
                if (t >= t1 || t <= t2)
                {
                    slottime4++;
                    double newcost = Convert.ToDouble(BLL_BilliardWindowsApplication.costDetailsStataic.h4.Replace("€", "").Replace(",", ".")) / 60;
                    if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
                        dcost = dcost + (newcost * Convert.ToInt32(BLL_BilliardWindowsApplication.costDetailsStataic.SpecialCharge)) / 100 + newcost;
                    else dcost = dcost + newcost;

                }
            }

            string[] coststring = dcost.ToString().Replace(",", ".").Split('.');
            lblCost.Text = coststring[0];
            try
            {
                if (coststring[1].Length > 1)
                    lblCost.Text = coststring[0] + "," + coststring[1].Substring(0, 2) + " €";
                else lblCost.Text = coststring[0] + "," + coststring[1] + "0" + " €";
            }
            catch { lblCost.Text = coststring[0] + "," + "00" + " €"; }


            if (BilliardWindowsApplication.Properties.Settings.Default.billiardno > 0 && BLL_BilliardWindowsApplication.costDetailsStataic.coston == "t")
            {
                string cost = lblCost.Text.Replace("€", "").Replace(",", ".");
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.billno = BilliardWindowsApplication.Properties.Settings.Default.billiardno.ToString();
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.clubid = BilliardWindowsApplication.Properties.Settings.Default.CID;
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.date = DateTime.Now.Date.Day + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year;
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.duration = (starttime.Hours * 60) / 10 + (starttime.Hours * 60) % 10 + starttime.Minutes / 10 + starttime.Minutes % 10 + "";
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.fromtime = currentTime.Hours / 10 + "" + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + "" + currentTime.Minutes % 10 + "";
                noofplayers = 0;
                if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p1 = BLL_BilliardWindowsApplication.player1.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p1 = "0";
                if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p2 = BLL_BilliardWindowsApplication.player2.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p2 = "0";
                if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p3 = BLL_BilliardWindowsApplication.player3.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p3 = "0";
                if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p4 = BLL_BilliardWindowsApplication.player4.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p4 = "0";
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.totcost = cost.Replace("€", "").Replace(",", ".");

                if (BLL_BilliardWindowsApplication.languague == 0)
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer = (double.Parse(lblCost.Text.Replace("€", "").Replace(",", ".")) / noofplayers).ToString().Replace(",", ".");
                else if (BLL_BilliardWindowsApplication.languague == 1)
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer = (double.Parse(lblCost.Text.Replace("€", "")) / noofplayers).ToString().Replace(",", ".");

                BLL_BilliardWindowsApplication.gamecostdetailsStatic.totime = DateTime.Now.Hour / 10 + "" + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + "" + DateTime.Now.Minute % 10 + "";
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.noplayers = noofplayers.ToString();
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.gameover = false.ToString();
                insertgamecost(BLL_BilliardWindowsApplication.gamecostdetailsStatic);
            }


        }
        //--------------------------------------------GAMESCORELOAD---------------------------------------------------------
        private void frmGameScore_Load(object sender, EventArgs e)
        {
            try
            {
				//if (!BallTrackAPI.mbInitialized)
				//	BallTrackAPI.mbInitialized = BallTrackAPI.InitSDK(BallTrackAPI.PtrBallPosProc, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

				Rectangle rcTableInside = m_rcDrawRect;
				rcTableInside.Inflate(-m_nMarginX, -m_nMarginY);
				m_fScaleX = (float)rcTableInside.Width / (float)(BallTrackAPI.g_rcClip.bottom - BallTrackAPI.g_rcClip.top);
				m_fScaleY = (float)rcTableInside.Height / (float)(BallTrackAPI.g_rcClip.right - BallTrackAPI.g_rcClip.left);

				if (BallTrackAPI.m_nInputMethod == 1)
				{
                    BallTrackAPI.BTAPI_OpenVideoFile("k:\\samples\\seq.avi");
				}
				else if (!BallTrackAPI.BTAPI_IsCameraConnected())
                    BallTrackAPI.BTAPI_ConnectCamera(IntPtr.Zero);

                int hrs = DateTime.Now.Hour;
                int min = DateTime.Now.Minute;

                currentTime = new TimeSpan(hrs, min, 0);

                lblCost.Text = "0,00 €";

                tmstart.Tick += t_Tick;
                tmextratime.Tick += tmextratime_Tick;
                tmhandicap.Tick += tmhandicap_Tick;
                tmstart.Interval = tmextratime.Interval = tmhandicap.Interval = 1;

                nbprogressbar1.ButtonClick += nbprogressbar1_ButtonClick;
                nbprogressbar1.setBarFont(nbprogressbar1.Font);

                pbClub1.Image = BLL_BilliardWindowsApplication.c1;
                pbClub2.Image = BLL_BilliardWindowsApplication.c2;
                pbClub3.Image = BLL_BilliardWindowsApplication.c3;
                pbClub4.Image = BLL_BilliardWindowsApplication.c4;
                pbPlayer1.Image = BLL_BilliardWindowsApplication.p1;
                pbPlayer2.Image = BLL_BilliardWindowsApplication.p2;
                pbPlayer3.Image = BLL_BilliardWindowsApplication.p3;
                pbPlayer4.Image = BLL_BilliardWindowsApplication.p4;

                if (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
                    lblPlayer1.Text = "WHITE";
                else
                {
                    lblPlayer1.Text = BLL_BilliardWindowsApplication.player1.Name + " " + BLL_BilliardWindowsApplication.player1.FamilyName;
                    pbSwap.Location = new Point(712, 296);
                }

                if (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
                    lblPlayer2.Text = "YELLOW";
                else
                {
                    lblPlayer2.Text = BLL_BilliardWindowsApplication.player2.Name + " " + BLL_BilliardWindowsApplication.player2.FamilyName;
                    pbSwap.Location = new Point(712, 296);
                }

                if (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
                    lblPlayer3.Text = "WHITE";
                else
                {
                    lblPlayer3.Text = BLL_BilliardWindowsApplication.player3.Name + " " + BLL_BilliardWindowsApplication.player3.FamilyName;
                    pbSwap.Location = new Point(712, 296);
                }

                if (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
                    lblPlayer4.Text = "YELLOW";
                else
                {
                    lblPlayer4.Text = BLL_BilliardWindowsApplication.player4.Name + " " + BLL_BilliardWindowsApplication.player4.FamilyName;
                    pbSwap.Location = new Point(712, 296);
                }

                lblHeading.Text = "Italian Game " + BLL_BilliardWindowsApplication.quills + " Quills " + BLL_BilliardWindowsApplication.point + "P";

                lblWPoint.Text = lblYPoint.Text = "p" + BLL_BilliardWindowsApplication.point;
                lblWPoint.Tag = lblYPoint.Tag = BLL_BilliardWindowsApplication.point;

                lblWSet.Text = lblYSet.Text = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;

                timer1.Enabled = BLL_BilliardWindowsApplication.gametime;

                nbprogressbar1.setBar(Convert.ToInt32(BLL_BilliardWindowsApplication.timer1), 0, false);

                btnWM.Enabled = btnWD.Enabled = btnWE.Enabled = btnWC.Enabled = btnWR.Enabled = btnWU.Enabled = false;
                btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb1;

                btnYM.Enabled = btnYD.Enabled = btnYE.Enabled = btnYC.Enabled = btnYR.Enabled = btnYU.Enabled = false;
                btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma1;
            }
            catch (Exception ex) { MessageBox.Show("exno tgs6" + ex.ToString()); }
        }
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            try
            {
                tmstart.Interval = 1;
                tmstart.Enabled = false;
                btnStartGame.Location = new Point(260, -80);

                if (playturn1 == true && gamepause == false)
                {
                    colorwhite(false);
                    btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBv1;
                }
                else
                {
                    colorYellow(false);
                    btnYU.Image = BilliardWindowsApplication.Properties.Resources.fca___v1;
                }
                timer2.Enabled = true;
                if (!BallTrackAPI.m_bStartTracking)
                {
					BallTrackAPI.StartTracking();
                }

                //if (!BallTrackAPI.BTAPI_IsRecordVideoEnabled())
                //    BallTrackAPI.BTAPI_EnableRecordVideo(true);

                timer4.Enabled = true;
               
            }
            catch (Exception ex) { MessageBox.Show("exno tgs7" + ex.ToString()); }
        }
        void t_Tick(object sender, EventArgs e)
        {
            try
            {
                if (btnStartGame.Location != new Point(260, 140))
                    btnStartGame.Location = new Point(260, btnStartGame.Location.Y + 2);
                else
                {

                    if (tmstart.Interval == 1)
                    {
                        tmstart.Interval = 3000;
                    }
                    else
                    {
                        tmstart.Interval = 1;
                        tmstart.Enabled = false;
                        btnStartGame.Location = new Point(260, -80);

                        if (playturn1 == true && gamepause == false)
                        {
                            colorwhite(false);
                            btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBv1;
                        }
                        else
                        {
                            colorYellow(false);
                            btnYU.Image = BilliardWindowsApplication.Properties.Resources.fca___v1;
                        }
                        timer2.Enabled = true;
                        nbprogressbar1.Visible = true;
                        if (!BallTrackAPI.m_bStartTracking)
                        {
                            BallTrackAPI.StartTracking();
                        }
                        //if (!BallTrackAPI.BTAPI_IsRecordVideoEnabled())
                        //    BallTrackAPI.BTAPI_EnableRecordVideo(true);
                       
                        timer4.Enabled = true;
                        
                    }

                }

            }
            catch (Exception ex) { MessageBox.Show("exno tgs8" + ex.ToString()); }
        }
        void colorwhite(bool disable)
        {
            try
            {
                if (disable == true)
                {
                    colorw = true;
                    btnW0.Image = BilliardWindowsApplication.Properties.Resources._0g;
                    btnW1.Image = BilliardWindowsApplication.Properties.Resources._1g;
                    btnW2.Image = BilliardWindowsApplication.Properties.Resources._2g;
                    btnW3.Image = BilliardWindowsApplication.Properties.Resources._3g;
                    btnW4.Image = BilliardWindowsApplication.Properties.Resources._4g;
                    btnW5.Image = BilliardWindowsApplication.Properties.Resources._5g;
                    btnW6.Image = BilliardWindowsApplication.Properties.Resources._6g;
                    btnW7.Image = BilliardWindowsApplication.Properties.Resources._7g;
                    btnW8.Image = BilliardWindowsApplication.Properties.Resources._8g;
                    btnW9.Image = BilliardWindowsApplication.Properties.Resources._9g;

                    btnWC.Image = BilliardWindowsApplication.Properties.Resources.Cg;
                    btnWD.Image = BilliardWindowsApplication.Properties.Resources._bg;
                    btnWE.Image = BilliardWindowsApplication.Properties.Resources.Eg;
                    btnWG.Image = BilliardWindowsApplication.Properties.Resources.Gg;
                    btnWH.Image = BilliardWindowsApplication.Properties.Resources.Hg;
                    btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mg;
                    btnWP.Image = BilliardWindowsApplication.Properties.Resources.ROG;
                    btnWR.Image = BilliardWindowsApplication.Properties.Resources.fcag;
                    btnWR.Tag = "fcag";
                    btnWS.Image = BilliardWindowsApplication.Properties.Resources.Sg;
                    btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBg;
                    btnWU.Tag = "fcaBg";

                    lblWPoint.ForeColor = lblWscoreboard.ForeColor = lblWSet.ForeColor = lblWTurn.ForeColor = Color.FromArgb(153, 153, 153);
                }
                else
                {
                    colorw = false;
                    btnW0.Image = BilliardWindowsApplication.Properties.Resources._0b;
                    btnW1.Image = BilliardWindowsApplication.Properties.Resources._1b;
                    btnW2.Image = BilliardWindowsApplication.Properties.Resources._2b;
                    btnW3.Image = BilliardWindowsApplication.Properties.Resources._3b;
                    btnW4.Image = BilliardWindowsApplication.Properties.Resources._4b;
                    btnW5.Image = BilliardWindowsApplication.Properties.Resources._5b;
                    btnW6.Image = BilliardWindowsApplication.Properties.Resources._6b;
                    btnW7.Image = BilliardWindowsApplication.Properties.Resources._7b;
                    btnW8.Image = BilliardWindowsApplication.Properties.Resources._8b;
                    btnW9.Image = BilliardWindowsApplication.Properties.Resources._9b;

                    btnWC.Image = BilliardWindowsApplication.Properties.Resources.Cb1;
                    btnWD.Image = BilliardWindowsApplication.Properties.Resources._b1;
                    btnWE.Image = BilliardWindowsApplication.Properties.Resources.Eb;
                    btnWG.Image = BilliardWindowsApplication.Properties.Resources.Gb1;
                    btnWH.Image = BilliardWindowsApplication.Properties.Resources.Hb1;
                    btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb;
                    btnWP.Image = BilliardWindowsApplication.Properties.Resources.roB;
                    btnWR.Image = BilliardWindowsApplication.Properties.Resources.fcaB;
                    btnWR.Tag = "fcaB";
                    btnWS.Image = BilliardWindowsApplication.Properties.Resources.Sb;
                    btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBv;
                    btnWU.Tag = "fcaBv";

                    lblWPoint.ForeColor = lblWscoreboard.ForeColor = lblWSet.ForeColor = lblWTurn.ForeColor = SystemColors.ControlLightLight;
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs9" + ex.ToString()); }
        }
        void colorYellow(bool disable)
        {
            try
            {
                if (disable == true)
                {
                    colory = true;

                    btnY0.Image = BilliardWindowsApplication.Properties.Resources._0c;
                    btnY1.Image = BilliardWindowsApplication.Properties.Resources._1c;
                    btnY2.Image = BilliardWindowsApplication.Properties.Resources._2c;
                    btnY3.Image = BilliardWindowsApplication.Properties.Resources._3c;
                    btnY4.Image = BilliardWindowsApplication.Properties.Resources._4c;
                    btnY5.Image = BilliardWindowsApplication.Properties.Resources._5c;
                    btnY6.Image = BilliardWindowsApplication.Properties.Resources._6c;
                    btnY7.Image = BilliardWindowsApplication.Properties.Resources._7c;
                    btnY8.Image = BilliardWindowsApplication.Properties.Resources._8c;
                    btnY9.Image = BilliardWindowsApplication.Properties.Resources._9c;

                    btnYC.Image = BilliardWindowsApplication.Properties.Resources.Cc;
                    btnYD.Image = BilliardWindowsApplication.Properties.Resources._c;
                    btnYE.Image = BilliardWindowsApplication.Properties.Resources.Ec;
                    btnYG.Image = BilliardWindowsApplication.Properties.Resources.Gc;
                    btnYH.Image = BilliardWindowsApplication.Properties.Resources.Hc;
                    btnYM.Image = BilliardWindowsApplication.Properties.Resources.Mc;
                    btnYP.Image = BilliardWindowsApplication.Properties.Resources.Rc;
                    btnYR.Image = BilliardWindowsApplication.Properties.Resources.fcc;
                    btnYR.Tag = "fcc";
                    btnYS.Image = BilliardWindowsApplication.Properties.Resources.Sc;
                    btnYU.Image = BilliardWindowsApplication.Properties.Resources.fcc___v;
                    btnYU.Tag = "fcc___v";


                    lblYPoint.ForeColor = lblYscoreboard.ForeColor = lblYSet.ForeColor = lblYTurn.ForeColor = Color.FromArgb(146, 69, 6);
                }
                else
                {
                    colory = false;
                    btnY0.Image = BilliardWindowsApplication.Properties.Resources._0a;
                    btnY1.Image = BilliardWindowsApplication.Properties.Resources._1a;
                    btnY2.Image = BilliardWindowsApplication.Properties.Resources._2a;
                    btnY3.Image = BilliardWindowsApplication.Properties.Resources._3a;
                    btnY4.Image = BilliardWindowsApplication.Properties.Resources._4a;
                    btnY5.Image = BilliardWindowsApplication.Properties.Resources._5a;
                    btnY6.Image = BilliardWindowsApplication.Properties.Resources._6a;
                    btnY7.Image = BilliardWindowsApplication.Properties.Resources._7a;
                    btnY8.Image = BilliardWindowsApplication.Properties.Resources._8a;
                    btnY9.Image = BilliardWindowsApplication.Properties.Resources._9a;

                    btnYC.Image = BilliardWindowsApplication.Properties.Resources.Ca1;
                    btnYD.Image = BilliardWindowsApplication.Properties.Resources._a1;
                    btnYE.Image = BilliardWindowsApplication.Properties.Resources.Ea;
                    btnYG.Image = BilliardWindowsApplication.Properties.Resources.Ga1;
                    btnYH.Image = BilliardWindowsApplication.Properties.Resources.Ha1;
                    btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma;
                    btnYP.Image = BilliardWindowsApplication.Properties.Resources.Ra;
                    btnYR.Image = BilliardWindowsApplication.Properties.Resources.fca1;
                    btnYR.Tag = "fca1";
                    btnYS.Image = BilliardWindowsApplication.Properties.Resources.Sa;
                    btnYU.Image = BilliardWindowsApplication.Properties.Resources.fca___v;
                    btnYU.Tag = "fca___v";

                    lblYPoint.ForeColor = lblYscoreboard.ForeColor = lblYSet.ForeColor = lblYTurn.ForeColor = Color.FromArgb(228, 108, 10);
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs10" + ex.ToString()); }
        }
        private void pbWG_Click(object sender, EventArgs e)
        {
            try
            {
                pbSwap.Location = new Point(712, -296);
                pbSetinparity.Location = new Point(689, -262);
                startset = true;
                playclicksound();
                playturn1 = true;
                gamepause = false;

                pbBilliardIcon.Visible = false;
                nbprogressbar1.Visible = false;
                //  check this
                tmstart.Enabled = true;
                btnWM.Enabled = btnWE.Enabled = btnWC.Enabled = btnWR.Enabled = btnWU.Enabled = true;
                btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb;
                btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma;
                btnYM.Enabled = btnYE.Enabled = btnYC.Enabled = btnYR.Enabled = btnYU.Enabled = true;
                btnWG.Enabled = btnWH.Enabled = btnWD.Enabled = btnWC.Enabled = btnWU.Enabled = false;
                btnYG.Enabled = btnYH.Enabled = btnYD.Enabled = btnYC.Enabled = btnYU.Enabled = false;
                colorwhite(true);
                colorYellow(true);

                //BallTrackAPI.StartTracking();

            }
            catch (Exception ex) { MessageBox.Show("exno tgs11" + ex.ToString()); }
        }
        private void pbYG_Click(object sender, EventArgs e)
        {
            try
            {
                pbSwap.Location = new Point(712, -296);
                pbSetinparity.Location = new Point(689, -262);
                playclicksound();
                startset = false;
                playturn1 = false;
                gamepause = false;

                pbBilliardIcon.Visible = false;
                nbprogressbar1.Visible = false;


                tmstart.Enabled = true;
                btnWM.Enabled = btnWE.Enabled = btnWC.Enabled = btnWR.Enabled = btnWU.Enabled = true;
                btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb;
                btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma;
                btnYM.Enabled = btnYE.Enabled = btnYC.Enabled = btnYR.Enabled = btnYU.Enabled = true;
                btnWG.Enabled = btnWH.Enabled = btnWD.Enabled = btnWC.Enabled = btnWU.Enabled = false;
                btnYG.Enabled = btnYH.Enabled = btnYD.Enabled = btnYC.Enabled = btnYU.Enabled = false;
                colorwhite(true);
                colorYellow(true);

                //BallTrackAPI.StartTracking();
            }
            catch (Exception ex) { MessageBox.Show("exno tgs12" + ex.ToString()); }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (nbprogressbar1.GetBartotal() > nbprogressbar1.GetBarValue())
                {
                    nbprogressbar1.setBar(nbprogressbar1.GetBarValue() + 1);
                }
                else
                {
                    new SoundPlayer(BilliardWindowsApplication.Properties.Resources.beep_07).Play();
                    if (BLL_BilliardWindowsApplication.panalty)
                    {
                        if (playturn1)
                        {
                            lblYscoreboard.Text = (Convert.ToInt32(lblYscoreboard.Text) + 2) + "";
                            lblYPoint.Tag = Convert.ToInt32(lblYPoint.Tag) - 2;
                            lblYPoint.Text = "p" + Convert.ToInt32(lblYPoint.Tag);
                            memorydetails m = new memorydetails();
                            m.score = 2;
                            if (nbprogressbar1.GetBarTimer2())
                                m.extra = 2;
                            else m.extra = 1;
                            m.player1 = true;
                            m.point = lblWPoint.Text;
                            m.turn = "t" + (turntag + 1);
                            m.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;
                            m.isreplayrecord = false;

                            memory.Add(m);
                            ScoreDatauploadtemp(m);
                        }
                        else
                        {
                            lblWscoreboard.Text = (Convert.ToInt32(lblWscoreboard.Text) + 2) + "";
                            lblWPoint.Tag = Convert.ToInt32(lblWPoint.Tag) - 2;
                            lblWPoint.Text = "p" + Convert.ToInt32(lblWPoint.Tag);
                            memorydetails m = new memorydetails();
                            m.score = 2;
                            if (nbprogressbar1.GetBarTimer2())
                                m.extra = 2;
                            else m.extra = 1;
                            m.player1 = false;
                            m.point = lblYPoint.Text;
                            m.turn = "t"+(turntag + 1);
                            m.set = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;
                            m.isreplayrecord = false;
                            memory.Add(m);
                            ScoreDatauploadtemp(m);
                        }
                    }
                    if (nbprogressbar1.GetBarTimer2())
                    {
                        timer2.Enabled = false;
                        pbRafreeAction.Visible = true;
                        pbRafreeAction.Location = new Point(659, 289);
                    }
                    else
                    {
                        nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer2, 0, true);
                        if (!BallTrackAPI.m_bStartTracking)
                        {
                            BallTrackAPI.StartTracking();
                        } timer4.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs13" + ex.ToString()); }
        }
        //-----------------------------------------------END GAMESCOREBOARD-------------------------------------------------
        //------------------------------------------------PointCHECK--------------------------------------------------------
        private void pbW1_Click(object sender, EventArgs e)
        {
            //try
           // {
                if (playturn1 == true )
                {
                    playclicksound();
                  //  if (!timer3.Enabled)
                    //{
                        if (rightscore || UpScore)
                        {
                            addm2 = true;
                            lblYscoreboard.Tag = Convert.ToInt32(lblYscoreboard.Text);
                            lblYscoreboard.Text = Convert.ToInt32(((PictureBox)sender).Tag) + "";
                        }
                        else
                        {
                            addm1 = true;
                            lblWscoreboard.Tag = Convert.ToInt32(lblWscoreboard.Text);
                            lblWscoreboard.Text = Convert.ToInt32(((PictureBox)sender).Tag) + "";
                        }
                        timer2.Enabled = false;
                        timer3.Enabled = true;
                        btnWD.Enabled = btnWC.Enabled = true;
                        btnWD.Image = BilliardWindowsApplication.Properties.Resources._b;
                        btnWC.Image = BilliardWindowsApplication.Properties.Resources.Cb;
                        btnYD.Enabled = false;
                        btnYC.Enabled = true;
                   // }
                   // else
                    //{
                        if (rightscore)
                        {
                            try { Convert.ToInt32(lblYscoreboard.Text); }
                            catch { lblYscoreboard.Text = "0"; }
                            lblYscoreboard.Text = lblYscoreboard.Text + ((PictureBox)sender).Tag;
                        }
                        else
                        {
                            try { Convert.ToInt32(lblWscoreboard.Text); }
                            catch { lblWscoreboard.Text = "0"; }
                            lblWscoreboard.Text = lblWscoreboard.Text + ((PictureBox)sender).Tag;
                        }
                   // }
                }
           // }
          //  catch (Exception ex) { MessageBox.Show("exno tgs14" + ex.ToString()); }
        }
        private void pbY1_Click(object sender, EventArgs e)
        {
            try
            {
                if (playturn1 == false && colory == false && !m_bRepositionBall && ((!m_bRefereeEnteredPoint && CheckAbleToInputScore()) || rafreeaction || BallTrackAPI.m_nCurrentStep > 0 || timeoutPenalty || sendbackScore))//gamepause == false)
                {
                    playclicksound();
                    if (!timer3.Enabled)
                    {
                        if (rightscore || UpScore)
                        {
                            addm1 = true;
                            lblWscoreboard.Tag = Convert.ToInt32(lblWscoreboard.Text);
                            lblWscoreboard.Text = Convert.ToInt32(((PictureBox)sender).Tag) + "";
                        }
                        else
                        {
                            addm2 = true;
                            lblYscoreboard.Tag = Convert.ToInt32(lblYscoreboard.Text);
                            lblYscoreboard.Text = Convert.ToInt32(((PictureBox)sender).Tag) + "";
                        }
                        timer2.Enabled = false;
                        timer3.Enabled = true;
                        btnYD.Enabled = btnYC.Enabled = true;
                        btnYD.Image = BilliardWindowsApplication.Properties.Resources._a;
                        btnYC.Image = BilliardWindowsApplication.Properties.Resources.Ca;
                        btnWD.Enabled = false;
                        btnWC.Enabled = true;
                    }
                    else
                    {
                        if (rightscore)
                        {
                            try { Convert.ToInt32(lblWscoreboard.Text); }
                            catch { lblWscoreboard.Text = "0"; }
                            lblWscoreboard.Text = lblWscoreboard.Text + ((PictureBox)sender).Tag;
                        }
                        else
                        {
                            try { Convert.ToInt32(lblYscoreboard.Text); }
                            catch { lblYscoreboard.Text = "0"; }
                            lblYscoreboard.Text = lblYscoreboard.Text + ((PictureBox)sender).Tag;
                        }

                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs15" + ex.ToString()); }
		}
        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (playturn1 == true)
                {
                    if (timertick < 10)
                    {
                        timertick++;
                        if (UpScore)
                        {
                            if (btnWU.Visible)
                                btnWU.Visible = false;
                            else btnWU.Visible = true;
                        }
                        else
                        {
                            if (rightscore)
                            {
                                if (lblYscoreboard.Visible)
                                    lblYscoreboard.Visible = false;
                                else lblYscoreboard.Visible = true;
                            }
                            else
                            {
                                if (lblWscoreboard.Visible)
                                    lblWscoreboard.Visible = false;
                                else lblWscoreboard.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        rafreeaction = false;
                        timeoutPenalty = false;
                        m_bRefereeEnteredPoint = true;
                        if (sendbackScore)
                            turntag++;

                        sendbackScore = false;
                        if (UpScore)
                        {
                            btnWU.Visible = true;
                            int no = 0;
                            try { no = Convert.ToInt32(lblYscoreboard.Text); }
                            catch { }
                        	int turn = 0;
                            for (int i = 0; i < memory.Count; i++)
                            {
                                memorydetails md = memory.LastOrDefault();
	                            if(turn==0)
	                            turn=Convert.ToInt32(md.turn.Split('t')[1]);
                                if (md.extra == 0)
                                {
                                    if (md.player1 == false)
                                    {
	                                    turntag = turntag - 2;
                                        int nShowTag = Convert.ToInt32(lblYTurn.Tag) - 2;
                                        if (nShowTag < 0)
                                            nShowTag = 0;
	                                    lblYscoreboard.Text = (no - md.score).ToString();
	                                    lblYTurn.Text = "t" + nShowTag.ToString();
	                                    lblYTurn.Tag = (Convert.ToInt32(lblYTurn.Tag) - 2).ToString();
                                        lblYPoint.Text = (BLL_BilliardWindowsApplication.point - (no - md.score)).ToString();
                                        memory.RemoveAt(memory.Count - 1);
                                        ScoreDataDeletetemp();
                                    }
                                    break;
                                }
                                else
                                {
	                            
	                                if (turn > Convert.ToInt32(md.turn.Split('t')[1]))
	                                    break;

	                                lblWscoreboard.Tag = (Convert.ToInt32(lblWscoreboard.Tag) - md.score).ToString();
	                                lblWPoint.Text = (BLL_BilliardWindowsApplication.point - (Convert.ToInt32(lblWscoreboard.Tag))).ToString();
	                                memory.RemoveAt(memory.Count - 1);
                                    ScoreDataDeletetemp();
	                                
                                }

                                if (memory.Count > 0)
                                    turntag = Convert.ToInt32(memory.LastOrDefault().turn.Split('t')[1]);
                                else
                                    turntag = 1;
                                lblYTurn.Text = "t" + (turntag-1);
                                lblYTurn.Tag = lblYTurn.Text;
                            }

                            sendbackScore = true;
                        }
                  //  else
                    //{
                        //UpScore = false;
                        lblWscoreboard.Visible = true;
                        lblYscoreboard.Visible = true;
                        try { Convert.ToInt32(lblWscoreboard.Text); }
                        catch { lblWscoreboard.Text = "0"; }



                        memorydetails m1 = new memorydetails();
                        memorydetails m2 = new memorydetails();

                        try
                        {
                            m1.isreplayrecord = true;
							m1.recordplayer = 0;
							m1.replayset = 0;
                            m1.replayrecord = BallTrackAPI.BTAPI_GetWhiteHistoryCount() - 1;
                            m2.isreplayrecord = true;
							m2.recordplayer = 0;
							m2.replayset = 1;
							m2.replayrecord = BallTrackAPI.BTAPI_GetYellowHistoryCount() - 1;

                        }
                        catch { }

                        m1.player1 = true;
                        if (handicap)
                            m1.extra = 4;
                        else
                            m1.extra = 0;
                        m2.player1 = false;
                        m2.extra = 3;    //extra point

                        m1.score = Convert.ToInt32(lblWscoreboard.Text);
                        m2.score = Convert.ToInt32(lblYscoreboard.Text);
                        timer3.Enabled = false;
                        nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer1, 0, false);

                        if (!sendbackScore)
                            BallTrackAPI.StartTracking();
                        timer4.Enabled = true;
                        //if (!BallTrackAPI.BTAPI_IsRecordVideoEnabled())
                        //    BallTrackAPI.BTAPI_EnableRecordVideo(true);
                        // bdll.BTAPI_StartRecord("");
                        pictureBox3.Invalidate();
                        pictureBox3.Visible = false;
                        nbprogressbar1.Visible = true;


                        timertick = 0;
                        lblWscoreboard.Text = Convert.ToInt32(lblWscoreboard.Tag) + Convert.ToInt32(lblWscoreboard.Text) + "";
                        lblWscoreboard.Tag = 0;
                        lblYscoreboard.Text = Convert.ToInt32(lblYscoreboard.Tag) + Convert.ToInt32(lblYscoreboard.Text) + "";
                        lblYscoreboard.Tag = 0;

                        if (addm1)
                        {
                            if (!handicap)
                            {

                                turntag = turntag + 1;
                                lblWTurn.Tag = turntag;
                                lblWTurn.Text = "t" + lblWTurn.Tag;
                            }
                        }
                        else if (addm2)
                        {
                            if (!handicap)
                            {
                                turntag = turntag + 1;
                                lblWTurn.Tag = turntag;
                                lblWTurn.Text = "t" + lblWTurn.Tag;
                            }
                        }
                        lblWPoint.Tag = BLL_BilliardWindowsApplication.point - Convert.ToInt32(lblWscoreboard.Text) + "";
                        lblWPoint.Text = "p" + lblWPoint.Tag;
                        lblYPoint.Tag = BLL_BilliardWindowsApplication.point - Convert.ToInt32(lblYscoreboard.Text) + "";
                        lblYPoint.Text = "p" + lblYPoint.Tag;



                        btnWU.Enabled = btnYC.Enabled = btnYD.Enabled = btnWD.Enabled = btnWC.Enabled = false;

                        btnYU.Enabled = true;

                        if (!handicap)
                        {
                            colorwhite(true);
                            colorYellow(false);

                            timer2.Enabled = true;
                            playturn1 = false;
                            gamepause = false;
                        }
                        else
                        {
                            colorwhite(false);
                            colorYellow(false);
                            btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb1;
                            btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma1;

                            btnYE.Image = BilliardWindowsApplication.Properties.Resources.Ea1;
                            btnWE.Image = BilliardWindowsApplication.Properties.Resources.Eb1;

                            btnYR.Image = BilliardWindowsApplication.Properties.Resources.fca2;
                            btnWR.Image = BilliardWindowsApplication.Properties.Resources.fcaB1;

                            btnYU.Image = BilliardWindowsApplication.Properties.Resources.fca___v1;
                            btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBv1;

                            btnYH.Image = BilliardWindowsApplication.Properties.Resources.Ha;
                            btnWH.Image = BilliardWindowsApplication.Properties.Resources.Hb;

                            btnYG.Image = BilliardWindowsApplication.Properties.Resources.Ga;
                            btnWG.Image = BilliardWindowsApplication.Properties.Resources.Gb;

                            handicap = false; gamepause = true;

                        }


                        m1.point = lblWPoint.Text;
                        m1.turn = "t"+turntag.ToString();
                        m1.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;

                        m2.point = lblYPoint.Text;
                        m2.turn = "t"+turntag.ToString();
                        m2.set = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;

                        if (!UpScore)
                        {
                            if (memory.Count > 0)
                            {
                                if (memory[memory.Count() - 1].extra == 6)
                                    memory.RemoveAt(memory.Count() - 1);
                            }
                            if (BallTrackAPI.m_bWrongBallShotted)
                            {
                                addm1 = !addm1;
                                addm2 = !addm2;

                                m2.extra = 5;
                                m2.player1 = true;
                                memory.Add(m2);
                                ScoreDatauploadtemp(m2);
                            }
                            else if (rightscore)
                            {
                                m2.extra = 3;
                                m2.player1 = true;
                                m2.replayset = m1.replayset;
                                m2.replayrecord = m1.replayrecord;
                                m2.point = m1.point;
                                memory.Add(m2);
                                ScoreDatauploadtemp(m2);
                            }
                            else
                            {
                                if (addm1)
                                {
                                    memory.Add(m1);
                                    ScoreDatauploadtemp(m1);

                                } if (addm2)
                                {
                                    memory.Add(m2);
                                    ScoreDatauploadtemp(m2);
                                }
                            }
                            
                        }
                        rightscore = false;
                        UpScore = false; 
                        addm2 = addm1 = false;
						if (Convert.ToInt32(lblWPoint.Tag) < 1 && BallTrackAPI.m_nInputMethod == 0)                  //  if w win the set
                        {
                            gamepause = true;
                            timer2.Enabled = timer3.Enabled = false;
                            set1++;
                            memorylist.Add(memory);
                            lblWSet.Text = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;

                            if (BLL_BilliardWindowsApplication.set / 2 < set1)    // win the game
                            {
                                costmail();  // cost of game end
                                timer1.Enabled = false;
                                if (BLL_BilliardWindowsApplication.email)
                                {
                                    string body = htmlcode(starttime.ToString(), true, Convert.ToInt32(lblWscoreboard.Text), Convert.ToInt32(lblYscoreboard.Text), memorylist);
                                    List<string> repmail = new List<string>();
                                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
                                        repmail.Add(BLL_BilliardWindowsApplication.player1.PlayerId);
                                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
                                        repmail.Add(BLL_BilliardWindowsApplication.player2.PlayerId);
                                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
                                        repmail.Add(BLL_BilliardWindowsApplication.player3.PlayerId);
                                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
                                        repmail.Add(BLL_BilliardWindowsApplication.player4.PlayerId);

                                    SendHtmlFormattedEmail(repmail, "Game Summary", body);
                                }
								//if (BallTrackAPI.BTAPI_IsCameraConnected())
								//	BallTrackAPI.BTAPI_DisconnectCamera();
                                new frmwiner(lblPlayer1.Text, lblPlayer3.Text, set1, set2, lblWscoreboard.Text, lblYscoreboard.Text, true).ShowDialog();
                                wanttoclose = true;
                                timer1.Enabled = timer2.Enabled = timer3.Enabled = false;
                                this.Close();
                            }
                            else
                                if (BLL_BilliardWindowsApplication.set > (set1 + set2))        // if not win the game go to next set
                                {
                                    new frmwiner(lblPlayer1.Text, lblPlayer3.Text, set1, set2, lblWscoreboard.Text, lblYscoreboard.Text, false).ShowDialog();

                                    if ((BLL_BilliardWindowsApplication.set % 2) == 1 && BLL_BilliardWindowsApplication.set - (set1 + set2) == 1)
                                    {

                                        pbSetinparity.Location = new Point(689, 262);
                                        //MessageBox.Show("rafree must select the last set");
                                        gamepause = true;
                                        timer2.Enabled = timer3.Enabled = false;
                                        colorwhite(false);
                                        colorYellow(false);
                                        btnYG.Image = BilliardWindowsApplication.Properties.Resources.Ga;
                                        btnWG.Image = BilliardWindowsApplication.Properties.Resources.Gb;
                                        btnWG.Enabled = btnYG.Enabled = true;
                                    }
                                    else
                                        if (startset)
                                        {
                                            gamepause = false; timer2.Enabled = true;
                                            playturn1 = false;
                                            startset = false;
                                            colorwhite(true);
                                            colorYellow(false);
                                        }
                                        else
                                        {
                                            gamepause = false; timer2.Enabled = true;
                                            playturn1 = true;
                                            startset = true;
                                            colorwhite(false);
                                            colorYellow(true);
                                        }


                                    lblWPoint.Text = lblYPoint.Text = "p" + BLL_BilliardWindowsApplication.point;
                                    lblWPoint.Tag = lblYPoint.Tag = BLL_BilliardWindowsApplication.point;
                                    lblWTurn.Tag = lblYTurn.Tag =turntag = 0;
                                    lblWTurn.Text = lblYTurn.Text = "t" + lblWTurn.Tag;
                                    lblWscoreboard.Tag = lblYscoreboard.Tag = lblWscoreboard.Text = lblYscoreboard.Text = 0 + "";
                                    nbprogressbar1.setBar(Convert.ToInt32(BLL_BilliardWindowsApplication.timer1), 0, false);
                                    timertick = 0;
                                    extratime1 = extratime2 = true;

                                    //timer2.Enabled = true;

                                    memory = new List<memorydetails>();
                                }
                                else
                                {
                                    costmail();  // cost of game end

                                    timer1.Enabled = false;
                                    string body = htmlcode(starttime.ToString(), true, Convert.ToInt32(lblWscoreboard.Text), Convert.ToInt32(lblYscoreboard.Text), memorylist);
                                        
                                    if (BLL_BilliardWindowsApplication.email)
                                    {

                                        List<string> repmail = new List<string>();
                                        if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
                                            repmail.Add(BLL_BilliardWindowsApplication.player1.PlayerId);
                                        if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
                                            repmail.Add(BLL_BilliardWindowsApplication.player2.PlayerId);
                                        if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
                                            repmail.Add(BLL_BilliardWindowsApplication.player3.PlayerId);
                                        if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
                                            repmail.Add(BLL_BilliardWindowsApplication.player4.PlayerId);

                                        SendHtmlFormattedEmail(repmail, "Game Summary", body);

                                    }
                                    new frmwiner(lblPlayer1.Text, lblPlayer3.Text, set1, set2, lblWscoreboard.Text, lblYscoreboard.Text, true).ShowDialog();
                                    wanttoclose = true;
                                    timer1.Enabled = timer2.Enabled = timer3.Enabled = false;

									//if (BallTrackAPI.BTAPI_IsCameraConnected())
									//	BallTrackAPI.BTAPI_DisconnectCamera();
                                    this.Close();
                                }
                        }
                    }
                }
                else if (playturn1 == false)
                {
                    if (timertick < 10)
                    {
                        timertick++;
                        if (UpScore)
                        {
                            if (btnYU.Visible)
                                btnYU.Visible = false;
                            else btnYU.Visible = true;
                        }
                        else
                        {
                            if (rightscore)
                            {
                                if (lblWscoreboard.Visible)
                                    lblWscoreboard.Visible = false;
                                else lblWscoreboard.Visible = true;
                            }
                            else
                            {
                                if (lblYscoreboard.Visible)
                                    lblYscoreboard.Visible = false;
                                else lblYscoreboard.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        rafreeaction = false;
                        timeoutPenalty = false;
                        m_bRefereeEnteredPoint = true;
                        if (sendbackScore)
                            turntag++;

                        sendbackScore = false;
                        if (UpScore)
                        {
                            btnYU.Visible = true;
                            int no = 0;
                            try { no = Convert.ToInt32(lblWscoreboard.Text); }
                            catch { }
                            int turn = 0;
                            for (int i = 0; i < memory.Count; i++)
                            {
                                memorydetails md = memory.LastOrDefault();
                                if (turn == 0)
                                    turn = Convert.ToInt32(md.turn.Split('t')[1]);
                                if (md.extra == 0)
                                {
                                    if (md.player1 == true)
                                    {
                                        turntag = turntag - 2;
                                        lblWscoreboard.Text = (no - md.score).ToString();
                                        int nShowTag = Convert.ToInt32(lblWTurn.Tag) - 2;
                                        if (nShowTag < 0)
                                            nShowTag = 0;
                                        lblWTurn.Text = "t" + nShowTag.ToString();
                                        lblWTurn.Tag = (Convert.ToInt32(lblWTurn.Tag) - 2).ToString();
                                        lblWPoint.Text = (BLL_BilliardWindowsApplication.point - (no - md.score)).ToString();
                                        memory.RemoveAt(memory.Count - 1);
                                        ScoreDataDeletetemp();
                                    }
                                    break;
                                }
                                else
                                {
                                    if (turn > Convert.ToInt32(md.turn.Split('t')[1]))
                                        break;
                                    lblYscoreboard.Tag = (Convert.ToInt32(lblYscoreboard.Tag) - md.score).ToString();
                                    lblYPoint.Text = (BLL_BilliardWindowsApplication.point - (Convert.ToInt32(lblYscoreboard.Tag))).ToString();
                                    memory.RemoveAt(memory.Count - 1);
                                    ScoreDataDeletetemp();
  
                                }
                                if (memory.Count > 0)
                                    turntag = Convert.ToInt32(memory.LastOrDefault().turn.Split('t')[1]);
                                else
                                    turntag = 1;

                                lblWTurn.Text = "t" + (turntag - 1);
                                lblWTurn.Tag = lblWTurn.Text;
                            }
                            sendbackScore = true;
                        }

                        
                        lblWscoreboard.Visible = true;
                        lblYscoreboard.Visible = true;

                        try { Convert.ToInt32(lblYscoreboard.Text); }
                        catch { lblYscoreboard.Text = "0"; }

                        memorydetails m1 = new memorydetails();
                        memorydetails m2 = new memorydetails();

                        try
                        {
                            m1.isreplayrecord = true;
                            m1.recordplayer = 1;
                            m1.replayset = 0;
                            m1.replayrecord = BallTrackAPI.BTAPI_GetWhiteHistoryCount() - 1;
                            m2.isreplayrecord = true;
                            m2.recordplayer = 1;
                            m2.replayset = 1;
                            m2.replayrecord = BallTrackAPI.BTAPI_GetYellowHistoryCount() - 1;

                        }
                        catch { }

                        m2.player1 = false;
                        if (handicap)
                            m2.extra = 4;
                        else
                            m2.extra = 0;
                        m1.player1 = true;
                        m1.extra = 3;    //extra point
                        m2.score = Convert.ToInt32(lblYscoreboard.Text);
                        m1.score = Convert.ToInt32(lblWscoreboard.Text);

                        timer3.Enabled = false;
                        nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer1, 0, false);

                        //bdll.BTAPI_StartTracking();
                        if (!sendbackScore)
                            BallTrackAPI.StartTracking();
                        timer4.Enabled = true;
                        //if (!BallTrackAPI.BTAPI_IsRecordVideoEnabled())
                        //    BallTrackAPI.BTAPI_EnableRecordVideo(true);
                        // bdll.BTAPI_StartRecord("");
                        pictureBox3.Invalidate();
                        pictureBox3.Visible = false;
                        nbprogressbar1.Visible = true;

                        timertick = 0;

                        lblYscoreboard.Text = Convert.ToInt32(lblYscoreboard.Tag) + Convert.ToInt32(lblYscoreboard.Text) + "";
                        lblYscoreboard.Tag = 0;
                        lblWscoreboard.Text = Convert.ToInt32(lblWscoreboard.Tag) + Convert.ToInt32(lblWscoreboard.Text) + "";
                        lblWscoreboard.Tag = 0;
                        if (addm2)
                        {
                            if (!handicap)
                            {
                                turntag = turntag + 1;
                                lblYTurn.Tag = turntag;
                                lblYTurn.Text = "t" + lblYTurn.Tag;
                            }
                        }
                        else if (addm1)
                        {
                            if (!handicap)
                            {
                                turntag = turntag + 1;
                                lblYTurn.Tag = turntag;
                                lblYTurn.Text = "t" + lblYTurn.Tag;
                            }
                        }

                        lblYPoint.Tag = BLL_BilliardWindowsApplication.point - Convert.ToInt32(lblYscoreboard.Text) + "";
                        lblYPoint.Text = "p" + lblYPoint.Tag;
                        lblWPoint.Tag = BLL_BilliardWindowsApplication.point - Convert.ToInt32(lblWscoreboard.Text) + "";
                        lblWPoint.Text = "p" + lblWPoint.Tag;

                        btnYU.Enabled = btnWC.Enabled = btnWD.Enabled = btnYD.Enabled = btnYC.Enabled = false;
                        btnWU.Enabled = true;

                        if (!handicap)
                        {
                            colorwhite(false);
                            colorYellow(true);

                            timer2.Enabled = true;
                            playturn1 = true;
                            gamepause = false;
                        }
                        else
                        {
                            colorwhite(false);
                            colorYellow(false);
                            btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb1;
                            btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma1;

                            btnYE.Image = BilliardWindowsApplication.Properties.Resources.Ea1;
                            btnWE.Image = BilliardWindowsApplication.Properties.Resources.Eb1;

                            btnYR.Image = BilliardWindowsApplication.Properties.Resources.fca2;
                            btnWR.Image = BilliardWindowsApplication.Properties.Resources.fcaB1;

                            btnYU.Image = BilliardWindowsApplication.Properties.Resources.fca___v1;
                            btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBv1;

                            btnYH.Image = BilliardWindowsApplication.Properties.Resources.Ha;
                            btnWH.Image = BilliardWindowsApplication.Properties.Resources.Hb;

                            btnYG.Image = BilliardWindowsApplication.Properties.Resources.Ga;
                            btnWG.Image = BilliardWindowsApplication.Properties.Resources.Gb;
                            handicap = false; gamepause = true;
                        }

                        m2.point = lblYPoint.Text;
                        m2.turn = "t" + turntag.ToString();
                        m2.set = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;

                        m1.point = lblWPoint.Text;
                        m1.turn = "t" + turntag.ToString();
                        m1.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;

                        if (!UpScore)
                        {
                            if (memory.Count > 0)
                            {
                                if (memory[memory.Count() - 1].extra == 6)
                                    memory.RemoveAt(memory.Count() - 1);
                            }

                            if (BallTrackAPI.m_bWrongBallShotted)
                            {
                                addm1 = !addm1;
                                addm2 = !addm2;

                                m1.extra = 5;
                                m1.player1 = false;
                                memory.Add(m1);
                                ScoreDatauploadtemp(m1);
                            }
                            else if (rightscore)
                            {
                                m1.extra = 3;
                                m1.player1 = false;
                                m1.replayset = m2.replayset;
                                m1.replayrecord = m2.replayrecord;
                                m1.point = m2.point;
                                memory.Add(m1);
                                ScoreDatauploadtemp(m1);
                            }
                            else
                            {
                                if (addm1)
                                {
                                    memory.Add(m1);
                                    ScoreDatauploadtemp(m1);
                                }
                                if (addm2)
                                {
                                    memory.Add(m2);
                                    ScoreDatauploadtemp(m2);
                                }
                            }

                        }

                        rightscore = false;
                        UpScore = false;
                        addm2 = addm1 = false;



                        if (Convert.ToInt32(lblYPoint.Tag) < 1 && BallTrackAPI.m_nInputMethod == 0)
                        {
                            gamepause = true;
                            timer2.Enabled = timer3.Enabled = false;

                            set2++;
                            memorylist.Add(memory);
                            lblYSet.Text = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;

                            if (BLL_BilliardWindowsApplication.set / 2 < set2)
                            {
                                costmail();  // cost of game end
                                timer1.Enabled = false;
                                string body = htmlcode(starttime.ToString(), false, Convert.ToInt32(lblWscoreboard.Text), Convert.ToInt32(lblYscoreboard.Text), memorylist);
                                    
                                if (BLL_BilliardWindowsApplication.email)
                                {
                                    List<string> repmail = new List<string>();
                                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
                                        repmail.Add(BLL_BilliardWindowsApplication.player1.PlayerId);
                                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
                                        repmail.Add(BLL_BilliardWindowsApplication.player2.PlayerId);
                                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
                                        repmail.Add(BLL_BilliardWindowsApplication.player3.PlayerId);
                                    if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
                                        repmail.Add(BLL_BilliardWindowsApplication.player4.PlayerId);
                                    SendHtmlFormattedEmail(repmail, "Game Summary", body);

                                }
                                new frmwiner(lblPlayer2.Text, lblPlayer4.Text, set1, set2, lblWscoreboard.Text, lblYscoreboard.Text, true).ShowDialog();
                                wanttoclose = true;
                                timer1.Enabled = timer2.Enabled = timer3.Enabled = false; 
                                
                                //if (BallTrackAPI.BTAPI_IsCameraConnected())
                                //	BallTrackAPI.BTAPI_DisconnectCamera(); 
                                this.Close();
                            }
                            else
                            {
                                if (BLL_BilliardWindowsApplication.set > (set2 + set1))
                                {
                                    new frmwiner(lblPlayer2.Text, lblPlayer4.Text, set1, set2, lblWscoreboard.Text, lblYscoreboard.Text, false).ShowDialog();
                                    if ((BLL_BilliardWindowsApplication.set % 2) == 1 && BLL_BilliardWindowsApplication.set - (set1 + set2) == 1)
                                    {
                                        pbSetinparity.Location = new Point(689, 262);
                                        // MessageBox.Show("rafree must select the last set");
                                        gamepause = true;
                                        timer2.Enabled = timer3.Enabled = false;
                                        colorwhite(false);
                                        colorYellow(false);
                                        btnYG.Image = BilliardWindowsApplication.Properties.Resources.Ga;
                                        btnWG.Image = BilliardWindowsApplication.Properties.Resources.Gb;
                                        btnWG.Enabled = btnYG.Enabled = true;
                                    }
                                    else
                                        if (startset)
                                        {
                                            gamepause = false; timer2.Enabled = true;
                                            playturn1 = false;
                                            startset = false;
                                            colorwhite(true);
                                            colorYellow(false);
                                        }
                                        else
                                        {
                                            gamepause = false; timer2.Enabled = true;
                                            playturn1 = true;
                                            startset = true;
                                            colorwhite(false);
                                            colorYellow(true);
                                        }

                                    lblWPoint.Text = lblYPoint.Text = "p" + BLL_BilliardWindowsApplication.point;
                                    lblWPoint.Tag = lblYPoint.Tag = BLL_BilliardWindowsApplication.point;
                                    lblWTurn.Tag = lblYTurn.Tag = turntag = 0;
                                    lblWTurn.Text = lblYTurn.Text = "t" + lblWTurn.Tag;
                                    lblWscoreboard.Tag = lblYscoreboard.Tag = lblWscoreboard.Text = lblYscoreboard.Text = 0 + "";
                                    nbprogressbar1.setBar(Convert.ToInt32(BLL_BilliardWindowsApplication.timer1), 0, false);
                                    timertick = 0;
                                    extratime1 = extratime2 = true;

                                    memory = new List<memorydetails>();
                                }
                                else
                                {
                                    costmail();  // cost of game end
                                    timer1.Enabled = false;
                                    string body = htmlcode(starttime.ToString(), false, Convert.ToInt32(lblWscoreboard.Text), Convert.ToInt32(lblYscoreboard.Text), memorylist);
                                        
                                    if (BLL_BilliardWindowsApplication.email)
                                    {
                                        List<string> repmail = new List<string>();
                                        if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
                                            repmail.Add(BLL_BilliardWindowsApplication.player1.PlayerId);
                                        if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
                                            repmail.Add(BLL_BilliardWindowsApplication.player2.PlayerId);
                                        if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
                                            repmail.Add(BLL_BilliardWindowsApplication.player3.PlayerId);
                                        if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
                                            repmail.Add(BLL_BilliardWindowsApplication.player4.PlayerId);
                                        SendHtmlFormattedEmail(repmail, "Game Summary", body);

                                    }
                                    new frmwiner(lblPlayer2.Text, lblPlayer4.Text, set1, set2, lblWscoreboard.Text, lblYscoreboard.Text, true).ShowDialog();
                                    wanttoclose = true;
                                    timer1.Enabled = timer2.Enabled = timer3.Enabled = false; 
                                    
                                    //if (BallTrackAPI.BTAPI_IsCameraConnected())
                                    //	BallTrackAPI.BTAPI_DisconnectCamera();
                                    this.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs16" + ex.ToString()); }
        }
        //-----------------------------------------------END POINT CHECK----------------------------------------------------
        //----------------------------------------------EXTRA BUTTON  WORK--------------------------------------------------
        private void pbWS_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colorw)
                {
                    playclicksound();
                    starttime = new TimeSpan(0, 0, 0);
                    lblWPoint.Text = lblYPoint.Text = "p" + BLL_BilliardWindowsApplication.point;
                    lblWPoint.Tag = lblYPoint.Tag = BLL_BilliardWindowsApplication.point;
                    lblWSet.Text = lblYSet.Text = "s0/" + BLL_BilliardWindowsApplication.set;
                    lblWTurn.Tag = lblYTurn.Tag = turntag = 0;
                    lblWTurn.Text = lblYTurn.Text = "t" + lblWTurn.Tag;
                    lblWscoreboard.Tag = lblYscoreboard.Tag = lblWscoreboard.Text = lblYscoreboard.Text = 0 + "";

                    timer1.Enabled = BLL_BilliardWindowsApplication.gametime;
                    nbprogressbar1.setBar(Convert.ToInt32(BLL_BilliardWindowsApplication.timer1), 0, false);
                    timer2.Enabled = timer3.Enabled = false;
                    playturn1 = true;
                    gamepause = true;
                    timertick = 0;
                    extratime1 = extratime2 = true;
                    set1 = 0;
                    set2 = 0;
                    colorwhite(false);
                    colorYellow(false);
                    btnWM.Enabled = btnWD.Enabled = btnWE.Enabled = btnWC.Enabled = btnWR.Enabled = btnWU.Enabled = false;
                    btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb1;
                    btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma1;

                    btnYE.Image = BilliardWindowsApplication.Properties.Resources.Ea1;
                    btnWE.Image = BilliardWindowsApplication.Properties.Resources.Eb1;

                    btnYR.Image = BilliardWindowsApplication.Properties.Resources.fca2;
                    btnWR.Image = BilliardWindowsApplication.Properties.Resources.fcaB1;

                    btnYU.Image = BilliardWindowsApplication.Properties.Resources.fca___v1;
                    btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBv1;

                    btnYH.Image = BilliardWindowsApplication.Properties.Resources.Ha;
                    btnWH.Image = BilliardWindowsApplication.Properties.Resources.Hb;

                    btnYG.Image = BilliardWindowsApplication.Properties.Resources.Ga;
                    btnWG.Image = BilliardWindowsApplication.Properties.Resources.Gb;

                    btnYM.Enabled = btnYD.Enabled = btnYE.Enabled = btnYC.Enabled = btnYR.Enabled = btnYU.Enabled = false;
                    btnWG.Enabled = btnWH.Enabled = true;
                    pbBilliardIcon.Visible = true;
                    //nbprogressbar1.Visible = false;
                    nbprogressbar1.setBar(Convert.ToInt32(BLL_BilliardWindowsApplication.timer1), 0, false);
                    btnYG.Enabled = btnYH.Enabled = true;
                    if (memory != null)
                        memory.Clear();
                    timer4.Enabled = false;
                    pictureBox3.Visible = false;
                   // bdll.BTAPI_StopRecord();
                    if (BallTrackAPI.m_bStartTracking)
                    {
                        BallTrackAPI.BTAPI_StopTracking();
                        BallTrackAPI.m_bStartTracking = false;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs17" + ex.ToString()); }
        }
        private void pbYS_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colory)
                {
                    playclicksound();
                    starttime = new TimeSpan(0, 0, 0);
                    lblWPoint.Text = lblYPoint.Text = "p" + BLL_BilliardWindowsApplication.point;
                    lblWPoint.Tag = lblYPoint.Tag = BLL_BilliardWindowsApplication.point;
                    lblWSet.Text = lblYSet.Text = "s0/" + BLL_BilliardWindowsApplication.set;
                    lblWTurn.Tag = lblYTurn.Tag = turntag = 0;
                    lblWTurn.Text = lblYTurn.Text = "t" + lblWTurn.Tag;
                    lblWscoreboard.Tag = lblYscoreboard.Tag = lblWscoreboard.Text = lblYscoreboard.Text = 0 + "";

                    timer1.Enabled = BLL_BilliardWindowsApplication.gametime;
                    nbprogressbar1.setBar(Convert.ToInt32(BLL_BilliardWindowsApplication.timer1), 0, false);
                    timer2.Enabled = timer3.Enabled = false;
                    playturn1 = true;
                    gamepause = true;
                    timertick = 0;
                    extratime1 = extratime2 = true;
                    set1 = 0;
                    set2 = 0;
                    colorwhite(false);
                    colorYellow(false);

                    btnWM.Enabled = btnWD.Enabled = btnWE.Enabled = btnWC.Enabled = btnWR.Enabled = btnWU.Enabled = false;
                    btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb1;
                    btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma1;
                    btnYE.Image = BilliardWindowsApplication.Properties.Resources.Ea1;
                    btnWE.Image = BilliardWindowsApplication.Properties.Resources.Eb1;
                    btnYR.Image = BilliardWindowsApplication.Properties.Resources.fca2;
                    btnWR.Image = BilliardWindowsApplication.Properties.Resources.fcaB1;
                    btnYU.Image = BilliardWindowsApplication.Properties.Resources.fca___v1;
                    btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBv1;
                    btnYH.Image = BilliardWindowsApplication.Properties.Resources.Ha;
                    btnWH.Image = BilliardWindowsApplication.Properties.Resources.Hb;
                    btnYG.Image = BilliardWindowsApplication.Properties.Resources.Ga;
                    btnWG.Image = BilliardWindowsApplication.Properties.Resources.Gb;

                    btnYM.Enabled = btnYD.Enabled = btnYE.Enabled = btnYC.Enabled = btnYR.Enabled = btnYU.Enabled = false;
                    btnWG.Enabled = btnWH.Enabled = true;
                    pbBilliardIcon.Visible = true;
                    //nbprogressbar1.Visible = false;
                    nbprogressbar1.setBar(Convert.ToInt32(BLL_BilliardWindowsApplication.timer1), 0, false);
                    btnYG.Enabled = btnYH.Enabled = true;


                    if (memory != null)
                        memory.Clear();
                    timer4.Enabled = false;
                    pictureBox3.Visible = false;

					BallTrackAPI.m_playHistory[0] = new List<List<CRASH_ELEMENT>>();
					BallTrackAPI.m_playHistory[1] = new List<List<CRASH_ELEMENT>>();
					BallTrackAPI.m_playHistory[2] = new List<List<CRASH_ELEMENT>>();

                    //if (BallTrackAPI.BTAPI_IsRecordVideoEnabled())
                    //    BallTrackAPI.BTAPI_StopRecord();
                    if (BallTrackAPI.m_bStartTracking)
                    {
                        BallTrackAPI.BTAPI_StopTracking();
                        BallTrackAPI.m_bStartTracking = false;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs18" + ex.ToString()); }
        }
        private void pbWE_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colorw && timer2.Enabled)
                {
                    playclicksound();

                    if (playturn1 == true && extratime1 == true)
                    {
                        btnExtraTimeN.Visible = true;
                        btnExtraTimeY.Visible = true;
                        btnExtraTimeOk.Visible = false;
                        label2.Text = ">Press - YES or NO - in this label  to Confirm or Reset";
                        colorwhite(true);
                    }
                    else
                    {
                        if (playturn1 == false && extratime2 == true)
                        {
                            btnExtraTimeN.Visible = true;
                            btnExtraTimeY.Visible = true;
                            btnExtraTimeOk.Visible = false;
                            label2.Text = ">Press - YES or NO - in this label  to Confirm or Reset";
                            colorYellow(true);
                        }
                        else
                        {

                            colorwhite(false);
                            colorYellow(false);
                            btnExtraTimeN.Visible = false;
                            btnExtraTimeY.Visible = false;
                            btnExtraTimeOk.Visible = true;
                            label2.Text = ">Sorry Extra Time is not Permitted";

                        }
                    }
                    tmextratime.Enabled = true;
                    //pnlExtraTime.Location = new Point(253, 158);
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs19" + ex.ToString()); }
        }
        private void pbYE_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colory && timer2.Enabled)
                {
                    playclicksound();

                    if (playturn1 == true && extratime1 == true)
                    {
                        btnExtraTimeN.Visible = true;
                        btnExtraTimeY.Visible = true;
                        btnExtraTimeOk.Visible = false;
                        label2.Text = ">Press - YES or NO - in this label  to Confirm or Reset";
                        colorwhite(true);
                    }
                    else
                    {
                        if (playturn1 == false && extratime2 == true)
                        {
                            btnExtraTimeN.Visible = true;
                            btnExtraTimeY.Visible = true;
                            btnExtraTimeOk.Visible = false;
                            label2.Text = ">Press - YES or NO - in this label  to Confirm or Reset";
                            colorYellow(true);
                        }
                        else
                        {

                            colorwhite(false);
                            colorYellow(false);
                            btnExtraTimeN.Visible = false;
                            btnExtraTimeY.Visible = false;
                            btnExtraTimeOk.Visible = true;
                            label2.Text = ">Sorry Extra Time is not Permitted";

                        }
                    }
                    tmextratime.Enabled = true;
                    //pnlExtraTime.Location = new Point(253, 158);
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs20" + ex.ToString()); }
        }
        private void pbWC_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colorw)
                {
                    timer3.Enabled = false;
                    playclicksound();
                    if (playturn1)
                        lblWscoreboard.Text = lblWscoreboard.Tag.ToString();
                    else lblYscoreboard.Text = lblYscoreboard.Tag.ToString();
                    lblWscoreboard.Visible = lblYscoreboard.Visible = true;
                    timertick = 0;
                    // timer3.Enabled = true;
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs21" + ex.ToString()); }
        }
        private void pbYC_Click(object sender, EventArgs e)
        {
            try
            {
                timer3.Enabled = false;
                playclicksound();
                if (playturn1)
                    lblWscoreboard.Text = lblWscoreboard.Tag.ToString();
                else lblYscoreboard.Text = lblYscoreboard.Tag.ToString();
                lblWscoreboard.Visible = lblYscoreboard.Visible = true;
                timertick = 0;
                //timer3.Enabled = true;
            }
            catch (Exception ex) { MessageBox.Show("exno tgs22" + ex.ToString()); }
        }
        private void pbWD_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colorw)
                {
                    playclicksound();
                    try
                    {
                        if (playturn1) lblWscoreboard.Text = Convert.ToInt32(lblWscoreboard.Text) * -1 + "";
                        else lblYscoreboard.Text = Convert.ToInt32(lblYscoreboard.Text) * -1 + "";
                    }
                    catch { }
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs23" + ex.ToString()); }
        }
        private void pbYD_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colory)
                {
                    playclicksound();
                    try
                    {
                        if (playturn1) lblWscoreboard.Text = Convert.ToInt32(lblWscoreboard.Text) * -1 + "";
                        else lblYscoreboard.Text = Convert.ToInt32(lblYscoreboard.Text) * -1 + "";
                    }
                    catch { }
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs24" + ex.ToString()); }
        }
        private void pbWM_Click(object sender, EventArgs e)
        {
            try { 
            if (!colorw)
            {
                colorwhite(true);
                colorYellow(true);
                timer4.Enabled = false;
                try
                {
                    playclicksound();
                    timer2.Enabled = false;
                    if (nbprogressbar1.Getbarcolor() == Color.LawnGreen)
                        nbprogressbar1.setbarcolor(Color.FromArgb(86, 176, 0));
                    else nbprogressbar1.setbarcolor(Color.DarkRed);
                }
                catch { }
				BallTrackAPI.BTAPI_StopTracking();
                frmMemoryDetails frmmemo = new frmMemoryDetails(memory);
                frmmemo.FormClosed += formCloseEvent;
                frmmemo.ShowDialog();

            }}catch (Exception ex) { MessageBox.Show("exno tgs25" + ex.ToString()); }
        }
        private void pbYM_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colory)
                {
                    colorwhite(true);
                    colorYellow(true);
                    timer4.Enabled = false;

                    playclicksound();
                    timer2.Enabled = false;
                    if (nbprogressbar1.Getbarcolor() == Color.LawnGreen)
                        nbprogressbar1.setbarcolor(Color.FromArgb(86, 176, 0));
                    else nbprogressbar1.setbarcolor(Color.DarkRed);

					BallTrackAPI.BTAPI_StopTracking();
                    frmMemoryDetails frmmemo = new frmMemoryDetails(memory);
                    frmmemo.FormClosed += formCloseEvent;
                    frmmemo.ShowDialog();

                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs26" + ex.ToString()); }
        }
        private void formCloseEvent(object sender, FormClosedEventArgs e)
        {
            try
            {
                // memory details 
                if (playturn1 == true && gamepause == false)
                {
                    colorwhite(false);
                    if (nbprogressbar1.Getbarcolor() == Color.FromArgb(86, 176, 0))
                        nbprogressbar1.setbarcolor(Color.LawnGreen);
                    else nbprogressbar1.setbarcolor(Color.Red);
                }
                else
                {
                    if (nbprogressbar1.Getbarcolor() == Color.FromArgb(86, 176, 0))
                        nbprogressbar1.setbarcolor(Color.LawnGreen);
                    else nbprogressbar1.setbarcolor(Color.Red);

                    colorYellow(false);
                }

                if (!m_bRepositionBall)
                {
                    if (nbprogressbar1.GetBartotal() > nbprogressbar1.GetBarValue())
                        timer2.Enabled = true;

                    if (!rafreeaction && m_bRefereeEnteredPoint)
                    {
                        BallTrackAPI.StartTracking();
                        timer4.Enabled = true;
                    }
               }
               BallTrackAPI.m_bReplay = false;
            }
            catch (Exception ex) { MessageBox.Show("exno tgs27" + ex.ToString()); }
        }
        private void pbWH_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colorw)
                {
                    pbSwap.Location = new Point(712, -296);
                    if (Convert.ToInt32(((PictureBox)sender).Tag) == 1)
                        playturn1 = true;
                    else playturn1 = false;
                    tmhandicap.Enabled = true;
                    // pnlHandicap.Location = new Point(253, 124);
                    playclicksound();
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs28" + ex.ToString()); }
        }
        private void pbYH_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colory)
                {
                    pbSwap.Location = new Point(712, -296);
                    if (Convert.ToInt32(((PictureBox)sender).Tag) == 1)
                        playturn1 = true;
                    else playturn1 = false;
                    tmhandicap.Enabled = true;
                    // pnlHandicap.Location = new Point(253, 124);
                    playclicksound();
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs29" + ex.ToString()); }
        }
        private void pbWR_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnWR.Tag.ToString() == "fcaB")
                {

                    colorwhite(false);
                    btnWR.Tag = "fcar";
                    btnWR.Image = BilliardWindowsApplication.Properties.Resources.fcar;
                    rightscore = true;
                    timertick = 0;
                    timer3.Enabled = false;
                    lblWscoreboard.Visible = lblYscoreboard.Visible = true;
                    playclicksound();

                }
                else if (btnWR.Tag.ToString() == "fcar")
                {
                    btnWR.Tag = "fcaB";
                    btnWR.Image = BilliardWindowsApplication.Properties.Resources.fcaB;
                    rightscore = false;
                    playclicksound();
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs30" + ex.ToString()); }
        }
        private void pbYR_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnYR.Tag.ToString() == "fca1")
                {
                    colorYellow(false);
                    btnWR.Tag = "fcar1";
                    btnYR.Image = BilliardWindowsApplication.Properties.Resources.fcar1;
                    rightscore = true;
                    timertick = 0;
                    timer3.Enabled = false;
                    lblWscoreboard.Visible = lblYscoreboard.Visible = true;
                    playclicksound();
                }
                else if (btnYR.Tag.ToString() == "fcar1")
                {
                    btnYR.Tag = "fca1";
                    btnYR.Image = BilliardWindowsApplication.Properties.Resources.fca1;
                    rightscore = false;
                    playclicksound();
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs31" + ex.ToString()); }
        }
        private void pbWU_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnWU.Tag.ToString() == "fcaBv")
                {
                    btnWU.Tag = "fcaBr";
                    btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBr;
                    UpScore = true;
                    //timertick = 0;
                    timer2.Enabled = false;
                    timer3.Enabled = true;
                    //lblWscoreboard.Visible = lblYscoreboard.Visible = true;
                    playclicksound();
                    BallTrackAPI.StopTracking();


                }
                else if (btnWU.Tag.ToString() == "fcaBr")
                {
                    btnWU.Tag = "fcaBv";
                    btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBv;
                    //rightscore = false;
                    timer2.Enabled = true;
                    UpScore = false;
                    playclicksound();
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs32" + ex.ToString()); }
        }
        private void btnYU_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnYU.Tag.ToString() == "fca___v")
                {
                    btnYU.Tag = "fcaBr";
                    btnYU.Image = BilliardWindowsApplication.Properties.Resources.fcaBr;
                    UpScore = true;
                    //rightscore = true;
                    //timertick = 0;
                    timer2.Enabled = false;
                    timer3.Enabled = true;
                    //lblWscoreboard.Visible = lblYscoreboard.Visible = true;
                    playclicksound();
                    BallTrackAPI.StopTracking();
                }
                else if (btnYU.Tag.ToString() == "fcaBr")
                {
                    btnWU.Tag = "fca___v";
                    btnYU.Image = BilliardWindowsApplication.Properties.Resources.fca___v;
                    //rightscore = false;
                    timer2.Enabled = true;
                    UpScore = false;
                    playclicksound();
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs33" + ex.ToString()); }
        }
        private void btnWP_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colorw)
                {
                    try
                    {
                        //bdll.BTAPI_StopRecord();
                        memory = new List<memorydetails>();
                        tmstart.Enabled = tmhandicap.Enabled = tmextratime.Enabled = timer1.Enabled = timer2.Enabled = timer3.Enabled = timer4.Enabled = false;
                        if (BallTrackAPI.m_bStartTracking)
                        {
                            BallTrackAPI.BTAPI_StopTracking();
                            BallTrackAPI.m_bStartTracking = false;
                        }
						//if (BallTrackAPI.BTAPI_IsCameraConnected())
						//	BallTrackAPI.BTAPI_DisconnectCamera();
                    }
                    catch (Exception ex){MessageBox.Show("exmo tgs200"+ex.ToString()); }
                    costmail();
                    wanttoclose = true;
                    timer1.Enabled = timer2.Enabled = timer3.Enabled = false;
                    playclicksound();
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs34" + ex.ToString()); }
        }
        private void btnYP_Click(object sender, EventArgs e)
        {
            try
            {
                if (!colory)
                {
                    try
                    {
                       // bdll.BTAPI_StopRecord();
                        memory = new List<memorydetails>();
                        tmstart.Enabled = tmhandicap.Enabled = tmextratime.Enabled = timer1.Enabled = timer2.Enabled = timer3.Enabled = timer4.Enabled = false;
                        if (BallTrackAPI.m_bStartTracking)
                        {
                            BallTrackAPI.BTAPI_StopTracking();
                            BallTrackAPI.m_bStartTracking = false;
                        }
                            
                    }
                    catch (Exception ex) { MessageBox.Show("exmo tgs201" + ex.ToString()); }
                    costmail();
                    wanttoclose = true;
                    timer1.Enabled = timer2.Enabled = timer3.Enabled = false;
                
                    playclicksound();
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs35" + ex.ToString()); }
        }
        private void pictureboxRound1_Click(object sender, EventArgs e)
        {
            try
            {
                playclicksound();
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("exno tgs36" + ex.ToString()); }
        }
        private void pbRafreeAction_Click(object sender, EventArgs e)
        {
            try
            {
                playclicksound();
                rafreeaction = true;
                timeoutPenalty = true;
                pbRafreeAction.Location = new Point(pbRafreeAction.Location.X, -1 * pbRafreeAction.Location.Y);
            }
            catch (Exception ex) { MessageBox.Show("exno tgs37" + ex.ToString()); }
        }
        void nbprogressbar1_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (rafreeaction)
                {
                    playclicksound();
                    if (playturn1 == true)
                    {

                        colorwhite(true);
                        btnWR.Tag = "fcaB";
                        btnWR.Image = BilliardWindowsApplication.Properties.Resources.fcaB;
                    
                        ////timer3.Enabled = false;
                        ////nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer1, 0, false);
                        ////nbprogressbar1.Visible = true;
                        ////timertick = 0;
                        ////btnWU.Enabled = btnYC.Enabled = btnYD.Enabled = btnWD.Enabled = btnWC.Enabled = false;
                        ////btnYU.Enabled = true;
                        ////colorwhite(true);
                        ////colorYellow(false);
                        ////timer2.Enabled = true;
                        ////playturn1 = false;
                        ////gamepause = false;
                        ////memorydetails m = new memorydetails();
                        ////m.score = 0;
                        ////m.extra = 0;
                        ////m.player1 = true;
                        ////m.point = lblWPoint.Text;
                        ////lblWTurn.Tag = Convert.ToInt32(lblWTurn.Tag) + 1;
                        ////m.turn = lblWTurn.Text = "t" + lblWTurn.Tag.ToString();
                        ////m.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;

                        ////memory.Add(m);
                    }
                    else if (playturn1 == false)
                    {
                        colorYellow(true);
                        btnYR.Tag = "fca1";
                        btnYR.Image = BilliardWindowsApplication.Properties.Resources.fca1;
                    
                        //timer3.Enabled = false;
                        //nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer1, 0, false);
                        //timertick = 0;
                        //btnYU.Enabled = btnWC.Enabled = btnWD.Enabled = btnYD.Enabled = btnYC.Enabled = false;
                        //btnWU.Enabled = true;
                        //colorwhite(false);
                        //colorYellow(true);
                        //timer2.Enabled = true;
                        //playturn1 = true;
                        //gamepause = false;

                        //memorydetails m = new memorydetails();
                        //m.score = 0;
                        //m.extra = 0;
                        //m.player1 = false;
                        //m.point = lblYPoint.Text;
                        //lblYTurn.Tag = Convert.ToInt32(lblYTurn.Tag) + 1;
                        //m.turn = lblYTurn.Text = "t" + lblYTurn.Tag.ToString();
                        //m.set = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;
                        //memory.Add(m);
                    }
                    rafreeaction = false;
                    timeoutPenalty = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs38" + ex.ToString()); }
        }
        private void nbprogressbar1_Click(object sender, EventArgs e)
        {
            try { }
            catch (Exception ex) { MessageBox.Show("exno tgs39" + ex.ToString()); }
        }
        private void btnExtraTimeN_Click(object sender, EventArgs e)
        {
            try
            {
                playclicksound();
                if (playturn1 == true)
                    colorwhite(false);
                else colorYellow(false);
                pnlExtraTime.Location = new Point(253, -130);
            }
            catch (Exception ex) { MessageBox.Show("exno tgs40" + ex.ToString()); }
        }
        private void btnExtraTimeY_Click(object sender, EventArgs e)
        {
            try
            {
                playclicksound();
                if (playturn1 == true && extratime1 == true)
                {
                    nbprogressbar1.setBar(0);
                    extratime1 = false;
                    pnlExtraTime.Location = new Point(253, -130);
                    colorwhite(false);
                }
                else
                    if (playturn1 == false && extratime2 == true)
                    {
                        nbprogressbar1.setBar(0);
                        extratime2 = false;
                        pnlExtraTime.Location = new Point(253, -130);
                        colorYellow(false);
                    }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs41" + ex.ToString()); }
        }
        private void btnExtraTimeOk_Click(object sender, EventArgs e)
        {
            try
            {
                playclicksound();
                if (playturn1 == true)
                    colorwhite(false);
                else colorYellow(false);
                pnlExtraTime.Location = new Point(253, -130);
            }
            catch (Exception ex) { MessageBox.Show("exno tgs42" + ex.ToString()); }
        }
        private void btnHandicapY_Click(object sender, EventArgs e)
        {
            try
            {
                playclicksound();
                gamepause = false;
                handicap = true;
                pnlHandicap.Location = new Point(253, -300);
            }
            catch (Exception ex) { MessageBox.Show("exno tgs43" + ex.ToString()); }
        }
        private void btnHandicapN_Click(object sender, EventArgs e)
        {
            try
            {
                playclicksound();
                pnlHandicap.Location = new Point(253, -300);
            }
            catch (Exception ex) { MessageBox.Show("exno tgs44" + ex.ToString()); }
        }
        void tmhandicap_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pnlHandicap.Location.Y <= 140)
                {
                    pnlHandicap.Location = new Point(253, pnlHandicap.Location.Y + 2);
                }
                else { tmhandicap.Enabled = false; }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs45" + ex.ToString()); }
        }
        void tmextratime_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pnlExtraTime.Location.Y <= 140)
                {
                    pnlExtraTime.Location = new Point(253, pnlExtraTime.Location.Y + 5);
                }
                else { tmextratime.Enabled = false; }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs46" + ex.ToString()); }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                playclicksound();
                pbSetinparity.Location = new Point(689, -262);
            }
            catch (Exception ex) { MessageBox.Show("exno tgs47" + ex.ToString()); }
        }
        private void pbSwap_Click(object sender, EventArgs e)
        {
            try
            {
                playclicksound();
                biliardService.Details temp;
                temp = BLL_BilliardWindowsApplication.player1;
                BLL_BilliardWindowsApplication.player1 = BLL_BilliardWindowsApplication.player2;
                BLL_BilliardWindowsApplication.player2 = temp;

                temp = BLL_BilliardWindowsApplication.player3;
                BLL_BilliardWindowsApplication.player3 = BLL_BilliardWindowsApplication.player4;
                BLL_BilliardWindowsApplication.player4 = temp;

                Image tempi;
                tempi = BLL_BilliardWindowsApplication.c1;
                pbClub1.Image = BLL_BilliardWindowsApplication.c1 = BLL_BilliardWindowsApplication.c2;
                pbClub2.Image = BLL_BilliardWindowsApplication.c2 = tempi;

                tempi = BLL_BilliardWindowsApplication.c3;
                pbClub3.Image = BLL_BilliardWindowsApplication.c3 = BLL_BilliardWindowsApplication.c4;
                pbClub4.Image = BLL_BilliardWindowsApplication.c4 = tempi;

                tempi = BLL_BilliardWindowsApplication.p1;
                pbPlayer1.Image = BLL_BilliardWindowsApplication.p1 = BLL_BilliardWindowsApplication.p2;
                pbPlayer2.Image = BLL_BilliardWindowsApplication.p2 = tempi;

                tempi = BLL_BilliardWindowsApplication.p3;
                pbPlayer3.Image = BLL_BilliardWindowsApplication.p3 = BLL_BilliardWindowsApplication.p4;
                pbPlayer4.Image = BLL_BilliardWindowsApplication.p4 = tempi;

                if (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
                    lblPlayer1.Text = "WHITE";
                else
                    lblPlayer1.Text = BLL_BilliardWindowsApplication.player1.Name + " " + BLL_BilliardWindowsApplication.player1.FamilyName;

                if (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
                    lblPlayer2.Text = "YELLOW";
                else
                    lblPlayer2.Text = BLL_BilliardWindowsApplication.player2.Name + " " + BLL_BilliardWindowsApplication.player2.FamilyName;

                if (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
                    lblPlayer3.Text = "WHITE";
                else
                    lblPlayer3.Text = BLL_BilliardWindowsApplication.player3.Name + " " + BLL_BilliardWindowsApplication.player3.FamilyName;

                if (string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
                    lblPlayer4.Text = "YELLOW";
                else
                    lblPlayer4.Text = BLL_BilliardWindowsApplication.player4.Name + " " + BLL_BilliardWindowsApplication.player4.FamilyName;
            }
            catch (Exception ex) { MessageBox.Show("exno tgs48" + ex.ToString()); }
        }
        private void frmGameScore3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4 && e.Alt)
                {
                    e.SuppressKeyPress = true;

                }
                else e.SuppressKeyPress = false;
            }
            catch (Exception ex) { MessageBox.Show("exno tgs49" + ex.ToString()); }
        }
        bool hitstart = false;
        bool ONETIME = true;
		private void EnableScoreKeys(bool bEnable)
		{
			PictureBox[] pictureGroup = {btnW0, btnW1, btnW2, btnW3, btnW4, btnW5, btnW6, btnW7, btnW8, btnW9, 
										btnY0, btnY1, btnY2, btnY3, btnY4, btnY5, btnY6, btnY7, btnY8, btnY9};

			foreach (PictureBox btn in pictureGroup)
			{
				btn.Enabled = bEnable;
			}

			if (!bEnable)
			{
				if (playturn1)
				{
					btnW0.Image = BilliardWindowsApplication.Properties.Resources._0g;
					btnW1.Image = BilliardWindowsApplication.Properties.Resources._1g;
					btnW2.Image = BilliardWindowsApplication.Properties.Resources._2g;
					btnW3.Image = BilliardWindowsApplication.Properties.Resources._3g;
					btnW4.Image = BilliardWindowsApplication.Properties.Resources._4g;
					btnW5.Image = BilliardWindowsApplication.Properties.Resources._5g;
					btnW6.Image = BilliardWindowsApplication.Properties.Resources._6g;
					btnW7.Image = BilliardWindowsApplication.Properties.Resources._7g;
					btnW8.Image = BilliardWindowsApplication.Properties.Resources._8g;
					btnW9.Image = BilliardWindowsApplication.Properties.Resources._9g;
				}
				else
				{
					btnY0.Image = BilliardWindowsApplication.Properties.Resources._0c;
					btnY1.Image = BilliardWindowsApplication.Properties.Resources._1c;
					btnY2.Image = BilliardWindowsApplication.Properties.Resources._2c;
					btnY3.Image = BilliardWindowsApplication.Properties.Resources._3c;
					btnY4.Image = BilliardWindowsApplication.Properties.Resources._4c;
					btnY5.Image = BilliardWindowsApplication.Properties.Resources._5c;
					btnY6.Image = BilliardWindowsApplication.Properties.Resources._6c;
					btnY7.Image = BilliardWindowsApplication.Properties.Resources._7c;
					btnY8.Image = BilliardWindowsApplication.Properties.Resources._8c;
					btnY9.Image = BilliardWindowsApplication.Properties.Resources._9c;
				}
				
			}
			else
			{
				if (playturn1)
				{
					btnW0.Image = BilliardWindowsApplication.Properties.Resources._0b;
					btnW1.Image = BilliardWindowsApplication.Properties.Resources._1b;
					btnW2.Image = BilliardWindowsApplication.Properties.Resources._2b;
					btnW3.Image = BilliardWindowsApplication.Properties.Resources._3b;
					btnW4.Image = BilliardWindowsApplication.Properties.Resources._4b;
					btnW5.Image = BilliardWindowsApplication.Properties.Resources._5b;
					btnW6.Image = BilliardWindowsApplication.Properties.Resources._6b;
					btnW7.Image = BilliardWindowsApplication.Properties.Resources._7b;
					btnW8.Image = BilliardWindowsApplication.Properties.Resources._8b;
					btnW9.Image = BilliardWindowsApplication.Properties.Resources._9b;
				}
				else
				{
					btnY0.Image = BilliardWindowsApplication.Properties.Resources._0a;
					btnY1.Image = BilliardWindowsApplication.Properties.Resources._1a;
					btnY2.Image = BilliardWindowsApplication.Properties.Resources._2a;
					btnY3.Image = BilliardWindowsApplication.Properties.Resources._3a;
					btnY4.Image = BilliardWindowsApplication.Properties.Resources._4a;
					btnY5.Image = BilliardWindowsApplication.Properties.Resources._5a;
					btnY6.Image = BilliardWindowsApplication.Properties.Resources._6a;
					btnY7.Image = BilliardWindowsApplication.Properties.Resources._7a;
					btnY8.Image = BilliardWindowsApplication.Properties.Resources._8a;
					btnY9.Image = BilliardWindowsApplication.Properties.Resources._9a;
				}
			}
		}
        private void timer4_Tick(object sender, EventArgs e)
        {
            //try
            {
                if (hitstart && BallTrackAPI.m_nCurrentStep == 1 && !m_bRepositionBall)
				{
					if (playturn1)
						colorwhite(false);
					else
						colorYellow(false);
				}
                //BallTrackAPI.BTAPI_QueryDrawInfo(ref BallTrackAPI.drawInfo, ref BallTrackAPI.m_WhiteHitPos[0], ref BallTrackAPI.m_YellowHitPos[0], ref BallTrackAPI.m_RedHitPos[0], BallTrackAPI.m_bReplay, 1, 1);
				//if (Math.Abs(BallTrackAPI.drawInfo.white_point.x-BallTrackAPI.drawInfo.start_point_white.x)>3 ||Math.Abs(BallTrackAPI.drawInfo.white_point.y - BallTrackAPI.drawInfo.start_point_white.y)>3 ||
				//   Math.Abs(BallTrackAPI.drawInfo.red_point.x -BallTrackAPI.drawInfo.start_point_red.x)>3 ||Math.Abs(BallTrackAPI.drawInfo.red_point.y - BallTrackAPI.drawInfo.start_point_red.y)>3 ||
				//   Math.Abs(BallTrackAPI.drawInfo.yellow_point.x - BallTrackAPI.drawInfo.start_point_yellow.x)>3 || Math.Abs(BallTrackAPI.drawInfo.yellow_point.y - BallTrackAPI.drawInfo.start_point_yellow.y)>3)
				if (BallTrackAPI.m_nCurrentPlayer != -1 && BallTrackAPI.m_nCurrentStep == 0)
                {   //BALL START MOVING.
                    //if (BallTrackAPI.drawInfo.start_point_red.x == 0 && BallTrackAPI.drawInfo.start_point_red.y == 0 && BallTrackAPI.drawInfo.start_point_white.x == 0 && BallTrackAPI.drawInfo.start_point_white.y == 0)
                    //{
                    //}
                    //else
                    {
                        //drawInforecord.Add(BallTrackAPI.drawInfo);
						//EnableScoreKeys(false);
						m_bRefereeEnteredPoint = false;
                        hitstart = true;
                        nbprogressbar1.Visible = false;
                        pictureBox3.Visible = true;
                        timer2.Enabled = false;
						if (BallTrackAPI.m_nCurrentStep == 0)
						{
							if (playturn1)
							    colorwhite(true);
							else
								colorYellow(true);
						}
                    }
                }
                else
                {   
                    if (hitstart == true)
                    {  // WHEN BALL STOP AFTER MOVING
                        if (BallTrackAPI.drawInfo.start_point_red.x == 0 && BallTrackAPI.drawInfo.start_point_red.y == 0 && BallTrackAPI.drawInfo.start_point_white.x == 0 && BallTrackAPI.drawInfo.start_point_white.y == 0)
                        { }
                        else
                        {
                            hitstart = false;
                            //MAY BE USE.
                           // pictureBox3.Invalidate();

                            //drawInforecord.Add(BallTrackAPI.drawInfo);
                        }
                    }
                    if(ONETIME)
                    {
                        ONETIME = false;
                        if (pictureBox3.Visible)
                        {
                            pictureBox3.Invalidate();
                            pictureBox3.Visible = false;
                        }
                        nbprogressbar1.Visible = true;
                       
                    }
                }
                //showGraphic();
				//DrawPlay(BallTrackAPI.m_nCurrentStep);
            }
            //catch (Exception ex) { MessageBox.Show("exno tgs50" + ex.ToString()); }
            //finally { BallTrackAPI.drawInfobackup = BallTrackAPI.drawInfo; }
        }
        public void checkinvalidate()
        {
            try
            {
                // hope checkinterval is used ball is not at 00 position.
                if (BallTrackAPI.checkinval[0].x == BallTrackAPI.drawInfo.start_point_red.x || BallTrackAPI.checkinval[0].y == BallTrackAPI.drawInfo.start_point_red.y)
                    pictureBox3.Invalidate();
                if (BallTrackAPI.checkinval[1].x == BallTrackAPI.drawInfo.start_point_white.x || BallTrackAPI.checkinval[1].y == BallTrackAPI.drawInfo.start_point_white.y)
                    pictureBox3.Invalidate();
                if (BallTrackAPI.checkinval[2].x == BallTrackAPI.drawInfo.start_point_yellow.x || BallTrackAPI.checkinval[2].y == BallTrackAPI.drawInfo.start_point_yellow.y)
                    pictureBox3.Invalidate();
                if (BallTrackAPI.drawInfobackup.start_point_red.y != BallTrackAPI.drawInfo.start_point_red.y || BallTrackAPI.drawInfobackup.start_point_white.y != BallTrackAPI.drawInfo.start_point_white.y || BallTrackAPI.drawInfobackup.start_point_yellow.y != BallTrackAPI.drawInfo.start_point_yellow.y)
                    pictureBox3.Invalidate();
            }
            catch (Exception ex) { MessageBox.Show("exno tgs51" + ex.ToString()); }
        }
		public void DrawPlay(int nStep)
		{
			pictureBox3.Invalidate();
		}
		
		//public void showGraphic()
		//{
		//	try
		//	{
		//		checkinvalidate();

		//		if (BallTrackAPI.drawInfo.start_point_red.x == 0 && BallTrackAPI.drawInfo.start_point_red.y == 0 && BallTrackAPI.drawInfo.start_point_white.x == 0 && BallTrackAPI.drawInfo.start_point_white.y == 0)
		//		{ }
		//		else
		//		{
		//			m_gCanvas.FillEllipse(new SolidBrush(Color.Red), BallTrackAPI.drawInfo.start_point_red.x * mulx - 6 + TEMPX, BallTrackAPI.drawInfo.start_point_red.y * muly - 6 + TEMPY, 12.0f, 12.0f);
		//			m_gCanvas.FillEllipse(new SolidBrush(Color.White), BallTrackAPI.drawInfo.start_point_white.x * mulx - 6 + TEMPX, BallTrackAPI.drawInfo.start_point_white.y * muly - 6 + TEMPY, 12.0f, 12.0f);
		//			m_gCanvas.FillEllipse(new SolidBrush(Color.Yellow), BallTrackAPI.drawInfo.start_point_yellow.x * mulx - 6 + TEMPX, BallTrackAPI.drawInfo.start_point_yellow.y * muly - 6 + TEMPY, 12.0f, 12.0f);

		//			try
		//			{   // used to connect lines from start point to first point.
		//				if (drawInforecord[1].start_point_red.x == BallTrackAPI.drawInfo.start_point_red.x && drawInforecord[1].start_point_red.y == BallTrackAPI.drawInfo.start_point_red.y)
		//				m_gCanvas.DrawLine(new Pen(Color.Red), BallTrackAPI.drawInfo.start_point_red.x * mulx + TEMPX, BallTrackAPI.drawInfo.start_point_red.y * muly + TEMPY, drawInforecord[1].red_point.x * mulx + TEMPX, drawInforecord[1].red_point.y * muly + TEMPY);
		//				if (drawInforecord[1].start_point_white.x == BallTrackAPI.drawInfo.start_point_white.x && drawInforecord[1].start_point_white.y == BallTrackAPI.drawInfo.start_point_white.y)
		//				m_gCanvas.DrawLine(new Pen(Color.White), BallTrackAPI.drawInfo.start_point_white.x * mulx + TEMPX, BallTrackAPI.drawInfo.start_point_white.y * muly + TEMPY, drawInforecord[1].white_point.x * mulx + TEMPX, drawInforecord[1].white_point.y * muly + TEMPY);
		//				if (drawInforecord[1].start_point_yellow.x == BallTrackAPI.drawInfo.start_point_yellow.x && drawInforecord[1].start_point_yellow.y == BallTrackAPI.drawInfo.start_point_yellow.y)
		//				m_gCanvas.DrawLine(new Pen(Color.Yellow), BallTrackAPI.drawInfo.start_point_yellow.x * mulx + TEMPX, BallTrackAPI.drawInfo.start_point_yellow.y * muly + TEMPY, drawInforecord[1].yellow_point.x * mulx + TEMPX, drawInforecord[1].yellow_point.y * muly + TEMPY);
		//			}
		//			catch { }
                    
		//			//drow current lines
		//			if (BallTrackAPI.drawInfobackup.white_point.x != 0 && BallTrackAPI.drawInfobackup.white_point.y != 0)
		//			{ m_gCanvas.DrawLine(new Pen(Color.White), BallTrackAPI.drawInfo.white_point.x * mulx + TEMPX, BallTrackAPI.drawInfo.white_point.y * muly + TEMPY, BallTrackAPI.drawInfobackup.white_point.x * mulx + TEMPX, BallTrackAPI.drawInfobackup.white_point.y * muly + TEMPY); }
		//			if (BallTrackAPI.drawInfobackup.red_point.x != 0 && BallTrackAPI.drawInfobackup.red_point.y != 0)
		//			{ m_gCanvas.DrawLine(new Pen(Color.Red), BallTrackAPI.drawInfo.red_point.x * mulx + TEMPX, BallTrackAPI.drawInfo.red_point.y * muly + TEMPY, BallTrackAPI.drawInfobackup.red_point.x * mulx + TEMPX, BallTrackAPI.drawInfobackup.red_point.y * muly + TEMPY); }
		//			if (BallTrackAPI.drawInfobackup.yellow_point.x != 0 && BallTrackAPI.drawInfobackup.yellow_point.y != 0)
		//			{ m_gCanvas.DrawLine(new Pen(Color.Yellow), BallTrackAPI.drawInfo.yellow_point.x * mulx + TEMPX, BallTrackAPI.drawInfo.yellow_point.y * muly + TEMPY, BallTrackAPI.drawInfobackup.yellow_point.x * mulx + TEMPX, BallTrackAPI.drawInfobackup.yellow_point.y * muly + TEMPY); }

		//		}
		//	}
		//	catch (Exception ex) { MessageBox.Show("exno tgs52" + ex.ToString()); }
		//}
        private void btnWRe_Click(object sender, EventArgs e)
        {
            if (playturn1 == false || BallTrackAPI.m_nCurrentStep >= 1 || rafreeaction)
                return;
            try
            {
                playclicksound();
                m_bRepositionBall = !m_bRepositionBall;
                timer1.Enabled = !m_bRepositionBall;
                if (m_bRepositionBall)
                {
                    BallTrackAPI.StopTracking();
                    timer4.Enabled = false;
                    timer2.Enabled = false;
                    if (tmstart.Enabled)
                        tmstart.Enabled = false;
                    btnStartGame.Location = new Point(260, -80);
                    btnWRe.Image = BilliardWindowsApplication.Properties.Resources.RW_r;
                }
                else
                {
                    pbSwap.Location = new Point(712, -296);
                    pbSetinparity.Location = new Point(689, -262);
                    startset = true;
                
                    playturn1 = true;
                    gamepause = false;

                    pbBilliardIcon.Visible = false;
                    nbprogressbar1.Visible = false;
                    pictureBox3.Visible = false;

                    timer3.Enabled = false;
                    nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer1, 0, false);

                    tmstart.Enabled = true;
                    tmstart.Interval = 1;
                    btnWM.Enabled = btnWE.Enabled = btnWC.Enabled = btnWR.Enabled = btnWU.Enabled = true;
                    btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb;
                    btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma;
                    btnYM.Enabled = btnYE.Enabled = btnYC.Enabled = btnYR.Enabled = btnYU.Enabled = true;
                    btnWG.Enabled = btnWH.Enabled = btnWD.Enabled = btnWC.Enabled = btnWU.Enabled = false;
                    btnYG.Enabled = btnYH.Enabled = btnYD.Enabled = btnYC.Enabled = btnYU.Enabled = false;
                    colorwhite(true);
                    colorYellow(true);

                    if (memory.Count > 0)
                    {
                        if (memory.Last().extra == 6)
                            memory.RemoveAt(memory.Count - 1);
                    }
                    btnWRe.Image = BilliardWindowsApplication.Properties.Resources.R___trk___W;
                }
                
            }
            catch (Exception ex) { MessageBox.Show("exno tgs53" + ex.ToString()); }
        }
        private void btnYRe_Click(object sender, EventArgs e)
        {
            if (playturn1 == true || BallTrackAPI.m_nCurrentStep >= 1 || rafreeaction)
                return;
            try
            {
                m_bRepositionBall = !m_bRepositionBall;
                timer1.Enabled = !m_bRepositionBall;
                playclicksound();
                if (m_bRepositionBall)
                {
                    BallTrackAPI.StopTracking();
                    timer4.Enabled = false;
                    timer2.Enabled = false;
                    if (tmstart.Enabled)
                        tmstart.Enabled = false;
                    btnStartGame.Location = new Point(260, -80);
                    btnYRe.Image = BilliardWindowsApplication.Properties.Resources.RY_r;
                }
                else
                {
                    pbSwap.Location = new Point(712, -296);
                    pbSetinparity.Location = new Point(689, -262);
                    startset = false;
                    playturn1 = false;
                    gamepause = false;

                    pbBilliardIcon.Visible = false;
                    nbprogressbar1.Visible = false;
                    pictureBox3.Visible = false;

                    timer3.Enabled = false;
                    nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer1, 0, false);

                    tmstart.Enabled = true;
                    tmstart.Interval = 1;
                    btnWM.Enabled = btnWE.Enabled = btnWC.Enabled = btnWR.Enabled = btnWU.Enabled = true;
                    btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb;
                    btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma;
                    btnYM.Enabled = btnYE.Enabled = btnYC.Enabled = btnYR.Enabled = btnYU.Enabled = true;
                    btnWG.Enabled = btnWH.Enabled = btnWD.Enabled = btnWC.Enabled = btnWU.Enabled = false;
                    btnYG.Enabled = btnYH.Enabled = btnYD.Enabled = btnYC.Enabled = btnYU.Enabled = false;
                    colorwhite(true);
                    colorYellow(true);

                    if (memory.Count > 0)
                    {
                        if (memory.Last().extra == 6)
                            memory.RemoveAt(memory.Count - 1);
                    }

                    btnYRe.Image = BilliardWindowsApplication.Properties.Resources.R___trk____O;
                }
            }
            catch (Exception ex) { MessageBox.Show("exno tgs53" + ex.ToString()); }
        }
        private void CaptureMyScreen()
        {
            try
            {
                //Creating a new Bitmap object
                Bitmap captureBitmap = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);

                //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);
                //Creating a Rectangle object which will  
                //capture our Current Screen
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;

                //Creating a New Graphics Object
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);

                //Copying Image from The Screen
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

                //Saving the Image File (I am here Saving it in My E drive).
                captureBitmap.Save("Capture.jpg", ImageFormat.Jpeg);

                List<string> li1 = new List<string>();
                li1.Add("info@biliardoprofessionale.it");
                SendHtmlFormattedEmail(li1, "Screen Captured", "Screen Captured");
                //Displaying the Successfull Result

                //MessageBox.Show("Screen Captured and mail is send");
            }
            catch (Exception ex) { MessageBox.Show("exno tgs54" + ex.ToString()); }
        }
        private void frmGameScore3NEW_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!wanttoclose)
            //{
            //    if (e.CloseReason == CloseReason.UserClosing)
            //        e.Cancel = true;
            //}
            try
            {
                tmstart.Enabled = tmhandicap.Enabled = tmextratime.Enabled = timer1.Enabled = timer2.Enabled = timer3.Enabled = timer4.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show("exno tgs55" + ex.ToString()); }
        }

		private void PlayArea_Paint(object sender, PaintEventArgs e)
		{
			//m_gCanvas = this.pictureBox3.CreateGraphics();
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			if (BallTrackAPI.m_whiteBallInfo.vptTrajectory.Count() > 0)
			{
				BallTrackAPI.DrawTrajectory(e.Graphics, BallTrackAPI.m_whiteBallInfo, BallTrackAPI.m_nCurrentStep, m_fScaleX, m_fScaleY, m_nMarginX, m_nMarginY, 0);
			}

			if (BallTrackAPI.m_yellowBallInfo.vptTrajectory.Count() > 0)
			{
                BallTrackAPI.DrawTrajectory(e.Graphics, BallTrackAPI.m_yellowBallInfo, BallTrackAPI.m_nCurrentStep, m_fScaleX, m_fScaleY, m_nMarginX, m_nMarginY, 1);
			}

			if (BallTrackAPI.m_redBallInfo.vptTrajectory.Count() > 0)
			{
                BallTrackAPI.DrawTrajectory(e.Graphics, BallTrackAPI.m_redBallInfo, BallTrackAPI.m_nCurrentStep, m_fScaleX, m_fScaleY, m_nMarginX, m_nMarginY, 2);
			}
		}
		private void pbRefereePresence_Click_1(object sender, EventArgs e)
		{
			try
			{
				playclicksound();
				rafreeaction = true;
				pbRefereePresence.Location = new Point(pbRefereePresence.Location.X, -1 * pbRefereePresence.Location.Y);
				if (playturn1)
					colorwhite(false);
				else
					colorYellow(false);
			}
			catch (Exception ex) { MessageBox.Show("exno tgs37" + ex.ToString()); }
		}

        bool tm1, tm2, tm3;
        private void panel5_Click(object sender, EventArgs e)
        {
            if (panel5.BackColor == Color.Black)
            {
                tm1 = tm2 = tm3 = false;
                panel5.BackColor = Color.Maroon;
                if (timer1.Enabled)
                {
                    tm1 = true;
                    timer1.Enabled = false;
                }
                if (timer2.Enabled)
                {
                    tm2 = true;
                    timer2.Enabled = false;
                }
                if (timer3.Enabled)
                {
                    tm3 = true;
                    timer3.Enabled = false;
                }


                colorwhite(true);
                colorYellow(true);

            }
            else
            {
                panel5.BackColor = Color.Black;
                if (tm1)
                    timer1.Enabled = true;
                if (tm2)
                    timer2.Enabled = true;
                if (tm3)
                    timer3.Enabled = true;

                if (playturn1 == true && gamepause == false)
                    colorwhite(false);
                else colorYellow(false);


            }
        }

        private void pbCheckBallColor_Click(object sender, EventArgs e)
        {
            try
            {
                playclicksound();
                rafreeaction = true;
                pbCheckBallColor.Location = new Point(pbCheckBallColor.Location.X, -1 * pbCheckBallColor.Location.Y);
				if (playturn1)
					colorwhite(false);
				else
					colorYellow(false);
            }
            catch (Exception ex) { MessageBox.Show("exno tgs38" + ex.ToString()); }
        }

        private bool wanttoclose = false;
        public void ScoreDataDeletetemp()
        {
            biliardService.ScoreData scdata = new biliardService.ScoreData();
            scdata.gameid = Int32.Parse(BLL_BilliardWindowsApplication.gamecostdetailsStatic.id);
            API.delScoreData(scdata);
        }
        public void ScoreDatauploadtemp(memorydetails memo)
        {
            biliardService.ScoreData scdata = new biliardService.ScoreData();
            string TEMPSCORE;
            if (memo.score >= 0)
                TEMPSCORE = "+" + memo.score;
            else TEMPSCORE = memo.score + "";
            if (memo.extra == 0)
                scdata.text = TEMPSCORE + " | " + memo.point + " | " + memo.turn + " | " + memo.set;
            else
                if (memo.extra == 1)
                    scdata.text = "@2T1" + TEMPSCORE + " | " + memo.point + " | " + memo.turn + " | " + memo.set;

                else
                    if (memo.extra == 2)
                        scdata.text = "@2T2" + TEMPSCORE + " | " + memo.point + " | " + memo.turn + " | " + memo.set;

                    else
                        if (memo.extra == 3)
                            scdata.text = "@" + TEMPSCORE + " | " + memo.point + " | " + memo.turn + " | " + memo.set;

                        else
                            if (memo.extra == 4)
                                scdata.text = "H" + TEMPSCORE + " | " + memo.point + " | " + memo.turn + " | " + memo.set;

            if (memo.player1)
            {
                scdata.color = "White";
            }
            else
            {
                scdata.color = "#D2691E";
            }
            scdata.setid = memorylist.Count + 1;
            scdata.setdata = true;
            scdata.status = "live";
            scdata.tlavail = false;
            scdata.gameid = Int32.Parse(BLL_BilliardWindowsApplication.gamecostdetailsStatic.id);
            API.setScoreData(scdata);
        }
        private void frmGameScore3NEW_Shown(object sender, EventArgs e)
        {
            if (BilliardWindowsApplication.Properties.Settings.Default.billiardno > 0)
            {
                string cost = lblCost.Text.Replace("€", "").Replace(",", ".");
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.billno = BilliardWindowsApplication.Properties.Settings.Default.billiardno.ToString();
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.clubid = BilliardWindowsApplication.Properties.Settings.Default.CID;
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.date = DateTime.Now.Date.Day + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year;
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.duration = (starttime.Hours * 60) / 10 + (starttime.Hours * 60) % 10 + starttime.Minutes / 10 + starttime.Minutes % 10 + "";
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.fromtime = currentTime.Hours / 10 + "" + currentTime.Hours % 10 + ":" + currentTime.Minutes / 10 + "" + currentTime.Minutes % 10 + "";
                noofplayers = 0;
                if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p1 = BLL_BilliardWindowsApplication.player1.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p1 = "0";
                if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p2 = BLL_BilliardWindowsApplication.player2.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p2 = "0";
                if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p3 = BLL_BilliardWindowsApplication.player3.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p3 = "0";
                if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name)) { BLL_BilliardWindowsApplication.gamecostdetailsStatic.p4 = BLL_BilliardWindowsApplication.player4.PlayerId; noofplayers++; } else BLL_BilliardWindowsApplication.gamecostdetailsStatic.p4 = "0";

                if (BLL_BilliardWindowsApplication.costDetailsStataic.coston == "t")
                {
                    BLL_BilliardWindowsApplication.gamecostdetailsStatic.totcost = cost.Replace("€", "").Replace(",", ".");

                    if (BLL_BilliardWindowsApplication.languague == 0)
                        BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer = (double.Parse(lblCost.Text.Replace("€", "").Replace(",", ".")) / noofplayers).ToString().Replace(",", ".");
                    else if (BLL_BilliardWindowsApplication.languague == 1)
                        BLL_BilliardWindowsApplication.gamecostdetailsStatic.costplayer = (double.Parse(lblCost.Text.Replace("€", "")) / noofplayers).ToString().Replace(",", ".");
                }
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.totime = DateTime.Now.Hour / 10 + DateTime.Now.Hour % 10 + ":" + DateTime.Now.Minute / 10 + DateTime.Now.Minute % 10 + "";
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.noplayers = noofplayers.ToString();
                BLL_BilliardWindowsApplication.gamecostdetailsStatic.gameover = false.ToString();
                insertgamecost(BLL_BilliardWindowsApplication.gamecostdetailsStatic);
            }

            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player1.Name))
                API.UpdatePlayerLoginAsync(BLL_BilliardWindowsApplication.player1.PlayerId, "1");
            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player2.Name))
                API.UpdatePlayerLoginAsync(BLL_BilliardWindowsApplication.player2.PlayerId, "1");

            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player3.Name))
                API.UpdatePlayerLoginAsync(BLL_BilliardWindowsApplication.player3.PlayerId, "1");

            if (!string.IsNullOrEmpty(BLL_BilliardWindowsApplication.player4.Name))
                API.UpdatePlayerLoginAsync(BLL_BilliardWindowsApplication.player4.PlayerId, "1");

        }
    }
}