using System;
using System.Collections.Generic;
using Oxide.Ext.Hive.Net.Inbound;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.Hive {
	// A really dirty class to save global vars -- I'm not proud of it
	public class HiveVars : GlobalVars {

		// Player which should be synced
		public static Dictionary<ulong, PlayerInfo> playerQueue;
		public static List<CuiElement> GUI;
		public static int hivecount;

		public HiveVars()
		{
		}
	}
}

