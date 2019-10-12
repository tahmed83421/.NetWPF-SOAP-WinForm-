using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilliardWindowsApplication
{
	public class ShotMetaInfo
	{
		public string Name;
		public string Description;
		public int nPlayer;
		public int nShot;
		public List<int> lstRailMarkValue = new List<int>();
		public int InSide = -1;
		public int OutSide = -1;
	}
}
