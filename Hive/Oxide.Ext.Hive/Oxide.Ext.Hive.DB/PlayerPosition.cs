using System;
namespace Oxide.Ext.Hive
{
	public class PlayerPosition
	{
		public ulong steamID;
		public float x;
		public float y;
		public float z;

		public PlayerPosition(ulong steamID, float x, float y, float z)
		{
			this.steamID = steamID;
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}
}

