using System;
namespace Oxide.Ext.Hive.Net.Requests
{
	public class BaseRequest
	{
		[NonSerialized]
		public string type;

		public BaseRequest(string type)
		{
			this.type = type;
		}
	}
}

