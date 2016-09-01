using System;
using System.Collections.Generic;
namespace Oxide.Ext.Hive.Net.Requests
{
	public class ChatLog : BaseRequest
	{
		public List<Message> chat;
		
		public ChatLog(List<Message> list) : base("ChatLog")
		{
			this.chat = list;
		}

		public class Message
		{
			public string playername;
			public string message;
			public string time;

			public Message(string playername, string message, string time)
			{
				this.playername = playername;
				this.message = message;
				this.time = time;
			}

		}
	}
}

