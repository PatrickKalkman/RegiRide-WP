namespace RegiRide.Views
{
    using Microsoft.Phone.Controls;
    using ViewModels;

    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ApplicationBarIconButtonAdd_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as RideListViewModel;
            if (vm != null)
            {
                vm.AddRideCommand.Execute(null);
            }
        }

        private void ApplicationBarIconButtonSettings_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as RideListViewModel;
            if (vm != null)
            {
                vm.SettingsCommand.Execute(null);
            }
        }

        private void ApplicationBarIconButtonEmail_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as RideListViewModel;
            if (vm != null)
            {
                vm.EmailCommand.Execute(null);
            }
        }

        private void ApplicationBarMenuItemAddresses_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as RideListViewModel;
            if (vm != null)
            {
                vm.AddressesCommand.Execute(null);
            }
        }

        private void ApplicationBarMenuItemAbout_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as RideListViewModel;
            if (vm != null)
            {
                vm.AboutCommand.Execute(null);
            }
        }
    }
}