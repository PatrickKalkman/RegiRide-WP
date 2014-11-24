namespace RegiRide.Views
{
    using System;
    using System.Windows;
    using System.Windows.Navigation;

    using Microsoft.Phone.Controls;

    using RegiRide.Resources;
    using RegiRide.Utils;
    using RegiRide.ViewModels;

    public partial class Dropbox : PhoneApplicationPage
    {
        private readonly NetworkConnection networkConnection;

        public Dropbox()
        {
            InitializeComponent();
            networkConnection = new NetworkConnection();
            LoadDropbox();
        }

        private void LoadDropbox()
        {
            bool isConnected = networkConnection.IsAvailable();
            if (isConnected)
            {
                var viewModel = DataContext as DropboxViewModel;
                BusyIndicator.Visibility = Visibility.Visible;
                viewModel.DropNetClient.GetTokenAsync(
                    userToken =>
                        {
                            var tokenUrl = viewModel.DropNetClient.BuildAuthorizeUrl("http://www.semanticarchitecture.net/regiride/");
                            loginBrowser.LoadCompleted += this.loginBrowser_LoadCompleted;
                            loginBrowser.Navigate(new Uri(tokenUrl));
                        },
                    error =>
                    Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(error.Response.ErrorMessage)));
            }
            else
            {
                MessageBox.Show(AppResources.NoNetworkAvailable);
            }
        }

        private void loginBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            BusyIndicator.Visibility = Visibility.Collapsed;
            bool isConnected = networkConnection.IsAvailable();
            if (isConnected)
            {
                if (e.Uri.AbsolutePath == "/regiride/")
                {
                    var viewModel = DataContext as DropboxViewModel;
                    viewModel.DropNetClient.GetAccessTokenAsync(
                        response =>
                            {
                                viewModel.StoreToken(response.Token);
                                viewModel.StoreSecret(response.Secret);
                                viewModel.AuthenticatedSuccessfully();
                            },
                        error =>
                        Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(error.Response.ErrorMessage)));
                }
            }
            else
            {
                MessageBox.Show(AppResources.NoNetworkAvailable);
            }        
        }
    }
}