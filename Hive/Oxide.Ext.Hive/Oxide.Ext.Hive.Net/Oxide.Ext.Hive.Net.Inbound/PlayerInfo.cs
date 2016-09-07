using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Oxide.Ext.Hive.Utils;
using Rust.Xp;

namespace Oxide.Ext.Hive.Net.Answers
{
	// Answer from GetPlayer request
	public class PlayerInfo : BaseAnswer
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

		public PlayerInfo()
		{

		}

		public override void function(string id)
		{
			BasePlayer player = BasePlayer.FindByID(steamid);

			if (player != null)
			{
				if (HiveVars.playerQueue == null)
				{
					HiveVars.playerQueue = new Dictionary<ulong, PlayerInfo>();
				}

				HiveVars.playerQueue.Add(steamid, this);

				if (GlobalVars.DEBUG)
				{
					OxideUtils.Puts("Hive", "ID: " + steamid + " wurde in die Queue verschoben");
					OxideUtils.Puts("Hive", "Health: " + health);
				}
				Thread.Sleep(2750);
				player.Respawn();

				if (GlobalVars.DEBUG)
					OxideUtils.Puts("Hive", "ID: " + steamid + " respawned");

				// Player is dead in HiveNet answer
				if (health <= 0.0f)
				{
					HiveVars.playerQueue.Remove(steamid);
				}
			}

		}


	}
}

