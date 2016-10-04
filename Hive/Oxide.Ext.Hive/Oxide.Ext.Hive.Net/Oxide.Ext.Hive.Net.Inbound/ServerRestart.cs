using System;
using Oxide.Core;
namespace Oxide.Ext.Hive.Net.Answers
{
	public class ServerRestart : BaseAnswer
	{
		public ServerRestart()
		{
		}

		public override void function(string id)
		{
			GlobalVars.save = false;
			ServerMgr.RestartServer("Hive Backup", 0);
		}
	}
}

