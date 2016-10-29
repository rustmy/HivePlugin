using System;
using System.Collections.Generic;
using Network;
using Newtonsoft.Json;
using Oxide.Ext.Hive.Plugin;
using Oxide.Ext.Hive.Utils;
using UnityEngine;

namespace Oxide.Ext.Hive.Net.Answers {
	public class DisposePlayer : BaseAnswer {
		public ulong steamID;

		public DisposePlayer()
		{
		}


		// Disposes this player and sends his information to HiveNet

		public override void function(string id) {
			
			// Get Player
			BasePlayer player = null;
			foreach(BasePlayer p in BasePlayer.sleepingPlayerList) {
				if(steamID == p.userID) {
					player = p;
					break;
				}
			}


			// Player not existing
			if(player == null) {
				// Send Error to HiveNet
				Requests.DisposePlayerFailed failreq = new Requests.DisposePlayerFailed();
				string failmsg = HiveNetHandler.createHiveNetReq(failreq.type, failreq, id);
				HiveNetHandler.getInstance().SendHiveNetRequest(failmsg);
				return;
			}



			// Get Player Data
			ulong steamid = player.userID;
			float x = player.GetCenter().x;
			float y = player.GetCenter().y;
			float z = player.GetCenter().z;
			float rotX = 0;
			float rotY = 0;
			float rotZ = 0;
			string serInvent = JsonConvert.SerializeObject(HiveInventory.fromInventory(player.inventory));
			float health = player.health;

			// Send Player Data
			Net.Requests.DisposePlayerResult req = new Net.Requests.DisposePlayerResult(steamid, x, y, z, rotX, rotY, rotZ, serInvent, health);
			string msg = HiveNetHandler.createHiveNetReq(req.type, req, id);
			HiveNetHandler.getInstance().SendHiveNetRequest(msg);


			// Clear and kill
			player.inventory.Strip();
			player.KillMessage();

			// Dispose corpse
			PlayerCorpse[] c = Resources.FindObjectsOfTypeAll<PlayerCorpse>();
			foreach(PlayerCorpse p in c) {
				if(p.playerSteamID == steamID) {
					p.KillMessage();
					break;
				}
			}



		}



	}
}

