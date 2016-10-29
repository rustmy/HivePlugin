﻿using System;

namespace Oxide.Ext.Hive.Net.Inbound {
	public class Restart : BaseAnswer {
		public int secs;
		public string message;

		public override void function(string id) {
			ServerMgr.RestartServer(message, secs);
		}
	}
}

