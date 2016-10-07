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
				// Create playerQueue if not inited
				if (HiveVars.playerQueue == null)
				{
					HiveVars.playerQueue = new Dictionary<ulong, PlayerInfo>();
				}

				// Check if player is already sleeping on this server
				foreach (BasePlayer p in BasePlayer.sleepingPlayerList)
				{
					// If existing, player can be ignored for respawn
					if (p.userID == steamid)
					{
						HiveVars.playerQueue.Add(steamid, this);
						return;
					}
				}

				HiveVars.playerQueue.Add(steamid, this);

				if (GlobalVars.DEBUG)
				{
					OxideUtils.Puts("Hive", "ID: " + steamid + " wurde in die Queue verschoben");
					OxideUtils.Puts("Hive", "Health: " + health);
				}

				// Player is dead in Hive and is not sleeping so he needs to respawn, so we don't need to respawn him manually (he can maybe see his death screen)
				if (health <= 0.0)
				{
					return;
				}

				Thread.Sleep(2750);
				player.Respawn();

				if (GlobalVars.DEBUG)
					OxideUtils.Puts("Hive", "ID: " + steamid + " respawned");
			}
		}
	}
}

