using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Runtime.InteropServices;

namespace BilliardWindowsApplication
{
    class BLL_BilliardWindowsApplication
    {
        //public static string weblocation = api.getlocation;
        public static string weblocation = "http://score.biliardoprofessionale.it/";
      
        public static biliardService.Details player1 = new biliardService.Details();
        public static biliardService.Details player2 = new biliardService.Details();
        public static biliardService.Details player3 = new biliardService.Details();
        public static biliardService.Details player4 = new biliardService.Details();
		public static biliardService.Details teacher = new biliardService.Details();


        public static System.Drawing.Image p1 = null;
        public static System.Drawing.Image p2 = null;
        public static System.Drawing.Image p3 = null;
        public static System.Drawing.Image p4 = null;
        public static System.Drawing.Image c1 = null;
        public static System.Drawing.Image c2 = null;
        public static System.Drawing.Image c3 = null;
        public static System.Drawing.Image c4 = null;

        public static bool id = false;
        public static bool panalty = false;
        public static bool zero = false;
        public static bool email = false;

        public static int point = 0;
        public static int set = 1;
        public static int quills = 5;
        public static int timer1 = 40;
        public static int timer2 = 20;

        public static bool gametime = true;
        public List<memorydetails> memo = new List<memorydetails>();

        public static biliardService.clubDetails clubDetailsStatic = new biliardService.clubDetails();
        public static biliardService.costDetails costDetailsStataic = new biliardService.costDetails();
        public static biliardService.gamecostdetails gamecostdetailsStatic = new biliardService.gamecostdetails();
        public static int languague = 1; //0 indian , 1 italiano
		public static void playclicksound()
		{
			try
			{
				new SoundPlayer(BilliardWindowsApplication.Properties.Resources.button_16).Play();
			}
			catch (Exception ex) { Console.Write("exno tgs2" + ex.ToString()); }
		}
    }

    class bllBillardLogic
    {
        biliardService.BilliardScoreboard API = new biliardService.BilliardScoreboard();
        public TimeSpan convertDbToTime(string DbTime)
        {

            int hrs = 0, min = 0, sec = 0;
            DbTime = DbTime.Trim();

            try
            {
                if (DbTime.Contains("PM"))
                {
                    DbTime = DbTime.Replace("PM", " ").Trim();
                    List<string> str = DbTime.Split(':').ToList();
                    hrs = int.Parse(str[0]) + 12;
                    min = int.Parse(str[1]);
                }
                else
                {
                    DbTime = DbTime.Replace("AM", " ").Trim();
                    List<string> str = DbTime.Split(':').ToList();
                    hrs = int.Parse(str[0]);
                    min = int.Parse(str[1]);
                }
                hrs = hrs % 24;
            }
            catch (Exception ex) { API.getmailAsync("info", "golumolu808@gmail.com", "nbaghel777@gmail.com", "error in bll project", "dbtime=" + DbTime.ToString() + "time current =" + hrs.ToString() + " : " + min.ToString() + " error message is :- " + ex.ToString()); }
            return new TimeSpan(hrs, min, sec);


        }
    }
   public class memorydetails
    {
      public bool player1 = false;
      public int  extra = 0;      // 0 for not extra, 1 for timer 1, 2 for timer 2, 3 for extra other player, 4 for handicap, 5 wrong ball
      public int score = 0;
      public string turn = "";
      public string point = "";
      public string set="";
      
      public bool isreplayrecord = false;
      public int replayrecord = 0;
	  public int recordplayer = 0;
	  public int replayset = 0;
    }

   public class usbrelay
   {
       //-------------------------------------------usbrelay start--------------------------------------------------------
       public const string strPath = "usb_relay_device.dll";
       static usb_relay_device_info relayDevice;
       enum usb_relay_device_type
       {
           USB_RELAY_DEVICE_ONE_CHANNEL = 1,
           USB_RELAY_DEVICE_TWO_CHANNEL = 2,
           USB_RELAY_DEVICE_FOUR_CHANNEL = 4,
           USB_RELAY_DEVICE_EIGHT_CHANNEL = 8
       };
       /*usb relay board info structure*/
       [StructLayout(LayoutKind.Sequential)]
       unsafe struct usb_relay_device_info
       {
           public char* serial_number;
           public char* device_path;
           public usb_relay_device_type type;
           public usb_relay_device_info* next;
       };

       /*init the USB Relay Libary
  @returns: This function returns 0 on success and -1 on error.
  */
       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern int usb_relay_init();
       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern int usb_relay_exit();

       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern IntPtr usb_relay_device_enumerate();
       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern void usb_relay_device_free_enumerate(IntPtr hWnd);



       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern int usb_relay_device_open(IntPtr hWnd);
       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern unsafe int usb_relay_device_open_with_serial_number(char* serial_number, byte len);
       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern int usb_relay_device_close(int hHandle);



       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern int usb_relay_device_open_one_relay_channel(int hHandle, int index);
       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern int usb_relay_device_open_all_relay_channel(int hHandle);
       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern int usb_relay_device_close_one_relay_channel(int hHandle, int index);
       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern int usb_relay_device_close_all_relay_channel(int hHandle);


       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern int usb_relay_device_get_status(int hHandle, IntPtr status);
       [DllImport(strPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
       public static extern int usb_relay_device_set_serial(int hHandle, char[] serial);

       public static string serial_number = "";
       public unsafe static char* serial_numberchar = null;
       public static string device_path = "";
       public static int type = 0;
       public static int hHandle = 0;

       public static int Status = 0;
       public static int relay1on = 0;     //light
       public static int relay2on = 0;     // ball



       public static void nbinitusbrelay()
       {
           if (Status == 0)
           {
               Status = 1;
               unsafe
               {
                   int a = usb_relay_init();
                   usb_relay_device_info* allrelayDevice = (usb_relay_device_info*)usb_relay_device_enumerate();
                   if (allrelayDevice != null)
                   {
                       serial_numberchar = allrelayDevice->serial_number;
                       serial_number = new string(allrelayDevice->serial_number);
                       device_path = new string(allrelayDevice->device_path);
                       type = (int)allrelayDevice->type;
                   }
                   else
                   {
                       //lblserial_number.Text = "null";
                   }
                   hHandle = usb_relay_device_open_with_serial_number(serial_numberchar, (byte)serial_number.Count());

                   a = usb_relay_device_open_one_relay_channel(hHandle, 01);
                   if (a == 0) relay1on = 1;

                   //a = usb_relay_device_open_one_relay_channel(hHandle, 02);
                   //if (a == 0)  relay2on = true;

               }
           }
           else
           {
               //MessageBox.Show("already init");
           }

       }
       public static void nbreleaseusbrelay()
       {
           if (Status == 1)
           {
               int a = 1;
               if (relay1on == 1)
               {
                   a = usb_relay_device_close_one_relay_channel(hHandle, 01);
                   if (a == 0) relay1on = 0;
               }
               if (relay2on == 1)
               {
                   a = usb_relay_device_close_one_relay_channel(hHandle, 02);
                   if (a == 0) relay2on = 0;
               }

               if (relay1on == 0)
               {
                   if (relay2on == 0)
                   {
                       Status = 0;
                       usb_relay_device_free_enumerate(IntPtr.Zero);
                       usb_relay_device_close(hHandle);
                       a = usb_relay_exit();
                   }
                   //else MessageBox.Show("close relay2 first");
               }
               //else MessageBox.Show("close relay1 first");
           }
       }

       //------------------------------------------usbrelay end------------------------------------------------------------

   }
}
