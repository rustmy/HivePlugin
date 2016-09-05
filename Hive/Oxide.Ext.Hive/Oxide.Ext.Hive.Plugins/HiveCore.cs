using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Game.Rust.Cui;
using Oxide.Core.Plugins;
using Oxide.Core;
using Oxide.Ext.Hive.Net;
using Oxide.Ext.Hive.Utils;
using Oxide.Ext.Hive.Config;
using System.IO;
using Oxide.Ext.Utils;
using WebSocketSharp;


namespace Oxide.Ext.Hive.Plugins
{

	class HiveCore : CSPlugin
	{
		public static readonly VersionNumber VERSION = new Core.VersionNumber(0, 8, 0);

		// Timeout for WebRequests

		private static JsonSerializerSettings JSONCONF;
		private const float GUITIMEOUT = 15f;
		private HiveNetHandler nethandler;


		public HiveCore()
		{
			Name = "Hive";
			Version = VERSION;
			Author = "Maverick Applications";
			ResourceId = 0;
			HasConfig = true;


		}
		// Main-Method - Not relevant
		public static void Main()
		{
			
		}


		// *Oxide Event* - Server initilized
		[HookMethod("OnServerInitialized")]
		void OnServerInitialized()
		{
			GlobalVars.save = true;

			// Send SleeperList to Hive
			Net.Requests.SleeperList req = new Net.Requests.SleeperList(BasePlayer.sleepingPlayerList);
			nethandler.SendHiveNetRequest(HiveNetHandler.createHiveNetReq(req.type, req));

			// Start listening to HiveNet
			nethandler.StartListening();


			// Get GUI
			Net.Requests.GetGUI req2 = new Net.Requests.GetGUI();
			nethandler.SendHiveNetRequest(HiveNetHandler.createHiveNetReq(req2.type, req2));
		}


		// *Oxide Event* - Plugin loaded
		[HookMethod("Init")]
		void Init()
		{
			OxideUtils.Puts("Hive", "Hive is initializing...");

			// Loading default configuration
			LoadDefaultConfig();


			GlobalVars.DEBUG = Boolean.Parse(OxideConfiguration.getConfigKey("hive", "debug"));
			GlobalVars.logger = new Ext.Utils.Logger(Interface.Oxide.DataDirectory + Path.DirectorySeparatorChar + "hive.log");


			// Set JSON settings
			JSONCONF = new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore
			};


			// Init DB
			PlayerPosDB.getInstance().Init();


			// Init NetHandler
			nethandler = HiveNetHandler.getInstance(OxideConfiguration.getConfigKey("hive","server"), Convert.ToInt32(OxideConfiguration.getConfigKey("hive","server_port")));
			nethandler.SetOnReceive((o) => onHiveNetRequest(o));
			nethandler.Connect();
		}


		// *Oxide Event* - Creates a new default config file -> Gets automatically called by Oxide!!
		protected override void LoadDefaultConfig()
		{
			if (!OxideConfiguration.exists("hive") || OxideConfiguration.isEmpty("hive"))
			{
				OxideUtils.PrintWarning("Hive", "Loading default config. You have to configure it for your server!");

				OxideConfiguration.setConfigKey("hive","hive_name","My Hive Name");
				OxideConfiguration.setConfigKey("hive", "hive_password", "My Hive Password");
				OxideConfiguration.setConfigKey("hive","server","91.205.175.222");
				OxideConfiguration.setConfigKey("hive", "server_port", "8035");
				OxideConfiguration.setConfigKey("hive", "debug", "false");
			}

		}


		// *Oxide Event* - Player connects
		// TODO: Check if player is sleeping then ignore hive
		[HookMethod("OnPlayerInit")]
		void OnPlayerInit(BasePlayer player)
		{
			// Change name if needed
			changeDisplayName(player);

			// Get SteamID
			ulong steamID = player.userID;


			Net.Requests.GetPlayerInfo req = new Net.Requests.GetPlayerInfo(steamID);
			nethandler.SendHiveNetRequest(HiveNetHandler.createHiveNetReq(req.type, req));
		}


		// *Oxide Event* - Player wakes up
		[HookMethod("OnPlayerSleepEnded")]
		void OnPlayerSleepEnded(BasePlayer player)
		{
			// GUI anzeigen


			//if (HiveVars.GUI != null)
			//ShowGUI(player, HiveVars.GUI);



			// Get ID
			ulong id = player.userID;



			// Apply Info if in Queue
			if (HiveVars.playerQueue != null && HiveVars.playerQueue.ContainsKey(id))
			{
				
				PlayerUtils.ApplyPlayerInfo(player,HiveVars.playerQueue[id]);

				// Debug
				if (GlobalVars.DEBUG)
				{
					OxideUtils.PrintSuccess("Hive", "PlayerInfo for " + player.displayName + " applied");
					GlobalVars.logger.Print("PlayerInfo for " + player.displayName + " applied", LogLevel.OP);
				}

				// Is applied, so remove
				HiveVars.playerQueue.Remove(id);


				// Teleport to real server position from data file
				PlayerPosition pos = PlayerPosDB.getInstance().GetUser(player.userID);

				if (pos != null)
					player.Teleport(new UnityEngine.Vector3(pos.x, pos.y, pos.z));

				// Debug
				if (GlobalVars.DEBUG)
				{
					OxideUtils.PrintSuccess("Hive", player.displayName + "successfully teleported for player init");
					GlobalVars.logger.Print(player.displayName + "successfully teleported for player init", LogLevel.OP);
				}
			}
		}


		// Log player chat
		[HookMethod("OnPlayerChat")]
		void OnPlayerChat(ConsoleSystem.Arg arg)
		{
			// Write Into chat file
			String file = Interface.Oxide.LogDirectory + Path.DirectorySeparatorChar + "Log.chat.txt";

			if (!File.Exists(file))
			{
				File.Create(file);
			} 
			File.AppendAllText(file, arg + "\r\n", System.Text.Encoding.UTF8);
		}



		// Send Kill noti to HiveNet
		[HookMethod("OnEntityDeath")]
		void OnEntityDeath(BaseCombatEntity entity, HitInfo info)
		{
			if (entity as BasePlayer == null)
			{
				return;
			}

			BasePlayer player = entity.ToPlayer();

				Net.Requests.PlayerDeath req = new Net.Requests.PlayerDeath(player.userID);
				nethandler.SendHiveNetRequest(HiveNetHandler.createHiveNetReq(req.type, req));
			
		}


		// Changes the display name of a player if another player has the same name -> optimizable
		void changeDisplayName(BasePlayer player, int round = 1)
		{
			foreach (BasePlayer b in BasePlayer.activePlayerList)
			{
				if (player.displayName == b.displayName && player.userID != b.userID)
				{
					if (round == 1)
						player.displayName += player.displayName + " (" + round + ")";
					else {
						player.displayName = player.displayName.Substring(0, player.displayName.LastIndexOf('(')) + "(" + round + ")";
					}
					changeDisplayName(player, round++);
				}
			}
		}


		// HiveNet server sends information
		private void onHiveNetRequest(String ans)
		{

			// Show HiveNetAnswer
			if (GlobalVars.DEBUG)
			{
				OxideUtils.Puts("Hive", "HiveNet sent: " + ans);
				GlobalVars.logger.Print("HiveNet sent: " + ans, LogLevel.OP);
			}

			// No answer given
			if (String.IsNullOrEmpty(ans))
			{
				return;
			}

			HiveNetPackage answer = JsonConvert.DeserializeObject<HiveNetPackage>(ans);

			// Failed answer
			if (answer.header["Type"] == "Error")
			{
				OxideUtils.Puts("Hive", "HiveNet didn't return a successful result: " + answer.body);

			}

			try {
				object o = Activator.CreateInstance(Type.GetType("Oxide.Ext.Hive.Net.Answers." + answer.header["Type"]));
				JsonConvert.PopulateObject(answer.body, o, JSONCONF);
				Net.Answers.BaseAnswer obj = (Net.Answers.BaseAnswer)o;
				obj.function(answer.header["ID"]);
			} catch (Exception ex)
			{
				if (GlobalVars.DEBUG)
					OxideUtils.PrintError("Hive","HiveNet answer could not be parsed");

				GlobalVars.logger.Print(ex.StackTrace + "\r\n" + ex.Message, LogLevel.FatalError);
			}

		}


		// SHow GUI to Player
		void ShowGUI(BasePlayer player, List<CuiElement> elems)
		{
			CuiHelper.AddUi(player, elems);

			// GUI delay timer
			//new OxideTimer().Once(GUITIMEOUT, () =>
			//{
			//	foreach (CuiElement c in elems)
			//	{
			//		CuiHelper.DestroyUi(player, c.Name);
			//	}
			//
			//});
		}


		// *Oxide Event* - Called when the server saves
		[HookMethod("OnServerSave")]
		void OnServerSave()
		{
			OxideUtils.PrintWarning("Hive", "Hive is saving some user data...");
			PlayerPosDB db = PlayerPosDB.getInstance();


			if (GlobalVars.save)
			{
				foreach (BasePlayer p in BasePlayer.activePlayerList)
				{

					db.SaveUser(p.userID, p.GetCenter().x, p.GetCenter().y, p.GetCenter().z);
					SendPlayerToServer(p);
				}
			}

		}

		// *Oxide Event* -  Player disconnects
		[HookMethod("OnPlayerDisconnected")]
		void OnPlayerDisconnected(BasePlayer player, string reason)
		{
			// Save player position to data file
			PlayerPosDB.getInstance().SaveUser(player.userID, player.GetCenter().x, player.GetCenter().y, player.GetCenter().z);
			// Send HiveNet sync data
			SendPlayerToServer(player);

		}


		// Sends the player information to HiveNet
		private void SendPlayerToServer(BasePlayer player)
		{

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
			float hunger = player.metabolism.calories.value;
			float thirst = player.metabolism.hydration.value;
			float expspent = player.xp.SpentXp;
			float exp = player.xp.EarnedXp;
			string serUnlocks = JsonConvert.SerializeObject(PlayerUtils.getUnlockedItems(player));


			// Build request
			Net.Requests.PlayerInfo req;
			if (health < 1)
			{
				req = new Net.Requests.PlayerInfo(steamid, expspent, exp, serUnlocks);
			}
			else {
				req = new Net.Requests.PlayerInfo(steamid, x, y, z, rotX, rotY, rotZ, serInvent, health, hunger, thirst, expspent, exp, serUnlocks);
			}

			// Send Request
			string msg = HiveNetHandler.createHiveNetReq(req.type, req);
			nethandler.SendHiveNetRequest(msg);
		}

		// Gets a random short inside the positive short-Range
		private short getRandomID()
		{
			return System.Convert.ToInt16(new System.Random().Next(0, short.MaxValue));
		}

	}
}