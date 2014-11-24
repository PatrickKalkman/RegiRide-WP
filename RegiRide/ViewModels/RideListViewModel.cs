using System.Windows.Media;

namespace RegiRide.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows;

    using DropNet;
    using DropNet.Exceptions;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Export;
    using Mappers;

    using Navigation;
    using Repository;
    using Resources;
    using Services.Interface;
    using Utils;

    using RestSharp;

    public class RideListViewModel : ViewModelBase, INavigable
    {
        private readonly IRideRepository rideRepository;

        private readonly RideListMapper rideListMapper;

        private readonly WeekNumberCalculator weekNumberCalculator;

        private readonly ExportManager exportManager;

        private readonly DropNetClient dropNetClient;

        private readonly RegiRideSettingsManager regiRideSettingsManager;

        private readonly NetworkConnection networkConnection;

        private readonly RelayCommand<RideViewModel> selectionChangedCommand;

        private RideViewModel selectedRide;

        private bool navigationIsAllowed = true;

        private ObservableCollection<RideViewModel> rideList;

        private readonly BackgroundImageBrush backgroundImageBrush;

        public RideListViewModel(
            IRideRepository rideRepository, 
            RideListMapper rideListMapper, 
            WeekNumberCalculator weekNumberCalculator, 
            ExportManager exportManager,
            DropNetClient dropNetClient,
            RegiRideSettingsManager regiRideSettingsManager,
            BackgroundImageBrush backgroundImageBrush,
            NetworkConnection networkConnection)
        {
            this.rideRepository = rideRepository;
            this.rideListMapper = rideListMapper;
            this.weekNumberCalculator = weekNumberCalculator;
            this.exportManager = exportManager;
            this.dropNetClient = dropNetClient;
            this.regiRideSettingsManager = regiRideSettingsManager;
            this.networkConnection = networkConnection;
            this.backgroundImageBrush = backgroundImageBrush;

            selectionChangedCommand = new RelayCommand<RideViewModel>(Execute);
            AddRideCommand = new RelayCommand(AddRide);
            SettingsCommand = new RelayCommand(Settings);
            EmailCommand = new RelayCommand(Export);
            LoadRideListCommand = new RelayCommand(LoadRideList);
            AddressesCommand = new RelayCommand(Addresses);
            AboutCommand = new RelayCommand(About);
            MessengerInstance.Register(this, (bool result) => Export());
#if DEBUG
            if (IsInDesignMode)
            {
                LoadRideList();
            }
#endif
        }

        #region Commands

        public RelayCommand AboutCommand
        {
            get; private set;
        }

        public RelayCommand AddressesCommand
        {
            get;
            private set;
        }

        public RelayCommand SettingsCommand
        {
            get;
            private set;
        }

        public RelayCommand EmailCommand
        {
            get;
            private set;
        }

        public RelayCommand LoadRideListCommand
        {
            get;
            private set;
        }

        public RelayCommand AddRideCommand
        {
            get;
            private set;
        }

        public RelayCommand<RideViewModel> SelectionChangedCommand
        {
            get
            {
                return selectionChangedCommand;
            }
        }

#endregion

        public ObservableCollection<RideViewModel> RideList
        {
            get
            {
                return rideList;
            }

            set
            {
                rideList = value;
                RaisePropertyChanged(() => RideList);
            }
        }

        public INavigationService NavigationService { get; set; }

        public RideViewModel SelectedRide
        {
            get
            {
                return selectedRide;
            }

            set
            {
                if (value != null && value != selectedRide)
                {
                    var oldValue = selectedRide;
                    selectedRide = value;
                    RaisePropertyChanged(() => SelectedRide, oldValue, selectedRide, true);
                    if (navigationIsAllowed)
                    {
                        NavigationService.Navigate("/Views/RideDetailView.xaml");
                    } 
                }
            }
        }

        public ObservableCollection<RideViewModel> GroupedRideViewModels
        {
            get
            {
                return RideList;
            }
        }

        public ImageBrush BackgroundImageBrush
        {
            get { return backgroundImageBrush.GetBackground(); }
        }

        public void Execute(RideViewModel rideViewModel)
        {
            SelectedRide = rideViewModel;
        }

        public void AddRide()
        {
            SelectedRide = new RideViewModel(rideRepository.CreateNewRide(), weekNumberCalculator, true);
            NavigationService.Navigate("/Views/RideDetailView.xaml");
        }

        public void Settings()
        {
            NavigationService.Navigate("/Views/SettingsView.xaml");
        }

        public void Export()
        {
            if (networkConnection.IsAvailable())
            {
                if (regiRideSettingsManager.Authenticated && dropNetClient.UserLogin != null && dropNetClient.UserLogin.Token != null)
                {
                    string export = exportManager.GenerateExport(rideRepository.GetAll());
                    byte[] exportBytes = Encoding.Unicode.GetBytes(export);
                    string fileName = exportManager.GenerateExportFileName();
                    dropNetClient.UploadFileAsync(regiRideSettingsManager.DropboxFolder, fileName, exportBytes, UploadSuccess, UploadFailure);
                }
                else
                {
                    MessageBox.Show(AppResources.ExportDropboxNotification);
                    NavigationService.Navigate("/Views/DropboxView.xaml");
                }
            }
            else
            {
                MessageBox.Show(AppResources.NoNetworkAvailable);
            }
        }

        public void UploadSuccess(RestResponse restResponse)
        {
            MessageBox.Show(AppResources.SuccessfullExportedRides);
        }

        public void UploadFailure(DropboxException dropboxException)
        {
            MessageBox.Show(dropboxException.Message);
        }

        private void About()
        {
            NavigationService.Navigate("/YourLastAboutDialog;component/AboutPage.xaml");
        }

        private void Addresses()
        {
            NavigationService.Navigate("/Views/AddressListView.xaml");
        }

        private void LoadRideList()
        {
            try
            {
                navigationIsAllowed = false;
                RideList = new ObservableCollection<RideViewModel>();
                rideListMapper.Map(rideRepository.GetAll(), RideList);
                RaisePropertyChanged(() => GroupedRideViewModels);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
            finally
            {
                navigationIsAllowed = true;
            }
        }
    }
}