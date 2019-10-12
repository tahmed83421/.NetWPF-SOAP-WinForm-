using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
    public partial class frmGameScore3 : Form
    {
        public frmGameScore3()
        {
            InitializeComponent();
        }

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
        bool addm1 = false;
        bool addm2 = false;
        bool colorw = false;
        bool colory = false;
        

        List<List<memorydetails>> memorylist = new List<List<memorydetails>>();
        List<memorydetails> memory = new List<memorydetails>();
        void playclicksound()
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            starttime = starttime.Add(new TimeSpan(0, 0, 1));
            lbltimer.Text = starttime.ToString();
        }
        string htmlcode(string TimeFinish, bool firstwin, int p1, int p2, List<List<memorydetails>> tabledetails)
        {
            string TEMPSCORE = "";
            string table = "";
            for (int i = 0; i < tabledetails.Count; i++)
            {

               

                table += "&nbsp;&nbsp;Result  Set " + (i + 1) + "<br><br>";
                for (int j = 0; j < tabledetails[i].Count; j++)
                {

                    if (tabledetails[i][j].score >= 0)
                        TEMPSCORE = "+" + tabledetails[i][j].score;
                    else TEMPSCORE = memory[i].score + "";

                    if (memory[i].extra == 1)
                        TEMPSCORE = "@2T1" + TEMPSCORE;
                    if (memory[i].extra == 2)
                        TEMPSCORE = "@2T2" + TEMPSCORE;
                    if (memory[i].extra == 3)
                        TEMPSCORE = "@" + TEMPSCORE;
                    if (memory[i].extra == 4)
                        TEMPSCORE = "@H" + TEMPSCORE;
                    

                    if (tabledetails[i][j].player1)
                        table += "<br/><font style=" + '"' + "color:black" + '"' + " >" + TEMPSCORE + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     " + tabledetails[i][j].point + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     " + tabledetails[i][j].turn + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     " + tabledetails[i][j].set + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     " + "</font> <br>";
                    else table += "<br/><font style=" + '"' + "color:orange" + '"' + " >" + TEMPSCORE + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     " + tabledetails[i][j].point + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     " + tabledetails[i][j].turn + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    " + tabledetails[i][j].set + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     " + "</font> <br>";
                }
            }
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

            string home = "<!DOCTYPE html> <html xmlns=" + '"' + "http://www.w3.org/1999/xhtml" + '"' + ">" +
            "<head> <title> </title> </head> <body>    <img src = " + '"' + "http://score.biliardoprofessionale.it/img/logoTop.png" + '"' + " /><br /><br />" +
            " <div style =" + '"' + "border-top:3px solid #22BCE5; border-top-width: 1px;" + '"' + "></div>" +
            "<span style = " + '"' + "font-family:Arial;font-size:10pt" + '"' + ">      <h1>  <strong>Game Summary</strong><br /></h1>" +
            "<font style=" + '"' + "color:blue" + '"' + " ><u><strong>Game settled at : </strong></u></font>  &nbsp;   Point : &nbsp; " + BLL_BilliardWindowsApplication.point + " --- Set n. : " + BLL_BilliardWindowsApplication.set + " ---- Quills n. : " + BLL_BilliardWindowsApplication.quills + " ---  Timer 1 : " + BLL_BilliardWindowsApplication.timer1 + "’’ --- Time 2  : " + BLL_BilliardWindowsApplication.timer2 + "’’ " +
            "<br /><br/>" +
            "<font style=" + '"' + "color:blue" + '"' + " ><u><strong>Time Info :  </strong></u></font> &nbsp;  Start time : 00.00.00 --- Finish Time : " + TimeFinish + "   --- Total Duration : " + TimeFinish +
            " <br /><br />" +
            " <strong> <font style=" + '"' + "color:blue" + '"' + " ><u>Game Held Between : </u></font></strong>&nbsp;" +
            "    <strong>" + teamAname + " / " + teamAclub + "   <font style=" + '"' + "color:red" + '"' + " > < vs > </font><font style=" + '"' + "color:orange" + '"' + " >  " + teamBname + " / " + teamBclub + " :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong/></font>" +
            "	<br/><br/>" +
            "      <font style=" + '"' + "color:blue" + '"' + " ><u> Winner of the match : </u></font> &nbsp; ";

            if (firstwin)
                home += teamAname;
            else home += teamBname;
            home += " , With <font style=" + '"' + "color:red" + '"' + " > ";

            if (firstwin)
                home += p1;
            else home += p2;
            home += "  </font> points.  <br/><br/>  " +
                 "<font style=" + '"' + "color:blue" + '"' + " ><u>Loser of the match : </u></font> &nbsp; ";
            if (firstwin)
                home += teamBname;
            else home += teamAname;
            home += "  , With <font style=" + '"' + "color:red" + '"' + " > ";
            if (firstwin)
                home += p2;
            else home += p1;
            home += "  </font> points." +
              "        </strong>" +
              "        <br />" +
              "    <br />" +
              "<h1>  <strong>  Score Report</strong><br /></h1>" +
              "</span>" +
              table +
              "</strong>" +
              "    Thanks<br />" +
              "      Biliardo Professionale" +
              "</body>" +
              "</html>";
            return home;
        }
        private void SendHtmlFormattedEmail(List<string> recepientEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(BilliardWindowsApplication.Properties.Settings.Default.setupemail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    for (int i = 0; i < recepientEmail.Count; i++)
                        mailMessage.To.Add(new MailAddress(recepientEmail[i]));
                    SmtpClient smtp = new SmtpClient(BilliardWindowsApplication.Properties.Settings.Default.host, BilliardWindowsApplication.Properties.Settings.Default.port);
                    smtp.EnableSsl = false;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential(BilliardWindowsApplication.Properties.Settings.Default.setupemail, BilliardWindowsApplication.Properties.Settings.Default.pass);
                    smtp.Credentials = NetworkCred;
                    smtp.Send(mailMessage);
                }
            }
            catch
            {
                
            }
        }
        //--------------------------------------------GAMESCORELOAD---------------------------------------------------------
        private void frmGameScore_Load(object sender, EventArgs e)
        {
            tmstart.Tick += t_Tick;
            tmextratime.Tick += tmextratime_Tick;
            tmhandicap.Tick += tmhandicap_Tick;
            tmstart.Interval = tmextratime.Interval = tmhandicap.Interval = 1;

            nbprogressbar1.ButtonClick+=nbprogressbar1_ButtonClick;
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
        private void btnStartGame_Click(object sender, EventArgs e)
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
            } timer2.Enabled = true;
           
           
        }
        void t_Tick(object sender, EventArgs e)
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
                    { colorwhite(false); }
                    else colorYellow(false);
                    timer2.Enabled = true;
                }

            }

        }
        void colorwhite(bool disable)
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
        void colorYellow(bool disable)
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
                 btnY4.Image =BilliardWindowsApplication.Properties.Resources._4a;
                 btnY5.Image =BilliardWindowsApplication.Properties.Resources._5a;
                 btnY6.Image =BilliardWindowsApplication.Properties.Resources._6a;
                 btnY7.Image =BilliardWindowsApplication.Properties.Resources._7a;
                 btnY8.Image =BilliardWindowsApplication.Properties.Resources._8a;
                 btnY9.Image =BilliardWindowsApplication.Properties.Resources._9a;

                 btnYC.Image =BilliardWindowsApplication.Properties.Resources.Ca1;
                 btnYD.Image =BilliardWindowsApplication.Properties.Resources._a1;
                 btnYE.Image =BilliardWindowsApplication.Properties.Resources.Ea;
                 btnYG.Image =BilliardWindowsApplication.Properties.Resources.Ga1;
                 btnYH.Image =BilliardWindowsApplication.Properties.Resources.Ha1;
                 btnYM.Image =BilliardWindowsApplication.Properties.Resources.Ma;
                 btnYP.Image =BilliardWindowsApplication.Properties.Resources.Ra;
                 btnYR.Image =BilliardWindowsApplication.Properties.Resources.fca1;
                 btnYR.Tag = "fca1";
                 btnYS.Image =BilliardWindowsApplication.Properties.Resources.Sa;
                 btnYU.Image = BilliardWindowsApplication.Properties.Resources.fca___v;
                 btnYU.Tag = "fca___v";
                 
                lblYPoint.ForeColor = lblYscoreboard.ForeColor = lblYSet.ForeColor = lblYTurn.ForeColor = Color.FromArgb(228, 108, 10);
            }
        }
        private void pbWG_Click(object sender, EventArgs e)
        {
            pbSwap.Location = new Point(712,-296);
            pbSetinparity.Location = new Point(689,-262);
            startset = true;
            playclicksound();
            playturn1 = true;
            gamepause = false;

            pbBilliardIcon.Visible = false;
            nbprogressbar1.Visible = true;
            
            tmstart.Enabled = true;
            btnWM.Enabled  = btnWE.Enabled = btnWC.Enabled = btnWR.Enabled = btnWU.Enabled = true;
            btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb;
            btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma;
            btnYM.Enabled = btnYE.Enabled = btnYC.Enabled = btnYR.Enabled = btnYU.Enabled = true;
            btnWG.Enabled = btnWH.Enabled = btnWD.Enabled = btnWC.Enabled = btnWU.Enabled = false;
            btnYG.Enabled = btnYH.Enabled = btnYD.Enabled = btnYC.Enabled = btnYU.Enabled = false;

            colorwhite(true);
            colorYellow(true);

           
        }
        private void pbYG_Click(object sender, EventArgs e)
        {
            pbSwap.Location = new Point(712, -296);
            pbSetinparity.Location = new Point(689, -262);
            playclicksound();
            startset = false;
            playturn1 = false;
            gamepause = false;

            pbBilliardIcon.Visible = false;
            nbprogressbar1.Visible = true;
            

            tmstart.Enabled = true;
            btnWM.Enabled = btnWE.Enabled = btnWC.Enabled = btnWR.Enabled = btnWU.Enabled = true;
            btnWM.Image = BilliardWindowsApplication.Properties.Resources.Mb;
            btnYM.Image = BilliardWindowsApplication.Properties.Resources.Ma;
            btnYM.Enabled = btnYE.Enabled = btnYC.Enabled = btnYR.Enabled = btnYU.Enabled = true;
            btnWG.Enabled = btnWH.Enabled = btnWD.Enabled = btnWC.Enabled = btnWU.Enabled = false;
            btnYG.Enabled = btnYH.Enabled = btnYD.Enabled = btnYC.Enabled = btnYU.Enabled = false;
            colorwhite(true);
            colorYellow(true);
            
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (nbprogressbar1.GetBartotal() > nbprogressbar1.GetBarValue())
                nbprogressbar1.setBar(nbprogressbar1.GetBarValue() + 1);
            else
            {
                new SoundPlayer(BilliardWindowsApplication.Properties.Resources.beep_07).Play();
                if (BLL_BilliardWindowsApplication.panalty)
                {
                    if (playturn1)
                    {
                        lblYscoreboard.Text = (Convert.ToInt32(lblYscoreboard.Text)+2)+"";
                        lblYPoint.Tag = Convert.ToInt32(lblYPoint.Tag) - 2;
                        lblYPoint.Text = "p" + Convert.ToInt32(lblYPoint.Tag);
                        memorydetails m = new memorydetails();
                        m.score = 2;
                        if (nbprogressbar1.GetBarTimer2())
                            m.extra = 2;
                        else m.extra = 1;
                        m.player1 = true;
                        m.point = lblWPoint.Text;
                        m.turn = lblWTurn.Text;
                        m.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;
                        memory.Add(m);

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
                        m.turn = lblYTurn.Text;
                        m.set = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;
                        memory.Add(m);
                    }
                }
                if (nbprogressbar1.GetBarTimer2())
                {
                    //if (BLL_BilliardWindowsApplication.panalty && Convert.ToInt32(lblWTurn.Tag) == 0)
                    //{
                    //    if (playturn1)
                    //    {
                    //        lblWscoreboard.Text = "-4";
                    //        lblWPoint.Text = "p" + (BLL_BilliardWindowsApplication.point + 4);
                    //        memorydetails m = new memorydetails();
                    //        m.score = -2;
                    //        m.extra = 2;
                    //        m.player1 = true;
                    //        m.point = lblWPoint.Text;
                    //        m.turn = lblWTurn.Text;
                    //        m.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;
                    //        memory.Add(m);
                    //    }
                    //    else
                    //    {
                    //        lblYscoreboard.Text = "-4";
                    //        lblYPoint.Text = "p" + (BLL_BilliardWindowsApplication.point + 4);
                    //        memorydetails m = new memorydetails();
                    //        m.score = -2;
                    //        m.extra = 2;
                    //        m.player1 = false;
                    //        m.point = lblYPoint.Text;
                    //        m.turn = lblYTurn.Text;
                    //        m.set = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;
                    //        memory.Add(m);
                    //    }
                    //}
                    timer2.Enabled = false;
                    pbRafreeAction.Visible = true;
                    pbRafreeAction.Location = new Point(659, 289);
                }
                else
                    nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer2, 0, true);
            }
        }
        //-----------------------------------------------END GAMESCOREBOARD-------------------------------------------------
        //------------------------------------------------PointCHECK--------------------------------------------------------
        private void pbW1_Click(object sender, EventArgs e)
        {

            if (playturn1 == true && gamepause == false)
            {
                playclicksound();
                if (!timer3.Enabled)
                {
                    if (rightscore)
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
                }
                else
                {
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
                }


            }

        }
        private void pbY1_Click(object sender, EventArgs e)
        {
            if ((playturn1 == false) && gamepause == false)
            {

                playclicksound();
                if (!timer3.Enabled)
                {
                    if (rightscore)
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
                        lblWscoreboard.Text = lblWscoreboard.Text+((PictureBox)sender).Tag;
                    }
                    else
                    {
                        try { Convert.ToInt32(lblYscoreboard.Text); }
                        catch { lblYscoreboard.Text = "0"; }
                        lblYscoreboard.Text = lblYscoreboard.Text+ ((PictureBox)sender).Tag;
                    }

                }
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            rafreeaction = false;
            if (playturn1 == true)
            {
                if (timertick < 10)
                {
                    timertick++;
                    if(UpScore)
                    {
                        if (btnWU.Visible)
                            btnWU.Visible = false;
                        else btnWU.Visible = true;
                    }
                    else 
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
                else
                {
                    if (UpScore)
                    {
                        btnWU.Visible = true;
                        int no = 0;
                        try { no = Convert.ToInt32(lblYscoreboard.Text); }
                        catch { }
                        for (int i = 0; i < memory.Count; i++)
                        {
                            memorydetails md = memory.LastOrDefault();
                            if (md.extra == 0  )
                            {
                                if (md.player1 == false)
                                {
                                    lblYscoreboard.Text = (no - md.score).ToString();
                                    lblYTurn.Text = "t" + (Convert.ToInt32(lblYTurn.Tag) - 1).ToString();
                                    lblYPoint.Text = (BLL_BilliardWindowsApplication.point - (no - md.score)).ToString();
                                    memory.RemoveAt(memory.Count - 1);
                                }
                                break;
                            }
                            else
                            {
                                if (md.player1)
                                {
                                    lblWscoreboard.Tag = (Convert.ToInt32(lblWscoreboard.Tag) - md.score).ToString();
                                    //lblWTurn.Text = "t" + (Convert.ToInt32(lblWTurn.Tag) - 1).ToString();
                                    lblWPoint.Text = (BLL_BilliardWindowsApplication.point - (Convert.ToInt32(lblWscoreboard.Tag))).ToString();
                                    memory.RemoveAt(memory.Count - 1);
                                }
                                else
                                {
                                    lblYscoreboard.Text = (no - md.score).ToString();
                                    //lblWTurn.Text = "t" + (Convert.ToInt32(lblYTurn.Tag) - 1).ToString();
                                    lblYPoint.Text = (BLL_BilliardWindowsApplication.point - (no - md.score)).ToString();
                                    memory.RemoveAt(memory.Count - 1);
                                }
                            }
                            
                        }
                    }
                    UpScore = false;
                    rightscore = false;
                    lblWscoreboard.Visible = true;
                    lblYscoreboard.Visible = true;
                    try { Convert.ToInt32(lblWscoreboard.Text); }
                    catch { lblWscoreboard.Text = "0"; }


                    memorydetails m1 = new memorydetails();
                    memorydetails m2 = new memorydetails();
                    m1.player1 = true;
                    if (handicap)
                        m1.extra = 4;
                    else 
                    m1.extra = 0;
                    m2.player1 = false;
                    m2.extra= 3;    //extra point
                    
                    
                    m1.score = Convert.ToInt32(lblWscoreboard.Text);
                    m2.score = Convert.ToInt32(lblYscoreboard.Text);
                    timer3.Enabled = false;
                    nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer1, 0, false);

                    timertick = 0;
                    lblWscoreboard.Text = Convert.ToInt32(lblWscoreboard.Tag) + Convert.ToInt32(lblWscoreboard.Text) + "";
                    lblWscoreboard.Tag = 0;
                    lblYscoreboard.Text = Convert.ToInt32(lblYscoreboard.Tag) + Convert.ToInt32(lblYscoreboard.Text) + "";
                    lblYscoreboard.Tag = 0;
                    if (addm1)
                    {
                        lblWTurn.Tag = Convert.ToInt32(lblWTurn.Tag) + 1;
                        lblWTurn.Text = "t" + lblWTurn.Tag;
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
                    m1.turn = lblWTurn.Text;
                    m1.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;

                    m2.point = lblYPoint.Text;
                    m2.turn = lblYTurn.Text;
                    m2.set = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;
                    if (addm1)
                     memory.Add(m1); 
                    if (addm2)
                    memory.Add(m2); 
                    addm2 = addm1=false; 
                    if (Convert.ToInt32(lblWPoint.Tag) < 1)                  //  if w win the set
                    {
                        gamepause = true;
                        timer2.Enabled = timer3.Enabled = false;
                        set1++;
                        memorylist.Add(memory);
                        lblWSet.Text = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;
                        if (BLL_BilliardWindowsApplication.set / 2 < set1)    // win the game
                        {
                            new frmwiner(lblPlayer1.Text, lblPlayer3.Text, set1, set2, lblWscoreboard.Text, lblYscoreboard.Text, true).ShowDialog();
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
                            this.Close();
                        }
                        else
                        if (BLL_BilliardWindowsApplication.set > (set1+set2))        // if not win the game go to next set
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
                            lblWTurn.Tag = lblYTurn.Tag = 0;
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
                            timer1.Enabled = false;
                            new frmwiner(lblPlayer1.Text, lblPlayer3.Text, set1, set2, lblWscoreboard.Text, lblYscoreboard.Text, true).ShowDialog();
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
                else
                {
                    if (UpScore)
                    {
                        btnYU.Visible = true;
                        int no = 0;
                        try { no = Convert.ToInt32(lblWscoreboard.Text); }
                        catch { }
                        for (int i = 0; i < memory.Count; i++)
                        {
                            memorydetails md = memory.LastOrDefault();
                            if (md.extra == 0  )
                            {
                                if (md.player1 == true)
                                {
                                    lblWscoreboard.Text = (no - md.score).ToString();
                                    lblWTurn.Text = "t" + (Convert.ToInt32(lblWTurn.Tag) - 1).ToString();
                                    lblWPoint.Text = (BLL_BilliardWindowsApplication.point - (no - md.score)).ToString();
                                    memory.RemoveAt(memory.Count - 1);
                                }
                                break;
                            }
                            else
                            {
                                if (md.player1)
                                {
                                    lblWscoreboard.Text = (no - md.score).ToString();
                                    //lblWTurn.Text = "t" + (Convert.ToInt32(lblWTurn.Tag) - 1).ToString();
                                    lblWPoint.Text = (BLL_BilliardWindowsApplication.point - (no - md.score)).ToString();
                                    memory.RemoveAt(memory.Count - 1);
                                }
                                else
                                {
                                    lblYscoreboard.Tag = (Convert.ToInt32(lblYscoreboard.Tag) - md.score).ToString();
                                    //lblYTurn.Text = "t" + (Convert.ToInt32(lblYTurn.Tag) - 1).ToString();
                                    lblYPoint.Text = (BLL_BilliardWindowsApplication.point - (Convert.ToInt32(lblYscoreboard.Tag))).ToString();
                                    memory.RemoveAt(memory.Count - 1);
                                }
                            }
                            
                        }
                    }
                    UpScore = false;
                    rightscore = false;
                    lblWscoreboard.Visible = true;
                    lblYscoreboard.Visible = true;

                    try { Convert.ToInt32(lblYscoreboard.Text); }
                    catch { lblYscoreboard.Text = "0"; }

                    memorydetails m1 = new memorydetails();
                    memorydetails m2 = new memorydetails();
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

                    timertick = 0;

                    lblYscoreboard.Text = Convert.ToInt32(lblYscoreboard.Tag) + Convert.ToInt32(lblYscoreboard.Text) + "";
                    lblYscoreboard.Tag = 0;
                    lblWscoreboard.Text = Convert.ToInt32(lblWscoreboard.Tag) + Convert.ToInt32(lblWscoreboard.Text) + "";
                    lblWscoreboard.Tag = 0;
                    if (addm2)
                    {
                        lblYTurn.Tag = Convert.ToInt32(lblYTurn.Tag) + 1;
                        lblYTurn.Text = "t" + lblYTurn.Tag;
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
                    m2.turn = lblYTurn.Text;
                    m2.set = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;

                    m1.point = lblWPoint.Text;
                    m1.turn = lblWTurn.Text;
                    m1.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;
                    if (addm1)
                     memory.Add(m1);
                    if (addm2)
                     memory.Add(m2);
                    addm2 = addm1= false; 


                 
                    if (Convert.ToInt32(lblYPoint.Tag) < 1)
                    {
                        gamepause = true;
                        timer2.Enabled = timer3.Enabled = false;
                        

                        set2++;
                        memorylist.Add(memory);
                        lblYSet.Text = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;
                        if(BLL_BilliardWindowsApplication.set/2<set2)
                        {
                            new frmwiner(lblPlayer2.Text, lblPlayer4.Text, set1, set2 ,lblWscoreboard.Text, lblYscoreboard.Text,true).ShowDialog();
                            timer1.Enabled = false;
                            if (BLL_BilliardWindowsApplication.email)
                            {
                                string body = htmlcode(starttime.ToString(), false, Convert.ToInt32(lblWscoreboard.Text), Convert.ToInt32(lblYscoreboard.Text), memorylist);
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

                            } this.Close();
                        }
                        else
                        if (BLL_BilliardWindowsApplication.set > (set2+set1))
                        {
                            new frmwiner(lblPlayer2.Text, lblPlayer4.Text, set1, set2 , lblWscoreboard.Text, lblYscoreboard.Text, false).ShowDialog();
                            if ((BLL_BilliardWindowsApplication.set % 2) == 1 && BLL_BilliardWindowsApplication.set-(set1+set2)==1)
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
                            lblWTurn.Tag = lblYTurn.Tag = 0;
                            lblWTurn.Text = lblYTurn.Text = "t" + lblWTurn.Tag;
                            lblWscoreboard.Tag = lblYscoreboard.Tag = lblWscoreboard.Text = lblYscoreboard.Text = 0 + "";
                            nbprogressbar1.setBar(Convert.ToInt32(BLL_BilliardWindowsApplication.timer1), 0, false);
                            timertick = 0;
                            extratime1 = extratime2 = true;
                        
                            memory = new List<memorydetails>();
                        }
                        else
                        {
                            new frmwiner(lblPlayer2.Text, lblPlayer4.Text, set1, set2 , lblWscoreboard.Text, lblYscoreboard.Text, true).ShowDialog();
                            timer1.Enabled = false;
                            if (BLL_BilliardWindowsApplication.email)
                            {
                                string body = htmlcode(starttime.ToString(), false, Convert.ToInt32(lblWscoreboard.Text), Convert.ToInt32(lblYscoreboard.Text), memorylist);
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

                            } this.Close();
                        }
                    }
                }
            }
        }
        //-----------------------------------------------END POINT CHECK----------------------------------------------------
        //----------------------------------------------EXTRA BUTTON  WORK--------------------------------------------------
        private void pbWS_Click(object sender, EventArgs e)
        {
            if (!colorw)
            {
                playclicksound();
                starttime = new TimeSpan(0, 0, 0);
                lblWPoint.Text = lblYPoint.Text = "p" + BLL_BilliardWindowsApplication.point;
                lblWPoint.Tag = lblYPoint.Tag = BLL_BilliardWindowsApplication.point;
                lblWSet.Text = lblYSet.Text = "s0/" + BLL_BilliardWindowsApplication.set;
                lblWTurn.Tag = lblYTurn.Tag = 0;
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
                nbprogressbar1.Visible = false;
                btnYG.Enabled = btnYH.Enabled = true;

                

                memory = new List<memorydetails>();

            }
        }
        private void pbYS_Click(object sender, EventArgs e)
        {
            if (!colory)
            {
                playclicksound();
                starttime = new TimeSpan(0, 0, 0);
                lblWPoint.Text = lblYPoint.Text = "p" + BLL_BilliardWindowsApplication.point;
                lblWPoint.Tag = lblYPoint.Tag = BLL_BilliardWindowsApplication.point;
                lblWSet.Text = lblYSet.Text = "s0/" + BLL_BilliardWindowsApplication.set;
                lblWTurn.Tag = lblYTurn.Tag = 0;
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
                nbprogressbar1.Visible = false;
                btnYG.Enabled = btnYH.Enabled = true;

                

                memory = new List<memorydetails>();

            }

        }
        private void pbWE_Click(object sender, EventArgs e)
        {
            if (!colorw)
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
        private void pbYE_Click(object sender, EventArgs e)
        {
            if (!colory)
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
        private void pbWC_Click(object sender, EventArgs e)
        {
            if (!colorw)
            {
                playclicksound();
                if (playturn1)
                    lblWscoreboard.Text = "";
                else lblYscoreboard.Text = "";
            }
        }
        private void pbYC_Click(object sender, EventArgs e)
        {
            if (!colory)
            {
                playclicksound();
                if (playturn1)
                    lblWscoreboard.Text = "";
                else lblYscoreboard.Text = "";
            }
        }
        private void pbWD_Click(object sender, EventArgs e)
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
        private void pbYD_Click(object sender, EventArgs e)
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
        private void pbWM_Click(object sender, EventArgs e)
        {
            if (!colorw)
            {
                colorwhite(true);
                colorYellow(true);

                playclicksound();
                timer2.Enabled = false;
                if (nbprogressbar1.Getbarcolor() == Color.LawnGreen)
                    nbprogressbar1.setbarcolor(Color.FromArgb(86, 176, 0));
                else nbprogressbar1.setbarcolor(Color.DarkRed);

                frmMemoryDetails frmmemo = new frmMemoryDetails(memory);
                frmmemo.FormClosed += formCloseEvent;
                frmmemo.ShowDialog();

            }
        }
        private void pbYM_Click(object sender, EventArgs e)
        {
            if (!colory)
            {
                colorwhite(true);
                colorYellow(true);

                playclicksound();
                timer2.Enabled = false;
                if (nbprogressbar1.Getbarcolor() == Color.LawnGreen)
                    nbprogressbar1.setbarcolor(Color.FromArgb(86, 176, 0));
                else nbprogressbar1.setbarcolor(Color.DarkRed);

                frmMemoryDetails frmmemo = new frmMemoryDetails(memory);
                frmmemo.FormClosed += formCloseEvent;
                frmmemo.ShowDialog();

            }
        }
        private void formCloseEvent(object sender, FormClosedEventArgs e)
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
            timer2.Enabled = true;
        }
        private void pbWH_Click(object sender, EventArgs e)
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
        private void pbYH_Click(object sender, EventArgs e)
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
        private void pbWR_Click(object sender, EventArgs e)
        {
            
             if (btnWR.Tag.ToString()=="fcaB")
            {
                btnWR.Tag = "fcar";
                btnWR.Image=BilliardWindowsApplication.Properties.Resources.fcar;
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
        private void pbYR_Click(object sender, EventArgs e)
        {
            if (btnYR.Tag.ToString() == "fca1")
            {btnWR.Tag = "fcar1";
                
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
        private void pbWU_Click(object sender, EventArgs e)
        {
            if (btnWU.Tag.ToString() == "fcaBv")
            {
                btnWU.Tag = "fcaBr";
                btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBr;
                UpScore = true;
                //timertick = 0;
                timer3.Enabled = true;
                //lblWscoreboard.Visible = lblYscoreboard.Visible = true;
                playclicksound();

            }
            else if (btnWU.Tag.ToString()== "fcaBr")
            {
                btnWU.Tag = "fcaBv";
                btnWU.Image = BilliardWindowsApplication.Properties.Resources.fcaBv;
                //rightscore = false;
                UpScore = false;
                playclicksound();
            }
        }
        private void btnYU_Click(object sender, EventArgs e)
        {
            if (btnYU.Tag.ToString() == "fca___v")
            {
                btnYU.Tag= "fcaBr";
                btnYU.Image = BilliardWindowsApplication.Properties.Resources.fcaBr;
                UpScore = true;
                //rightscore = true;
                //timertick = 0;
                timer3.Enabled = true;
                //lblWscoreboard.Visible = lblYscoreboard.Visible = true;
                playclicksound();
            }
            else if (btnYU.Tag.ToString() == "fcaBr")
            {
                btnWU.Tag = "fca___v";
                btnYU.Image = BilliardWindowsApplication.Properties.Resources.fca___v;
                //rightscore = false;
                UpScore = false;
                playclicksound();
            }
        }
        private void btnWP_Click(object sender, EventArgs e)
        {
            if (!colorw)
            {
                playclicksound();
                this.Close();
            }
        }
        private void btnYP_Click(object sender, EventArgs e)
        {
            if (!colory)
            {
                playclicksound();
                this.Close();
            }
        }
        private void pictureboxRound1_Click(object sender, EventArgs e)
        {
            playclicksound();
            this.Close();

        }
        private void pbRafreeAction_Click(object sender, EventArgs e)
        {
            playclicksound();
            rafreeaction = true;
            pbRafreeAction.Location = new Point(pbRafreeAction.Location.X, -1 * pbRafreeAction.Location.Y);
        }
        void nbprogressbar1_ButtonClick(object sender, EventArgs e)
        {
            if (rafreeaction)
            {
                playclicksound();
                if (playturn1 == true)
                {

                    timer3.Enabled = false;
                    nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer1, 0, false);
                    timertick = 0;
                    btnWU.Enabled = btnYC.Enabled = btnYD.Enabled = btnWD.Enabled = btnWC.Enabled = false;
                    btnYU.Enabled = true;
                    colorwhite(true);
                    colorYellow(false);
                    timer2.Enabled = true;
                    playturn1 = false;
                    gamepause = false;
                    memorydetails m = new memorydetails();
                    m.score = 0;
                    m.extra = 0;
                    m.player1 = true;
                    m.point = lblWPoint.Text;
                    lblWTurn.Tag = Convert.ToInt32(lblWTurn.Tag) + 1;
                    m.turn = lblWTurn.Text = "t" + lblWTurn.Tag.ToString();
                    m.set = "s" + set1 + "/" + BLL_BilliardWindowsApplication.set;
                    memory.Add(m);
                }
                else if (playturn1 == false)
                {
                    timer3.Enabled = false;
                    nbprogressbar1.setBar(BLL_BilliardWindowsApplication.timer1, 0, false);
                    timertick = 0;
                    btnYU.Enabled = btnWC.Enabled = btnWD.Enabled = btnYD.Enabled = btnYC.Enabled = false;
                    btnWU.Enabled = true;
                    colorwhite(false);
                    colorYellow(true);
                    timer2.Enabled = true;
                    playturn1 = true;
                    gamepause = false;

                    memorydetails m = new memorydetails();
                    m.score = 0;
                    m.extra = 0;
                    m.player1 = false;
                    m.point = lblYPoint.Text;
                    lblYTurn.Tag = Convert.ToInt32(lblYTurn.Tag) + 1;
                    m.turn = lblYTurn.Text = "t" + lblYTurn.Tag.ToString();
                    m.set = "s" + set2 + "/" + BLL_BilliardWindowsApplication.set;
                    memory.Add(m);

                }
                rafreeaction = false;
            }
            
        }
        private void nbprogressbar1_Click(object sender, EventArgs e)
        {
            
                
              
        }
        private void btnExtraTimeN_Click(object sender, EventArgs e)
        {
            playclicksound();
            if (playturn1 == true)
                colorwhite(false);
            else colorYellow(false);
            pnlExtraTime.Location = new Point(253, -130);

        }
        private void btnExtraTimeY_Click(object sender, EventArgs e)
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
        private void btnExtraTimeOk_Click(object sender, EventArgs e)
        {
            playclicksound();
            if (playturn1 == true)
                colorwhite(false);
            else colorYellow(false);
            pnlExtraTime.Location = new Point(253, -130);
        }
        private void btnHandicapY_Click(object sender, EventArgs e)
        {
            playclicksound();
            gamepause = false;
            handicap = true;
            pnlHandicap.Location = new Point(253, -300);
        }
        private void btnHandicapN_Click(object sender, EventArgs e)
        {
            playclicksound();
            pnlHandicap.Location = new Point(253, -300);
        }
        void tmhandicap_Tick(object sender, EventArgs e)
        {
            if (pnlHandicap.Location.Y <= 140)
            {
                pnlHandicap.Location = new Point(253, pnlHandicap.Location.Y + 2);
            }
            else { tmhandicap.Enabled = false; }
        }
        void tmextratime_Tick(object sender, EventArgs e)
        {
            if (pnlExtraTime.Location.Y <= 140)
            {
                pnlExtraTime.Location = new Point(253, pnlExtraTime.Location.Y + 5);
            }
            else { tmextratime.Enabled = false; }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            playclicksound();
            pbSetinparity.Location = new Point(689, -262);
        }
        private void pbSwap_Click(object sender, EventArgs e)
        {
            playclicksound();
            biliardService.Details temp;
            temp=BLL_BilliardWindowsApplication.player1;
            BLL_BilliardWindowsApplication.player1 = BLL_BilliardWindowsApplication.player2;
            BLL_BilliardWindowsApplication.player2 = temp;

            temp = BLL_BilliardWindowsApplication.player3;
            BLL_BilliardWindowsApplication.player3 = BLL_BilliardWindowsApplication.player4;
            BLL_BilliardWindowsApplication.player4 = temp;

            Image tempi;
            tempi = BLL_BilliardWindowsApplication.c1;
            pbClub1.Image = BLL_BilliardWindowsApplication.c1=BLL_BilliardWindowsApplication.c2;
            pbClub2.Image = BLL_BilliardWindowsApplication.c2=tempi;

            tempi = BLL_BilliardWindowsApplication.c3;
            pbClub3.Image = BLL_BilliardWindowsApplication.c3=BLL_BilliardWindowsApplication.c4;
            pbClub4.Image = BLL_BilliardWindowsApplication.c4=tempi;

            tempi = BLL_BilliardWindowsApplication.p1;
            pbPlayer1.Image = BLL_BilliardWindowsApplication.p1=BLL_BilliardWindowsApplication.p2;
            pbPlayer2.Image = BLL_BilliardWindowsApplication.p2=tempi;

            tempi = BLL_BilliardWindowsApplication.p3;
            pbPlayer3.Image = BLL_BilliardWindowsApplication.p3=BLL_BilliardWindowsApplication.p4;
            pbPlayer4.Image = BLL_BilliardWindowsApplication.p4=tempi;

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

        private void frmGameScore3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Alt)
            {
                e.SuppressKeyPress = true;

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}