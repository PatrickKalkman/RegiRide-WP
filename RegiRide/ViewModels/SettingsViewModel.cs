using System.Windows.Media;

namespace RegiRide.ViewModels
{
    using System.Windows;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using RegiRide.Navigation;
    using RegiRide.Repository;
    using RegiRide.Resources;
    using RegiRide.Services.Interface;
    using RegiRide.Utils;

    public class SettingsViewModel : ViewModelBase, INavigable
    {
        private readonly ISettingsHelper settingsHelper;

        private readonly IRideRepository rideRepository;
        private readonly BackgroundImageBrush backgroundImageBrush;

        private readonly RelayCommand saveCommand;

        private readonly RelayCommand removeAllRegistrationsCommand;

        public SettingsViewModel(ISettingsHelper settingsHelper, IRideRepository rideRepository, BackgroundImageBrush backgroundImageBrush)
        {
            this.settingsHelper = settingsHelper;
            this.rideRepository = rideRepository;
            this.backgroundImageBrush = backgroundImageBrush;
            this.saveCommand = new RelayCommand(Save);
            this.removeAllRegistrationsCommand = new RelayCommand(RemoveAllRegistrations);
        }

        public INavigationService NavigationService { get; set; }

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand;
            }
        }

        public RelayCommand RemoveAllRegistrationsCommand
        {
            get
            {
                return removeAllRegistrationsCommand;
            }
        }

        public ImageBrush BackgroundImageBrush
        {
            get { return backgroundImageBrush.GetBackground(); }
        }

        public string DropboxFolder
        {
            get
            {
                return settingsHelper.GetSetting(Constants.Settings.DropboxFolderSetting, Constants.Settings.DefaultDropboxFolder);
            }

            set
            {
                settingsHelper.UpdateSetting(Constants.Settings.DropboxFolderSetting, value);
                this.RaisePropertyChanged(() => DropboxFolder);
            }
        }

        private void RemoveAllRegistrations()
        {
            if (MessageBoxResult.OK == MessageBox.Show(AppResources.AreYouSureRemoveRides, AppResources.ConfirmDeleteTitle, MessageBoxButton.OKCancel))
            {
                rideRepository.DeleteAll();
                MessageBox.Show(AppResources.AllRidesAreDeleted);
            }
        }

        private void Save()
        {
            NavigationService.Back();
        }
    }
}
