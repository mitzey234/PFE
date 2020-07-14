namespace PFE
{
	class Config
	{
		internal static int magnitude;

		internal static float delay;

		internal static bool isEnabled;

		internal static void Reload()
		{
			magnitude = Plugin.Config.GetInt("pfe_magnitude", 1);

			delay = Plugin.Config.GetFloat("pfe_delay", 0f);

			isEnabled = Plugin.Config.GetBool("pfe_enabled", true);
		}
	}
}
