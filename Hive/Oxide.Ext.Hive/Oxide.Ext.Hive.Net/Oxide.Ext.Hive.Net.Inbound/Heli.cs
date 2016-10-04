using System;
using Oxide.Ext.Hive.Utils;
using UnityEngine;

namespace Oxide.Ext.Hive.Net.Answers
{
	public class Heli : BaseAnswer
	{
		public override void function(string id)
		{
			try
			{

				BaseEntity entity = GameManager.server.CreateEntity("assets/prefabs/npc/patrol helicopter/patrolhelicopter.prefab", new Vector3(), new Quaternion());
				entity.Spawn();
				OxideUtils.PrintSuccess("Hive", "Spawned new Helicopter");
			} catch (Exception ex)
			{
				OxideUtils.PrintError("Hive", "Error while spawning a Helicopter");
			}
		}
	}
}

