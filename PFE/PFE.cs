using System;
using Exiled.API.Features;
using Handlers = Exiled.Events.Handlers;

namespace PFE
{
    public class Plugin : Plugin<Config>
    {
		public override string Author { get; } = "Wafel & Cyanox";
		public override string Name { get; } = "PeanutFckingExplodes";
		public override string Prefix { get; } = "PFE";
		public override Version Version { get; } = new Version(3, 1, 0);
		public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);
		private bool state = false;

		public static Plugin Singleton;
		public PlayerEvents PlayerEvents;

		public override void OnEnabled() 
		{
			if (state) return;

			Singleton = this;
			PlayerEvents = new PlayerEvents(this);

            Handlers.Player.Dying += PlayerEvents.OnPlayerDeath;

			state = true;
			base.OnEnabled();
		}

		public override void OnDisabled() 
		{
			if (!state) return;

			Handlers.Player.Dying -= PlayerEvents.OnPlayerDeath;

			PlayerEvents = null;

			state = false;
			base.OnDisabled();
		}
	}
}
