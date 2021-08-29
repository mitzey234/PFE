using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;
using Exiled.API.Features;
using Exiled.Events;
using Config = Exiled.Loader.Config;
using Handlers = Exiled.Events.Handlers;

namespace PFE
{
    public class Plugin : Plugin<Config>
    {
		public override string Author { get; } = "Wafel & Cyanox";
		public override string Name { get; } = "PeanutFckingExplodes";
		public override string Prefix { get; } = "PFE";
		public override Version Version { get; } = new Version(3, 0, 0);
		public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);


		public static Plugin Singleton;
		public PlayerEvents PlayerEvents;

		public override void OnEnabled() 
		{
			if (!Config.IsEnabled) return;

			base.OnEnabled();

			Singleton = this;
			PlayerEvents = new PlayerEvents(this);

            Handlers.Player.Dying += PlayerEvents.OnPlayerDeath;
		}

		public override void OnDisabled() 
		{
			base.OnDisabled();

			Handlers.Player.Dying -= PlayerEvents.OnPlayerDeath;

			PlayerEvents = null;

		}
	}
}
