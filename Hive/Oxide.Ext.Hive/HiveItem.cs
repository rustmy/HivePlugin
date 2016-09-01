using System;
using System.Collections.Generic;

namespace Oxide.Ext.Hive
{
	// A normal ingame Item saved as a HiveItem for serialization
	class HiveItem
	{
		public string shortname;
		public int itemid;
		public string container;
		public float condition;
		public int amount;
		public int ammoamount;
		public string ammotype;
		public int skinid;
		public bool weapon;
		public List<HiveItem> mods;

		public static HiveItem fromItem(Item item, string container)
		{
			HiveItem iItem = new HiveItem();
			iItem.shortname = item.info.shortname;
			iItem.amount = item.amount;
			iItem.mods = new List<HiveItem>();
			iItem.container = container;
			iItem.skinid = item.skin;
			iItem.itemid = item.info.itemid;
			iItem.weapon = false;
			if (item.hasCondition)
				iItem.condition = item.condition;
			if (item.info.category.ToString() == "Weapon")
			{
				BaseProjectile weapon = item.GetHeldEntity() as BaseProjectile;
				if (weapon != null)
				{
					if (weapon.primaryMagazine != null)
					{
						iItem.ammoamount = weapon.primaryMagazine.contents;
						iItem.ammotype = weapon.primaryMagazine.ammoType.shortname;
						iItem.weapon = true;
						if (item.contents != null)
							foreach (var mod in item.contents.itemList)
								if (mod.info.itemid != 0)
									iItem.mods.Add(HiveItem.fromItem(mod, "none"));
					}
				}
			}
			return iItem;
		}

		public Item toItem()
		{
			if (!weapon)
			{
				return BuildItem(this);
			}
			else {
				return BuildWeapon(this);
			}
		}


		private Item BuildItem(HiveItem sItem)
		{
			if (sItem.amount < 1) sItem.amount = 1;
			Item item = ItemManager.CreateByItemID(sItem.itemid, sItem.amount, sItem.skinid);
			if (item.hasCondition)
				item.condition = sItem.condition;
			return item;
		}


		private Item BuildWeapon(HiveItem sItem)
		{
			Item item = ItemManager.CreateByItemID(sItem.itemid, 1, sItem.skinid);
			if (item.hasCondition)
				item.condition = sItem.condition;
			var weapon = item.GetHeldEntity() as BaseProjectile;

			// Ammo
			if (weapon != null)
			{
				var def = ItemManager.FindItemDefinition(sItem.ammotype);
				weapon.primaryMagazine.ammoType = def;
				weapon.primaryMagazine.contents = sItem.ammoamount;
			}

			// Add mods
			if (sItem.mods != null)
				foreach (var mod in sItem.mods)
					item.contents.AddItem(BuildItem(mod).info, 1);
			return item;
		}
	}
}

