using System;
using System.Collections.Generic;
using System.IO;
using Oxide.Core;
using Oxide.Ext.Hive.Net.Requests;

namespace Oxide.Ext.Hive.Net.Answers
{
	// TODO: Partial reading because file can go big
	public class GetChat : BaseAnswer
	{
		public GetChat()
		{
		}

		public override void function(string id)
		{
			// Get Chat file
			string file = new DirectoryInfo(Interface.Oxide.LogDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Log.Chat.txt";
			if (!File.Exists(file))
			{
				File.Create(file);
			}

			// Get last 50 lines
			string[] lines = File.ReadAllLines(file);
			List<string> log = new List<string>(50);

			for (int i = lines.Length - 1, j = 0; i > 0 && j < 50; i--, j++)
			{
				// TODO: Per line parsing for playername - time
				log.Add(lines[i]);
			}

			// Send ChatLog
			Requests.ChatLog req = new Requests.ChatLog(log);
			string msg = HiveNetHandler.createHiveNetReq(req.type, req, id);
			HiveNetHandler.getInstance().SendHiveNetRequest(msg);
		}
	}
}

