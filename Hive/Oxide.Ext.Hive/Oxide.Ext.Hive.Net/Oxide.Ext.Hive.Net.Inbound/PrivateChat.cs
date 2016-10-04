using System;
namespace Oxide.Ext.Hive.Net.Answers
{
	public class PrivateChat : BaseAnswer
	{
		public string message;
		public bool admin;
		public string player;

		public override void function(string id)
		{
			BasePlayer play = BasePlayer.Find(player);

			if (play != null)
				play.ChatMessage(message);
		}
	}
}

