using System;
namespace Oxide.Ext.Hive.Net.Requests
{
	public class PlayerInfo : BaseRequest
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
		public float hunger;
		public float thirst;
		public float expspent;
		public float exp;
		public string blueprints;


		public PlayerInfo(ulong steamid, double x, double y, double z, double rot_x, double rot_y, double rot_z, string inventory, float health, float hunger, float thirst, float expspent, float exp, string blueprints) : base("PlayerInfo")
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
			this.hunger = hunger;
			this.thirst = thirst;
			this.expspent = expspent;
			this.exp = exp;
			this.blueprints = blueprints;
		}

		public PlayerInfo(ulong steamid, float expspent, float exp, string blueprints) : base("PlayerInfo")
		{
			this.steamid = steamid;
			this.x = -1;
			this.y = -1;
			this.z = -1;
			this.rot_x = -1;
			this.rot_y = -1;
			this.rot_z = -1;
			this.inventory = "";
			this.health = -1;
			this.hunger = -1;
			this.thirst = -1;
			this.expspent = expspent;
			this.exp = exp;
			this.blueprints = blueprints;
		}
	}
}

