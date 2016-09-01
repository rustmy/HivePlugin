using System;
using Oxide.Core.Plugins;

namespace Oxide.Ext.Hive.Plugins
{
	public class HivePluginLoader : PluginLoader
	{
		public HivePluginLoader()
		{
		}

		public override Type[] CorePlugins
		{
			get
			{
				return new Type[] { typeof(HiveCore) };
			}
		}

		public override Plugin Load(string directory, string name)
		{
			HiveCore plugin = new HiveCore();
			LoadedPlugins.Add(name, plugin);
			return plugin;
		}
	}
}

