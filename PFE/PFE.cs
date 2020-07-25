using UnityEngine;
using Exiled.API.Features;
using Grenades;
using Mirror;
using Exiled.Events.EventArgs;

namespace PFE
{
	public class PFE : Plugin<Config>
	{
		public static PFE instance;

		private EventHandlers ev;

		public override void OnEnabled() 
		{
			base.OnEnabled();

			if (!Config.IsEnabled) return;

			instance = this;

			ev = new EventHandlers();

			Exiled.Events.Handlers.Player.Died += ev.OnPlayerDeath;
		}

		public override void OnDisabled() 
		{
			base.OnDisabled();

			Exiled.Events.Handlers.Player.Died -= ev.OnPlayerDeath;

			ev = null;
		}

		public override string Name => "pfe";
	}

	class EventHandlers
	{
		public void OnPlayerDeath(DiedEventArgs ev)
		{
			if (PFE.instance.Config.IsEnabled && ev.Target.Role == RoleType.Scp173)
			{
				for (int i = 0; i < PFE.instance.Config.Magnitude; i++)
				{
					Grenade grenade = GameObject.Instantiate(ev.Target.ReferenceHub.GetComponent<GrenadeManager>().availableGrenades[0].grenadeInstance).GetComponent<Grenade>();
					grenade.fuseDuration = PFE.instance.Config.Delay;
					grenade.InitData(ev.Target.ReferenceHub.GetComponent<GrenadeManager>(), Vector3.zero, Vector3.zero);
					NetworkServer.Spawn(grenade.gameObject);
				}
			}
		}
	}
}
