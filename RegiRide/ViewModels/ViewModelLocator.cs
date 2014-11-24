namespace RegiRide.ViewModels
{
    using GalaSoft.MvvmLight.Ioc;

    using RegiRide.Services;

    using AddressDetailViewModel = RegiRide.AddressDetailViewModel;

    public class ViewModelLocator
    {
        private readonly ContainerLocator containerLocator;

        public ViewModelLocator()
        {
            this.containerLocator = new ContainerLocator();
        }

        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<SettingsViewModel>();
            }
        }

        public RideListViewModel RideListViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RideListViewModel>();
            }
        }

        public RideDetailViewModel RideDetailViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<RideDetailViewModel>();
            }
        }

        public AddressListViewModel AddressListViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<AddressListViewModel>();
            }
        }

        public AddressDetailViewModel AddressDetailViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<AddressDetailViewModel>();
            }
        }

        public DropboxViewModel DropboxViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<DropboxViewModel>();
            }
        }
    }
}