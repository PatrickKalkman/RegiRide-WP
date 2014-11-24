namespace RegiRide.Utils
{
    using System.IO.IsolatedStorage;

    public class SettingsHelper : ISettingsHelper
    {
        private readonly IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public T GetSetting<T>(string settingName, T defaultValue)
        {
            if (!this.settings.Contains(settingName))
            {
                this.settings.Add(settingName, defaultValue);
            }

            return (T)this.settings[settingName];
        }

        public void UpdateSetting<T>(string settingName, T value)
        {
            if (!this.settings.Contains(settingName))
            {
                this.settings.Add(settingName, value);
            }
            else
            {
                this.settings[settingName] = value;
            }

            this.settings.Save();
        }
    }
}
