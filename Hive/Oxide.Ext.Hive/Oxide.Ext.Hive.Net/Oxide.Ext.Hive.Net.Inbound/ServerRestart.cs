using System;
using Oxide.Core;

namespace Oxide.Ext.Hive.Net.Answers {

	// ServerRestart for backup
	public class ServerRestart : BaseAnswer {
		public ServerRestart ()
		{
		}

		public override void function(string id) {
			GlobalVars.save = false;
			ServerMgr.RestartServer ("Hive Backup", 0);
		}
	}
}

