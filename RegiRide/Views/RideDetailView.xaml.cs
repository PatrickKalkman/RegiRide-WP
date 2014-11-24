namespace RegiRide.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using Microsoft.Phone.Controls;

    public partial class RideDetailView : PhoneApplicationPage
    {
        public RideDetailView()
        {
            InitializeComponent();
            var lightThemeVisibility = (Visibility)Resources["PhoneLightThemeVisibility"];
            if (lightThemeVisibility == Visibility.Visible)
            {
                AddStartAddress.Style = (Style)Application.Current.Resources["ImageButtonStyleLight"];
                AddEndAddress.Style = (Style)Application.Current.Resources["ImageButtonStyleLight"];
            }
            else
            {
                AddStartAddress.Style = (Style)Application.Current.Resources["ImageButtonStyleDark"];
                AddEndAddress.Style = (Style)Application.Current.Resources["ImageButtonStyleDark"];
            }
        }

        private void ApplicationBarIconButtonSave_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as RideDetailViewModel;
            if (vm != null)
            {
            	PerformFocusHandler();
                vm.SaveRideCommand.Execute(null);
            }
        }

    	private void PerformFocusHandler()
    	{
			object focusObj = FocusManager.GetFocusedElement();
			if (focusObj != null && focusObj is TextBox)
			{
				var binding = (focusObj as TextBox).GetBindingExpression(TextBox.TextProperty);
				if (binding != null)
				{
					binding.UpdateSource();
				}
			}
		}

    	private void ApplicationBarIconButtonCancel_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as RideDetailViewModel;
            if (vm != null)
            {
                vm.CancelCommand.Execute(null);
            }
        }

        private void ApplicationBarIconButtonDelete_Click(object sender, System.EventArgs e)
        {
            var vm = DataContext as RideDetailViewModel;
            if (vm != null)
            {
                vm.DeleteCommand.Execute(null);
            }
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var vm = DataContext as RideDetailViewModel;
            if (vm != null)
            {
                vm.GoBackCommand.Execute(null);
            }
        }
    }
}