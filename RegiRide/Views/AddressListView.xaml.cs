namespace RegiRide.Views
{
    using Microsoft.Phone.Controls;

    using RegiRide.ViewModels;

    public partial class AddressListView : PhoneApplicationPage
    {
        public AddressListView()
        {
            this.InitializeComponent();
        }

        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as AddressListViewModel;
            if (vm != null)
            {
                vm.AddNewAddressCommand.Execute(null);
            }
        }
    }
}