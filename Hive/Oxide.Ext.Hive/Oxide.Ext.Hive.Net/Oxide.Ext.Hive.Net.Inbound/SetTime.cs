using System;

namespace Oxide.Ext.Hive.Net.Inbound {
	public class SetTime: BaseAnswer {
		// (0-24)
		public byte time;

		public SetTime(string id)
		{
		}

		public override void function(string id) {
			
		}
	}
}

