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
	public enum MessageStyle {SaveShot, SetupIncomplete, CancelShot}
	public partial class FrmMessageBox : Form
	{
		private MessageStyle m_msgType = MessageStyle.SaveShot;
		public FrmMessageBox()
		{
			InitializeComponent();
		}

		public MessageStyle MessageMode
		{
			get
			{
				return m_msgType;
			}
			set
			{
				m_msgType = value;
				if (m_msgType == MessageStyle.SaveShot)
				{
					picOK.Visible = false;
					picYes.Visible = true;
					picCancel.Visible = true;
					picBackground.Image = BilliardWindowsApplication.Properties.Resources.ask_save;
				}
				else if (m_msgType == MessageStyle.SetupIncomplete)
				{
					picOK.Visible = true;
					picYes.Visible = false;
					picCancel.Visible = false;
					picBackground.Image = BilliardWindowsApplication.Properties.Resources.setup_complete;
				}
				else if (m_msgType == MessageStyle.CancelShot)
				{
					picOK.Visible = false;
					picYes.Visible = true;
					picCancel.Visible = true;
					picBackground.Image = BilliardWindowsApplication.Properties.Resources.ask_cancel_shot;
				}
			}
		}
		private void picOK_Click(object sender, EventArgs e)
		{
			BLL_BilliardWindowsApplication.playclicksound();
			
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void picCancel_Click(object sender, EventArgs e)
		{
			BLL_BilliardWindowsApplication.playclicksound();

			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void picOK_Click_1(object sender, EventArgs e)
		{
			BLL_BilliardWindowsApplication.playclicksound();
			DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
