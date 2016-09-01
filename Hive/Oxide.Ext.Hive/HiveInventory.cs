using System;
using System.Collections.Generic;

namespace Oxide.Ext.Hive
{
	// A normal ingame Inventory saved as a HiveInventory for serialization
	class HiveInventory
	{
		public HiveItemContainer containerMain;
		public HiveItemContainer containerBelt;
		public HiveItemContainer containerWear;

		// Converting an PlayerInventory to an HiveInventory
		public static HiveInventory fromInventory(PlayerInventory invent)
		{
			HiveInventory result = new HiveInventory();

			result.containerBelt = HiveItemContainer.fromContainer(invent.containerBelt, "belt");
			result.containerMain = HiveItemContainer.fromContainer(invent.containerMain, "main");
			result.containerWear = HiveItemContainer.fromContainer(invent.containerWear, "wear");

			return result;
		}

		// Status and item container
		// Returns: 
		public string getStatus(string container)
		{
			switch(container)
			{
				case "belt":
					return containerBelt.getStatus();
				break;
				case "wear":
					return containerWear.getStatus();
				break;
				case "main":
					return containerMain.getStatus();
				break;
			}

			return null;
		}

		// Get items in this inventory
		public List<Item> getItems(string container)
		{
			List<Item> result = new List<Item>();

			switch (container)
			{
				case "belt":
					result.AddRange(containerBelt.getItems());
					break;
				case "wear":
					result.AddRange(containerWear.getItems());
					break;
				case "main":
					result.AddRange(containerMain.getItems());
					break;
			}

			return result;
		}
	}
}

