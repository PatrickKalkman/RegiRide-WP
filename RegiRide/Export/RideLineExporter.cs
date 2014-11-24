namespace RegiRide.Export
{
    using RegiRide.Model;
    using RegiRide.Utils;

    public class RideLineExporter
    {
        private readonly RideTypeConverter rideTypeConverter;

        public RideLineExporter(RideTypeConverter rideTypeConverter)
        {
            this.rideTypeConverter = rideTypeConverter;
        }

        public virtual string GenerateLine(Ride rideToExport)
        {
            return string.Format(
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                rideToExport.Date.ToString(Constants.Settings.ExportDateFormat),
                rideToExport.StartAddress.Name,
                rideToExport.StartAddress.Street,
                rideToExport.StartAddress.PostalCode,
                rideToExport.StartAddress.City,
                rideToExport.EndAddress.Name,
                rideToExport.EndAddress.Street,
                rideToExport.EndAddress.PostalCode,
                rideToExport.EndAddress.City,
                rideToExport.StartMilage,
                rideToExport.EndMilage,
                this.rideTypeConverter.ConvertToString((RideType)rideToExport.RideType),
                rideToExport.Remark);
        }
    }
}