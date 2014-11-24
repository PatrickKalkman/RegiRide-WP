namespace RegiRide.Views
{
    using System.Windows;

    using Microsoft.Phone.Controls;

    public partial class AddressDetailView : PhoneApplicationPage
    {
        public AddressDetailView()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButtonSave_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as AddressDetailViewModel;
            if (vm != null)
            {
                vm.SaveAddressCommand.Execute(null);
            }
        }

        private void ApplicationBarIconButtonCancel_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as AddressDetailViewModel;
            if (vm != null)
            {
                vm.CancelCommand.Execute(null);
            }
        }

        private void ApplicationBarIconButtonDelete_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as AddressDetailViewModel;
            if (vm != null)
            {
                vm.DeleteCommand.Execute(null);
            }
        }
    }
}