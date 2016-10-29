using System;
using Oxide.Ext.Hive.Utils;

namespace Oxide.Ext.Hive.Net.Inbound {
	public class KickPlayer : BaseAnswer {
		public string player;
		public string reason;
		public float delay;

		public override void function(string id) {
			BasePlayer play = BasePlayer.Find(player);

			play.Kick(reason);


		}
	}
}

