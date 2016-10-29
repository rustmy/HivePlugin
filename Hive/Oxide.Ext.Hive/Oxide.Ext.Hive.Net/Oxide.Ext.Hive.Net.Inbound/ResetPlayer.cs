
using Oxide.Ext.Hive.Net.Inbound;

namespace Oxide.Ext.Hive.Net.Inbound {

	// Removes a user from the PlayerPosDB because he died on another server
	public class ResetPlayer : BaseAnswer {

		public ulong steamid;

		public ResetPlayer()
		{
			
		}

		public override void function(string id) {
			// TODO only remove, not pop for more performance
			PlayerPosDB.getInstance().PopUser(steamid);
		}


	}
}

