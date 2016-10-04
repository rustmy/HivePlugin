
using System.Collections.Generic;
using Oxide.Ext.Hive.Utils;

namespace Oxide.Ext.Hive.Net.Requests
{
	public class SleeperList : BaseRequest
	{
		private List<ulong> steamIDs;


		public SleeperList(List<BasePlayer> ids) : base("SleeperList")
		{
			steamIDs = convertToSteamIDList(ids);
		}


		private List<ulong> convertToSteamIDList(List<BasePlayer> list)
		{
			List<ulong> result = new List<ulong>();

			foreach(BasePlayer p in list)
			{
				result.Add(p.userID);
			}

			return result;
		}
	}
}

