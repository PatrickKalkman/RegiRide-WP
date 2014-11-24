using System.Windows.Media;

namespace RegiRide.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using RegiRide.Mappers;
    using RegiRide.Navigation;
    using RegiRide.Repository;
    using RegiRide.Services.Interface;

    public class AddressListViewModel : ViewModelBase, INavigable 
    {
        private readonly IAddressRepository addressRepository;
        private readonly BackgroundImageBrush backgroundImageBrush;

        private readonly RelayCommand addNewAddressCommand;

        private readonly RelayCommand loadAddressListCommand;

        private ObservableCollection<AddressViewModel> addressList;

        private AddressViewModel selectedAddress;

        private bool navigationIsAllowed = true;

        private bool isProgressBarVisible = true;

        public AddressListViewModel(IAddressRepository addressRepository, BackgroundImageBrush backgroundImageBrush)
        {
            this.addressRepository = addressRepository;
            this.backgroundImageBrush = backgroundImageBrush;
            this.addNewAddressCommand = new RelayCommand(this.AddNewAddress);
            this.loadAddressListCommand = new RelayCommand(this.LoadAddressList);
#if DEBUG
            if (this.IsInDesignMode)
            {
                this.LoadAddressList();
            }
#endif
        }

        public bool IsProgressBarVisible
        {
            get
            {
                return isProgressBarVisible;
            }

            set
            {
                isProgressBarVisible = value;
                this.RaisePropertyChanged(() => IsProgressBarVisible);
            }
        }

        public RelayCommand AddNewAddressCommand
        {
            get
            {
                return addNewAddressCommand;
            }
        }

        public RelayCommand LoadAddressListCommand
        {
            get
            {
                return loadAddressListCommand;
            }
        }

        public ImageBrush BackgroundImageBrush
        {
            get { return backgroundImageBrush.GetBackground(); }
        }

        public INavigationService NavigationService { get; set; }

        public ObservableCollection<AddressViewModel> AddressList
        {
            get
            {
                return addressList;
            }

            set
            {
                addressList = value;
                this.RaisePropertyChanged(() => AddressList);
            }
        }

        public AddressViewModel SelectedAddress
        {
            get
            {
                return selectedAddress;
            }

            set
            {
                if (value != selectedAddress)
                {
                    var oldValue = selectedAddress;
                    selectedAddress = value;
                    this.RaisePropertyChanged(() => SelectedAddress, oldValue, selectedAddress, true);
                    if (navigationIsAllowed)
                    {
                        NavigationService.Navigate("/Views/AddressDetailView.xaml");
                    }
                }
            }
        }

        private void AddNewAddress()
        {
            this.SelectedAddress = new AddressViewModel(this.addressRepository.CreateNewAddress(), WhichAddress.None, backgroundImageBrush);
            this.NavigationService.Navigate("/Views/AddressDetailView.xaml");
        }

        private void LoadAddressList()
        {
            try
            {
                navigationIsAllowed = false;
                AddressList = AddressListMapper.Map(this.addressRepository.GetAllWithNumberOfRides());
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                navigationIsAllowed = true;
            }
        }
    }
}