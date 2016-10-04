using System;
using System.Collections.Generic;
namespace Oxide.Ext.Hive.Net.Requests
{
	public class ChatLog : BaseRequest
	{
		public List<string> chat;
		
		public ChatLog(List<string> list) : base("ChatLog")
		{
			this.chat = list;
		}

	}
}

