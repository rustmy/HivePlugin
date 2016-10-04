using System;



namespace Oxide.Ext.Hive.Net.Requests
               
{
	public class PlayerDeath : BaseRequest
	{
		public ulong steamid;


		public PlayerDeath(ulong steamID) : base ("PlayerDeath")
		{
			this.steamid = steamID;
		}
	}
}

