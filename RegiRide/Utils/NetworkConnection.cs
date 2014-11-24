namespace RegiRide.Utils
{
    using System.Net.NetworkInformation;

    public class NetworkConnection
    {
        public bool IsAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
