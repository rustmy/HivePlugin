
using Oxide.Core;
using Oxide.Core.Extensions;
using UnityEngine;
using Oxide.Ext.Hive.Plugins;

namespace Oxide.Ext.Hive.HiveExtension
{
	public class HiveExtension : Extension
	{

		public override string Name
		{
			get
			{
				return "Hive";
			}
		}


		// Author of the extension
		public override string Author
		{
			get
			{
				return "Maverick Applications";
			}
		}

		public override VersionNumber Version
		{
			get
			{
				return new VersionNumber(1, 0, 0);
			}
		}

		public HiveExtension(ExtensionManager manager) : base(manager)
		{
			
		}


		public override void Load()
		{
			Manager.RegisterPluginLoader(new HivePluginLoader());
		}
	}
}

