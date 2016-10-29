using System;
using Rust.Xp;

namespace Oxide.Ext.Hive.Net.Inbound {
	public class GiveExp : BaseAnswer {
		public string player;
		public int multiplier;

		public override void function(string id) {
			BasePlayer play = BasePlayer.Find(player);

			if(play != null)
				play.xp.Add(Definitions.Cheat, multiplier);
		}
	}
}

