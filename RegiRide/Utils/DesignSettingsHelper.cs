namespace RegiRide.Utils
{
    public class DesignSettingsHelper : ISettingsHelper
    {
        public T GetSetting<T>(string settingName, T defaultValue)
        {
            return defaultValue;
        }

        public void UpdateSetting<T>(string settingName, T value)
        {
        }
    }
}
