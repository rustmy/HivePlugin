using System;
namespace Oxide.Ext.Hive.Net.Answers
{
	public class Chat : BaseAnswer
	{
		public string message;
		public bool admin;
		public string name;

		public override void function(string id)
		{
			ConsoleSystem.Run.Server.Normal("say", new string[] { "[" + name + "] " + message });
		}
	}
}

