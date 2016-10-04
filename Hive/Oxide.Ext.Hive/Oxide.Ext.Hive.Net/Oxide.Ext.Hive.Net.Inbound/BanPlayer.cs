using System;
using Oxide.Ext.Hive.Utils;

namespace Oxide.Ext.Hive.Net.Answers
{
	public class BanPlayer : BaseAnswer
	{
		public string player;
		public string reason;

		public override void function(string id)
		{
			BasePlayer play = BasePlayer.Find(player);
			if (play != null)
				ConsoleSystem.Run.Server.Normal("banid", new String[] { play.userID.ToString(), reason });
		}
	}
}

