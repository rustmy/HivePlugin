using System;
namespace Oxide.Ext.Hive.Net.Answers
{
	public class GetOnlinePlayers : BaseAnswer
	{
		public GetOnlinePlayers()
		{
		}

		public override void function(string id)
		{
			Net.Requests.OnlinePlayers req = new Requests.OnlinePlayers(BasePlayer.activePlayerList);
			HiveNetHandler.getInstance().SendHiveNetRequest(HiveNetHandler.createHiveNetReq(req.type, req));
		}
	}
}

