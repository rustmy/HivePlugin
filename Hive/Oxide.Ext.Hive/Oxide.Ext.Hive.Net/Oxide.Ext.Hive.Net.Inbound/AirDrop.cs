using System;
using Oxide.Ext.Hive.Utils;

namespace Oxide.Ext.Hive.Net.Answers
{
	public class AirDrop : BaseAnswer
	{
		public int delay;

		public override void function(string id)
		{
			try
			{
				
				CargoPlane entity = (CargoPlane)GameManager.server.CreateEntity("assets/prefabs/npc/cargo plane/cargo_plane.prefab");
				entity.Spawn();
				OxideUtils.PrintSuccess("Hive", "Spawned new AirDrop Plane");
			} catch (Exception ex)
			{
				OxideUtils.PrintError("Hive", "Error while spawning a new Cargo Plane");
			}
		}
	}
}

