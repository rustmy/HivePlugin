using System;
namespace Oxide.Ext.Hive.Net.Requests
{
	public class GetPlayerInfo : BaseRequest
	{
		public ulong steamid;

		public GetPlayerInfo(ulong steamid) : base("GetPlayerInfo")
		{
			this.steamid = steamid;
		}
	}
}

