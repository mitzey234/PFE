﻿using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using System;

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
					for (int i = 0; i < Plugin.Singleton.Config.Magnitude; i++)
					{
						ExplosiveGrenade explosiveGrenade = (ExplosiveGrenade) new Item(ItemType.GrenadeHE);
						explosiveGrenade.FuseTime = Plugin.Singleton.Config.Delay;
						explosiveGrenade.SpawnActive(ev.Target.Position, ev.Target);
						Log.Debug($"SCP-173 ({ev.Target.Nickname}) has exploded ({ev.Target.Position}) with a {Plugin.Singleton.Config.Delay} delay.", Plugin.Singleton?.Config?.Debug ?? false);
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
