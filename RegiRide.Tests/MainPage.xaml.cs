namespace RegiRide.Tests
{
    using System.Windows;

    using Microsoft.Phone.Controls;
    using Microsoft.Silverlight.Testing;

    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var testPage = UnitTestSystem.CreateTestPage();
            var imobileTPage = testPage as IMobileTestPage;
            this.BackKeyPress += (s, arg) =>
            {
                bool navigateBackSuccessfull = imobileTPage.NavigateBack();
                arg.Cancel = navigateBackSuccessfull;
            };

            (Application.Current.RootVisual as PhoneApplicationFrame).Content = testPage;
        }
    }
}