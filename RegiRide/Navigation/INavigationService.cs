namespace RegiRide.Navigation
{
    public interface INavigationService
    {
        void Navigate(string url);

        void Back();
    }
}
