﻿using System;

namespace Oxide.Ext.Hive.Net.Inbound {
	public class RawCommand : BaseAnswer {
		public string command;
		public string[] args;

		public override void function(string id) {
			ConsoleSystem.Run.Server.Normal(command, args);
		}
	}
}

