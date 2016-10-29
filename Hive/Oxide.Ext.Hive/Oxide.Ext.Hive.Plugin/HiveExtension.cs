
using Oxide.Core;
using Oxide.Core.Extensions;
using UnityEngine;
using Oxide.Ext.Hive.Plugin;

namespace Oxide.Ext.Hive.Plugin {
	
	public class HiveExtension : Extension {

		public override string Name {
			get {
				return "Hive";
			}
		}


		// Author of the extension
		public override string Author {
			get {
				return "Maverick Applications";
			}
		}

		public override VersionNumber Version {
			get {
				return new VersionNumber(0, 8, 9);
			}
		}

		public HiveExtension(ExtensionManager manager) : base(manager)
		{
			
		}


		public override void Load() {
			Manager.RegisterPluginLoader(new HivePluginLoader());
		}
	}
}

