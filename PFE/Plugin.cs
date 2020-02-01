using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;
using MEC;
using System;
using System.Collections.Generic;

namespace PFE
{
	[PluginDetails(
	author = "Cyanox",
	name = "PFE",
	description = "Peanut explodes when he dies.",
	id = "cyan.pfe",
	version = "1.0.0",
	SmodMajor = 3,
	SmodMinor = 0,
	SmodRevision = 0
	)]
	public class Plugin : Smod2.Plugin
	{
		public override void OnDisable() { }

		public override void OnEnable() { }

		public override void Register()
		{
			AddEventHandlers(new EventHandler());
		}
	}

	class EventHandler : IEventHandlerPlayerDie
	{
		private IEnumerator<float> DelayAction(Action x, float delay)
		{
			yield return Timing.WaitForSeconds(delay);
			x();
		}

		public void OnPlayerDie(PlayerDeathEvent ev)
		{
			if (ev.Player.TeamRole.Role == Role.SCP_173)
			{
				ev.Player.ThrowGrenade(GrenadeType.FRAG_GRENADE, false, Vector.Zero, true, ev.Player.GetPosition(), false, 0);
				GrenadeManager gm = ((GameObject)ev.Player.GetGameObject()).GetComponent<GrenadeManager>();
				gm.availableGrenades[GrenadeManager.grenadesOnScene.Count - 1].timeUnitilDetonation = 0f;
				Timing.RunCoroutine(DelayAction(() =>
				{
					gm.availableGrenades[GrenadeManager.grenadesOnScene.Count - 1].timeUnitilDetonation = 4.7f;
					GrenadeManager.grenadesOnScene.RemoveAt(GrenadeManager.grenadesOnScene.Count - 1);
				}, 0.1f));
			}
		}
	}
}
