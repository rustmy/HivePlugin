using System;
namespace Oxide.Ext.Hive.Net.Requests
{
	public class SetPlayer : BaseRequest
	{
		public long steamid;
		public double x;
		public double y;
		public double z;
		public double rot_x;
		public double rot_y;
		public double rot_z;
		public string inventory;
		public float hunger;
		public float thirst;
		public float expspent;
		public float exp;
		public string blueprints;

		public SetPlayer(long steamid, double x, double y, double z, double rot_x, double rot_y, double rot_z, string inventory, float hunger, float thirst, float expspent, float exp, string blueprints) : base("SetPlayer")
		{
			this.steamid = steamid;
			this.x = x;
			this.y = y;
			this.z = z;
			this.rot_x = rot_x;
			this.rot_y = rot_y;
			this.rot_z = rot_z;
			this.inventory = inventory;
			this.hunger = hunger;
			this.thirst = thirst;
			this.expspent = expspent;
			this.exp = exp;
			this.blueprints = blueprints;
		}
	}
}

