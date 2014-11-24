namespace RegiRide.ViewModels
{
    using System;

    using GalaSoft.MvvmLight;

    using RegiRide.Model;
    using RegiRide.Utils;

    public class RideViewModel : ObservableObject
    {
        private readonly Ride ride;

        private readonly WeekNumberCalculator weekNumberCalculator;

        public RideViewModel(Ride ride, WeekNumberCalculator weekNumberCalculator, bool isNew)
        {
            this.ride = ride;
            this.weekNumberCalculator = weekNumberCalculator;
            this.IsNew = isNew;
        }

        public bool IsNew { get; set; }

        public DateTime RideDate
        {
            get
            {
                return ride.Date;
            }
        }

        public string RideDateFormatted
        {
            get
            {
                return ride.Date.ToString(Constants.Settings.DateFormat);
            }
        }

        public string Distance
        {
            get
            {
                int difference = ride.EndMilage - ride.StartMilage;
                if (difference > 0)
                {
                    return string.Format("( {0} )", difference);
                }

                return "( ? )";
            }
        }

        public string AddressFromName
        {
            get
            {
                return string.Format("{0} ( {1} )", ride.StartAddress.Name, ride.StartAddress.City);
            }
        }

        public string AddressToName
        {
            get
            {
                return string.Format("{0} ( {1} )", ride.EndAddress.Name, ride.EndAddress.City);
            }
        }

        public Ride Model
        {
            get
            {
                return ride;
            }
        }

        public int WeekNumber
        {
            get
            {
                return weekNumberCalculator.GetWeekNumber(ride.Date);
            }
        }

        public string WeekNumberFormatted
        {
            get
            {
                return string.Format("{0} - W {1:00}", ride.Date.Year, WeekNumber);
            }
        }
    }
}