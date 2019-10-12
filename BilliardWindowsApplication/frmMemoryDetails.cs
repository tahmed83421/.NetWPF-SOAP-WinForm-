using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
    public partial class frmMemoryDetails : Form
    {
        public BallTrackAPI.NotifyProcDelegate notifyProc;
        List<memorydetails> memory ;

		int m_nMarginX;
		int m_nMarginY;
		float m_fMarginScaleX;
		float m_fMarginScaleY;
		float m_fScaleX;
		float m_fScaleY;
		private int nBallRadius = 8;
		Rectangle m_rcDrawRect;
		Size m_szImageBackground = new Size(523, 950);
        bool m_bReplayingNow = false;
        public frmMemoryDetails(List<memorydetails> memo)
        {
            this.memory = memo;
            InitializeComponent();

			m_rcDrawRect = new Rectangle(0, 0, pb1.Width, pb1.Height);
			m_fMarginScaleX = (float)m_rcDrawRect.Width / (float)m_szImageBackground.Width;
			m_fMarginScaleY = (float)m_rcDrawRect.Height / (float)m_szImageBackground.Height;

			m_nMarginX = (int)(50 * m_fMarginScaleX + 0.5f);
			m_nMarginY = (int)(50 * m_fMarginScaleY + 0.5f);

			Rectangle rcTableInside = m_rcDrawRect;
			rcTableInside.Inflate(-m_nMarginX, -m_nMarginY);
			m_fScaleX = (float)rcTableInside.Width / (float)(BallTrackAPI.g_rcClip.bottom - BallTrackAPI.g_rcClip.top);
			m_fScaleY = (float)rcTableInside.Height / (float)(BallTrackAPI.g_rcClip.right - BallTrackAPI.g_rcClip.left);

            notifyProc = new BallTrackAPI.NotifyProcDelegate(NotifyProc);
            BallTrackAPI.BTAPI_SetNotifyCallback(System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(notifyProc));
        }

        private void frmMemoryDetails_Load(object sender, EventArgs e)
        {
			dataGridView1.Rows.Clear();
            string TEMPSCORE = "";
            for (int i = 0; i < memory.Count; i++)
            {
                if (memory[i].score >= 0)
                    TEMPSCORE = "+" + memory[i].score;
                else TEMPSCORE = memory[i].score+"";
                if (memory[i].extra == 0)
					dataGridView1.Rows.Add(TEMPSCORE, memory[i].point, memory[i].turn, memory[i].set, memory[i].recordplayer, memory[i].replayrecord, memory[i].isreplayrecord, memory[i].replayset);
                else if (memory[i].extra == 1)
					dataGridView1.Rows.Add("@2T1" + TEMPSCORE, memory[i].point, memory[i].turn, memory[i].set, memory[i].recordplayer, memory[i].replayrecord, memory[i].isreplayrecord, memory[i].replayset);
                else if (memory[i].extra == 2)
					dataGridView1.Rows.Add("@2T2" + TEMPSCORE, memory[i].point, memory[i].turn, memory[i].set, memory[i].recordplayer, memory[i].replayrecord, memory[i].isreplayrecord, memory[i].replayset);
                else if (memory[i].extra == 3)
					dataGridView1.Rows.Add("@" + TEMPSCORE, memory[i].point, memory[i].turn, memory[i].set, memory[i].recordplayer, memory[i].replayrecord, memory[i].isreplayrecord, memory[i].replayset);
                else if (memory[i].extra == 4)
					dataGridView1.Rows.Add("@H" + TEMPSCORE, memory[i].point, memory[i].turn, memory[i].set, memory[i].recordplayer, memory[i].replayrecord, memory[i].isreplayrecord, memory[i].replayset);
                else if (memory[i].extra == 5)
                {
                    string label = "@" + memory[i].score.ToString() + "cBo";
                    dataGridView1.Rows.Add(label + TEMPSCORE, memory[i].point, memory[i].turn, memory[i].set, memory[i].recordplayer, memory[i].replayrecord, memory[i].isreplayrecord, memory[i].replayset);
                }
                else if (memory[i].extra == 6)
                    dataGridView1.Rows.Add("+++++++", memory[i].point, memory[i].turn, memory[i].set, memory[i].recordplayer, memory[i].replayrecord, memory[i].isreplayrecord, memory[i].replayset);                    
                if (memory[i].player1)
					dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                else
					dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Orange;
            }

            dataGridView1.ClearSelection();
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;	
        }

        private void pictureboxRound1_Click(object sender, EventArgs e)
        {
            if (m_bReplayingNow)
            {
                
            }
            else
			{
				BallTrackAPI.m_bReplay = false;
                this.Close();
			}
        }
        int listi = 1;
        int a;
        int player = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if ((bool)dataGridView1[6, e.RowIndex].Value)
                {
                    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
                    
                    if ((int)dataGridView1[4, e.RowIndex].Value == 0)
                    {
                        player = 1;
                        pb2.Visible = false;
                        pb1.Visible = true;
                        pb1.Invalidate();
                       
                        a = (int)dataGridView1[5, e.RowIndex].Value;
						int replayset = (int)dataGridView1[7, e.RowIndex].Value;
						int nMaxPlaySet = replayset == 0 ? BallTrackAPI.BTAPI_GetWhiteHistoryCount() : BallTrackAPI.BTAPI_GetYellowHistoryCount();
						if (a >= nMaxPlaySet)
							a = nMaxPlaySet - 1;
                        if (a < 0)
                        {
                            NotifyProc(4, 0);
                            return;
                        }
						BallTrackAPI.m_bReplay = true;
						BallTrackAPI.BTAPI_DrawReplay(IntPtr.Zero, replayset, a);
                        dataGridView1.Enabled = false;
                        m_bReplayingNow = true;
                        listi = 1;
                        timer1.Enabled = true;
                    }
                    if ((int)dataGridView1[4, e.RowIndex].Value == 1)
                    {
                        player = 2;
                        pb1.Visible = false;
                        pb2.Visible = true;
                        pb2.Invalidate();
                        
                        a = (int)dataGridView1[5, e.RowIndex].Value;
						int replayset = (int)dataGridView1[7, e.RowIndex].Value;

						int nMaxPlaySet = replayset == 0 ? BallTrackAPI.BTAPI_GetWhiteHistoryCount() : BallTrackAPI.BTAPI_GetYellowHistoryCount();
						if (a >= nMaxPlaySet)
							a = nMaxPlaySet - 1;

                        if (a < 0)
                        {
                            NotifyProc(4, 0);
                            return;
                        }
						BallTrackAPI.m_bReplay = true;
						BallTrackAPI.BTAPI_DrawReplay(IntPtr.Zero, replayset, a);
                        dataGridView1.Enabled = false;
                        m_bReplayingNow = true;

                        listi = 1;
                        timer1.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
		
		
        private void timer1_Tick(object sender, EventArgs e)
        {
			try
			{
				BallTrackAPI.BTAPI_QueryDrawInfo(ref BallTrackAPI.drawInfo, ref BallTrackAPI.m_WhiteHitPos[0], ref BallTrackAPI.m_YellowHitPos[0], ref BallTrackAPI.m_RedHitPos[0], BallTrackAPI.m_bReplay, 0, 0);
				BallTrackAPI.ScaleCurrentDrawInfo(m_fScaleX, m_fScaleY, m_nMarginX, m_nMarginY, pb1.Width, pb1.Height);
				if (player == 1)
					pb1.Invalidate();
				else
					pb2.Invalidate();

				//	if (player == 1)
				//	{
				//		g1.FillEllipse(new SolidBrush(Color.Red), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_red.x - 6 + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_red.y - 6 + TEMPY, 12, 12);
				//		g1.FillEllipse(new SolidBrush(Color.White), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_white.x - 6 + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_white.y - 6 + TEMPY, 12, 12);
				//		g1.FillEllipse(new SolidBrush(Color.Yellow), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_yellow.x - 6 + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_yellow.y - 6 + TEMPY, 12, 12);
				//		if (BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].white_point.x != 0 && BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].white_point.y != 0)
				//			g1.DrawLine(new Pen(Color.White), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].white_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].white_point.y + TEMPY, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].white_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].white_point.y + TEMPY);
				//		if (BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].red_point.x != 0 && BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].red_point.y != 0)
				//			g1.DrawLine(new Pen(Color.Red), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].red_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].red_point.y + TEMPY, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].red_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].red_point.y + TEMPY);
				//		if (BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].yellow_point.x != 0 && BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].yellow_point.y != 0)
				//			g1.DrawLine(new Pen(Color.Yellow), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].yellow_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].yellow_point.y + TEMPY, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].yellow_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].yellow_point.y + TEMPY);
				//	}
				//	else if(player==2)
				//	{
				//		g2.FillEllipse(new SolidBrush(Color.Red), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_red.x - 6 + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_red.y - 6 + TEMPY, 12, 12);
				//		g2.FillEllipse(new SolidBrush(Color.White), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_white.x - 6 + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_white.y - 6 + TEMPY, 12, 12);
				//		g2.FillEllipse(new SolidBrush(Color.Yellow), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_yellow.x - 6 + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].start_point_yellow.y - 6 + TEMPY, 12, 12);
				//		if (BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].white_point.x != 0 && BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].white_point.y != 0)
				//			g2.DrawLine(new Pen(Color.White), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].white_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].white_point.y + TEMPY, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].white_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].white_point.y + TEMPY);
				//		if (BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].red_point.x != 0 && BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].red_point.y != 0)
				//			g2.DrawLine(new Pen(Color.Red), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].red_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].red_point.y + TEMPY, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].red_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].red_point.y + TEMPY);
				//		if (BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].yellow_point.x != 0 && BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].yellow_point.y != 0)
				//			g2.DrawLine(new Pen(Color.Yellow), BallTrackAPI.lastrecordlist[a].drawInforecord[listi].yellow_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi].yellow_point.y + TEMPY, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].yellow_point.x + TEMPX, BallTrackAPI.lastrecordlist[a].drawInforecord[listi - 1].yellow_point.y + TEMPY);
				//	}
			}
			catch { timer1.Enabled = false; }
        }

		private void WhiteRePlayer_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			BallTrackAPI.DrawReplay(e.Graphics, BallTrackAPI.drawInfo.state, BallTrackAPI.drawInfo.player, 
				BallTrackAPI.drawInfo.white_point, BallTrackAPI.drawInfo.yellow_point, BallTrackAPI.drawInfo.red_point, 
				BallTrackAPI.drawInfo.start_point_white, BallTrackAPI.drawInfo.start_point_yellow, BallTrackAPI.drawInfo.start_point_red, 
				BallTrackAPI.drawInfo.hit_count_white, BallTrackAPI.m_WhiteHitPos, 
				BallTrackAPI.drawInfo.hit_count_yellow, BallTrackAPI.m_YellowHitPos, 
				BallTrackAPI.drawInfo.hit_count_red, BallTrackAPI.m_RedHitPos, 
				BallTrackAPI.m_bShowBallSpeed, BallTrackAPI.drawInfo.ball_speed, BallTrackAPI.drawInfo.impact_state,
				BallTrackAPI.drawInfo.teacher_mark_in, BallTrackAPI.drawInfo.teacher_mark_out,
					BallTrackAPI.drawInfo.teacher_point_in, BallTrackAPI.drawInfo.teacher_point_out);
		}

		private void YellowReplayer_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			BallTrackAPI.DrawReplay(e.Graphics, BallTrackAPI.drawInfo.state, BallTrackAPI.drawInfo.player,
				BallTrackAPI.drawInfo.white_point, BallTrackAPI.drawInfo.yellow_point, BallTrackAPI.drawInfo.red_point,
				BallTrackAPI.drawInfo.start_point_white, BallTrackAPI.drawInfo.start_point_yellow, BallTrackAPI.drawInfo.start_point_red,
				BallTrackAPI.drawInfo.hit_count_white, BallTrackAPI.m_WhiteHitPos,
				BallTrackAPI.drawInfo.hit_count_yellow, BallTrackAPI.m_YellowHitPos,
				BallTrackAPI.drawInfo.hit_count_red, BallTrackAPI.m_RedHitPos,
				BallTrackAPI.m_bShowBallSpeed, BallTrackAPI.drawInfo.ball_speed, BallTrackAPI.drawInfo.impact_state,
				BallTrackAPI.drawInfo.teacher_mark_in, BallTrackAPI.drawInfo.teacher_mark_out,
					BallTrackAPI.drawInfo.teacher_point_in, BallTrackAPI.drawInfo.teacher_point_out);
		}

		private void label1_Click(object sender, EventArgs e)
		{
			//BallTrackAPI.BTAPI_StopTracking();
			//int nCount = BallTrackAPI.BTAPI_GetWhiteHistoryCount();
			//nCount = BallTrackAPI.BTAPI_GetYellowHistoryCount();

			//BallTrackAPI.m_bReplay = true;
			//BallTrackAPI.BTAPI_DrawReplay(IntPtr.Zero, 0, 0);
			//pb1.Visible = true;
			//player = 1;
			//timer1.Enabled = true;
		}

        private void NotifyProc(int nNotifyCode, int nValue)
        {
            if (nNotifyCode == 4)
            {
                dataGridView1.BeginInvoke(new Action(() => dataGridView1.Enabled = true));
				//BallTrackAPI.m_bReplay = false;
				timer1.Enabled = false;
                m_bReplayingNow = false ;
            }
        }
    }
}