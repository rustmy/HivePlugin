using System;
namespace Oxide.Ext.Hive.Net.Requests
{
	public class DisposePlayerResult : BaseRequest
	{
		public ulong steamid;
		public double x;
		public double y;
		public double z;
		public double rot_x;
		public double rot_y;
		public double rot_z;
		public string inventory;
		public float health;


		public DisposePlayerResult(ulong steamid, double x, double y, double z, double rot_x, double rot_y, double rot_z, string inventory, float health) : base("DisposePlayerResult")
		{
			this.steamid = steamid;
			this.x = x;
			this.y = y;
			this.z = z;
			this.rot_x = rot_x;
			this.rot_y = rot_y;
			this.rot_z = rot_z;
			this.inventory = inventory;
			this.health = health;
		}
	}
}

