namespace RegiRide.Mappers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using RegiRide.Model;
    using RegiRide.Utils;
    using RegiRide.ViewModels;

    public class RideListMapper
    {
        private readonly WeekNumberCalculator weekNumberCalculator;

        public RideListMapper(WeekNumberCalculator weekNumberCalculator)
        {
            this.weekNumberCalculator = weekNumberCalculator;
        }

        public void Map(List<Ride> rideList, ObservableCollection<RideViewModel> rideListViewModels)
        {
            if (rideList != null)
            {
                foreach (Ride ride in rideList)
                {
                    var rideViewModel = new RideViewModel(ride, weekNumberCalculator, false);
                    rideListViewModels.Add(rideViewModel);
                }
            }
        }
    }
}