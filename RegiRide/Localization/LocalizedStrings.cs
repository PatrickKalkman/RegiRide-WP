namespace RegiRide.Localization
{
    using RegiRide.Resources;

    public class LocalizedStrings
    {
        private readonly AppResources localizedResources = new AppResources();

        public AppResources LocalizedResources
        {
            get
            {
                return this.localizedResources;
            }
        }
    }
}
