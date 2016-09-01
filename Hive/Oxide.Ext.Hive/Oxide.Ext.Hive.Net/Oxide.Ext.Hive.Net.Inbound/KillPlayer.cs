using System;
using Oxide.Ext.Hive.Utils;
namespace Oxide.Ext.Hive.Net.Answers
{
	public class KillPlayer : BaseAnswer
	{
		public string player;
		public string reason;
		public float delay;

		public override void function(string id)
		{
			BasePlayer play = BasePlayer.Find(player);

			if (play != null)
			{
				// TODO: Better dying with a reason that the player can see (instead of suicide)
				// 12.8.16: play.Kill() and play.KillMessage() and play.Hurt() dont work
				play.Command("kill");
				play.Command("kill");
			}
		}
	}
}

