namespace RegiRide.Services
{
    using DropNet;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;

    using RegiRide.DesignTime;
    using RegiRide.Export;
    using RegiRide.Mappers;
    using RegiRide.Model;
    using RegiRide.Repository;
    using RegiRide.Repository.DesignTime;
    using RegiRide.Utils;
    using RegiRide.ViewModels;

    public class ContainerLocator
    {
        public ContainerLocator()
        {
            this.ConfigureContainer();
        }

        private void ConfigureContainer()
        {
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IAddressRepository, AddressRepository>();
                SimpleIoc.Default.Register<IRideRepository, RideRepository>();
                SimpleIoc.Default.Register<ISettingsHelper, SettingsHelper>();
            }
            else
            {
                SimpleIoc.Default.Register<IAddressRepository, DesignAddressRepository>();
                SimpleIoc.Default.Register<IRideRepository, DesignRideRepository>();
                SimpleIoc.Default.Register<ISettingsHelper, DesignSettingsHelper>();
            }

            SimpleIoc.Default.Register(() =>
            {
                var context = new RideDataContext(Constants.Settings.DatabaseConnectionString);
                context.Initialize();
                return context;
            });

            SimpleIoc.Default.Register<NetworkConnection>();
            SimpleIoc.Default.Register<RegiRideSettingsManager>();
            SimpleIoc.Default.Register<PerformanceRepository>();

            SimpleIoc.Default.Register<RideTypeConverter>();
            SimpleIoc.Default.Register<RideLineExporter>();
            SimpleIoc.Default.Register<ExportHeaderGenerator>();
            SimpleIoc.Default.Register<ExportManager>();
            SimpleIoc.Default.Register<BackgroundImageBrush>();

            SimpleIoc.Default.Register<AddressListViewModel>();
            SimpleIoc.Default.GetInstance<AddressListViewModel>();

            SimpleIoc.Default.Register<AddressDetailViewModel>();
            SimpleIoc.Default.GetInstance<AddressDetailViewModel>();

            SimpleIoc.Default.Register<RideListMapper>();
            SimpleIoc.Default.Register<WeekNumberCalculator>();

            SimpleIoc.Default.Register(
                () =>
                {
                    var settingsHelper = SimpleIoc.Default.GetInstance<ISettingsHelper>();
                    string userToken = settingsHelper.GetSetting(Constants.Settings.DropboxUserTokenKey, Constants.Settings.DefaultValue);
                    string userSecret = settingsHelper.GetSetting(Constants.Settings.DropboxUserSecretKey, Constants.Settings.DefaultValue);

                    if (userToken != Constants.Settings.DefaultValue && userSecret != Constants.Settings.DefaultValue)
                    {
                        return new DropNetClient(Constants.Settings.DropBoxApplicationKey, Constants.Settings.DropboxAppSecret, userToken, userSecret);
                    }

                    return new DropNetClient(Constants.Settings.DropBoxApplicationKey, Constants.Settings.DropboxAppSecret);
                });

            SimpleIoc.Default.Register<RideListViewModel>();
            SimpleIoc.Default.GetInstance<RideListViewModel>();

            SimpleIoc.Default.Register<RideDetailViewModel>();
            SimpleIoc.Default.GetInstance<RideDetailViewModel>();

            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.GetInstance<SettingsViewModel>();

            SimpleIoc.Default.Register<DropboxViewModel>();
            SimpleIoc.Default.GetInstance<DropboxViewModel>();
        } 
    }
}