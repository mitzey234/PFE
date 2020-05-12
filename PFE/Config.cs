namespace PFE
{
	class Config
	{
		internal static int magnitude;
		internal static bool isEnabled;

		internal static void Reload()
		{
			magnitude = Plugin.Config.GetInt("pfe_magnitude", 1);

			isEnabled = Plugin.Config.GetBool("pfe_enabled", true);
		}
	}
}
