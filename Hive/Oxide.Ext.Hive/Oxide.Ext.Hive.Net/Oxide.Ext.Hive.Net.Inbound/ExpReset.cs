using System;
namespace Oxide.Ext.Hive.Net.Answers
{
	public class ExpReset : BaseAnswer
	{
		public string player;

		public override void function(string id)
		{
			BasePlayer play = BasePlayer.Find(player);

			if (play != null)
				play.xp.Reset();
		}
	}
}

