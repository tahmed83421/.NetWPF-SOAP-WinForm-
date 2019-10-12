using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
namespace BilliardWindowsApplication
{
	using TRAJECTORY_INFO = List<CRASH_ELEMENT>;
	using System.Windows.Forms;
	[StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
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
        public bool show_ball_speed;
        public float ball_speed;
        public int impact_state;
    };

	public class TEACHER_POINT_INFO
	{
		public int nType;		//0: no info, 1: in, 2: out
		public int nPoint;
		public int	nSide;
		public POINT pt;
		public TEACHER_POINT_INFO()
		{
			nType = -1;
			nPoint = 0;
			nSide = -1;
			pt.x = pt.y = 0;
		}
	};
	public class CRASH_ELEMENT
	{
		public int		nIndex;
		public float	fSpeed;
		public long	ltime;
		public POINT pt;
		public CRASH_TYPE	crashType;
		public int		nImpactType;
		public TEACHER_POINT_INFO tpi = new TEACHER_POINT_INFO();
		public CRASH_ELEMENT()
		{
			crashType = CRASH_TYPE.HIT_BOUNT;
		}
	};

	public enum CRASH_TYPE{HIT_BOUNT = -1, HIT_WHITE_BALL, HIT_YELLOW_BALL, HIT_RED_BALL};
    public class BallTrackAPI
    {
		public delegate void BallPosProcDelegate(POINT ptWhite, POINT ptYellow, POINT ptRed);
		public delegate void StateUpdateDelegate(int nState);
		public delegate void DrawDelegate(int nState);
		public delegate void NotifyProcDelegate(int nNotifyCode, int nValue);
		public static StateUpdateDelegate stateProc;
		public static DrawDelegate drawProc;
		public static RECT g_rcClip = new RECT();
		public static RECT g_rcTable = new RECT();
        public static bool m_bStartTracking = false;
        public static bool m_bReplay = false;
		public static int m_nInputMethod = 0;    //1: from file, 0: from camera
        public static POINT[] m_WhiteHitPos = new POINT[128];
        public static POINT[] m_YellowHitPos = new POINT[128];
        public static POINT[] m_RedHitPos = new POINT[128];
        public static DRAW_INFO drawInfo = new DRAW_INFO();
        public static DRAW_INFO drawInfobackup = new DRAW_INFO();
        public static bool mbInitialized = false;
        public static POINT[] ptCorners = new POINT[4];
        public static POINT[] ptCorner_new = new POINT[4];
        public static POINT ptCenter = new POINT();
        public static POINT[] checkinval = new POINT[3];
		public static BallPosProcDelegate ballPosProc = new BallPosProcDelegate(BallPosProc);
		public static IntPtr PtrBallPosProc = Marshal.GetFunctionPointerForDelegate(ballPosProc);
		public static BALL_TRACK_INFO m_whiteBallInfo = new BALL_TRACK_INFO();
		public static BALL_TRACK_INFO m_yellowBallInfo = new BALL_TRACK_INFO();
		public static BALL_TRACK_INFO m_redBallInfo = new BALL_TRACK_INFO();
		public static POINT m_ptTeacherPointIn = new POINT();
		public static POINT m_ptTeacherPointOut = new POINT();
		public static int m_nCurrentPlayer = -1;
		public static int m_nOutSide = -1;
		public static int m_nHitRailCount = 0;
		public static int m_nFirstImpactedBall = -1;
		public static int m_nInSide = -1;
		public static float m_fMillPerDot;
		public static int m_nPrevPlayer = -1;
		public static int m_nTeacherMarkIn = 0;
		public static bool m_bShowBallSpeed = true;
		public static int m_nTeacherMarkOut = 0;
		public static Size m_szCanvas = new Size(523, 950);
		public static int[] m_nMarkValue = new int[28];
		public static long[] nEndTime = new long[3];
		public static bool m_bStrikedTargetBall = false;
		public static long[] nStartTime = new long[3];
		public static int BALL_SPEED_INTERVAL = 3;
		public static int[] nFrameCountForSpeed= new int[3];
		public static int BALL_RADIUS_REAL = 25;
		public static int nSideHitMinIndex = -1;
		public static int m_nRailCount = 0;
		public static bool m_bShowTeacherPoint = false;
		public static int m_nCurrentStep = -1;
        public static bool m_bWrongBallShotted = false;
        public static bool m_bTwoBallTooClose = false;
        public static bool m_bFirstCheck = true;
        private static int nBallRadius = 8;
		public static ShotHistory shotHistory = new ShotHistory();
		public static int nDiamondMargin = 18;
		public static Point m_ptMarkIn = new Point();
		public static Point m_ptMarkOut = new Point();
		public static Point m_ptTextForMarkIn = new Point();
		public static Point m_ptTextForMarkOut = new Point();
		public static List<TRAJECTORY_INFO>[] m_playHistory = new List<TRAJECTORY_INFO>[3];
		public static bool m_bFirstInputOK = false;
		public static bool m_bLastOutputOK = false;
        public const string strPath = "BBTAPI.dll";

        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_Init(IntPtr procBallPos, IntPtr hwndMain, IntPtr hwndShowTracking, IntPtr hwndCamera);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_Free();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_OpenVideoFile([MarshalAs(UnmanagedType.LPWStr)] string lpszVideoPath);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_IsRecordVideoEnabled();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_EnableRecordVideo(bool bEnable);

        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int BTAPI_IsCameraReady();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_IsCameraConnected();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_ConnectCamera(IntPtr hwndCamera);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_DisconnectCamera();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_StopTracking();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_StartTracking();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_SetCanvasSize(int nWidth, int nHeight);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_QueryDrawInfo(ref DRAW_INFO di, ref POINT white_hit, ref POINT yellow_hit, ref POINT red_hit, bool bReplay, int player, int shotNum);

        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool BTAPI_GetCalibPoints(ref POINT ptCorners, ref POINT ptCenter);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_CameraClibration(ref POINT ptCorner_old, ref POINT ptCorner_new);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_StartRecord([MarshalAs(UnmanagedType.LPWStr)] string lpszVideoPath);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_StopRecord();
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_DrawReplay(IntPtr hwndReplay, int nPlayer, int nShotNum);
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void BTAPI_SetBallPosCallback(IntPtr proc);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void BTAPI_SetNotifyCallback(IntPtr proc);
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern RECT BTAPI_GetClipRect();
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern RECT BTAPI_GetTableRect();
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int BTAPI_GetWhiteHistoryCount();
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern int BTAPI_GetYellowHistoryCount();
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void BTAPI_ClearHistory();
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void BTAPI_StopReplay();
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool BTAPI_SaveHistoryToFile([MarshalAs(UnmanagedType.LPWStr)] string lpszFilePath);
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool BTAPI_LoadHistoryFromFile([MarshalAs(UnmanagedType.LPWStr)] string lpszFilePath);
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern bool BTAPI_DeleteShotRecord(int nPlayer, int nShot);
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void BTAPI_ShowTeacherPoint(bool bShowTeacherPoint);
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void BTAPI_SetTeacherParam(ref int nMarkValues, int nInSide, int nOutSide, int nHitRailCount);
		[DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		public static extern void BTAPI_ShowBallSpeed(bool bShowBallSpeed);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern float BTAPI_GetAveSpeedForReplay(int nPlayer, int nShot);
        [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern float BTAPI_GetAveSpeedForRealTime();
		public static void StartTracking()
		{
			ResetCurrentTrajectory();

			BallTrackAPI.BTAPI_EnableRecordVideo(false);
			string strCurDir = Environment.CurrentDirectory;
			strCurDir += "\\Record";
			try
			{
				if (!Directory.Exists(strCurDir))
				{
					Directory.CreateDirectory(strCurDir);
				}

				strCurDir += "\\last.avi";
				if (File.Exists(strCurDir))
				{
					File.Delete(strCurDir);
				}

				BallTrackAPI.BTAPI_StartRecord(strCurDir);
			}
			catch (Exception)
			{

			}

			BTAPI_StartTracking();
			m_bStartTracking = true;
            m_bFirstCheck = true;
		}
		public static void StopTracking()
		{
			BTAPI_StopTracking();
			m_bStartTracking = false;
			BTAPI_StopRecord();
		}
        public static string GetDrawInfo()
        {
            string strInfo = "";
            try
            {
                BallTrackAPI.BTAPI_QueryDrawInfo(ref BallTrackAPI.drawInfo, ref BallTrackAPI.m_WhiteHitPos[0], ref BallTrackAPI.m_YellowHitPos[0], ref BallTrackAPI.m_RedHitPos[0], BallTrackAPI.m_bReplay, 1, 1);


                if (BallTrackAPI.m_bReplay == true && BallTrackAPI.drawInfo.state == 1)
                {
                    strInfo += " ";
                }
                strInfo += BallTrackAPI.drawInfo.player + ",";           //player
                strInfo += BallTrackAPI.drawInfo.state + ",";            //state
                strInfo += BallTrackAPI.drawInfo.white_point.x + ",";    //white-x
                strInfo += BallTrackAPI.drawInfo.white_point.y + ",";    //white-y
                strInfo += BallTrackAPI.drawInfo.yellow_point.x + ",";   //yellow-x
                strInfo += BallTrackAPI.drawInfo.yellow_point.y + ",";   //yellow-y
                strInfo += BallTrackAPI.drawInfo.red_point.x + ",";      //red-x
                strInfo += BallTrackAPI.drawInfo.red_point.y + ",";      //red-y
                strInfo += BallTrackAPI.drawInfo.start_point_white.x + ",";
                strInfo += BallTrackAPI.drawInfo.start_point_white.y + ",";
                strInfo += BallTrackAPI.drawInfo.start_point_yellow.x + ",";
                strInfo += BallTrackAPI.drawInfo.start_point_yellow.y + ",";
                strInfo += BallTrackAPI.drawInfo.start_point_red.x + ",";
                strInfo += BallTrackAPI.drawInfo.start_point_red.y + ",";

                strInfo += BallTrackAPI.drawInfo.hit_count_white + ",";
                strInfo += BallTrackAPI.drawInfo.hit_count_yellow + ",";
                strInfo += BallTrackAPI.drawInfo.hit_count_red + ",";

                int i;
                for (i = 0; i < BallTrackAPI.drawInfo.hit_count_white; i++)
                {
                    strInfo += BallTrackAPI.m_WhiteHitPos[i].x + ",";
                    strInfo += BallTrackAPI.m_WhiteHitPos[i].y + ",";
                }

                for (i = 0; i < BallTrackAPI.drawInfo.hit_count_yellow; i++)
                {
                    strInfo += BallTrackAPI.m_YellowHitPos[i].x + ",";
                    strInfo += BallTrackAPI.m_YellowHitPos[i].y + ",";
                }

                for (i = 0; i < BallTrackAPI.drawInfo.hit_count_red; i++)
                {
                    strInfo += BallTrackAPI.m_RedHitPos[i].x + ",";
                    strInfo += BallTrackAPI.m_RedHitPos[i].y + ",";
                }

                strInfo += BallTrackAPI.drawInfo.show_ball_speed + ",";
                strInfo += BallTrackAPI.drawInfo.ball_speed + ",";
                strInfo += BallTrackAPI.drawInfo.impact_state;

                return strInfo;
            }
            catch { return strInfo; }
        }
		public BallTrackAPI()
		{
			
		}
		public static POINT ScalePoint(POINT pt, float fScaleX, float fScaleY, int nMarginX, int nMarginY)
		{
			POINT ptNew = new POINT();
			ptNew.x = (int)(pt.x * fScaleX + 0.5f) + nMarginX;
			ptNew.y = (int)(pt.y * fScaleY + 0.5f) + nMarginY;

			return ptNew;
		}
		public static void ScaleCurrentDrawInfo(float fScaleX, float fScaleY, int nMarginX, int nMarginY, int nClipWidth, int nClipHeight)
		{
			BallTrackAPI.drawInfo.white_point = ScalePoint(BallTrackAPI.drawInfo.white_point, fScaleX, fScaleY, nMarginX, nMarginY);
			BallTrackAPI.drawInfo.yellow_point = ScalePoint(BallTrackAPI.drawInfo.yellow_point, fScaleX, fScaleY, nMarginX, nMarginY);
			BallTrackAPI.drawInfo.red_point = ScalePoint(BallTrackAPI.drawInfo.red_point, fScaleX, fScaleY, nMarginX, nMarginY);

			BallTrackAPI.drawInfo.start_point_white = ScalePoint(BallTrackAPI.drawInfo.start_point_white, fScaleX, fScaleY, nMarginX, nMarginY);
			BallTrackAPI.drawInfo.start_point_yellow = ScalePoint(BallTrackAPI.drawInfo.start_point_yellow, fScaleX, fScaleY, nMarginX, nMarginY);
			BallTrackAPI.drawInfo.start_point_red = ScalePoint(BallTrackAPI.drawInfo.start_point_red, fScaleX, fScaleY, nMarginX, nMarginY);

			BallTrackAPI.drawInfo.teacher_point_in = ScalePoint(BallTrackAPI.drawInfo.teacher_point_in, fScaleX, fScaleY, nMarginX, nMarginY);
			BallTrackAPI.drawInfo.teacher_point_out = ScalePoint(BallTrackAPI.drawInfo.teacher_point_out, fScaleX, fScaleY, nMarginX, nMarginY);

			int nTextOffset = 20;

			int nSide = -1;
			int nPoint = -1;
			int nTeacherMarkIn = BallTrackAPI.drawInfo.teacher_mark_in;
			int nTeacherMarkOut = BallTrackAPI.drawInfo.teacher_mark_out;

			int nDiamondMargin_ = (int)(nDiamondMargin * fScaleX + 0.5f);
			if (nTeacherMarkIn >= 0)
			{
				nPoint = nTeacherMarkIn & 0xffff;
				nSide = (nTeacherMarkIn >> 16) & 0xffff;
				m_ptMarkIn = new Point(BallTrackAPI.drawInfo.teacher_point_in.x, BallTrackAPI.drawInfo.teacher_point_in.y);
				m_ptTextForMarkIn = m_ptMarkIn;
				if (nSide == 0)
				{
					m_ptMarkIn.Y = (int)(nDiamondMargin * fScaleY + 0.5f);
					m_ptTextForMarkIn.X = m_ptMarkIn.X - nTextOffset;
				}
				else if (nSide == 1)
				{
					m_ptMarkIn.X = (int)nClipWidth - (int)(nDiamondMargin * fScaleY + 0.5f);
					m_ptTextForMarkIn.Y = m_ptMarkIn.Y + nTextOffset;
				}
				else if (nSide == 2)
				{
					m_ptMarkIn.Y = (int)nClipHeight - (int)(nDiamondMargin * fScaleY + 0.5f);
					m_ptTextForMarkIn.X = m_ptMarkIn.X + nTextOffset;
				}
				else if (nSide == 3)
				{
					m_ptMarkIn.X = (int)(nDiamondMargin * fScaleY + 0.5f);
					m_ptTextForMarkIn.Y = m_ptMarkIn.Y - nTextOffset;
				}

				BallTrackAPI.drawInfo.teacher_mark_in = nPoint;
			}
			
			if (nTeacherMarkOut >= 0)
			{
				nPoint = nTeacherMarkOut & 0xffff;
				nSide = (nTeacherMarkOut >> 16) & 0xffff;

				m_ptMarkOut = new Point(BallTrackAPI.drawInfo.teacher_point_out.x, BallTrackAPI.drawInfo.teacher_point_out.y);
				m_ptTextForMarkOut = m_ptMarkOut;
				if (nSide == 0)
				{
					m_ptMarkOut.Y = (int)(nDiamondMargin * fScaleY + 0.5f);
					m_ptTextForMarkOut.X = m_ptMarkOut.X - nTextOffset;
				}
				else if (nSide == 1)
				{
					m_ptMarkOut.X = (int)nClipWidth - (int)(nDiamondMargin * fScaleY + 0.5f);
					m_ptTextForMarkOut.Y = m_ptMarkOut.Y + nTextOffset;
				}
				else if (nSide == 2)
				{
					m_ptMarkOut.Y = (int)nClipHeight - (int)(nDiamondMargin * fScaleY + 0.5f);
					m_ptTextForMarkOut.X = m_ptMarkOut.X + nTextOffset;
				}
				else if (nSide == 3)
				{
					m_ptMarkOut.X = (int)(nDiamondMargin * fScaleY + 0.5f);
					m_ptTextForMarkOut.Y = m_ptMarkOut.Y - nTextOffset;
				}

				BallTrackAPI.drawInfo.teacher_mark_out = nPoint;
			}

			int i;

			for (i = 0; i < BallTrackAPI.drawInfo.hit_count_white; i++)
				BallTrackAPI.m_WhiteHitPos[i] = ScalePoint(BallTrackAPI.m_WhiteHitPos[i], fScaleX, fScaleY, nMarginX, nMarginY);

			for (i = 0; i < BallTrackAPI.drawInfo.hit_count_yellow; i++)
				BallTrackAPI.m_YellowHitPos[i] = ScalePoint(BallTrackAPI.m_YellowHitPos[i], fScaleX, fScaleY, nMarginX, nMarginY);

			for (i = 0; i < BallTrackAPI.drawInfo.hit_count_red; i++)
				BallTrackAPI.m_RedHitPos[i] = ScalePoint(BallTrackAPI.m_RedHitPos[i], fScaleX, fScaleY, nMarginX, nMarginY);
		}
		//public static List<lastrecord> lastrecordlist = new List<lastrecord>();
		//public struct lastrecord
		//{
		//	public POINT[] WhiteHitPos;
		//	public POINT[] YellowHitPos;
		//	public POINT[] RedHitPos;
		//	public List<DRAW_INFO>drawInforecord;
		//}
		public static void BallPosProc(POINT ptWhite, POINT ptYellow, POINT ptRed)
		{
			if (m_nCurrentStep == 0)
			{
				if (ptWhite.x != -1 || ptWhite.x != -1)
					PushTrajectory(m_whiteBallInfo, ptWhite, 0);

				if (ptYellow.x != -1 || ptYellow.x != -1)
					PushTrajectory(m_yellowBallInfo, ptYellow, 1);

				if (ptRed.x != -1 || ptRed.x != -1)
					PushTrajectory(m_redBallInfo, ptRed, 2);

                if (m_bFirstCheck)
                {
                    if (ptWhite.x != -1 && ptYellow.x != -1)
                    {
                        int x_dist = Math.Abs(ptWhite.x - ptYellow.x);
                        int y_dist = Math.Abs(ptYellow.y - ptWhite.y);

                        if (Math.Max(x_dist, y_dist) <= 20)
                        {
                            m_bTwoBallTooClose = true;
                        }
                        else
                        {
                            m_bTwoBallTooClose = false;
                        }
                    }
                }
                m_bFirstCheck = false;
			}

			if (CheckCurrentState())
			{
				if (stateProc != null)
					stateProc(m_nCurrentStep);
			}

			if (drawProc != null)
			{
				drawProc(m_nCurrentStep);
			}
		}
		public static void InitTeacherSetting()
		{
			int i;
			for (i = 0; i < 28; i++)
				m_nMarkValue[i] = -1;

			m_nInSide = -1;
			m_nOutSide = -1;

			m_nRailCount = 0;
			//for (i = 5; i < 14; i++)
			//	m_nMarkValue[i] = (i - 5) * 10;

			//for (i = 14; i < 19; i++)
			//	m_nMarkValue[i] = (i - 14) * 10;

			//for (i = 19; i < 28; i++)
			//	m_nMarkValue[i] = (i - 19) * 10;

			//m_nInSide = 2;
			//m_nOutSide = 2;
		}
		public static bool InitSDK(IntPtr proc, IntPtr hwndMain, IntPtr hwndTracking, IntPtr hwndCamera)
		{
			bool bRet = BTAPI_Init(proc, hwndMain, hwndTracking, hwndCamera);
			if (bRet)
			{
				g_rcClip = BTAPI_GetClipRect();
				g_rcTable = BTAPI_GetTableRect();
				m_fMillPerDot = (float)2840.0f / (float)(g_rcTable.right - g_rcTable.left);
			}
			
			InitTeacherSetting();

			return bRet;
		}
		public static bool ShotNameExisting(string strShotName)
		{
			foreach (ShotMetaInfo shotInfo in shotHistory.Shot)
			{
				if (strShotName.ToLower() == shotInfo.Name.ToLower())
					return true;
			}

			return false;
		}
		public static void StorePreviousTrajectory()
		{
			int i;
			CRASH_ELEMENT crashElement = new CRASH_ELEMENT();
			if (m_redBallInfo.vptTrajectory.Count > 0 && m_whiteBallInfo.vptTrajectory.Count > 0 && m_yellowBallInfo.vptTrajectory.Count > 0)
			{
				crashElement.ltime = DateTime.Now.Millisecond - 7000;
				crashElement.nIndex = m_whiteBallInfo.vptTrajectory.Count - 1;
				crashElement.pt = m_whiteBallInfo.vptTrajectory[m_whiteBallInfo.vptTrajectory.Count - 1];
				crashElement.fSpeed = m_whiteBallInfo.fAveSpeed;
				if (m_bStrikedTargetBall)
					crashElement.nImpactType = 0;
				else
					crashElement.nImpactType = m_nFirstImpactedBall;
				m_whiteBallInfo.vIndexCrashed.Add(crashElement);

				crashElement = new CRASH_ELEMENT();
				crashElement.nIndex = m_yellowBallInfo.vptTrajectory.Count - 1;
				crashElement.fSpeed = m_yellowBallInfo.fAveSpeed;
				if (m_bStrikedTargetBall)
					crashElement.nImpactType = 0;
				else
					crashElement.nImpactType = m_nFirstImpactedBall;
				crashElement.pt = m_yellowBallInfo.vptTrajectory[m_yellowBallInfo.vptTrajectory.Count - 1];
				m_yellowBallInfo.vIndexCrashed.Add(crashElement);

				crashElement = new CRASH_ELEMENT();
				crashElement.nIndex = m_redBallInfo.vptTrajectory.Count - 1;
				crashElement.fSpeed = m_redBallInfo.fAveSpeed;
				if (m_bStrikedTargetBall)
					crashElement.nImpactType = 0;
				else
					crashElement.nImpactType = m_nFirstImpactedBall;
				crashElement.pt = m_redBallInfo.vptTrajectory[m_redBallInfo.vptTrajectory.Count - 1];
				m_redBallInfo.vIndexCrashed.Add(crashElement);
			}
			else
			{
				//ResetCurrentTrajectory();
				return;
			}

			if (m_playHistory[0] == null)
			{
				m_playHistory[0] = new List<TRAJECTORY_INFO>();
				m_playHistory[1] = new List<TRAJECTORY_INFO>();
				m_playHistory[2] = new List<TRAJECTORY_INFO>();
			}

			//if (m_nCurrentPlayer == 0)		//white player
			{
				TRAJECTORY_INFO info = new TRAJECTORY_INFO();
				for (i = 0; i < (int)m_whiteBallInfo.vIndexCrashed.Count; i++)
				{
					if (m_whiteBallInfo.vIndexCrashed[i].tpi.nType > 0)
					{

					}
					info.Add(m_whiteBallInfo.vIndexCrashed[i]);
				}
				

				m_playHistory[0].Add(info);

				//yellow ball
				info = new TRAJECTORY_INFO();
				for (i = 0; i < (int)m_yellowBallInfo.vIndexCrashed.Count; i++)
				{
					info.Add(m_yellowBallInfo.vIndexCrashed[i]);
				}

				m_playHistory[1].Add(info);
				//red ball
				info = new TRAJECTORY_INFO();
				info.Clear();
				for (i = 0; i < (int)m_redBallInfo.vIndexCrashed.Count; i++)
				{
					info.Add(m_redBallInfo.vIndexCrashed[i]);
				}

				m_playHistory[2].Add(info);
			}
		}
		public static void ResetCurrentTrajectory()
		{
			POINT ptWhite = new POINT();
			POINT ptYellow = new POINT();
			POINT ptRed = new POINT();

			if (m_whiteBallInfo.vptTrajectory.Count > 0)
			{
				ptWhite = m_whiteBallInfo.vptTrajectory[m_whiteBallInfo.vptTrajectory.Count - 1];
			}
			if (m_yellowBallInfo.vptTrajectory.Count > 0)
			{
				ptYellow = m_yellowBallInfo.vptTrajectory[m_yellowBallInfo.vptTrajectory.Count - 1];
			}
			if (m_redBallInfo.vptTrajectory.Count > 0)
			{
				ptRed = m_redBallInfo.vptTrajectory[m_redBallInfo.vptTrajectory.Count - 1];
			}

			m_whiteBallInfo.ClearInfo();

			m_yellowBallInfo.ClearInfo();

			m_redBallInfo.ClearInfo();

			m_nCurrentStep = 0;

			m_nPrevPlayer = m_nCurrentPlayer;

			m_nCurrentPlayer = -1;

			m_bStrikedTargetBall = false;

			m_nFirstImpactedBall = -1;

			nSideHitMinIndex = -1;

			nFrameCountForSpeed[0] = 0;
			nFrameCountForSpeed[1] = 0;
			nFrameCountForSpeed[2] = 0;

			m_nHitRailCount = 0;

			m_nTeacherMarkIn = 0;
			m_nTeacherMarkOut = 0;

			m_ptTeacherPointIn.x = 0;
			m_ptTeacherPointIn.y = 0;
			m_ptTeacherPointOut.x = 0;
			m_ptTeacherPointOut.y = 0;

			m_bFirstInputOK = false;
			m_bLastOutputOK = false;
            //if (ptWhite.x != 0)
            //    m_whiteBallInfo.vptTrajectory.Add(ptWhite);

            //if (ptYellow.x != 0)
            //    m_yellowBallInfo.vptTrajectory.Add(ptYellow);

            //if (ptRed.x != 0)
            //    m_redBallInfo.vptTrajectory.Add(ptRed);
		}
		public static void PushTrajectory(BALL_TRACK_INFO pBallInfo, POINT ptNew, int nBallIndex)
		{
			ptNew.x -= g_rcClip.left;
			ptNew.y -= g_rcClip.top;

			POINT ptTemp = ptNew;
			ptNew.y = ptTemp.x;
			ptNew.x = (g_rcClip.bottom - g_rcClip.top) - ptTemp.y;

			if (pBallInfo.vptTrajectory.Count() <= 0)
			{
				pBallInfo.vptTrajectory.Add(ptNew);
				return;
			}

			nEndTime[nBallIndex] = Environment.TickCount;

		// 	if (nBallIndex == 2 && !m_bStrikedTargetBall && m_nFirstImpactedBall == -1)		//red ball
		// 	{
		// 		pBallInfo.vptTrajectory.push_back(pBallInfo.vptTrajectory.front());
		// 		pBallInfo.nStableCount++;
		// 		return;
		// 	}
			//check crash
			int nPlayWidth = g_rcClip.bottom - g_rcClip.top;
			int nPlayHeight = g_rcClip.right - g_rcClip.left;

			int nSize = (int)pBallInfo.vptTrajectory.Count();
			POINT ptPrev1 = pBallInfo.vptTrajectory[pBallInfo.vptTrajectory.Count - 1];
			int nHorzMoveChange = ptPrev1.x - ptNew.x;
			int nVertMoveChange = ptPrev1.y - ptNew.y;
			if (Math.Abs(nVertMoveChange) > 100 || Math.Abs(nHorzMoveChange) > 100)
				if (pBallInfo.nLastMoveChange != 0 && (Math.Abs(nHorzMoveChange) > Math.Abs(pBallInfo.nLastMoveChange) * 2 || Math.Abs(nVertMoveChange) > Math.Abs(pBallInfo.nLastMoveChange) * 2))
 					return;

			pBallInfo.nLastMoveChange = Math.Max(Math.Abs(nHorzMoveChange), Math.Abs(nVertMoveChange));
			int[] nDist = new int[]{9999999, 9999999, 9999999, 9999999, 9999999, 9999999};
			nDist[0] = ptNew.x;
			nDist[1] = ptNew.y;
			nDist[2] = nPlayWidth - ptNew.x;
			nDist[3] = nPlayHeight - ptNew.y;

			if (nBallIndex == 0 && m_yellowBallInfo.vptTrajectory.Count() > 0 && m_redBallInfo.vptTrajectory.Count() > 0)		//white ball
			{
				POINT ptBall1;
				//if (m_nCurrentPlayer == nBallIndex && m_bStrikedTargetBall)
					ptBall1 = m_yellowBallInfo.vptTrajectory[m_yellowBallInfo.vptTrajectory.Count() - 1];
				//else
				//	ptBall1 = m_yellowBallInfo.vptTrajectory[0];

				POINT ptBall2 = m_redBallInfo.vptTrajectory[m_redBallInfo.vptTrajectory.Count() - 1];
				nDist[4] = (int)Math.Sqrt((float)((ptNew.x - ptBall1.x) * (ptNew.x - ptBall1.x) + (ptNew.y - ptBall1.y) * (ptNew.y - ptBall1.y)));
				nDist[5] = (int)Math.Sqrt((float)((ptNew.x - ptBall2.x) * (ptNew.x - ptBall2.x) + (ptNew.y - ptBall2.y) * (ptNew.y - ptBall2.y)));
			}
			else if (nBallIndex == 1 && m_whiteBallInfo.vptTrajectory.Count() > 0 && m_redBallInfo.vptTrajectory.Count() > 0)
			{
				POINT ptBall1;
				//if (m_nCurrentPlayer == nBallIndex && m_bStrikedTargetBall)
					ptBall1 = m_whiteBallInfo.vptTrajectory[m_whiteBallInfo.vptTrajectory.Count() - 1];
				//else
				//	ptBall1 = m_whiteBallInfo.vptTrajectory[0];

				POINT ptBall2 = m_redBallInfo.vptTrajectory[m_redBallInfo.vptTrajectory.Count() - 1];
				nDist[4] = (int)Math.Sqrt((float)((ptNew.x - ptBall1.x) * (ptNew.x - ptBall1.x) + (ptNew.y - ptBall1.y) * (ptNew.y - ptBall1.y)));
				nDist[5] = (int)Math.Sqrt((float)((ptNew.x - ptBall2.x) * (ptNew.x - ptBall2.x) + (ptNew.y - ptBall2.y) * (ptNew.y - ptBall2.y)));
			}
			else if (nBallIndex == 2 && m_whiteBallInfo.vptTrajectory.Count() > 0 && m_yellowBallInfo.vptTrajectory.Count() > 0)
			{
				POINT ptBall1 = m_whiteBallInfo.vptTrajectory[m_whiteBallInfo.vptTrajectory.Count() - 1];
				POINT ptBall2 = m_yellowBallInfo.vptTrajectory[m_yellowBallInfo.vptTrajectory.Count() - 1];
				nDist[4] = (int)Math.Sqrt((float)((ptNew.x - ptBall1.x) * (ptNew.x - ptBall1.x) + (ptNew.y - ptBall1.y) * (ptNew.y - ptBall1.y)));
				nDist[5] = (int)Math.Sqrt((float)((ptNew.x - ptBall2.x) * (ptNew.x - ptBall2.x) + (ptNew.y - ptBall2.y) * (ptNew.y - ptBall2.y)));
			}

			int nMinIndex = 0;
			int nMinVal = nPlayWidth * nPlayHeight;
			int i;
			for (i = 0; i < 6; i++)
			{
				if (nDist[i] < nMinVal)
				{
					nMinVal = nDist[i];
					nMinIndex = i;
				}
			}

			//===============estimate the speed of ball===================
			if (pBallInfo.vptTrajectory.Count() > BALL_SPEED_INTERVAL + 2 && m_nCurrentPlayer != -1 && nFrameCountForSpeed[nBallIndex] <= 0)		//ready for new estimation)
			{
				EstimateBallSpeed(pBallInfo, nBallIndex);
			}
			nFrameCountForSpeed[nBallIndex]--;
			//============================================================
			if (nMinVal < BALL_RADIUS_REAL * 3)
			{
				if (pBallInfo.nLastMinIndex != -1 && pBallInfo.nLastMinIndex != nMinIndex)
				{
					if (pBallInfo.nDist[pBallInfo.nLastMinIndex] < BALL_RADIUS_REAL && pBallInfo.nDist[pBallInfo.nLastMinIndex] < nDist[pBallInfo.nLastMinIndex])
					{
						CRASH_ELEMENT element = new CRASH_ELEMENT();
						element.crashType = CRASH_TYPE.HIT_BOUNT;
						element.nIndex = pBallInfo.vptTrajectory.Count() - 1;
						element.fSpeed = pBallInfo.fCurrentSpeed;
						element.pt = pBallInfo.vptTrajectory[pBallInfo.vptTrajectory.Count - 1];
						element.ltime = DateTime.Now.Millisecond;

						for (i = 0; i < 6; i++)
							pBallInfo.nDist[i] = nDist[i];

						if (nBallIndex == 0 && pBallInfo.nLastMinIndex <= 3 && nSideHitMinIndex != nMinIndex && nSideHitMinIndex != pBallInfo.nLastMinIndex)
						{
							nSideHitMinIndex = pBallInfo.nLastMinIndex;// 20180418 : Error in rail count
							m_nHitRailCount++;
							CheckTeacherType(pBallInfo, nBallIndex, nSideHitMinIndex, element);
						}
						pBallInfo.vIndexCrashed.Add(element);

						pBallInfo.nLastMinIndex = nMinIndex;
					}
					if (pBallInfo.nDist[pBallInfo.nLastMinIndex] < nMinVal)
						nMinIndex = pBallInfo.nLastMinIndex;
				}
				if (pBallInfo.nDist[nMinIndex] < nDist[nMinIndex] && pBallInfo.nDist[nMinIndex] < BALL_RADIUS_REAL)		//crashed
				{
			
					CRASH_ELEMENT element = new CRASH_ELEMENT();

					element.crashType = CRASH_TYPE.HIT_BOUNT;			
					element.nIndex = pBallInfo.vptTrajectory.Count() - 1;
					element.fSpeed = pBallInfo.fCurrentSpeed;
					element.pt = pBallInfo.vptTrajectory[pBallInfo.vptTrajectory.Count - 1];
					element.ltime = DateTime.Now.Millisecond;

					if (nBallIndex == 0 && nMinIndex <= 3 && nSideHitMinIndex != nMinIndex)
					{
						m_nHitRailCount++;
						if (pBallInfo.nDist[nMinIndex] <= 15)// || (pBallInfo.nDist[nMinIndex] <= 20 && (nDist[nMinIndex] - pBallInfo.nDist[nMinIndex]) > 20))
						{
							nSideHitMinIndex = nMinIndex;
							CheckTeacherType(pBallInfo, nBallIndex, nSideHitMinIndex, element);
						}

				
					}

					pBallInfo.vIndexCrashed.Add(element);
					pBallInfo.nDist[nMinIndex] = BALL_RADIUS_REAL * 3;
					pBallInfo.nLastMinIndex = -1;

				}
				else
				{
					for (i = 0; i < 6; i++)
						pBallInfo.nDist[i] = nDist[i];

					pBallInfo.nLastMinIndex = nMinIndex;
				}
			}
			else
			{
				for (i = 0; i < 6; i++)
						pBallInfo.nDist[i] = nDist[i];
			}
			pBallInfo.vptTrajectory.Add(ptNew);
			//
			//========check output/////////////
			if (m_nHitRailCount == m_nRailCount && m_nHitRailCount > 0)
			{
				if (m_bShowTeacherPoint)
				{
					BALL_TRACK_INFO pCurBallInfo = m_whiteBallInfo;
					if (pCurBallInfo.nLastOutputCandidate >= 0 && pCurBallInfo.vIndexCrashed.Count() > pCurBallInfo.nLastOutputCandidate)
					{
						CRASH_ELEMENT element = pCurBallInfo.vIndexCrashed[pCurBallInfo.nLastOutputCandidate];
						POINT ptLast = pCurBallInfo.vptTrajectory[pCurBallInfo.vptTrajectory.Count - 1];
				
						if (GetDotDistance(ptLast, element.pt) > 20)
						{
							element.tpi.pt = element.pt;
							element.tpi.nType = 2;
							element.tpi.nPoint = FindDiamondMark(ref element.tpi.pt, ptLast, TeachingToTracking(m_nOutSide));					
							pCurBallInfo.nLastOutputCandidate = -1;
							m_bLastOutputOK = true;
						}
					}
				}
			}
			if (m_nCurrentPlayer == -1)
			{
				nStartTime[nBallIndex] = Environment.TickCount;
				nFrameCountForSpeed[nBallIndex] = BALL_SPEED_INTERVAL;
			}

			if (nHorzMoveChange == 0 && nVertMoveChange == 0 && m_nCurrentPlayer >= 0)
			{
				pBallInfo.nStableCount++;
				if (pBallInfo.nStableCount > 2 && nBallIndex == 0)		//error, 20180413
				{
					if (m_bShowTeacherPoint)
					{
						BALL_TRACK_INFO pCurBallInfo = m_whiteBallInfo;
						if (pCurBallInfo.nLastOutputCandidate >= 0 && pCurBallInfo.vIndexCrashed.Count() > pCurBallInfo.nLastOutputCandidate)
						{
							CRASH_ELEMENT element = pCurBallInfo.vIndexCrashed[pCurBallInfo.nLastOutputCandidate];
							POINT ptLast = pCurBallInfo.vptTrajectory[pCurBallInfo.vptTrajectory.Count - 1];
							element.tpi.pt = element.pt;
							element.tpi.nType = 2;
							element.tpi.nPoint = FindDiamondMark(ref element.tpi.pt, ptLast, TeachingToTracking(m_nOutSide));
							m_bLastOutputOK = true;
						}
					}
				}
				return;
			}

			if (Math.Abs(nHorzMoveChange) > 2 || Math.Abs(nVertMoveChange) > 2)
			{
				pBallInfo.nStableCount = 0;
			}
			POINT ptStart = pBallInfo.vptTrajectory[0];

			if (!pBallInfo.bImpacted && m_nCurrentPlayer >= 0 && nBallIndex != m_nCurrentPlayer && (Math.Abs(ptStart.x - ptNew.x) >= 5 || Math.Abs(ptStart.y - ptNew.y) >= 5))		//impacted
			{
				pBallInfo.bImpacted = true;
				BALL_TRACK_INFO pPlayerBallInfo = m_nCurrentPlayer == 0 ? m_whiteBallInfo : m_yellowBallInfo;
				CRASH_ELEMENT newElement = new CRASH_ELEMENT();

 				newElement.crashType = (CRASH_TYPE)nBallIndex;
				newElement.ltime = DateTime.Now.Millisecond;
				if (m_nCurrentPlayer == 1)
					newElement.nIndex = pPlayerBallInfo.vptTrajectory.Count() - 1;
				else
					newElement.nIndex = pPlayerBallInfo.vptTrajectory.Count() - 2;
				newElement.pt = pPlayerBallInfo.vptTrajectory[newElement.nIndex];
		
				if (!m_bStrikedTargetBall)		//impacted red ball
				{
					if (nBallIndex == 1 - m_nCurrentPlayer)		//ball has been impacted 
					{
						if (pPlayerBallInfo.fAveSpeed == 0.0f)
						{
							EstimateBallSpeed(pPlayerBallInfo, m_nCurrentPlayer);
							nFrameCountForSpeed[m_nCurrentPlayer] = 0;
                            pPlayerBallInfo.fAveSpeed = BTAPI_GetAveSpeedForRealTime(); //pPlayerBallInfo.fTotalSpeed / (float)pPlayerBallInfo.nSpeedRecordCount;
						}
						m_bStrikedTargetBall = true;
					}

					if (m_nFirstImpactedBall == -1)
						m_nFirstImpactedBall = nBallIndex;
				}

				pPlayerBallInfo.vIndexCrashed.Add(newElement);
		
			}

			if (pBallInfo.nHorzMoveTotal != 0 && IsPositive(pBallInfo.nHorzMoveTotal) != IsPositive(nHorzMoveChange) && 
				nHorzMoveChange != 0)
			{
				if (IsPositive(pBallInfo.nHorzLastChange) == IsPositive(nHorzMoveChange))
				{
		//			pBallInfo.vIndexCrashed.push_back(pBallInfo.vptTrajectory.Count() - 2);
					pBallInfo.nHorzMoveTotal = 0;
					pBallInfo.nVertMoveTotal = 0;
				}
			}
			else if (pBallInfo.nHorzMoveTotal == 0 && IsPositive(pBallInfo.nHorzMoveTotal) != IsPositive(nHorzMoveChange) && 
				IsPositive(pBallInfo.nHorzLastChange) == 0)
			{
				int nLastDirectionChangeIndex = 0;

				if (pBallInfo.vIndexCrashed.Count() > 0)
				{
						//nLastDirectionChangeIndex = pBallInfo.vIndexCrashed.back();
				}
				else
					nLastDirectionChangeIndex = 0;

				if (pBallInfo.vptTrajectory.Count() - nLastDirectionChangeIndex > 3)
				{
		//			pBallInfo.vIndexCrashed.push_back(pBallInfo.vptTrajectory.Count() - 1);
					pBallInfo.nHorzMoveTotal = 0;
					pBallInfo.nVertMoveTotal = 0;
				}
			}

			if (pBallInfo.nVertMoveTotal != 0 && IsPositive(pBallInfo.nVertMoveTotal) != IsPositive(nVertMoveChange) 
				&& nVertMoveChange != 0)
			{
				if (IsPositive(pBallInfo.nVertLastChange) == IsPositive(nVertMoveChange))
				{
		//			pBallInfo.vIndexCrashed.push_back(pBallInfo.vptTrajectory.Count() - 2);
					pBallInfo.nHorzMoveTotal = 0;
					pBallInfo.nVertMoveTotal = 0;
				}
			}

			if (Math.Abs(pBallInfo.nVertMoveTotal) > 10 || Math.Abs(pBallInfo.nHorzMoveTotal) > 10)
			{
				BALL_TRACK_INFO pBall1 = null;
				BALL_TRACK_INFO pBall2 = null;

				if (nBallIndex == 0)
				{
					pBall1 = m_yellowBallInfo;
					pBall2 = m_redBallInfo;
				}
				else if (nBallIndex == 1)
				{
					pBall1 = m_whiteBallInfo;
					pBall2 = m_redBallInfo;
				}
				else if (nBallIndex == 2)
				{
					pBall1 = m_whiteBallInfo;
					pBall2 = m_yellowBallInfo;
				}

				POINT ptBall1, ptBall2;

				if (pBall1.vptTrajectory.Count() > 0 && pBall2.vptTrajectory.Count() > 0)
				{
					ptBall1 = pBall1.vptTrajectory[pBall1.vptTrajectory.Count - 1];
					ptBall2 = pBall2.vptTrajectory[pBall2.vptTrajectory.Count - 1];

					if (Math.Abs(ptNew.x - ptBall1.x) < BALL_RADIUS_REAL && Math.Abs(ptNew.y - ptBall1.y) < BALL_RADIUS_REAL)
					{
		//				pBallInfo.vIndexCrashed.push_back(pBallInfo.vptTrajectory.Count());
						pBallInfo.nHorzMoveTotal = 0;
						pBallInfo.nVertMoveTotal = 0;
					}
					else if (Math.Abs(ptNew.x - ptBall2.x) < BALL_RADIUS_REAL && Math.Abs(ptNew.y - ptBall2.y) < BALL_RADIUS_REAL)
					{
		//				pBallInfo.vIndexCrashed.push_back(pBallInfo.vptTrajectory.Count());
						pBallInfo.nHorzMoveTotal = 0;
						pBallInfo.nVertMoveTotal = 0;
					}
				}
			}
			pBallInfo.nHorzMoveTotal += nHorzMoveChange;
			pBallInfo.nVertMoveTotal += nVertMoveChange;
		}
		public static int IsPositive(int n)
		{
			int nRet = 0;

			if (n > 0)
				nRet = 1;
			else if (n < 0)
				nRet = -1;

			return nRet;
		}
		public static void EstimateBallSpeed(BALL_TRACK_INFO pBallInfo, int nBallIndex)
		{
			int i;
			{
				float fTotalMovedDistance = 0.0f;
				for (i = (int)pBallInfo.vptTrajectory.Count - (BALL_SPEED_INTERVAL - nFrameCountForSpeed[nBallIndex]) - 2; i < pBallInfo.vptTrajectory.Count - 2; i++)
				{
					if (i < 0)
						continue;
					POINT pt1 = pBallInfo.vptTrajectory[i];
					POINT pt2 = pBallInfo.vptTrajectory[i + 1];
					float fDistance = (float)(Math.Sqrt(((pt1.x - pt2.x) * (pt1.x - pt2.x) + (pt1.y - pt2.y) * (pt1.y - pt2.y))) * m_fMillPerDot);
					fTotalMovedDistance += fDistance;
				}

				float fTimeElapsed = (float)(nEndTime[nBallIndex] - nStartTime[nBallIndex]) / (float)1000;
				if (fTimeElapsed > 0.0f)
				{
					float fSpeed = fTotalMovedDistance / fTimeElapsed;
					if (fSpeed >= 1.0f)
					{
						if (!m_bStrikedTargetBall && (pBallInfo.fCurrentSpeed / fSpeed) > 2.0f)
							fSpeed = pBallInfo.fCurrentSpeed * 0.8f;

						pBallInfo.fCurrentSpeed = fSpeed;

						if (!m_bStrikedTargetBall && pBallInfo.fCurrentSpeed >= 100.0f)
						{
							pBallInfo.fTotalSpeed += pBallInfo.fCurrentSpeed;
							pBallInfo.nSpeedRecordCount++;
						}
						//if (nBallIndex == 0)
						//	TRACE(_T("Ball : %d ,  speed : %f\n"), nBallIndex, pBallInfo.fCurrentSpeed);
					}
				}
			}
			nFrameCountForSpeed[nBallIndex] = BALL_SPEED_INTERVAL;
			nStartTime[nBallIndex] = Environment.TickCount;
		}


		public static int TeachingToTracking(int n)
		{
			return (n + 1) & 3;	//mod 4
		}
		public static int FindDiamondMark(ref POINT ptCrashed, POINT ptPrev, int nSide)
		{
			int nFrom = 0;
			int nTo = 0;
			int nRet = 0;
			int nFocusValue = 0;
			int nStep = (int)((float)m_szCanvas.Width * 2.229166666666668f / 10.91666666666667f + 0.5f);
			int nOffset = 0;
			int nTableMargin = (int)((float)m_szCanvas.Width / 10.91666666666667f + 0.5f);
			int nDistToRail = (int)(nTableMargin * 0.3541666666666667f + 0.5f);
			float fScaleX = (float)(m_szCanvas.Width - nTableMargin * 2) / (float)(g_rcClip.bottom - g_rcClip.top);
			float fScaleY = (float)(m_szCanvas.Height - nTableMargin * 2) / (float)(g_rcClip.right - g_rcClip.left);
			switch (nSide)
			{
			case 1:
				{
					nFrom = 0;
					nTo = 4;

					nOffset = (int)((ptCrashed.x - ptPrev.x) * (float)nDistToRail / (float)(Math.Abs(ptCrashed.y - ptPrev.y)));
					ptCrashed.x += nOffset;

					nFocusValue = (int)(ptCrashed.x * fScaleX + 0.5f);
					break;
				}
			case 2:
				{
					nFrom = 5;
					nTo = 13;		

					nOffset = (int)((ptCrashed.y - ptPrev.y) * (float)nDistToRail / (float)(Math.Abs(ptCrashed.x - ptPrev.x)));
					ptCrashed.y += nOffset;

					nFocusValue = (int)(ptCrashed.y * fScaleY + 0.5f);
					break;
				}
			case 3:
				{
					nFrom = 14;
					nTo = 18;
			

					nOffset = (int)((ptCrashed.x - ptPrev.x) * (float)nDistToRail / (float)(Math.Abs(ptCrashed.y - ptPrev.y)));
					ptCrashed.x += nOffset;

					nFocusValue = (int)(ptCrashed.x * fScaleX + 0.5f);

					break;
				}
			case 0:
				{
					nFrom = 19;
					nTo = 27;

					nOffset = (int)((ptCrashed.y - ptPrev.y) * (float)nDistToRail / (float)(Math.Abs(ptCrashed.x - ptPrev.x)));
					ptCrashed.y += nOffset;

					nFocusValue = (int)(ptCrashed.y * fScaleY + 0.5f);
					break;
				}
			}
			int nMargin = 0;
			for (int i = nFrom; i <= nTo - 1; i++)
			{
				if (nFocusValue >= ((i - nFrom) * nStep +  nMargin) && nFocusValue <= ((i - nFrom + 1) * nStep + nMargin))
				{
					float fRatio = (float)(nFocusValue - (i - nFrom) * nStep) / (float)nStep;
					nRet = m_nMarkValue[i] + (int)((m_nMarkValue[i + 1] - m_nMarkValue[i]) * fRatio + 0.5f);
					break;
				}
			}

			return nRet;
		}
		public static bool CheckTeacherType(BALL_TRACK_INFO pBallInfo, int nBallIndex, int nMinSideIndex, CRASH_ELEMENT lpElement)
		{
            if (pBallInfo.vIndexCrashed.Count == 0)
                return false;

			if (nBallIndex != 0	)
				return false;

			if (m_nInSide == -1 || m_nOutSide == -1)
				return false;

			POINT ptPrev = pBallInfo.vIndexCrashed[pBallInfo.vIndexCrashed.Count - 1].pt;

			int nLastIndex = pBallInfo.vIndexCrashed.Count - 2;
			while (GetDotDistance(ptPrev, lpElement.pt) < 10 && nLastIndex >= 0)
			{
				ptPrev = pBallInfo.vIndexCrashed[nLastIndex--].pt;
			}
		//get rail number
			int nInSide = TeachingToTracking(m_nInSide);
			int nOutSide = TeachingToTracking(m_nOutSide);
			POINT ptCrashExtended = lpElement.pt;
			if (nMinSideIndex == nInSide && !pBallInfo.bFirstInputPresented)
			{
				m_nTeacherMarkIn = FindDiamondMark(ref ptCrashExtended, ptPrev, nMinSideIndex);
				lpElement.tpi.nType = 1;
				lpElement.tpi.nPoint = m_nTeacherMarkIn;
				lpElement.tpi.nSide = nMinSideIndex;
				lpElement.tpi.pt = ptCrashExtended;
				pBallInfo.bFirstInputPresented = true;
				m_bFirstInputOK = true;
		
				return true;
			}
	
			if (nMinSideIndex == nOutSide && m_nHitRailCount == m_nRailCount && m_nHitRailCount > 0)
			{
				m_nTeacherMarkOut = FindDiamondMark(ref ptCrashExtended, ptPrev, nMinSideIndex);
				pBallInfo.nLastOutputCandidate = pBallInfo.vIndexCrashed.Count;
				pBallInfo.nLastOutputMark = lpElement.tpi.nPoint = m_nTeacherMarkOut;
				lpElement.tpi.pt = ptCrashExtended;
				return true;
			}
			pBallInfo.nLastOutputCandidate = -1;
			pBallInfo.nLastOutputMark = 0;
			pBallInfo.bFirstInputPresented = true;
			return false;
		}

		public static int GetDotDistance(POINT pt1, POINT pt2)
		{
			return (int)(Math.Sqrt((float)((pt1.x - pt2.x) * (pt1.x - pt2.x) + (pt1.y - pt2.y) * (pt1.y - pt2.y))) + 0.5f);
		}

		public static int GetCurrentStep()
		{
			return m_nCurrentStep;
		}

		public static void SetCurrentStep(int nStep)
		{
			m_nCurrentStep = nStep;
		}

		public static bool CheckCurrentState()
		{
			try
			{
				int nThreshold = 40;
				bool bNoMovingBall = false;
				if (m_nCurrentStep == 0)		//
				{
					if (m_whiteBallInfo.nStableCount > nThreshold && m_yellowBallInfo.nStableCount > nThreshold &&
						m_redBallInfo.nStableCount > nThreshold && m_nCurrentPlayer >= 0)
					{
						m_nCurrentStep = 1;
						drawProc(m_nCurrentStep);
						bNoMovingBall = true;
						if (!m_bStrikedTargetBall)
						{
							if (m_nCurrentPlayer == 0)
								m_whiteBallInfo.fAveSpeed = BTAPI_GetAveSpeedForRealTime(); // m_whiteBallInfo.fTotalSpeed / (float)m_whiteBallInfo.nSpeedRecordCount;
							else
								m_yellowBallInfo.fAveSpeed = BTAPI_GetAveSpeedForRealTime(); // (float)m_yellowBallInfo.nSpeedRecordCount;
						}
					}
				}

				if (m_nCurrentPlayer == -1)
				{
					int nMoveTotalWhite = m_whiteBallInfo.nHorzMoveTotal * m_whiteBallInfo.nHorzMoveTotal + m_whiteBallInfo.nVertMoveTotal * m_whiteBallInfo.nVertMoveTotal;
					int nMoveTotalYellow = m_yellowBallInfo.nHorzMoveTotal * m_yellowBallInfo.nHorzMoveTotal + m_yellowBallInfo.nVertMoveTotal * m_yellowBallInfo.nVertMoveTotal;

					if (nMoveTotalWhite > 10 || nMoveTotalYellow > 10)
					{
						if (nMoveTotalWhite > nMoveTotalYellow)
							m_nCurrentPlayer = 0;
						else if (nMoveTotalYellow > nMoveTotalWhite)
							m_nCurrentPlayer = 1;

						CRASH_ELEMENT crashElement = new CRASH_ELEMENT();

						if (m_whiteBallInfo.vptTrajectory.Count() > 0 && m_whiteBallInfo.vIndexCrashed.Count == 0)
						{
							crashElement.ltime = DateTime.Now.Millisecond;
							crashElement.nIndex = 0;
							crashElement.pt = m_whiteBallInfo.vptTrajectory[0];
							crashElement.fSpeed = 0;
							m_whiteBallInfo.vIndexCrashed.Add(crashElement);
							nStartTime[0] = crashElement.ltime;
						}

						crashElement = new CRASH_ELEMENT();
						if (m_yellowBallInfo.vptTrajectory.Count() > 0 && m_yellowBallInfo.vIndexCrashed.Count == 0)
						{
							crashElement.nIndex = 0;
							crashElement.fSpeed = 0;
							crashElement.pt = m_yellowBallInfo.vptTrajectory[0];
							m_yellowBallInfo.vIndexCrashed.Add(crashElement);
							nStartTime[1] = crashElement.ltime;
						}

						crashElement = new CRASH_ELEMENT();
						if (m_redBallInfo.vptTrajectory.Count() > 0 && m_redBallInfo.vIndexCrashed.Count == 0)
						{
							crashElement.nIndex = 0;
							crashElement.fSpeed = 0;
							crashElement.pt = m_redBallInfo.vptTrajectory[0];
							m_redBallInfo.vIndexCrashed.Add(crashElement);
							nStartTime[2] = crashElement.ltime;
						}
					}
				}
				return bNoMovingBall;
			}
			catch(Exception e)
			{
				Console.Write(e.Message);
			}

			return false;
		}

		public static int CheckValidShot()
		{
			if (m_bStrikedTargetBall)
				return 0;

			return m_nFirstImpactedBall;
		}

        public static void ClearHistory()
        {
            BallTrackAPI.BTAPI_ClearHistory();
            if (m_playHistory[0] != null)
            {
                m_playHistory[0].Clear();
                m_playHistory[1].Clear();
                m_playHistory[2].Clear();
            }
        }
		public static bool LoadShotHistory(string strTeacherName)
		{
			string strDirHistory = Environment.CurrentDirectory;
			strDirHistory += "\\History";
			if (!Directory.Exists(strDirHistory))
				Directory.CreateDirectory(strDirHistory);

			strDirHistory += "\\" + strTeacherName;
			if (!Directory.Exists(strDirHistory))
				Directory.CreateDirectory(strDirHistory);

			string strShotMetaFile = strDirHistory + "\\meta.xml";
			string strShotDataFile = strDirHistory + "\\shot.dat";

			if (!ShotHistory.LoadFromFile(strShotMetaFile, ref shotHistory))
				return false;
			if (!BallTrackAPI.BTAPI_LoadHistoryFromFile(strShotDataFile))
				return false;

			return true;
		}
		public static void DeleteShotHistory(int nShotIndex)
		{
			ShotMetaInfo metaInfo = shotHistory.Shot[nShotIndex];
			BTAPI_DeleteShotRecord(metaInfo.nPlayer, metaInfo.nShot);
			shotHistory.Shot.RemoveAt(nShotIndex);

			for (int i = nShotIndex; i < shotHistory.Shot.Count; i++)
			{
				if (shotHistory.Shot[i].nPlayer != metaInfo.nPlayer)
					continue;

				if (shotHistory.Shot[i].nShot > metaInfo.nShot)		//shrink
					shotHistory.Shot[i].nShot--;
			}
		}
		public static bool SaveShotHistory(string strTeacherName)
		{
			string strDirHistory = Environment.CurrentDirectory;
			strDirHistory += "\\History";
			if (!Directory.Exists(strDirHistory))
				Directory.CreateDirectory(strDirHistory);

			strDirHistory += "\\" + strTeacherName;
			if (!Directory.Exists(strDirHistory))
				Directory.CreateDirectory(strDirHistory);

			string strShotMetaFile = strDirHistory + "\\meta.xml";
			string strShotDataFile = strDirHistory + "\\shot.dat";

			if (BallTrackAPI.shotHistory.Shot.Count == 0)
			{
				try
				{
					File.Delete(strShotDataFile);
					File.Delete(strShotMetaFile);
				}
				catch (Exception)
				{

				}
			}
			else
			{
				if (!ShotHistory.SaveToFile(strShotMetaFile, shotHistory))
					return false;
				if (!BallTrackAPI.BTAPI_SaveHistoryToFile(strShotDataFile))
					return false;
			}
			return true;
		}

		public static void DrawReplay(Graphics g, int nStep, int nPlayer, POINT ptWhite, POINT ptYellow,
			POINT ptRed, POINT ptStartWhite, POINT ptStartYellow, POINT ptStartRed,
			int nHitCountWhite, POINT[] ptAryHitPosWhite,
			int nHitCountYellow, POINT[] ptAryHitPosYellow,
			int nHitCountRed, POINT[] ptAryHitPosRed,
			bool bShowBallSpeed, float fBallSpeed, int nImpactState, int nTeacherMarkIn, int nTeacherMarkOut, POINT ptTeacherIn, POINT ptTeacherOut)
		{

			if (nStep == 0)
			{
				DrawTrackingState(g, nStep, ptStartWhite, nHitCountWhite, ptAryHitPosWhite, ptWhite, nPlayer == 0, false, Color.White, nTeacherMarkIn, nTeacherMarkOut, ptTeacherIn, ptTeacherOut);
				DrawTrackingState(g, nStep, ptStartYellow, nHitCountYellow, ptAryHitPosYellow, ptYellow, nPlayer == 1, false, Color.Yellow, nTeacherMarkIn, nTeacherMarkOut, ptTeacherIn, ptTeacherOut);
				DrawTrackingState(g, nStep, ptStartRed, nHitCountRed, ptAryHitPosRed, ptRed, false, false, Color.Red, nTeacherMarkIn, nTeacherMarkOut, ptTeacherIn, ptTeacherOut);

				if (bShowBallSpeed == true)// && nImpactState != 0)
				{
					if (nPlayer == 0)
					{
                        Rectangle rcSpeedLabel = new Rectangle(ptWhite.x - 140, ptWhite.y + 10, 130, 20);

                        if (rcSpeedLabel.X < 0)
                        {
                            rcSpeedLabel.X = ptWhite.x + 10;
                            rcSpeedLabel.Width = 130;
                        }

						BallSpeedDisplay(g, rcSpeedLabel.X, rcSpeedLabel.Y, rcSpeedLabel.Width, rcSpeedLabel.Height, fBallSpeed, nImpactState, Color.White);
					}
					else if (nPlayer == 1)
					{
                        Rectangle rcSpeedLabel = new Rectangle(ptYellow.x - 140, ptYellow.y + 10, 130, 20);

                        if (rcSpeedLabel.X < 0)
                        {
                            rcSpeedLabel.X = ptYellow.x + 10;
                            rcSpeedLabel.Width = 130;
                        }

                        BallSpeedDisplay(g, rcSpeedLabel.X, rcSpeedLabel.Y, rcSpeedLabel.Width, rcSpeedLabel.Height, fBallSpeed, nImpactState, Color.Yellow);
					}
				}
			}
			else if (nStep == 1)
			{
				DrawTrackingState(g, nStep, ptStartWhite, nHitCountWhite, ptAryHitPosWhite, ptWhite, nPlayer == 0, nStep == 1, Color.White, nTeacherMarkIn, nTeacherMarkOut, ptTeacherIn, ptTeacherOut);
				DrawTrackingState(g, nStep, ptStartYellow, nHitCountYellow, ptAryHitPosYellow, ptYellow, nPlayer == 1, nStep == 1, Color.Yellow, nTeacherMarkIn, nTeacherMarkOut, ptTeacherIn, ptTeacherOut);
				DrawTrackingState(g, nStep, ptStartRed, nHitCountRed, ptAryHitPosRed, ptRed, false, nStep == 1, Color.Red, nTeacherMarkIn, nTeacherMarkOut, ptTeacherIn, ptTeacherOut);

				if (bShowBallSpeed == true)
				{
					if (nPlayer == 0)
					{
						BallSpeedDisplay(g, ptWhite.x, ptWhite.y, 100, 30, fBallSpeed, nImpactState, Color.White);
					}
					else if (nPlayer == 1)
					{
						BallSpeedDisplay(g, ptYellow.x, ptYellow.y, 100, 30, fBallSpeed, nImpactState, Color.Yellow);
					}
				}
			}
			else if (nStep == 2)
			{
				DrawBall(g, ptWhite, nBallRadius, Color.White, true, true);
				DrawBall(g, ptYellow, nBallRadius, Color.Yellow, true, true);
				DrawBall(g, ptRed, nBallRadius, Color.Red, true, true);

				if (nPlayer == 0)
				{
					DrawMark(g, ptYellow, true, true, Color.Black);
					DrawMark(g, ptWhite, true, false, Color.Black);
					DrawMark(g, ptRed, true, false, Color.Black);
				}
				else
				{
					DrawMark(g, ptWhite, true, true, Color.Black);
					DrawMark(g, ptYellow, true, false, Color.Black);
					DrawMark(g, ptRed, true, false, Color.Black);
				}
			}
		}

		public static void DrawTrackingState(Graphics g, int nState, POINT ptStart, int nHitCount, POINT[] ptAryHitPos, POINT ptBall, bool bDrawBlackDot, bool bLineIsSolid, Color clrLine,
			int nTeacherMarkIn, int nTeacherMarkOut, POINT ptTeacherIn, POINT ptTeacherOut)
		{
			DrawBall(g, ptStart, nBallRadius, clrLine, bLineIsSolid, false);
			DrawMark(g, ptStart, bDrawBlackDot, nState <= 1, Color.Black);

			if (nHitCount > 0)
			{
				for (int i = 0; i < nHitCount - 1; i++)
				{
					POINT ptLineStart = ptAryHitPos[i];
					POINT ptLineEnd = ptAryHitPos[i + 1];

					DrawLine(g, ptLineStart, ptLineEnd, bLineIsSolid, clrLine);
				}
				if (bDrawBlackDot && (nTeacherMarkIn >= 0 || nTeacherMarkOut >= 0))
				{
					int nDiamondMarkSize = 6;
					if (m_bShowTeacherPoint)
					{
						if (nTeacherMarkIn >= 0)
						{
							using (Brush brushIn = new SolidBrush(Color.FromArgb(255, 0, 0)))
							{
								g.FillEllipse(brushIn, new Rectangle(m_ptMarkIn.X - nDiamondMarkSize, m_ptMarkIn.Y - nDiamondMarkSize, nDiamondMarkSize * 2, nDiamondMarkSize * 2));
							}

							string strMark = nTeacherMarkIn.ToString();
							using (Font font = new Font("Arial", 13))
							{
								using (Brush whiteBrush = new SolidBrush(Color.White))
								{
									PointF ptMark = new PointF(m_ptTextForMarkIn.X, m_ptTextForMarkIn.Y);
									g.DrawString(strMark, font, whiteBrush, ptMark);
								}
							}
						}

						if (nTeacherMarkOut >= 0)
						{
							using (Brush brushOut = new SolidBrush(Color.FromArgb(0, 175, 80)))
							{
								g.FillEllipse(brushOut, new Rectangle(m_ptMarkOut.X - nDiamondMarkSize, m_ptMarkOut.Y - nDiamondMarkSize, nDiamondMarkSize * 2, nDiamondMarkSize * 2));
							}

							string strMark = nTeacherMarkOut.ToString();

							using (Font font = new Font("Arial", 13))
							{
								using (Brush whiteBrush = new SolidBrush(Color.White))
								{
									PointF ptMark = new PointF(m_ptTextForMarkOut.X, m_ptTextForMarkOut.Y);
									g.DrawString(strMark, font, whiteBrush, ptMark);
								}
							}
						}
					}
				}
				DrawLine(g, ptAryHitPos[nHitCount - 1], ptBall, bLineIsSolid, clrLine);
			}
			else
			{
				DrawLine(g, ptStart, ptBall, bLineIsSolid, clrLine);
			}
			DrawBall(g, ptBall, nBallRadius, clrLine, bLineIsSolid, false);
			if (bDrawBlackDot)
			{
				DrawMark(g, ptBall, bDrawBlackDot, false, Color.Black);
			}
		}
		public static void DrawBall(Graphics g, POINT pt, int nRadius, Color clrBall, bool bSolidLine, bool bFill)
		{
			if (pt.x == 0 && pt.y == 0)
				return;
			Rectangle rcBall = new Rectangle();
			rcBall.X = pt.x - nRadius;
			rcBall.Y = pt.y - nRadius;
			rcBall.Width = nRadius * 2;
			rcBall.Height = nRadius * 2;

			if (bSolidLine)
			{
				if (bFill)
				{
					using (SolidBrush brush = new SolidBrush(clrBall))
					{
						g.FillEllipse(brush, rcBall);
					}
				}
				else
				{
					using (Pen pen = new Pen(clrBall, 2.0f))
					{
						g.DrawEllipse(pen, rcBall);
					}
				}
			}
			else
			{
				using (Pen pen = new Pen(clrBall, 2.0f))
				{
					pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
					g.DrawEllipse(pen, rcBall);
				}
			}
		}
		public static void DrawLine(Graphics g, POINT ptStart, POINT ptEnd, bool bSolidLine, Color clrLine)
		{
			using (Pen pen = new Pen(clrLine, 2.0f))
			{
				if (!bSolidLine)
					pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

				Point pt1 = new Point(ptStart.x, ptStart.y);
				Point pt2 = new Point(ptEnd.x, ptEnd.y);

				g.DrawLine(pen, pt1, pt2);
			}
		}
		public static void DrawMark(Graphics g, POINT pt, bool bDrawDot, bool bDrawCross, Color clrMark)
		{
			if (pt.x == 0 && pt.y == 0)
				return;

			int nCrossSize = 10;
			if (bDrawCross)
			{
				using (Pen pen = new Pen(clrMark))
				{
					g.DrawLine(pen, pt.x - nCrossSize, pt.y, pt.x + nCrossSize, pt.y);
					g.DrawLine(pen, pt.x, pt.y - nCrossSize, pt.x, pt.y + nCrossSize);
				}
			}

			if (bDrawDot)
			{
				using (SolidBrush brush = new SolidBrush(clrMark))
				{
					g.FillEllipse(brush, pt.x - 2, pt.y - 2, 4, 4);
				}
			}
		}
		public static void BallSpeedDisplay(Graphics g, int left, int top, int nWidth, int nHeight, float fBallSpeed, int nImpactState, Color clrBackground)
		{
            if (fBallSpeed <= 0.0f)
                return;

			Pen blackPen = new Pen(Color.Black);
			using (Brush br = new SolidBrush(clrBackground))
			{
				g.FillRectangle(br, left, top, nWidth, nHeight);
				g.DrawRectangle(blackPen, left, top, nWidth, nHeight);
				fBallSpeed = fBallSpeed / 1000.0f;
				using (Font arialBold = new Font("Tahoma", 12, FontStyle.Bold))
				{
					string strSpeed = fBallSpeed.ToString("0.##");
					strSpeed += "m/s";
					Size textSize = TextRenderer.MeasureText(strSpeed, arialBold);

					TextRenderer.DrawText(g, strSpeed, arialBold,
							new Rectangle(left + (nWidth - textSize.Width) / 2, top + (nHeight - textSize.Height) / 2, textSize.Width, textSize.Height), Color.Black);
				}
			}
		}

		public static void ResetReplayTrajectory()
		{
			BallTrackAPI.drawInfo = new DRAW_INFO();
			BallTrackAPI.m_WhiteHitPos = new POINT[128];
			BallTrackAPI.m_YellowHitPos = new POINT[128];
			BallTrackAPI.m_RedHitPos = new POINT[128];
		}
		public static void ShowTeacherPoint(bool bShow)
		{
			BTAPI_ShowTeacherPoint(bShow);
			m_bShowTeacherPoint = bShow;
		}
        public static void ShowBallSpeed(bool bShow)
        {
            BTAPI_ShowBallSpeed(bShow);
            m_bShowBallSpeed = bShow;
        }
		public static void DrawTrajectory(Graphics g, BALL_TRACK_INFO pBallInfo, int nStep, float fScaleX, float fScaleY, int nOffsetX, int nOffsetY, int nColorIndex)
        {
            if (pBallInfo.vptTrajectory.Count() <= 0)
                return;

            Color clrBall = Color.White;

            if (nColorIndex == 0)
                clrBall = Color.White;
            else if (nColorIndex == 1)
                clrBall = Color.Yellow;
            else if (nColorIndex == 2)
                clrBall = Color.Red;

            using (Pen penBlack = new Pen(Color.Black, 1.0f),
                penBall = new Pen(clrBall, 2.0f),
                penDash = new Pen(clrBall, 2.0f))
            {
                penDash.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                POINT ptOrg = pBallInfo.vptTrajectory[0];
                Point ptBall = new Point(ptOrg.x, ptOrg.y);

                ptBall.X = (int)(ptBall.X * fScaleX + 0.5f);
                ptBall.Y = (int)(ptBall.Y * fScaleY + 0.5f);

                ptBall.Offset(nOffsetX, nOffsetY);

                Rectangle rcBall = new Rectangle(ptBall.X - nBallRadius, ptBall.Y - nBallRadius, nBallRadius * 2, nBallRadius * 2);
                Pen penDraw = null;
                if (nStep == 0)
                {
                    penDraw = penDash;
                }
                else if (nStep == 1)
                {
                    penDraw = penBall;
                }
                else if (nStep == 2)
                {
                    penDraw = penBall;
                }

                if (nStep < 2)		//draw black mark
                {
                    g.DrawEllipse(penDraw, rcBall);

                    if (BallTrackAPI.m_nPrevPlayer == -1 || BallTrackAPI.m_nCurrentPlayer >= 0)		//Game started or player striked a ball
                    {
                        if (nColorIndex == BallTrackAPI.m_nCurrentPlayer)		//current player's ball
                        {
                            using (Brush brBlack = new SolidBrush(Color.Black))
                            {
                                g.FillEllipse(brBlack, ptBall.X - 3, ptBall.Y - 3, 6, 6);
                            }
                        }

                        g.DrawLine(penBlack, ptBall.X - nBallRadius - 2, ptBall.Y, ptBall.X + nBallRadius + 2, ptBall.Y);
                        g.DrawLine(penBlack, ptBall.X, ptBall.Y - nBallRadius - 2, ptBall.X, ptBall.Y + nBallRadius + 2);
                    }
                    else							//Ready for next player
                    {
                        g.FillEllipse(new SolidBrush(Color.Black), ptBall.X - 3, ptBall.Y - 3, 6, 6);

                        if (nColorIndex == (1 - BallTrackAPI.m_nPrevPlayer))
                        {
                            g.DrawLine(penBlack, ptBall.X - nBallRadius - 2, ptBall.Y, ptBall.X + nBallRadius + 2, ptBall.Y);
                            g.DrawLine(penBlack, ptBall.X, ptBall.Y - nBallRadius - 2, ptBall.X, ptBall.Y + nBallRadius + 2);
                        }
                    }
                }

                if (pBallInfo.vptTrajectory.Count <= 1 || BallTrackAPI.m_nCurrentPlayer < 0)
                    return;
                //draw trajectory
                Point ptTracjectory;

                if (nStep < 2)
                {
                    ptTracjectory = new Point(pBallInfo.vptTrajectory[0].x, pBallInfo.vptTrajectory[0].y);

                    ptTracjectory.X = (int)(ptTracjectory.X * fScaleX + 0.5f);
                    ptTracjectory.Y = (int)(ptTracjectory.Y * fScaleY + 0.5f);

                    ptTracjectory.Offset(nOffsetY, nOffsetY);

                    //draw speed of ball
                    if (nStep == 1 && BallTrackAPI.m_nCurrentPlayer == nColorIndex && BallTrackAPI.m_bShowBallSpeed)
                    {
                        Rectangle rcSpeedLabel = new Rectangle(ptTracjectory.X - 140, ptTracjectory.Y + 10, 130, 20);

                        if (rcSpeedLabel.X < 0)
                        {
                            rcSpeedLabel.X = rcBall.X + 10;
                            rcSpeedLabel.Width = 130;
                        }
                        
                        string strSpeed = "";
						if (BallTrackAPI.CheckValidShot() == 0 || m_bShowTeacherPoint)
                            strSpeed = (pBallInfo.fAveSpeed / 1000.0f).ToString("0.00") + "m/s";
                        else//wrong or no impact
                        {
                            if (BallTrackAPI.CheckValidShot() < 0)		//no impact
                                strSpeed = "NO IMPACT";
                            else if (BallTrackAPI.CheckValidShot() == 2)
                                strSpeed = "WRONG IMPACT";
                        }
                        using (Font drawFont = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Pixel))
                        {
							g.FillRectangle(new SolidBrush(clrBall), rcSpeedLabel);

							Size szText = TextRenderer.MeasureText(strSpeed, drawFont);
							PointF ptText = new PointF();
							ptText.X = (float)(rcSpeedLabel.X + (rcSpeedLabel.Width - szText.Width) / 2);
							ptText.Y = (float)(rcSpeedLabel.Y + (rcSpeedLabel.Height - szText.Height) / 2);

							g.DrawString(strSpeed, drawFont, new SolidBrush(Color.Black), ptText.X, ptText.Y);
                        }
                    }
                    List<Point> ptDrawList = new List<Point>();
                    ptDrawList.Add(ptBall);
                    for (int i = 0; i < (int)pBallInfo.vIndexCrashed.Count(); i++)
                    {
                        ptTracjectory = new Point(pBallInfo.vptTrajectory[pBallInfo.vIndexCrashed[i].nIndex].x, pBallInfo.vptTrajectory[pBallInfo.vIndexCrashed[i].nIndex].y);
                        ptTracjectory.X = (int)(ptTracjectory.X * fScaleX + 0.5f);
                        ptTracjectory.Y = (int)(ptTracjectory.Y * fScaleY + 0.5f);

                        ptTracjectory.Offset(nOffsetX, nOffsetY);

                        ptDrawList.Add(ptTracjectory);

                        if (m_bShowTeacherPoint)
                        {
                        	
                        	int nDiamondMarkSize = 6;
                        	int nTextOffset = 20;
                        	if (pBallInfo.vIndexCrashed[i].tpi.nType == 1)
                        	{
                                ptTracjectory = new Point(pBallInfo.vIndexCrashed[i].tpi.pt.x, pBallInfo.vIndexCrashed[i].tpi.pt.y);
                                ptTracjectory.X = (int)(ptTracjectory.X * fScaleX + 0.5f);
                                ptTracjectory.Y = (int)(ptTracjectory.Y * fScaleY + 0.5f);

                                ptTracjectory.Offset(nOffsetY, nOffsetY);

                                Point ptText = ptTracjectory;
                        		if (m_nInSide == 0)
                        		{
                        			ptTracjectory.Y = (int)(nDiamondMargin * fScaleY + 0.5f);
                        			ptText.X = ptTracjectory.X - nTextOffset;
                        		}
                        		else if (m_nInSide == 1)
                        		{
                        			ptTracjectory.X = (int)g.ClipBounds.Width - (int)(nDiamondMargin * fScaleX + 0.5f);
                        			ptText.Y = ptTracjectory.Y + nTextOffset;
                        		}
                        		else if (m_nInSide == 2)
                        		{
                        			ptTracjectory.Y = (int)g.ClipBounds.Height - (int)(nDiamondMargin * fScaleY + 0.5f);
                        			ptText.X = ptTracjectory.X + nTextOffset;
                        		}
                        		else if (m_nInSide == 3)
                        		{
                        			ptTracjectory.X = (int)(nDiamondMargin * fScaleX + 0.5f);
                        			ptText.Y = ptTracjectory.Y - nTextOffset;
                        		}
                                
                                using (Brush brushIn = new SolidBrush(Color.FromArgb(255, 0, 0)))
                                {
                                    g.FillEllipse(brushIn, new Rectangle(ptTracjectory.X - nDiamondMarkSize, ptTracjectory.Y - nDiamondMarkSize, nDiamondMarkSize * 2, nDiamondMarkSize * 2));
                                }

                                string strMark = pBallInfo.vIndexCrashed[i].tpi.nPoint.ToString();
                                using (Font font = new Font("Arial", 13))
                                {
                                    using (Brush whiteBrush = new SolidBrush(Color.White))
                                    {
                                        PointF ptMark = new PointF(ptText.X, ptText.Y);
                                        g.DrawString(strMark, font, whiteBrush, ptMark);
                                    }
                                }
                        	}
                        	else if (pBallInfo.vIndexCrashed[i].tpi.nType == 2)
                        	{
                        		ptTracjectory = new Point(pBallInfo.vIndexCrashed[i].tpi.pt.x, pBallInfo.vIndexCrashed[i].tpi.pt.y);
                        		ptTracjectory.X = (int)(ptTracjectory.X * fScaleX + 0.5f);
                        		ptTracjectory.Y = (int)(ptTracjectory.Y * fScaleY + 0.5f);

                        		ptTracjectory.Offset(nOffsetY, nOffsetY);

                        		Point ptText = ptTracjectory;
                        		if (m_nOutSide == 0)
                        		{
                        			ptTracjectory.Y = (int)(nDiamondMargin * fScaleY + 0.5f);
                        			ptText.X = ptTracjectory.X - nTextOffset;
                        		}
                        		else if (m_nOutSide == 1)
                        		{
                        			ptTracjectory.X = (int)g.ClipBounds.Width - (int)(nDiamondMargin * fScaleX + 0.5f);
                        			ptText.Y = ptTracjectory.Y + nTextOffset;
                        		}
                        		else if (m_nOutSide == 2)
                        		{
                        			ptTracjectory.Y = (int)g.ClipBounds.Height - (int)(nDiamondMargin * fScaleY + 0.5f);
                        			ptText.X = ptTracjectory.X + nTextOffset;
                        		}
                        		else if (m_nOutSide == 3)
                        		{
                        			ptTracjectory.X = (int)(nDiamondMargin * fScaleX + 0.5f);
                        			ptText.Y = ptTracjectory.Y - nTextOffset;
                        		}

                                using (Brush brushOut = new SolidBrush(Color.FromArgb(0, 175, 80)))
                                {
                                    g.FillEllipse(brushOut, new Rectangle(ptTracjectory.X - nDiamondMarkSize, ptTracjectory.Y - nDiamondMarkSize, nDiamondMarkSize * 2, nDiamondMarkSize * 2));
                                }

                                string strMark = pBallInfo.vIndexCrashed[i].tpi.nPoint.ToString();

                                using (Font font = new Font("Arial", 13))
                                {
                                    using (Brush whiteBrush = new SolidBrush(Color.White))
                                    {
                                        PointF ptMark = new PointF(ptText.X, ptText.Y);
                                        g.DrawString(strMark, font, whiteBrush, ptMark);
                                    }
                                }
                        	}
                        }
                    }

                    ptTracjectory = new Point(pBallInfo.vptTrajectory[pBallInfo.vptTrajectory.Count() - 2].x, pBallInfo.vptTrajectory[pBallInfo.vptTrajectory.Count() - 2].y);
                    ptTracjectory.X = (int)(ptTracjectory.X * fScaleX + 0.5f);
                    ptTracjectory.Y = (int)(ptTracjectory.Y * fScaleY + 0.5f);

                    ptTracjectory.Offset(nOffsetY, nOffsetY);
                    ptDrawList.Add(ptTracjectory);

                    rcBall.X = ptTracjectory.X - nBallRadius;
                    rcBall.Y = ptTracjectory.Y - nBallRadius;
                    rcBall.Width = nBallRadius * 2;
                    rcBall.Height = nBallRadius * 2;

                    g.DrawLines(penDraw, ptDrawList.ToArray());
                    g.DrawEllipse(penDraw, rcBall);

                    if (nStep == 0 && !BallTrackAPI.m_bStrikedTargetBall && nColorIndex == BallTrackAPI.m_nCurrentPlayer && pBallInfo.fCurrentSpeed > 50.0f && BallTrackAPI.m_bShowBallSpeed)
                    {
                        Rectangle rcSpeedLabel = new Rectangle();
                        rcSpeedLabel.X = rcBall.X - 140;
                        rcSpeedLabel.Y = rcBall.Y + 10;
                        rcSpeedLabel.Width = 130;
                        rcSpeedLabel.Height = 20;

                        if (rcSpeedLabel.X < 0)
                        {
                            rcSpeedLabel.X = rcBall.X + 10;
                        }

                        g.FillRectangle(new SolidBrush(clrBall), rcSpeedLabel);

						using (Font labelFont = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Pixel))
						{
							string strSpeed = (pBallInfo.fCurrentSpeed / 1000.0f).ToString("0.00") + "m/s";

							Size szText = TextRenderer.MeasureText(strSpeed, labelFont);
							PointF ptText = new PointF();
							ptText.X = (float)(rcSpeedLabel.X + (rcSpeedLabel.Width - szText.Width) / 2);
							ptText.Y = (float)(rcSpeedLabel.Y + (rcSpeedLabel.Height - szText.Height) / 2);

							g.DrawString(strSpeed, labelFont, new SolidBrush(Color.Black), ptText);
						}
                    }
                }
                else if (nStep == 2)
                {
                    ptTracjectory = new Point(pBallInfo.vptTrajectory[pBallInfo.vptTrajectory.Count() - 1].x, pBallInfo.vptTrajectory[pBallInfo.vptTrajectory.Count() - 1].y);
                    ptTracjectory.X = (int)(ptTracjectory.X * fScaleX + 0.5f);
                    ptTracjectory.Y = (int)(ptTracjectory.Y * fScaleY + 0.5f);

                    ptTracjectory.Offset(nOffsetY, nOffsetY);

                    rcBall.X = ptTracjectory.X - nBallRadius;
                    rcBall.Width = nBallRadius * 2;
                    rcBall.Y = ptTracjectory.Y - nBallRadius;
                    rcBall.Height = nBallRadius * 2;

                    g.FillEllipse(new SolidBrush(clrBall), rcBall);


                    if (nColorIndex == 1 - BallTrackAPI.m_nCurrentPlayer)		//current player's ball
                    {
                        using (Brush brBlack = new SolidBrush(Color.Black))
                        {
                            g.FillEllipse(brBlack, ptTracjectory.X - 3, ptTracjectory.Y - 3, 6, 6);
                        }
                    }

                    g.DrawLine(penBlack, ptTracjectory.X - nBallRadius - 2, ptTracjectory.Y, ptTracjectory.X + nBallRadius + 2, ptTracjectory.Y);
                    g.DrawLine(penBlack, ptTracjectory.X, ptTracjectory.Y - nBallRadius - 2, ptTracjectory.X, ptTracjectory.Y + nBallRadius + 2);
                }
            }

        }
    }

	
	public class BALL_TRACK_INFO
	{
		public List<POINT> vptTrajectory = new List<POINT>();
		public List<CRASH_ELEMENT> vIndexCrashed = new List<CRASH_ELEMENT>();
		public int nHorzMoveTotal;
		public int nVertMoveTotal;
		public int nHorzLastChange;
		public int nVertLastChange;
		public int nStableCount;
		public int[] nDist = new int[6];
		public int nLastMinIndex;
		public float fCurrentSpeed;
		public float fTotalSpeed;
		public float fAveSpeed;
		public int nSpeedRecordCount;
		public int nLastMoveChange;
		public bool bFirstInputPresented;
		public int nLastOutputCandidate;
		public int nLastOutputMark;
		public bool bImpacted;
		public void	ClearInfo()
		{
			vptTrajectory.Clear();
			vIndexCrashed.Clear();
			nHorzMoveTotal = 0;
			nVertMoveTotal = 0;
			nHorzLastChange = 0;
			nVertLastChange = 0;
			nStableCount = 0;
			nLastMinIndex = -1;
			fTotalSpeed = 0.0f;
			fCurrentSpeed = 0.0f;
			nSpeedRecordCount = 0;
			fAveSpeed = 0.0f;
			bFirstInputPresented = false;
			nLastOutputMark = 0;
			nLastOutputCandidate = -1;
			bImpacted = false;
			nLastMoveChange = 0;

			for (int i = 0; i < 6; i++)
				nDist[i] = 9999;
		}
		public BALL_TRACK_INFO()
		{
			ClearInfo();
		}
	}


}
