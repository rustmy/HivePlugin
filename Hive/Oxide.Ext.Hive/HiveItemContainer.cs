using System;
using System.Linq;

namespace Oxide.Ext.Hive
{
	// A normal ingame ItemContainer saved as a HiveItemContainer for serialization
	class HiveItemContainer
	{
		public HiveItem[] container;
		public string type;

		public HiveItemContainer(int size)
		{
			container = new HiveItem[size];
		}

		public static HiveItemContainer fromContainer(ItemContainer cont, string pType)
		{
			HiveItemContainer result = new HiveItemContainer(cont.itemList.Count);

			result.type = pType;
			for (int i = 0; i < result.container.Count(); i++)
			{
				result.container[i] = HiveItem.fromItem(cont.itemList[i], pType);
			}

			return result;
		}

		public string getStatus()
		{
			return (container == null) ? "null" : container.Length.ToString();
		}

		public Item[] getItems()
		{
			Item[] result = new Item[container.Count()];

			for (int i = 0; i < container.Count(); i++)
			{
				result[i] = container[i].toItem();
			}

			return result;
		}

	}
}

