using System;
using Oxide.Game.Rust.Cui;

namespace Oxide.Ext.Hive.Net.Answers
{
	public class GUI : BaseAnswer
	{
		public string json;

		public GUI()
		{
		}

		public override void function(string id)
		{
			HiveVars.GUI = CuiHelper.FromJson(json);
		}
	}
}

