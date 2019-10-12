using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BilliardWindowsApplication
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            g = this.pictureBox2.CreateGraphics();
            gg = this.pictureBox3.CreateGraphics();
        }

        biliardService.BilliardScoreboard obj = new biliardService.BilliardScoreboard();

        public struct POINT
        {
            public int x;
            public int y;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DRAW_INFO
        {
            public int state;
            public int player;
            public POINT white_point;
            public POINT yellow_point;
            public POINT red_point;
            public POINT start_point_white;
            public POINT start_point_yellow;
            public POINT start_point_red;
            public int hit_count_white;
            public int hit_count_yellow;
            public int hit_count_red;
            public POINT teacher_point_in;
            public POINT teacher_point_out;
            public int teacher_mark_in;
            public int teacher_mark_out;
            public Boolean show_ball_speed;
            public float ball_speed;
            public int impact_state;
        };

        public static bool m_bStartTracking = false;
        public static bool m_bReplay = false;
        public static int m_nInputMethod = 1;     //1: from file, 0: from camera
        public static POINT[] m_WhiteHitPos = new POINT[128];
        public static POINT[] m_YellowHitPos = new POINT[128];
        public static POINT[] m_RedHitPos = new POINT[128];
        public static DRAW_INFO drawInfo = new DRAW_INFO();
        public static DRAW_INFO drawInfobackup = new DRAW_INFO();
        public static bool mbInitialized = false;
        public static POINT[] ptCorners=new POINT[4];
        public static POINT[] ptCorner_new = new POINT[4];
        public static POINT ptCenter=new POINT();

        public static POINT[] checkinval = new POINT[3];

        Graphics g;
        Graphics gg;

        public const string strPath = "BBTAPI.dll";
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_Init(IntPtr hwndMain, IntPtr hwndShowTracking, IntPtr hwndCamera);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_OpenVideoFile([MarshalAs(UnmanagedType.LPWStr)] string lpszVideoPath);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int BTAPI_IsCameraReady();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_ConnectCamera(IntPtr hwndCamera);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_StartTracking();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_StopTracking();

        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_SetCanvasSize(int nWidth, int nHeight);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_QueryDrawInfo(ref DRAW_INFO di, ref POINT white_hit, ref POINT yellow_hit, ref POINT red_hit, bool bReplay, int player, int shotNum);

        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_GetCalibPoints( ref POINT ptCorners,ref POINT ptCenter);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_CameraClibration(ref POINT ptCorner_old,ref POINT ptCorner_new);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_Free();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_DrawReplay(IntPtr hwndReplay, int nPlayer, int nShotNum);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_EnableRecordVideo(bool bEnable);

        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_IsRecordVideoEnabled();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]

        public static extern void BTAPI_StartRecord([MarshalAs(UnmanagedType.LPWStr)] string lpszVideoPath);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_StopRecord();
//        ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        private void button1_Click(object sender, EventArgs e)
        {
                       
            if (!mbInitialized)
            {
                mbInitialized = BTAPI_Init(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                BTAPI_OpenVideoFile("7shots.avi");              
            }
            if (textBox1.Text == "false")
            {
                textBox1.Text = "true";
                if (!m_bStartTracking)
                BTAPI_StartTracking();
                timer1.Enabled = true;
            }
            else
            {
                textBox1.Text = "false";
                timer1.Enabled = false;
                if (m_bStartTracking)
                BTAPI_StopTracking();
            }
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                drawInfobackup = drawInfo;
               richTextBox1.Text = GetDrawInfo();
               showGraphic();
            }
            catch { }
        }

        public void checkinvalidate()
        {
            if (checkinval[0].x == drawInfo.start_point_red.x || checkinval[0].y == drawInfo.start_point_red.y)
                pictureBox2.Invalidate();
            if (checkinval[1].x == drawInfo.start_point_white.x || checkinval[1].y == drawInfo.start_point_white.y)
                pictureBox2.Invalidate();
            if (checkinval[2].x == drawInfo.start_point_yellow.x || checkinval[2].y == drawInfo.start_point_yellow.y)
                pictureBox2.Invalidate();

        }
        bool firstreplay=false;
        public void showGraphic()
        {
            checkinvalidate();
            g.FillEllipse(new SolidBrush(Color.Red), drawInfo.start_point_red.x-6, drawInfo.start_point_red.y-6, 12, 12);
            g.FillEllipse(new SolidBrush(Color.White), drawInfo.start_point_white.x-6, drawInfo.start_point_white.y-6, 12, 12);
            g.FillEllipse(new SolidBrush(Color.Yellow), drawInfo.start_point_yellow.x-6, drawInfo.start_point_yellow.y-6, 12, 12);
            if (firstreplay)
            {
                drawInfobackup = new DRAW_INFO();
                firstreplay = false;
            }
            if (drawInfobackup.white_point.x != 0 && drawInfobackup.white_point.y != 0)
            g.DrawLine(new Pen(Color.White), drawInfo.white_point.x,drawInfo.white_point.y,drawInfobackup.white_point.x,drawInfobackup.white_point.y);
            if (drawInfobackup.red_point.x != 0 && drawInfobackup.red_point.y != 0) 
            g.DrawLine(new Pen(Color.Red), drawInfo.red_point.x, drawInfo.red_point.y, drawInfobackup.red_point.x, drawInfobackup.red_point.y);
            if (drawInfobackup.yellow_point.x != 0 && drawInfobackup.yellow_point.y != 0) 
            g.DrawLine(new Pen(Color.Yellow), drawInfo.yellow_point.x, drawInfo.yellow_point.y, drawInfobackup.yellow_point.x, drawInfobackup.yellow_point.y);
        }
        public  string GetDrawInfo()
        {
            string strInfo = "";
                        
                BTAPI_QueryDrawInfo(ref drawInfo, ref m_WhiteHitPos[0], ref m_YellowHitPos[0], ref m_RedHitPos[0], m_bReplay, 0,0);


                //if (m_bReplay == true && drawInfo.state == 1)
                //{
                //    strInfo += " ";
                //}
                //strInfo += drawInfo.player + ",";           //player
                //strInfo += drawInfo.state + ",";            //state
                //strInfo += drawInfo.white_point.x + ",";    //white-x
                //strInfo += drawInfo.white_point.y + ",";    //white-y
                //strInfo += drawInfo.yellow_point.x + ",";   //yellow-x
                //strInfo += drawInfo.yellow_point.y + ",";   //yellow-y
                strInfo += drawInfo.red_point.x + ",";      //red-x
                strInfo += drawInfo.red_point.y + ",";      //red-y
                //strInfo += drawInfo.start_point_white.x + ",";
                //strInfo += drawInfo.start_point_white.y + ",";
                //strInfo += drawInfo.start_point_yellow.x + ",";
                //strInfo += drawInfo.start_point_yellow.y + ",";
                //strInfo += drawInfo.start_point_red.x + ",";
                //strInfo += drawInfo.start_point_red.y + ",";

                //strInfo += drawInfo.hit_count_white + ",";
                //strInfo += drawInfo.hit_count_yellow + ",";
                //strInfo += drawInfo.hit_count_red + ",";

                //dg1.Rows.Clear();
                //int i;
                //for (i = 0; i < drawInfo.hit_count_white; i++)
                //{
                //    strInfo += m_WhiteHitPos[i].x + ",";
                //    strInfo += m_WhiteHitPos[i].y + ",";
                    
                //    dg1.Rows.Add("white", m_WhiteHitPos[i].x, m_WhiteHitPos[i].y);
                //}

                //for (i = 0; i < drawInfo.hit_count_yellow; i++)
                //{
                //    strInfo += m_YellowHitPos[i].x + ",";
                //    strInfo += m_YellowHitPos[i].y + ",";
                //    dg1.Rows.Add("yellow", m_YellowHitPos[i].x, m_YellowHitPos[i].y);
                //}

                //for (i = 0; i < drawInfo.hit_count_red; i++)
                //{
                //    strInfo += m_RedHitPos[i].x + ",";
                //    strInfo += m_RedHitPos[i].y + ",";
                //    dg1.Rows.Add("red", m_RedHitPos[i].x, m_RedHitPos[i].y);
                //}

                //strInfo += drawInfo.show_ball_speed + ",";
                //strInfo += drawInfo.ball_speed + ",";
                //strInfo += drawInfo.impact_state;

                return strInfo;
            
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
         
            mbInitialized = BTAPI_Init(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			BTAPI_OpenVideoFile("D:\\opencv\\Outgrowth\\Samples\\5balls.avi");
            BTAPI_SetCanvasSize(100, 200);
            MessageBox.Show(BTAPI_GetCalibPoints(ref ptCorners[0], ref ptCenter).ToString());
            g.DrawLine(new Pen(Color.White), ptCorners[0].y, ptCorners[0].x, ptCorners[1].y, ptCorners[1].x);
            g.DrawLine(new Pen(Color.White), ptCorners[0].y, ptCorners[0].x, ptCorners[3].y, ptCorners[3].x);
            g.DrawLine(new Pen(Color.White), ptCorners[2].y, ptCorners[2].x, ptCorners[1].y, ptCorners[1].x);
            g.DrawLine(new Pen(Color.White), ptCorners[2].y, ptCorners[2].x, ptCorners[3].y, ptCorners[3].x);

            g.DrawLine(new Pen(Color.White), (ptCorners[0].y + ptCorners[1].y) / 2, (ptCorners[0].x + ptCorners[1].x) / 2, (ptCorners[2].y + ptCorners[3].y)/2, (ptCorners[2].x+ptCorners[3].x)/2);
            g.DrawLine(new Pen(Color.White), (ptCorners[0].y + ptCorners[3].y) / 2, (ptCorners[0].x + ptCorners[3].x) / 2, (ptCorners[1].y + ptCorners[3].y) / 2, (ptCorners[1].x + ptCorners[3].x) / 2);
            

            l0.Location = new Point(ptCorners[0].x + pictureBox3.Location.X, ptCorners[0].y + pictureBox3.Location.Y);
            l1.Location = new Point(ptCorners[1].x + pictureBox3.Location.X, ptCorners[1].y + pictureBox3.Location.Y);
            l2.Location = new Point(ptCorners[2].x + pictureBox3.Location.X, ptCorners[2].y + pictureBox3.Location.Y);
            l3.Location = new Point(ptCorners[3].x + pictureBox3.Location.X, ptCorners[3].y + pictureBox3.Location.Y);
        }
        private void l1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ((Label)sender).Location = e.Location;
                ((Label)sender).Size = new Size(10, 10);

            }
        }
        private void l1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }
        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Invalidate();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            g.FillEllipse(new SolidBrush(Color.Red), drawInfo.start_point_red.x - 6, drawInfo.start_point_red.y - 6, 12, 12);
            g.FillEllipse(new SolidBrush(Color.White), drawInfo.start_point_white.x - 6, drawInfo.start_point_white.y - 6, 12, 12);
            g.FillEllipse(new SolidBrush(Color.Yellow), drawInfo.start_point_yellow.x - 6, drawInfo.start_point_yellow.y - 6, 12, 12);
            int i;
            try
                {
                    for (i = 1; i < drawInfo.hit_count_white; i++)
                        g.DrawLine(new Pen(Color.White), m_WhiteHitPos[i - 1].x, m_WhiteHitPos[i - 1].y, m_WhiteHitPos[i].x, m_WhiteHitPos[i].y);
                    for (i = 1; i < drawInfo.hit_count_red; i++)
                        g.DrawLine(new Pen(Color.Red), m_RedHitPos[i-1].x, m_RedHitPos[i-1].y, m_RedHitPos[i].x, m_RedHitPos[i].y);
                    for (i = 1; i < drawInfo.hit_count_yellow; i++)
                        g.DrawLine(new Pen(Color.Yellow), m_YellowHitPos[i - 1].x, m_YellowHitPos[i - 1].y, m_YellowHitPos[i].x, m_YellowHitPos[i].y);
                }
                catch { }
            }
        private void button5_Click(object sender, EventArgs e)
        {
            BTAPI_DrawReplay(IntPtr.Zero,0,0 );
            pictureBox2.Invalidate();
            firstreplay = true;
            m_bReplay = true;
        }
        //public struct lastrecord
        //{
        //    POINT[] WhiteHitPos = new POINT[128];
        //    POINT[] YellowHitPos = new POINT[128];
        //    POINT[] RedHitPos = new POINT[128];
        //    DRAW_INFO drawInforecord = new DRAW_INFO();
        //}
    }
    
}