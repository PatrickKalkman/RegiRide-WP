namespace RegiRide.Model
{
    using System;

    using RegiRide.Resources;

    public class RideTypeConverter
    {
        public string ConvertToString(RideType rideType)
        {
            switch (rideType)
            {
                case RideType.Business:
                    return AppResources.RideTypeBusiness;
                case RideType.Private:
                    return AppResources.RideTypePrivate;
                default:
                    throw new ArgumentException(string.Format("Invalid type of ride specified {0}", (int)rideType));
            }
        }
    }
}