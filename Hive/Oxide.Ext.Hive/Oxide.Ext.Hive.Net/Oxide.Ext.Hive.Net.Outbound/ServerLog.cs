using System;
using System.Collections.Generic;
namespace Oxide.Ext.Hive.Net.Requests
{
	public class ServerLog : BaseRequest
	{
		public List<String> log;


		public ServerLog(List<String> log) : base("ServerLog")
		{
			this.log = log;
		}
	}
}

