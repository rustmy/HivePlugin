using System;

namespace Oxide.Ext.Hive.Net.Answers
{
	public class Hello : BaseAnswer
	{
		public bool sync;
		public string version;
		public int hivecount;

		public Hello()
		{
			
		}
		public override void function(string id)
		{
			HiveVars.hivecount = hivecount;
		}

	}
}

