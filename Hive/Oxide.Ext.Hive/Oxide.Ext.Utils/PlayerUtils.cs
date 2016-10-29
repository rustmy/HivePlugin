using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Hive.Net.Answers;
using Oxide.Game.Rust.Cui;
using Rust.Xp;
using Oxide.Ext.Hive.Plugin;

namespace Oxide.Ext.Hive.Utils {
	public static class PlayerUtils {
		

		// Returns the item ID of the itmes which the player unlocked
		public static List<int> getUnlockedItems(BasePlayer player) {
			List<int> unlockedItems = new List<int>();
			foreach(ItemDefinition i in ItemManager.itemList) {
				if(player.blueprints.IsUnlocked(i)) {
					unlockedItems.Add(i.itemid);
				}
			}

			return unlockedItems;
		}

		public static void DestroyPlayerUI(BasePlayer player, List<CuiElement> ui) {
			foreach(CuiElement c in ui) {
				CuiHelper.DestroyUi(player, c.Name);
			}
		}


		public static void ApplyPlayerInfo(BasePlayer player, PlayerInfo info) {
			// A param is null
			if(player == null || info == null) {
				return;
			}

			// Sync inventory
			if(HiveCore.sync_inventory) {
				// Restores the inventory of the player
				RestoreItems(player, info.inventory);
			}

			// Sync metabolism
			if(HiveCore.sync_inventory) {
				// Player statistics
				player.health = (info.health == 0.0) ? player.health : info.health;
				player.metabolism.calories.value = (info.hunger == -1.0) ? player.metabolism.calories.value : info.hunger;
				player.metabolism.hydration.value = (info.thirst == -1.0) ? player.metabolism.hydration.value : info.thirst;
			}

			// Sync EXP
			if(HiveCore.sync_exp) {
				player.xp.Reset();
				player.xp.Add(Definitions.Cheat, info.exp);
				player.xp.SpendXp((int)info.expspent, "Cheat");
			}

			// Sync blueprints
			if(HiveCore.sync_blueprints) {
				// Re-unlock blueprints
				List<int> unlockedItems = JsonConvert.DeserializeObject<List<int>>(info.blueprints);
				foreach(int i in unlockedItems) {
					player.blueprints.Unlock(ItemManager.FindItemDefinition(i));
				}
			}
		}

		// Restores the players original items via json
		private static void RestoreItems(BasePlayer player, string json) {
			// No json to restore
			if(String.IsNullOrEmpty(json) || player == null) {
				return;
			}

			// Clear inventory first
			player.inventory.Strip();

			HiveInventory inv = JsonConvert.DeserializeObject<HiveInventory>(json);

			foreach(Item i in inv.getItems("belt")) {
				player.inventory.GiveItem(i, player.inventory.containerBelt);
			}

			foreach(Item i in inv.getItems("main")) {
				player.inventory.GiveItem(i, player.inventory.containerMain);
			}

			foreach(Item i in inv.getItems("wear")) {
				player.inventory.GiveItem(i, player.inventory.containerWear);
			}

		}
	}
}

