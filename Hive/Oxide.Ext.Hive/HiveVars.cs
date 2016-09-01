using System;
using System.Collections.Generic;
using Oxide.Ext.Hive.Net.Answers;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.Hive
{
	// A really dirty class to save global vars -- I'm not proud of it
	public class HiveVars : GlobalVars
	{
		
		public static Dictionary<ulong, PlayerInfo> playerQueue;
		public static List<CuiElement> GUI;


		public HiveVars()
		{
		}
	}
}

