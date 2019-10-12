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
    public partial class frmCostSetup : Form
    {
        biliardService.BilliardScoreboard API = new biliardService.BilliardScoreboard();
        public frmCostSetup()
        {
            InitializeComponent();
        }
        private void pbGame_Click(object sender, EventArgs e)
        {
            wanttoclose = true;
            this.Close();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            if (pictureBox5.Tag.ToString() == "f")
            {
                pictureBox5.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox5.Tag = "t";
            }
            else
            {
                pictureBox5.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox5.Tag = "f";
            }
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            if (pictureBox6.Tag.ToString() == "f")
            {
                pictureBox6.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox6.Tag = "t";
            }
            else
            {
                pictureBox6.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox6.Tag = "f";
            }
        }
        private void label39_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            biliardService.costDetails cd = new biliardService.costDetails();
            cd.clubid = BilliardWindowsApplication.Properties.Settings.Default.CID;
            cd.bilino = lblBillno.Text;
            cd.f1 = lblf1.Text;
            cd.t1 = lblt1.Text;
            cd.h1 = lblh1.Text;
            cd.d1 = lbld1.Text;
            cd.f2 = lblf2.Text;
            cd.t2 = lblt2.Text;
            cd.h2 = lblh2.Text;
            cd.d2 = lbld2.Text;
            cd.f3 = lblf3.Text;
            cd.t3 = lblt3.Text;
            cd.h3 = lblh3.Text;
            cd.d3 = lbld3.Text;
            cd.f4 = lblf4.Text;
            cd.t4 = lblt4.Text;
            cd.h4 = lblh4.Text;
            cd.d4 = lbld4.Text;
            cd.SpecialCharge = lblSpecialCharge.Text.Replace('+', ' ').Replace('%', ' ').Trim();
            cd.SpecialBool = pictureBox5.Tag.ToString();
            cd.DownloadExel = pictureBox6.Tag.ToString();
            cd.costvisible=pictureBox2.Tag.ToString();
            cd.emailcostPlayer=pictureBox3.Tag.ToString();
            cd.emailcostowner=pictureBox4.Tag.ToString();
            cd.coston = pictureBox7.Tag.ToString();
            string result = API.updateCostDetails(cd);
            if (result != true.ToString())
                MessageBox.Show(result);
            else
            {
                if (BLL_BilliardWindowsApplication.costDetailsStataic.bilino.Trim() == lblBillno.Text.Trim())
                {
                    BLL_BilliardWindowsApplication.costDetailsStataic = cd;
                }
                MessageBox.Show("Settings Updated", "Biliardoprofessionale", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void frmCostSetup_Load(object sender, EventArgs e)
        {
            pnlmiddle.Location=  new Point((this.Width-pnlmiddle.Width)/2,pnlmiddle.Location.Y);
            lblBillno.Text=BLL_BilliardWindowsApplication.costDetailsStataic.bilino ;
            lblf1.Text=BLL_BilliardWindowsApplication.costDetailsStataic.f1 ;
            lblt1.Text=BLL_BilliardWindowsApplication.costDetailsStataic.t1 ;
            lblh1.Text=BLL_BilliardWindowsApplication.costDetailsStataic.h1 ;
            lbld1.Text=(int.Parse(BLL_BilliardWindowsApplication.costDetailsStataic.d1)/60).ToString() ;
            lblf2.Text=BLL_BilliardWindowsApplication.costDetailsStataic.f2 ;
            lblt2.Text=BLL_BilliardWindowsApplication.costDetailsStataic.t2 ;
            lblh2.Text=BLL_BilliardWindowsApplication.costDetailsStataic.h2 ;
            lbld2.Text = (int.Parse(BLL_BilliardWindowsApplication.costDetailsStataic.d2) / 60).ToString();
            lblf3.Text=BLL_BilliardWindowsApplication.costDetailsStataic.f3 ;
            lblt3.Text=BLL_BilliardWindowsApplication.costDetailsStataic.t3 ;
            lblh3.Text=BLL_BilliardWindowsApplication.costDetailsStataic.h3 ;
            lbld3.Text = (int.Parse(BLL_BilliardWindowsApplication.costDetailsStataic.d3) / 60).ToString();
            lblf4.Text=BLL_BilliardWindowsApplication.costDetailsStataic.f4 ;
            lblt4.Text=BLL_BilliardWindowsApplication.costDetailsStataic.t4 ;
            lblh4.Text=BLL_BilliardWindowsApplication.costDetailsStataic.h4 ;
            lbld4.Text = (int.Parse(BLL_BilliardWindowsApplication.costDetailsStataic.d4) / 60).ToString();
            lblSpecialCharge.Text="+"+BLL_BilliardWindowsApplication.costDetailsStataic.SpecialCharge+" %" ;

            if (BLL_BilliardWindowsApplication.costDetailsStataic.SpecialBool == "t")
            {
                pictureBox5.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox5.Tag = "t";
            }
            else
            {
                pictureBox5.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox5.Tag = "f";
            }

            if (BLL_BilliardWindowsApplication.costDetailsStataic.DownloadExel == "t")
            {
                pictureBox6.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox6.Tag = "t";
            }
            else
            {
                pictureBox6.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox6.Tag = "f";
            }

            if (BLL_BilliardWindowsApplication.costDetailsStataic.costvisible == "t")
            {
                pictureBox2.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox2.Tag = "t";
            }
            else
            {
                pictureBox2.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox2.Tag = "f";
            }

            if (BLL_BilliardWindowsApplication.costDetailsStataic.emailcostPlayer == "t")
            {
                pictureBox3.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox3.Tag = "t";
            }
            else
            {
                pictureBox3.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox3.Tag = "f";
            }

            if (BLL_BilliardWindowsApplication.costDetailsStataic.emailcostowner == "t")
            {
                pictureBox4.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox4.Tag = "t";
            }
            else
            {
                pictureBox4.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox4.Tag = "f";
            }

            if (BLL_BilliardWindowsApplication.costDetailsStataic.coston == "t")
            {
                pictureBox7.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox7.Tag = "t";
            }
            else
            {
                pictureBox7.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox7.Tag = "f";
            }

            dayscost(30);
           // lblBillno.Text = BilliardWindowsApplication.Properties.Settings.Default.billiardno.ToString();

        }

       async void dayscost(int nodays)
        {

            lbltotaldayscost.Text = await Task.Run(() => API.daysCost(BilliardWindowsApplication.Properties.Settings.Default.CID, lblBillno.Text, nodays));
            //MessageBox.Show(s);
        }
        private void flowLayoutPanel4_Click(object sender, EventArgs e)
        {
            hideAll();
        }
        void hideAll()
        {
            try
            {
                if (comboBox1.Visible)
                {
                    lblf1.Text = comboBox1.Text.Substring(0, 8);
                    comboBox1.Visible = false;
                }
                if (comboBox2.Visible)
                {
                    lblt1.Text = comboBox2.Text.Substring(0, 8);
                    comboBox2.Visible = false;
                }
                if (comboBox3.Visible)
                {
                    lblf2.Text = comboBox3.Text.Substring(0, 8);
                    comboBox3.Visible = false;
                }
                if (comboBox4.Visible)
                {
                    lblt2.Text = comboBox4.Text.Substring(0, 8);
                    comboBox4.Visible = false;
                }
                if (comboBox5.Visible)
                {
                    lblf3.Text = comboBox5.Text.Substring(0, 8);
                    comboBox5.Visible = false;
                }
                if (comboBox6.Visible)
                {
                    lblt3.Text = comboBox6.Text.Substring(0, 8);
                    comboBox6.Visible = false;
                }
                if (comboBox7.Visible)
                {
                    lblf4.Text = comboBox7.Text.Substring(0, 8);
                    comboBox7.Visible = false;
                }
                if (comboBox8.Visible)
                {
                    lblt4.Text = comboBox8.Text.Substring(0, 8);
                    comboBox8.Visible = false;
                }
                if (textBox1.Visible)
                {
                    lblh1.Text = textBox1.Text + " €";
                    textBox1.Visible = false;
                }
                if (textBox2.Visible)
                {
                    lblh2.Text = textBox2.Text + " €";
                    textBox2.Visible = false;
                }
                if (textBox3.Visible)
                {
                    lblh3.Text = textBox3.Text + " €";
                    textBox3.Visible = false;
                }
                if (textBox4.Visible)
                {
                    lblh4.Text = textBox4.Text + " €";
                    textBox4.Visible = false;
                }
                if (textBox5.Visible)
                {
                    lblSpecialCharge.Text = "+"+textBox5.Text + " %";
                    textBox5.Visible = false;
                }
                if (txtbillno.Visible)
                {
                    lblBillno.Text = txtbillno.Text;
                    txtbillno.Visible = false;
                }
                if (txtdayscost.Visible)
                {
                    
                    try
                    {
                        int days=1;
                        if (int.TryParse(txtdayscost.Text, out days))
                        dayscost(days);
                        lbldayscost.Text = days.ToString();
                        txtdayscost.Visible = false;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }

                bllBillardLogic bll = new bllBillardLogic();
               
                //TimeSpan t1,t2,t;
                //t1 = bll.convertDbToTime(lblf1.Text).Add(TimeSpan.FromMinutes(-1));
                //t2 = bll.convertDbToTime(lblt1.Text);
                //if (t1 < t2)
                //    t = t2 - t1;
                //else t = t2.Add(TimeSpan.FromHours(24)) - t1;
                //lbld1.Text = (t.Hours * 30 + (t.Minutes * 30) / 60).ToString();
                
                //t1 = bll.convertDbToTime(lblf2.Text).Add(TimeSpan.FromMinutes(-1));
                //t2 = bll.convertDbToTime(lblt2.Text);
                //if (t1 < t2)
                //    t = t2 - t1;
                //else t = t2.Add(TimeSpan.FromHours(24)) - t1;
                //lbld2.Text = (t.Hours * 30 + (t.Minutes * 30) / 60).ToString();

                //t1 = bll.convertDbToTime(lblf3.Text).Add(TimeSpan.FromMinutes(-1));
                //t2 = bll.convertDbToTime(lblt3.Text);
                //if (t1 < t2)
                //    t = t2 - t1;
                //else t = t2.Add(TimeSpan.FromHours(24)) - t1;
                //lbld3.Text = (t.Hours * 30 + (t.Minutes * 30) / 60).ToString();

                //t1 = bll.convertDbToTime(lblf4.Text).Add(TimeSpan.FromMinutes(-1));
                //t2 = bll.convertDbToTime(lblt4.Text);
                //if (t1 < t2)
                //    t = t2 - t1;
                //else t = t2.Add(TimeSpan.FromHours(24)) - t1;
                //lbld4.Text = (t.Hours * 30 + (t.Minutes * 30) / 60).ToString();

            }
            catch { }
        }
        private void label14_Click(object sender, EventArgs e)
        {
            hideAll();
            comboBox1.Text = lblf1.Text;
            comboBox1.Visible = true;
        }
        private void label15_Click(object sender, EventArgs e)
        {
            hideAll();
            comboBox2.Text = lblt1.Text;
            comboBox2.Visible = true;
        }
        private void label9_Click(object sender, EventArgs e)
        {
            hideAll();
            comboBox3.Text = lblf2.Text;
            comboBox3.Visible = true;
        }
        private void label10_Click(object sender, EventArgs e)
        {
            hideAll();
            comboBox4.Text = lblt2.Text;
            comboBox4.Visible = true;
        }
        private void label19_Click(object sender, EventArgs e)
        {
            hideAll();
            comboBox5.Text = lblf3.Text;
            comboBox5.Visible = true;
        }
        private void label20_Click(object sender, EventArgs e)
        {
            hideAll();
            comboBox6.Text = lblt3.Text;
            comboBox6.Visible = true;
        }
        private void label24_Click(object sender, EventArgs e)
        {
            hideAll();
            comboBox7.Text = lblf4.Text;
            comboBox7.Visible = true;
        }
        private void label25_Click(object sender, EventArgs e)
        {
            hideAll();
            comboBox8.Text = lblt4.Text;
            comboBox8.Visible = true;
        }
        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label16_Click(object sender, EventArgs e)
        {
            hideAll();
            textBox1.Text = lblh1.Text.Replace('€',' ').Trim();
            textBox1.Visible = true;
            textBox1.Focus();
        }
        private void label11_Click(object sender, EventArgs e)
        {
            hideAll();
            textBox2.Text = lblh2.Text.Replace('€', ' ').Trim();
            textBox2.Visible = true;
            textBox2.Focus();
        }
        private void label21_Click(object sender, EventArgs e)
        {
            hideAll();
            textBox3.Text = lblh3.Text.Replace('€', ' ').Trim();
            textBox3.Visible = true;
            textBox3.Focus();
        }
        private void label26_Click(object sender, EventArgs e)
        {
            hideAll();
            textBox4.Text = lblh4.Text.Replace('€', ' ').Trim();
            textBox4.Visible = true;
            textBox4.Focus();
        }
        private void label32_Click(object sender, EventArgs e)
        {
            hideAll();
            textBox5.Text = lblSpecialCharge.Text.Replace('%', ' ').Replace('+',' ').Trim();
            textBox5.Visible = true;
            textBox5.Focus();
        }
        private void label35_Click(object sender, EventArgs e)
        {
            hideAll();
            txtbillno.Text = lblBillno.Text;
            txtbillno.Visible = true;
            txtbillno.Focus();
        }
        private void pb9_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            SendKeys.Send(((PictureBox)sender).Tag.ToString());
        }
        private void label41_Click(object sender, EventArgs e)
        {
            hideAll();
            biliardService.costDetails cd = API.getCostDetails(BilliardWindowsApplication.Properties.Settings.Default.CID, lblBillno.Text);
            dayscost(int.Parse(txtdayscost.Text));
            
            lblf1.Text = cd.f1;
            lblt1.Text = cd.t1;
            lblh1.Text = cd.h1;
            lbld1.Text = (int.Parse(cd.d1)/ 60).ToString(); 
            lblf2.Text = cd.f2;
            lblt2.Text = cd.t2;
            lblh2.Text = cd.h2;
            lbld2.Text = (int.Parse(cd.d2) / 60).ToString();
            lblf3.Text = cd.f3;
            lblt3.Text = cd.t3;
            lblh3.Text = cd.h3;
            lbld3.Text = (int.Parse(cd.d3) / 60).ToString();
            lblf4.Text = cd.f4;
            lblt4.Text = cd.t4;
            lblh4.Text = cd.h4;
            lbld4.Text = (int.Parse(cd.d4) / 60).ToString();
            lblSpecialCharge.Text = "+" + cd.SpecialCharge + " %";

            if (cd.SpecialBool == "t")
            {
                pictureBox5.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox5.Tag = "t";
            }
            else
            {
                pictureBox5.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox5.Tag = "f";
            }

            if (cd.DownloadExel == "t")
            {
                pictureBox6.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox6.Tag = "t";
            }
            else
            {
                pictureBox6.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox6.Tag = "f";
            }

            if (cd.costvisible == "t")
            {
                pictureBox2.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox2.Tag = "t";
            }
            else
            {
                pictureBox2.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox2.Tag = "f";
            }
            
            if (cd.emailcostPlayer == "t")
            {
                pictureBox3.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox3.Tag = "t";
            }
            else
            {
                pictureBox3.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox3.Tag = "f";
            }
            if (cd.emailcostowner == "t")
            {
                pictureBox4.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox4.Tag = "t";
            }
            else
            {
                pictureBox4.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox4.Tag = "f";
            }

            if (cd.coston == "t")
            {
                pictureBox7.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox7.Tag = "t";
            }
            else
            {
                pictureBox7.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox7.Tag = "f";
            }
        }

        private void lbldayscost_Click(object sender, EventArgs e)
        {
            hideAll();
            txtdayscost.Text = lbldayscost.Text;
            txtdayscost.Visible = true;
            txtdayscost.Focus();
        }

        private bool wanttoclose = false;
        private void frmCostSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!wanttoclose)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                    e.Cancel = true;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            if (pictureBox7.Tag.ToString() == "f")
            {
                pictureBox7.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox7.Tag = "t";
            }
            else
            {
                pictureBox7.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox7.Tag = "f";
                pictureBox2.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox2.Tag = "f";
                pictureBox3.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox3.Tag = "f";
                pictureBox4.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox4.Tag = "f";
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            if (pictureBox2.Tag.ToString() == "f")
            {
                pictureBox2.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox2.Tag = "t";
            }
            else
            {
                pictureBox2.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox2.Tag = "f";
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            if (pictureBox3.Tag.ToString() == "f")
            {
                pictureBox3.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox3.Tag = "t";
            }
            else
            {
                pictureBox3.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox3.Tag = "f";
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
            if (pictureBox4.Tag.ToString() == "f")
            {
                pictureBox4.Image = BilliardWindowsApplication.Properties.Resources.rbtselected;
                pictureBox4.Tag = "t";
            }
            else
            {
                pictureBox4.Image = BilliardWindowsApplication.Properties.Resources.rbtunselected;
                pictureBox4.Tag = "f";
            }
        }

    }
}
