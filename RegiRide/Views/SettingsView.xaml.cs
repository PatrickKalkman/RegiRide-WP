namespace RegiRide.Views
{
    using RegiRide.ViewModels;

    using Microsoft.Phone.Controls;

    public partial class SettingsView : PhoneApplicationPage
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButtonSave_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as SettingsViewModel;
            if (vm != null)
            {
                vm.SaveCommand.Execute(null);
            }
        }
    }
}