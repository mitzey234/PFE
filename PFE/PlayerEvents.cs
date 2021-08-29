using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PFE
{
    public class PlayerEvents
    {
        public Plugin plugin;
        public PlayerEvents(Plugin plugin) => this.plugin = plugin;

		public void OnPlayerDeath(DyingEventArgs ev)
		{
			if (ev.Target.Role == RoleType.Scp173)
			{
				try
                {
					//float y = ev.Target.Position.y + 4;
					//Vector3 pos = new Vector3(ev.Target.Position.x, y, ev.Target.Position.z); 
					for (int i = 0; i < Plugin.Singleton.Config.Magnitude; i++)
					{
						new ExplosiveGrenade(ItemType.GrenadeHE, ev.Target) { FuseTime = Plugin.Singleton.Config.Delay }.SpawnActive(ev.Target.Position, ev.Target);
						//new ExplosiveGrenade(ItemType.GrenadeHE, ev.Target) { FuseTime = Plugin.Singleton.Config.Delay }.SpawnActive(pos, ev.Target);
						Log.Debug($"SCP-173 has exploded (pos goes here).", Plugin.Singleton?.Config?.Debug ?? false);
					}
				}
				catch (Exception e)
				{
					Log.Error($"PFE has encountered a problem while trying to make an explosion. Error available below: \n{e} \n--------- End of Error ---------");
				}
			}
		}
	}
}
