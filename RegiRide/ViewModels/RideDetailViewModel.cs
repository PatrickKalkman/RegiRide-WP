using System.Windows.Media;

namespace RegiRide
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using RegiRide.Mappers;
    using RegiRide.Messages;
    using RegiRide.Model;
    using RegiRide.Navigation;
    using RegiRide.Repository;
    using RegiRide.Resources;
    using RegiRide.Services.Interface;
    using RegiRide.Utils;
    using RegiRide.ViewModels;

    public partial class RideDetailViewModel : ViewModelBase, INavigable
    {
        private readonly IRideRepository rideRepository;

        private readonly IAddressRepository addressRepository;
        private readonly BackgroundImageBrush backgroundImageBrush;

        private RideViewModel rideViewModel;

        private int selectedStartAddressIndex = -1;

        private int selectedEndAddressIndex = -1;

        private bool allowedToChangeValue;

        private ObservableCollection<AddressViewModel> startAddresses;

        private ObservableCollection<AddressViewModel> endAddresses;

        private Guid? addeddAddressId;

        private WhichAddress whichAddress;

        public RideDetailViewModel(IRideRepository rideRepository, IAddressRepository addressRepository, BackgroundImageBrush backgroundImageBrush)
        {
            this.rideRepository = rideRepository;
            this.addressRepository = addressRepository;
            this.backgroundImageBrush = backgroundImageBrush;

            SaveRideCommand = new RelayCommand(SaveRide);
            CancelCommand = new RelayCommand(Cancel);
            DeleteCommand = new RelayCommand(Delete);
            AddStartAddressCommand = new RelayCommand(AddStartAddress);
            AddEndAddressCommand = new RelayCommand(AddEndAddress);
            LoadAddressCommand = new RelayCommand(LoadAddressList);
            GoBackCommand = new RelayCommand(GoBack);
            allowedToChangeValue = true;

            Messenger.Default.Register<PropertyChangedMessage<RideViewModel>>(
                this,
                message =>
                {
                    SelectedRide = null;
                    SelectedRide = message.NewValue;
                });

            Messenger.Default.Register<AddressAddedMessage>(
                this,
                message =>
                    { 
                        addeddAddressId = message.AddressId;
                        whichAddress = message.WhichAddress;
                    });

#if DEBUG
            if (IsInDesignMode)
            {
                CreateDesignTimeData();
            }
#endif
        }

        #region Commands
        public RelayCommand SaveRideCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteCommand
        {
            get;
            private set;
        }

        public RelayCommand GoBackCommand
        {
            get; 
            private set;
        }

        public RelayCommand AddStartAddressCommand
        {
            get;
            private set;
        }

        public RelayCommand AddEndAddressCommand
        {
            get;
            private set;
        }

        public RelayCommand LoadAddressCommand
        {
            get;
            private set;
        }
        #endregion

        public ObservableCollection<AddressViewModel> StartAddresses
        {
            get
            {
                return startAddresses;
            }

            set
            {
                startAddresses = value;
                if (value == null)
                {
                    selectedStartAddressIndex = -1;
                }

                this.RaisePropertyChanged(() => StartAddresses);
            }
        }

        public ObservableCollection<AddressViewModel> EndAddresses
        {
            get
            {
                return endAddresses;
            }

            set
            {
                endAddresses = value;
                this.RaisePropertyChanged(() => EndAddresses);
            }
        }

        public INavigationService NavigationService
        {
            get; set;
        }

        public bool IsBusinessType
        {
            get
            {
                return SelectedRide.Model.RideType == (int)RideType.Business;
            }

            set
            {
                if (value)
                {
                    SelectedRide.Model.RideType = (int)RideType.Business;
                    this.RaisePropertyChanged(() => IsBusinessType);
                }
            }
        }

        public ImageBrush BackgroundImageBrush
        {
            get { return backgroundImageBrush.GetBackground(); }
        }

        public bool IsPrivateType
        {
            get
            {
                return SelectedRide.Model.RideType == (int)RideType.Private;
            }

            set
            {
                if (value)
                {
                    SelectedRide.Model.RideType = (int)RideType.Private;
                    this.RaisePropertyChanged(() => IsPrivateType);
                }
            }
        }

        public RideViewModel SelectedRide
        {
            get
            {
                return rideViewModel;
            }

            set
            {
                if (rideViewModel == value)
                {
                    return;
                }

                rideViewModel = value;
                RaisePropertyChanged(() => SelectedRide);
            }
        }

        public int SelectedStartAddressIndex
        {
            get
            {
                return selectedStartAddressIndex;
            }

            set
            {
                selectedStartAddressIndex = value;
                if (allowedToChangeValue && selectedStartAddressIndex != 0)
                {
                    this.SelectedRide.Model.StartAddress = StartAddresses[value].Model;
                }

                this.RaisePropertyChanged(() => SelectedStartAddressIndex);
            }
        }

        public int SelectedEndAddressIndex
        {
            get
            {
                return selectedEndAddressIndex;
            }

            set
            {
                selectedEndAddressIndex = value;
                if (allowedToChangeValue && selectedEndAddressIndex != 0)
                {
                    this.SelectedRide.Model.EndAddress = EndAddresses[value].Model;
                }

                this.RaisePropertyChanged(() => SelectedEndAddressIndex);
            }
        }

        private void AddStartAddress()
        {
            this.RaisePropertyChanged("StartAddresses", null, new AddressViewModel(this.addressRepository.CreateNewAddress(), WhichAddress.StartAddress, new BackgroundImageBrush()), true);
            NavigationService.Navigate("/Views/AddressDetailView.xaml");
        }

        private void AddEndAddress()
        {
            this.RaisePropertyChanged("EndAddresses", null, new AddressViewModel(this.addressRepository.CreateNewAddress(), WhichAddress.EndAddress, new BackgroundImageBrush()), true);
            NavigationService.Navigate("/Views/AddressDetailView.xaml");
        }

        private void SaveRide()
        {
            if (selectedStartAddressIndex != 0 && selectedEndAddressIndex != 0)
            {
                this.rideRepository.Save(SelectedRide.Model);
                NavigationService.Back();
            }
            else
            {
                MessageBox.Show(AppResources.StartAddressAndEndAddressAreRequired);
            }
        }

        private void Cancel()
        {
            if (SelectedRide.IsNew)
            {
               rideRepository.Delete(SelectedRide.Model);
            }
            else
            {
                SelectedRide = new RideViewModel(rideRepository.GetPreviousState(SelectedRide.Model), new WeekNumberCalculator(), false);
            }

            NavigationService.Back();
        }

        private void GoBack()
        {
            if (SelectedRide.IsNew)
            {
                rideRepository.Delete(SelectedRide.Model);
            }
            else
            {
                SelectedRide = new RideViewModel(rideRepository.GetPreviousState(SelectedRide.Model), new WeekNumberCalculator(), false);
            }
        }

        private void Delete()
        {
            if (MessageBoxResult.OK == MessageBox.Show(AppResources.ConfirmDelete, AppResources.ConfirmDeleteTitle, MessageBoxButton.OKCancel))
            {
                rideRepository.Delete(SelectedRide.Model);
                NavigationService.Back();
            }
        }

        private void LoadAddressList()
        {
            try
            {
                allowedToChangeValue = false;
                ObservableCollection<AddressViewModel> adresses = AddressListMapper.MapForListPicker(addressRepository.GetAll());
                StartAddresses = adresses;
                EndAddresses = adresses;
                allowedToChangeValue = true;
                SetStartAndEndAddress();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void SetStartAndEndAddress()
        {
            if (addeddAddressId.HasValue)
            {
                if (whichAddress == WhichAddress.StartAddress)
                {
                    SelectedStartAddressIndex = this.GetAddressindexFromId(addeddAddressId.Value);
                }
                else
                {
                    SelectedEndAddressIndex = this.GetAddressindexFromId(addeddAddressId.Value);
                }

                addeddAddressId = null;
            }
            
            if (SelectedRide.Model.EndAddress != null)
            {
                SelectedEndAddressIndex = this.GetAddressindexFromId(SelectedRide.Model.EndAddress.Id);
            }
            else
            {
                if (StartAddresses != null && StartAddresses.Count > 0)
                {
                    SelectedEndAddressIndex = 0;
                }
            }

            if (SelectedRide.Model.StartAddress != null)
            {
                SelectedStartAddressIndex = this.GetAddressindexFromId(SelectedRide.Model.StartAddress.Id);
            }
            else
            {
                if (EndAddresses != null && EndAddresses.Count > 0)
                {
                    SelectedStartAddressIndex = 0;
                }
            }
        }

        private int GetAddressindexFromId(Guid id)
        {
            for (int index = 0; index < this.EndAddresses.Count; index++)
            {
                if (this.EndAddresses[index].Model.Id == id)
                {
                    return index;
                }
            }

            return 0;
        }
    }
}