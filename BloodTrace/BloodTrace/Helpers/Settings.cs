// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace BloodTrace.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;

		#endregion


		public static string UserName
		{
			get
			{
				return AppSettings.GetValueOrDefault("username", SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue("username", value);
			}
		}

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("password", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("password", value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("accesstoken", SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue("accesstoken", value);
            }
        }


    }
}