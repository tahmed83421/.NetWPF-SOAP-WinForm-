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
    public partial class frmCalibrationSave : Form
    {
        //UserRect rect;367, 684
        public frmCalibrationSave()
        {
            InitializeComponent();
            g = this.pictureBox1.CreateGraphics();

        }
        Graphics g;
        int i = 0;
        bool FIRST = true;// only one time calibration
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
				//if (BallTrackAPI.BTAPI_IsCameraConnected())
				//	BallTrackAPI.BTAPI_DisconnectCamera();
				//BallTrackAPI.BTAPI_Free();
                BallTrackAPI.mbInitialized = false;
                this.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no S2"); }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //strart
            try
            {
                if (FIRST == true)   //only one time calibration
                    FIRST = false;
                else return;
                new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();

                //TEMPX = pictureBox1.Size.Height / 2 - BallTrackAPI.ptCenter.x;
                //TEMPY = pictureBox1.Size.Width / 2 -( pictureBox1.Width - BallTrackAPI.ptCenter.y);
                
                //BilliardWindowsApplication.Properties.Settings.Default.tempx = TEMPX;
                //BilliardWindowsApplication.Properties.Settings.Default.tempy = TEMPY;
                //BilliardWindowsApplication.Properties.Settings.Default.Save();
                //pictureBox1.Invalidate();
                //label1.Location = new Point(pictureBox1.Width - BallTrackAPI.ptCorners[0].y + TEMPY - 5 + pictureBox1.Location.X, BallTrackAPI.ptCorners[0].x + TEMPX- 5 + pictureBox1.Location.Y);
                //label2.Location = new Point(pictureBox1.Width - BallTrackAPI.ptCorners[1].y + TEMPY - 5 + pictureBox1.Location.X, BallTrackAPI.ptCorners[1].x + TEMPX - 5 + pictureBox1.Location.Y);
                //label3.Location = new Point(pictureBox1.Width - BallTrackAPI.ptCorners[2].y + TEMPY - 5 + pictureBox1.Location.X, BallTrackAPI.ptCorners[2].x + TEMPX - 5 + pictureBox1.Location.Y);
                //label4.Location = new Point(pictureBox1.Width - BallTrackAPI.ptCorners[3].y + TEMPY - 5 + pictureBox1.Location.X, BallTrackAPI.ptCorners[3].x + TEMPX - 5 + pictureBox1.Location.Y);

                const int nDefaultWidth = 602;
                const int nDefaultHeight = 291;
                const int nDefaultMargin = 57;
                Point ptDefaultCenter = new Point(376, 240);
                Point ptCenterOffset = new Point(BallTrackAPI.ptCenter.x - ptDefaultCenter.X, BallTrackAPI.ptCenter.y - ptDefaultCenter.Y);

                float fScaleMapX = (float)(947 - nDefaultMargin * 2) / (float)nDefaultWidth;
                float fScaleMapY = (float)(519 - nDefaultMargin * 2) / (float)nDefaultHeight;

                float fScaleDrawX = (float)pictureBox1.Height / 947.0f;
                float fScaleDrawY = (float)pictureBox1.Width / 519.0f;
                POINT[] ptCalibPos = new POINT[5];
                ptCalibPos[0] = BallTrackAPI.ptCorners[0];
                ptCalibPos[1] = BallTrackAPI.ptCorners[1];
                ptCalibPos[2] = BallTrackAPI.ptCorners[2];
                ptCalibPos[3] = BallTrackAPI.ptCorners[3];
                ptCalibPos[4] = BallTrackAPI.ptCorners[0];

                int i;
                for (i = 0; i < 5; i++)
                {
                    ptCalibPos[i].x -= BallTrackAPI.ptCorners[0].x;
                    ptCalibPos[i].y -= BallTrackAPI.ptCorners[0].y;
                    ptCalibPos[i].x += ptCenterOffset.X;
                    ptCalibPos[i].y += ptCenterOffset.Y;

                    ptCalibPos[i].x = (int)((ptCalibPos[i].x * fScaleMapX + nDefaultMargin) * fScaleDrawX + 0.5f);
                    ptCalibPos[i].y = (int)((ptCalibPos[i].y * fScaleMapY + nDefaultMargin) * fScaleDrawY + 0.5f);
                }

                //rotate right 90
                int nDrawWidth = pictureBox1.Width;
                int nDrawHeight = pictureBox1.Height;
                label1.Location = new Point(nDrawWidth - ptCalibPos[0].y + pictureBox1.Location.X - 5, ptCalibPos[0].x + pictureBox1.Location.Y - 5);
                label2.Location = new Point(nDrawWidth - ptCalibPos[1].y + pictureBox1.Location.X - 5, ptCalibPos[1].x + pictureBox1.Location.Y - 5);
                label3.Location = new Point(nDrawWidth - ptCalibPos[2].y + pictureBox1.Location.X - 5, ptCalibPos[2].x + pictureBox1.Location.Y - 5);
                label4.Location = new Point(nDrawWidth - ptCalibPos[3].y + pictureBox1.Location.X - 5, ptCalibPos[3].x + pictureBox1.Location.Y - 5);

                label1.Visible = label2.Visible = label3.Visible = label4.Visible = true;
                timer1.Enabled = true;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no S3"); }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                drawract();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no S4"); }
        }
        public void drawract()
        {
            try
            {
                Pen p = new Pen(Color.White);
                p.Width = 2.0f;
                g.DrawLine(p, label1.Location.X + 5 - pictureBox1.Location.X, label1.Location.Y + 5 - pictureBox1.Location.Y, label2.Location.X + 5 - pictureBox1.Location.X, label2.Location.Y + 5 - pictureBox1.Location.Y);
                g.DrawLine(p, label1.Location.X + 5 - pictureBox1.Location.X, label1.Location.Y + 5 - pictureBox1.Location.Y, label4.Location.X + 5 - pictureBox1.Location.X, label4.Location.Y + 5 - pictureBox1.Location.Y);
                g.DrawLine(p, label3.Location.X + 5 - pictureBox1.Location.X, label3.Location.Y + 5 - pictureBox1.Location.Y, label2.Location.X + 5 - pictureBox1.Location.X, label2.Location.Y + 5 - pictureBox1.Location.Y);
                g.DrawLine(p, label3.Location.X + 5 - pictureBox1.Location.X, label3.Location.Y + 5 - pictureBox1.Location.Y, label4.Location.X + 5 - pictureBox1.Location.X, label4.Location.Y + 5 - pictureBox1.Location.Y);
                g.FillEllipse(new SolidBrush(Color.Red), pictureBox1.Width / 2 - 6, pictureBox1.Height / 2 - 6, 13, 13);
            }
            catch
            {
                timer1.Enabled = false;
                //dont show any messsage.
                //MessageBox.Show(ex.Message, "ex no S5");
            }
        }
        public void drawconfirm()
        {
            try
            {
                Pen p = new Pen(Color.Red);
                p.Width = 3.0f;
                g.DrawLine(p, label1.Location.X + 5 - pictureBox1.Location.X, label1.Location.Y + 5 - pictureBox1.Location.Y, label2.Location.X + 5 - pictureBox1.Location.X, label2.Location.Y + 5 - pictureBox1.Location.Y);
                g.DrawLine(p, label1.Location.X + 5 - pictureBox1.Location.X, label1.Location.Y + 5 - pictureBox1.Location.Y, label4.Location.X + 5 - pictureBox1.Location.X, label4.Location.Y + 5 - pictureBox1.Location.Y);
                g.DrawLine(p, label3.Location.X + 5 - pictureBox1.Location.X, label3.Location.Y + 5 - pictureBox1.Location.Y, label2.Location.X + 5 - pictureBox1.Location.X, label2.Location.Y + 5 - pictureBox1.Location.Y);
                g.DrawLine(p, label3.Location.X + 5 - pictureBox1.Location.X, label3.Location.Y + 5 - pictureBox1.Location.Y, label4.Location.X + 5 - pictureBox1.Location.X, label4.Location.Y + 5 - pictureBox1.Location.Y);
                g.FillEllipse(new SolidBrush(Color.Red), pictureBox1.Width / 2 - 6, pictureBox1.Height / 2 - 6, 13, 13);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no S6"); }
        }
        public void CameraClibration()
        {
            try
            {
                const int nDefaultWidth = 602;
                const int nDefaultHeight = 291;
                const int nDefaultMargin = 57;
                Point ptDefaultCenter = new Point(376, 240);
                Point ptCenterOffset = new Point(BallTrackAPI.ptCenter.x - ptDefaultCenter.X, BallTrackAPI.ptCenter.y - ptDefaultCenter.Y);

                float fScaleMapX = (float)(947 - nDefaultMargin * 2) / (float)nDefaultWidth;
                float fScaleMapY = (float)(519 - nDefaultMargin * 2) / (float)nDefaultHeight;

                float fScaleDrawX = (float)pictureBox1.Height / 947.0f;
                float fScaleDrawY = (float)pictureBox1.Width / 519.0f;

                POINT ptOrgTopLeft = BallTrackAPI.ptCorners[0];
                Label[] labels = new Label[] { label1, label2, label3, label4 };
                Point[] ptRotatedPoints = new Point[4];
                Point[] ptTemp = new Point[4];
                int nDrawWidth = pictureBox1.Width;
                int nDrawHeight = pictureBox1.Height;
                for (int i = 0; i < 4; i++)
                {
                    ptTemp[i] = labels[i].Location;
                    ptTemp[i].Offset(5, 5);
                    ptTemp[i].X -= pictureBox1.Location.X;
                    ptTemp[i].Y -= pictureBox1.Location.Y;

                    ptRotatedPoints[i] = new Point(ptTemp[i].Y, nDrawWidth - ptTemp[i].X);
                    BallTrackAPI.ptCorner_new[i].x = (int)((ptRotatedPoints[i].X / fScaleDrawX - nDefaultMargin) / fScaleMapX + 0.5f);
                    BallTrackAPI.ptCorner_new[i].y = (int)((ptRotatedPoints[i].Y / fScaleDrawY - nDefaultMargin) / fScaleMapY + 0.5f);

                    BallTrackAPI.ptCorner_new[i].x += ptOrgTopLeft.x;
                    BallTrackAPI.ptCorner_new[i].y += ptOrgTopLeft.y;
                    BallTrackAPI.ptCorner_new[i].x -= ptCenterOffset.X;
                    BallTrackAPI.ptCorner_new[i].y -= ptCenterOffset.Y;
                }

                    //BallTrackAPI.ptCorner_new[0].x = label1.Location.Y + 5 - pictureBox1.Location.Y - TEMPX; BallTrackAPI.ptCorner_new[0].y = pictureBox1.Width - label1.Location.X - 5 + pictureBox1.Location.X + TEMPY;
                    //BallTrackAPI.ptCorner_new[1].x = label2.Location.Y + 5 - pictureBox1.Location.Y - TEMPX; BallTrackAPI.ptCorner_new[1].y = pictureBox1.Width - label2.Location.X - 5 + pictureBox1.Location.X + TEMPY;
                    //BallTrackAPI.ptCorner_new[2].x = label3.Location.Y + 5 - pictureBox1.Location.Y - TEMPX; BallTrackAPI.ptCorner_new[2].y = pictureBox1.Width - label3.Location.X - 5 + pictureBox1.Location.X + TEMPY;
                    //BallTrackAPI.ptCorner_new[3].x = label4.Location.Y + 5 - pictureBox1.Location.Y - TEMPX; BallTrackAPI.ptCorner_new[3].y = pictureBox1.Width - label4.Location.X - 5 + pictureBox1.Location.X + TEMPY;
                    try
                    {
                        BallTrackAPI.BTAPI_CameraClibration(ref BallTrackAPI.ptCorners[0], ref BallTrackAPI.ptCorner_new[0]);
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message, "ex no S1"); }
               
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no S7"); }
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //stop
            try
            {
                new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
                timer1.Enabled = false;
                CameraClibration();
                pictureBox1.Invalidate();
                label1.Visible = label2.Visible = label3.Visible = label4.Visible = false;
                timer2.Enabled = true;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no S8"); }
        }
        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    pictureBox1.Invalidate();
                    Point PT = ((Label)sender).Location;
                    ((Label)sender).Location = new Point(e.X + PT.X, e.Y + PT.Y);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no S9"); }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (i < 6)
                {
                    i++;
                    if (i % 2 == 0)
                        drawconfirm();
                    else pictureBox1.Invalidate();
                }
                else timer2.Enabled = false;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "ex no S10"); }
        }

    }
}