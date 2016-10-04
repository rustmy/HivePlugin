using System;
using System.Collections.Generic;

namespace Oxide.Ext.Hive.Net
{
	// Answer from the Hive Network
	class HiveNetPackage
	{
		public Dictionary<string, string> header;
		public string body;

		public HiveNetPackage(Dictionary<string, string> header, string body)
		{
			this.header = header;
			this.body = body;
		}
	}
}

