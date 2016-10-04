using System;
using Oxide.Ext.Hive.Utils;

namespace Oxide.Ext.Hive.Net.Answers
{
	public class Error : BaseAnswer
	{
		public string message;

		public override void function(string id)
		{
			OxideUtils.PrintError("Hive", "Error: " + message);
		}
	}
}

