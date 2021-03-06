﻿using System;
using Oxide.Core;
using Oxide.Ext.Hive.Utils;
using System.IO;
using System.Collections.Generic;

namespace Oxide.Ext.Hive.Net.Inbound {
	// TODO: Partial reading at the end of file because file can get really big
	public class GetLog : BaseAnswer {
		public GetLog()
		{
		}

		public override void function(string id) {

			string file = new DirectoryInfo(Interface.Oxide.LogDirectory).Parent.Parent.FullName + Path.DirectorySeparatorChar + "Log.Log.txt";


			if(!File.Exists(file)) {
				File.Create(file);
			}

			string[] lines = File.ReadAllLines(file);
			List<string> log = new List<string>(50);

			for(int i = lines.Length - 1, j = 0; i > 0 && j < 50; i--,j++) {
				log.Add(lines[i]);
			}


			Requests.ServerLog req = new Requests.ServerLog(log);
			string msg = HiveNetHandler.createHiveNetReq(req.type, req, id);
			HiveNetHandler.getInstance().SendHiveNetRequest(msg); 
		}
	}
}

