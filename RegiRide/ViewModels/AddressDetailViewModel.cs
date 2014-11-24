using System.Windows.Media;

namespace RegiRide
{
    using System.Windows;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using RegiRide.Messages;
    using RegiRide.Navigation;
    using RegiRide.Repository;
    using RegiRide.Resources;
    using RegiRide.Services.Interface;
    using RegiRide.ViewModels;

    public partial class AddressDetailViewModel : ViewModelBase, INavigable
    {
        private readonly IAddressRepository addressRepository;

        private readonly IRideRepository rideRepository;
        private readonly BackgroundImageBrush backgroundImageBrush;

        private AddressViewModel addressViewModel;

        private WhichAddress whichAddress;

        public AddressDetailViewModel(IAddressRepository addressRepository, IRideRepository rideRepository, BackgroundImageBrush backgroundImageBrush)
        {
            this.addressRepository = addressRepository;
            this.rideRepository = rideRepository;
            this.backgroundImageBrush = backgroundImageBrush;

            SaveAddressCommand = new RelayCommand(SaveAddress);
            CancelCommand = new RelayCommand(Cancel);
            DeleteCommand = new RelayCommand(Delete);

            Messenger.Default.Register<PropertyChangedMessage<AddressViewModel>>(
                this,
                message =>
                {
                    SelectedAddress = null;
                    SelectedAddress = message.NewValue;
                    if (message.NewValue != null)
                    {
                        whichAddress = message.NewValue.WhichAddress;
                    }
                });

#if DEBUG
            if (IsInDesignMode)
            {
                CreateDesignTimeData();
            }
#endif
        }

        public ImageBrush BackgroundImageBrush
        {
            get { return backgroundImageBrush.GetBackground(); }
        }

        public INavigationService NavigationService { get; set; }

        public AddressViewModel SelectedAddress
        {
            get
            {
                return addressViewModel;
            }

            set
            {
                if (addressViewModel == value)
                {
                    return;
                }

                addressViewModel = value;
                RaisePropertyChanged(() => SelectedAddress);
            }
        }

        public RelayCommand SaveAddressCommand
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

        private void SaveAddress()
        {
            this.addressRepository.Save(SelectedAddress.Model);
            if (whichAddress != WhichAddress.None)
            {
                Messenger.Default.Send(new AddressAddedMessage { AddressId = SelectedAddress.Model.Id, WhichAddress = whichAddress });
            }

            NavigationService.Back();
        }

        private void Cancel()
        {
            SelectedAddress = new AddressViewModel(addressRepository.GetPreviousState(SelectedAddress.Model), WhichAddress.None, backgroundImageBrush);
            NavigationService.Back();
        }

        private void Delete()
        {
            if (MessageBoxResult.OK == MessageBox.Show(AppResources.ConfirmDelete, AppResources.ConfirmDeleteTitle, MessageBoxButton.OKCancel))
            {
                if (!rideRepository.IsAddressUsed(SelectedAddress.Model))
                {
                    addressRepository.Delete(SelectedAddress.Model);
                    NavigationService.Back();
                }
                else
                {
                    MessageBox.Show(AppResources.CannotDeleteAddressBecauseInUse, AppResources.CannotDeleteTitle, MessageBoxButton.OK);
                }
            }
        }
    }
}