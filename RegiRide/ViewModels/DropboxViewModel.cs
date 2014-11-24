using System.Windows.Media;

namespace RegiRide.ViewModels
{
    using System.ComponentModel;
    using System.Windows;

    using DropNet;

    using GalaSoft.MvvmLight;

    using RegiRide.Navigation;
    using RegiRide.Services.Interface;
    using RegiRide.Utils;

    public class DropboxViewModel : ViewModelBase, INavigable
    {
        private readonly DropNetClient dropNetClient;

        private readonly ISettingsHelper settingsHelper;

        private readonly RegiRideSettingsManager regiRideSettingsManager;
        private readonly BackgroundImageBrush backgroundImageBrush;

        public DropboxViewModel(DropNetClient dropNetClient, ISettingsHelper settingsHelper, RegiRideSettingsManager regiRideSettingsManager, BackgroundImageBrush backgroundImageBrush)
        {
            this.dropNetClient = dropNetClient;
            this.settingsHelper = settingsHelper;
            this.regiRideSettingsManager = regiRideSettingsManager;
            this.backgroundImageBrush = backgroundImageBrush;
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        public INavigationService NavigationService { get; set; }

        public DropNetClient DropNetClient
        {
            get
            {
                return dropNetClient;
            }
        }

        public void AuthenticatedSuccessfully()
        {
            regiRideSettingsManager.Authenticated = true;
            MessengerInstance.Send(true);
            NavigationService.Back();
        }

        public ImageBrush BackgroundImageBrush
        {
            get { return backgroundImageBrush.GetBackground(); }
        }

        public void StoreToken(string token)
        {
            settingsHelper.UpdateSetting(Constants.Settings.DropboxUserTokenKey, token);
        }

        public void StoreSecret(string secret)
        {
            settingsHelper.UpdateSetting(Constants.Settings.DropboxUserSecretKey, secret);
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
            }
        }
    }
}