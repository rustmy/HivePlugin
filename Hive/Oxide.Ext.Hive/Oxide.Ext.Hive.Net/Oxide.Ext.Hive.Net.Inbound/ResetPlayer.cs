using System;
using Oxide.Ext.Hive.Net.Answers;

namespace Oxide.Ext.Hive.Answers {
	public class ResetPlayer : BaseAnswer {

		public ulong steamid;

		public ResetPlayer()
		{
			
		}

		public override void function(string id) {
			PlayerPosDB.getInstance().PopUser(steamid);
		}


	}
}

