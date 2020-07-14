using UnityEngine;
using EXILED;
using Grenades;
using Mirror;
using Harmony;

namespace PFE
{
	public class Plugin : EXILED.Plugin
	{
		private EventHandlers EventHandlers;

		internal static bool fuse = false;

		public override void OnEnable() 
		{
			HarmonyInstance.Create("cyanox.pfe").PatchAll();

			EventHandlers = new EventHandlers();
			Events.WaitingForPlayersEvent += EventHandlers.OnWaitingForPlayers;
			Events.PlayerDeathEvent += EventHandlers.OnPlayerDeath;
		}

		public override void OnDisable() 
		{
			Events.WaitingForPlayersEvent -= EventHandlers.OnWaitingForPlayers;
			Events.PlayerDeathEvent -= EventHandlers.OnPlayerDeath;
			EventHandlers = null;
		}

		public override void OnReload() { }

		public override string getName { get; } = "PFE";
	}

	[HarmonyPatch(typeof(Grenade), "Awake")]
	class FusePatch
	{
		public static void Prefix(Grenade __instance)
		{
			if (Plugin.fuse && __instance.GetType() == typeof(FragGrenade)) __instance.fuseDuration = Config.delay;
			Plugin.fuse = false;
		}
	}

	class EventHandlers
	{
		public void OnWaitingForPlayers() => Config.Reload();

		public void OnPlayerDeath(ref PlayerDeathEvent ev)
		{
			if (Config.isEnabled && ev.Player.characterClassManager.CurClass == RoleType.Scp173)
			{
				for (int i = 0; i < Config.magnitude; i++)
				{
					Plugin.fuse = Config.delay != 0;
					Grenade grenade = GameObject.Instantiate(ev.Player.GetComponent<GrenadeManager>().availableGrenades[0].grenadeInstance).GetComponent<Grenade>();
					grenade.InitData(ev.Player.GetComponent<GrenadeManager>(), Vector3.zero, Vector3.zero);
					NetworkServer.Spawn(grenade.gameObject);
				}
			}
		}
	}
}
