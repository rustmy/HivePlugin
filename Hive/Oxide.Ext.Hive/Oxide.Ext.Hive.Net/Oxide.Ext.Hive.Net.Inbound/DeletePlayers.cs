using System;
using System.Collections.Generic;
namespace Oxide.Ext.Hive.Net.Answers
{
	public class DeletePlayers : BaseAnswer
	{
		public List<ulong> steamIDs;

		public DeletePlayers()
		{
			
		}

		public override void function(string id)
		{
			foreach(ulong pl in steamIDs)
			{
				BasePlayer player = BasePlayer.FindByID(pl);
				if (player != null)
				{
					player.KillMessage();
				}
			}
		}
	}
}

