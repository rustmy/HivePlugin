using System;
using Oxide.Ext.Hive.Utils;

namespace Oxide.Ext.Hive.Net.Answers
{
	public class Update : BaseAnswer
	{
		bool available;


		public Update ()
		{
		}

		public override void function (string id)
		{
			if (available)
			OxideUtils.Puts ("Hive", "A Hive update is available! Check out your control panel at http://hivenet.work");
		}
	}
}

