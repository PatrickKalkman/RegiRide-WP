namespace RegiRide
{
    using System;

    using RegiRide.Repository.DesignTime;
    using RegiRide.ViewModels;

    public partial class AddressDetailViewModel
    {
        public void CreateDesignTimeData()
        {
            SelectedAddress = new AddressViewModel(new DesignAddressRepository().GetAddress(Guid.NewGuid()), WhichAddress.None, new BackgroundImageBrush());
        }
    }
}