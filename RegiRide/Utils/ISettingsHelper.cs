namespace RegiRide.Utils
{
    public interface ISettingsHelper
    {
        T GetSetting<T>(string settingName, T defaultValue);

        void UpdateSetting<T>(string settingName, T value);
    }
}