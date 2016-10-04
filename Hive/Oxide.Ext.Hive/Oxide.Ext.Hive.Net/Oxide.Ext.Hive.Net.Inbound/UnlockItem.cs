using System;
namespace Oxide.Ext.Hive.Net.Answers
{
	public class UnlockItem : BaseAnswer
	{
		public string player;
		public int itemID;

		public override void function(string id)
		{
			BasePlayer play = BasePlayer.Find(player);

			if (play != null)
				play.blueprints.Unlock(ItemManager.FindItemDefinition(itemID));
		}
	}
}

