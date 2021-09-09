using Exiled.API.Interfaces;
using System.ComponentModel;

namespace PFE
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;
		public bool Debug { get; set; } = false;

		[Description("Magnitude is the quantity of explosions. A low number recommended.")]
		public int Magnitude { get; set; } = 1;
		[Description("Delay between death and explosion. Value below 0.15 will BREAK the explosion effect.")]
		public float Delay { get; set; } = 0.15f;
	}
}
