namespace RegiRide
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RegiRide.DesignTime;
    using RegiRide.Mappers;
    using RegiRide.Model;
    using RegiRide.Repository.DesignTime;
    using RegiRide.Utils;
    using RegiRide.ViewModels;

    public partial class RideDetailViewModel
    {
        public void CreateDesignTimeData()
        {
            SelectedRide = new RideViewModel(new DesignRideRepository().GetRide(Guid.NewGuid()), new WeekNumberCalculator(), false);
            IEnumerable<Address> adresses = new DesignAddressRepository().GetAll();
            var adressesForView = AddressListMapper.Map(adresses.ToList());
        }
    }
}
