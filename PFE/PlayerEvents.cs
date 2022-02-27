using Exiled.API.Features;
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
						ExplosiveGrenade explosiveGrenade = (ExplosiveGrenade) Item.Create(ItemType.GrenadeHE, ev.Target);
						explosiveGrenade.FuseTime = Plugin.Singleton.Config.Delay;
						explosiveGrenade.SpawnActive(ev.Target.Position, ev.Target);
						string text = string.Format("SCP-173 ({0}) has exploded ({1}) with a {2} delay.", ev.Target.Nickname, ev.Target.Position, Plugin.Singleton.Config.Delay);
						Plugin singleton = Plugin.Singleton;
						bool? flag;
						if (singleton == null)
						{
							flag = null;
						}
						else
						{
							Config config = singleton.Config;
							flag = ((config != null) ? new bool?(config.Debug) : null);
						}
						bool? flag2 = flag;
						Log.Debug(text, flag2.GetValueOrDefault());
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
