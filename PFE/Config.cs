using Exiled.API.Interfaces;

namespace PFE
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;

		public int Magnitude { get; set; } = 1;

		public float Delay { get; set; } = 0f;
	}
}
