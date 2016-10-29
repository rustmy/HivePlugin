using System;
using Oxide.Ext.Hive.Plugin;

namespace Oxide.Ext.Hive.Net.Answers {
	public class Hello : BaseAnswer {
		public string version;
		public int hivecount;

		public bool sync_exp;
		public bool sync_inventory;
		public bool sync_blueprints;

		public Hello()
		{
			
		}

		public override void function(string id) {
			HiveVars.hivecount = hivecount;

			HiveCore.sync_blueprints = sync_blueprints;
			HiveCore.sync_inventory = sync_inventory;
			HiveCore.sync_exp = sync_exp;
		}

	}
}

