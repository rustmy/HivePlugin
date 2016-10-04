using System;
using Oxide.Ext.Hive.Utils;

namespace Oxide.Ext.Hive.Net.Answers
{
	public class UnlockAllItems : BaseAnswer
	{
		public string player;

		public override void function(string id)
		{
			BasePlayer play = BasePlayer.Find(player);

			if (play != null)
			{
				foreach (ItemDefinition item in ItemManager.itemList)
				{
					play.blueprints.Unlock(item);
				}
			} else {
				OxideUtils.PrintError("Hive","Player " + player + " not found for unlocking all items");
			}
				
		}
	}
}

