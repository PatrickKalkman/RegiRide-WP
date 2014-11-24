namespace RegiRide.Services.Interface
{
    using RegiRide.Navigation;

    public interface INavigable
    {
        INavigationService NavigationService { get; set; }
    }
}
