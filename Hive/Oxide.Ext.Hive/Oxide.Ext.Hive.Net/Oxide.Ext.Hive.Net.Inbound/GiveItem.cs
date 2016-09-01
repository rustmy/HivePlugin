using System;
namespace Oxide.Ext.Hive.Net.Answers
{
	public class GiveItem : BaseAnswer
	{
		public int multiplier;
		public string player;
		public int item;

		public override void function(string id)
		{
			BasePlayer play = BasePlayer.Find(player);

			if (play != null)
			{
				if (!play.inventory.containerMain.IsFull())
				{
					play.inventory.GiveItem(ItemManager.CreateByItemID(item, multiplier));
				}
			}
		}
	}
}

