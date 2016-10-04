using System;
using System.Collections.Generic;

namespace Oxide.Ext.Hive.Net.Requests
{
	public class OnlinePlayers : BaseRequest
	{
		public List<ulong> playerList;

		public OnlinePlayers(List<BasePlayer> playerList) : base("OnlinePlayers")
		{
			this.playerList = new List<ulong>(playerList.Count);

			foreach(BasePlayer p in playerList)
			{
				this.playerList.Add(p.userID);
			}
		}
	}
}

