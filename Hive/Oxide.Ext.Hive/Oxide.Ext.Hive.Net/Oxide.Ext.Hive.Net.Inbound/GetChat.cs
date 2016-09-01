using System;
using System.Collections.Generic;
using System.IO;
using Oxide.Core;
using Oxide.Ext.Hive.Net.Requests;

namespace Oxide.Ext.Hive.Net.Answers
{
	public class GetChat : BaseAnswer
	{
		public GetChat()
		{
		}

		public override void function(string id)
		{
			// Get Chat file
			String file = Interface.Oxide.LogDirectory + Path.DirectorySeparatorChar + "Log.chat.txt";
			if (!File.Exists(file))
			{
				File.Create(file);
			}

			// Get last 50 lines
			string[] lines = File.ReadAllLines(file);
			List<ChatLog.Message> log = new List<ChatLog.Message>(50);

			for (int i = lines.Length - 1, j = 0; i > 0 && j < 50; i--, j++)
			{
				log.Add(new ChatLog.Message("", lines[i],""));
			}

			// Send ChatLog
			Requests.ChatLog req = new Requests.ChatLog(log);
			string msg = HiveNetHandler.createHiveNetReq(req.type, req, id);
			HiveNetHandler.getInstance().SendHiveNetRequest(msg);
		}
	}
}

