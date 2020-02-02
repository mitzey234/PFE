using UnityEngine;
using EXILED;
using Grenades;
using Mirror;

namespace PFE
{
	public class Plugin : EXILED.Plugin
	{
		private EventHandlers EventHandlers;

		public static int magnitude;

		public override void OnEnable() 
		{
			magnitude = Config.GetInt("pfe_magnitude", 1);
			EventHandlers = new EventHandlers();
			Events.PlayerDeathEvent += EventHandlers.OnPlayerDeath;
		}

		public override void OnDisable() 
		{
			Events.PlayerDeathEvent -= EventHandlers.OnPlayerDeath;
			EventHandlers = null;
		}

		public override void OnReload() { }

		public override string getName { get; } = "PFE";
	}

	class EventHandlers
	{
		public void OnPlayerDeath(ref PlayerDeathEvent ev)
		{
			if (ev.Player.characterClassManager.CurClass == RoleType.Scp173)
			{
				for (int i = 0; i < Plugin.magnitude; i++)
				{
					Grenade grenade = GameObject.Instantiate(ev.Player.GetComponent<GrenadeManager>().availableGrenades[0].grenadeInstance).GetComponent<Grenade>();
					grenade.InitData(ev.Player.GetComponent<GrenadeManager>(), Vector3.zero, Vector3.zero);
					NetworkServer.Spawn(grenade.gameObject);
					grenade.NetworkfuseTime = 0f;
				}
			}
		}
	}
}
