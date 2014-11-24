namespace RegiRide.Utils
{
    public class RegiRideSettingsManager
    {
        private readonly ISettingsHelper settingsHelper;

        public RegiRideSettingsManager(ISettingsHelper settingsHelper)
        {
            this.settingsHelper = settingsHelper;
        }

        public string DropboxFolder
        {
            get
            {
                return settingsHelper.GetSetting(Constants.Settings.DropboxFolderSetting, Constants.Settings.DefaultDropboxFolder);
            }
        }

        public bool Authenticated
        {
            get
            {
                return settingsHelper.GetSetting(Constants.Settings.AuthenticatedKey, false);
            }

            set
            {
                settingsHelper.UpdateSetting(Constants.Settings.AuthenticatedKey, value);
            }
        }
    }
}
