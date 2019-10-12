using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilliardWindowsApplication
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		private static Mutex mutex = null;
		[STAThread]

		static void Main()
		{
			
			
			try
			{
				const string appName = "BilliardWindowsApplication";
				bool createdNew;

				System.Threading.Thread.Sleep(1000);
				mutex = new Mutex(true, appName, out createdNew);

				if (!createdNew)
				{
					MessageBox.Show("app is already running! Exiting the application");
					return;
				}

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				WindowsIdentity identity = WindowsIdentity.GetCurrent();
				WindowsPrincipal principal = new WindowsPrincipal(identity);
				if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
				{
					MessageBox.Show("You must run this application as administrator. Terminating.");
					Application.Exit();
				}
				else
				{
					if (!BallTrackAPI.mbInitialized)
						BallTrackAPI.mbInitialized = BallTrackAPI.InitSDK(BallTrackAPI.PtrBallPosProc, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

					if (BallTrackAPI.m_nInputMethod == 0)
						Application.Run(new FrmGameSetup());
					else
					{
						//Application.Run(new frmClubWelcome(new biliardService.clubDetails()));
						Application.Run(new frmGameScore3NEW(new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0)));
						//Application.Run(new FrmTeaching());
					}
				}
			}
			catch (Exception ex)
			{
				if (ex != null) { }
			}
			if (BallTrackAPI.BTAPI_IsCameraConnected())
				BallTrackAPI.BTAPI_DisconnectCamera();
			if (BallTrackAPI.mbInitialized)
				BallTrackAPI.BTAPI_Free();
		}
	}
}