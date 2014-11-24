namespace RegiRide.Repository
{
    using System;
    using System.Collections.Generic;

    using RegiRide.Model;

    public class PerformanceRepository
    {
        private readonly RideDataContext rideDataContext;

        private readonly List<Address> addressList = new List<Address>();

        private readonly Random mileGenerator = new Random(13);

        public PerformanceRepository(RideDataContext rideDataContext)
        {
            this.rideDataContext = rideDataContext;
        }

        public void GenerateAddresses()
        {
            var address1 = new Address { Street = "Delftechpark 37i", Name = "Hinttech", PostalCode = "2628 XJ", City = "Delft" };
            addressList.Add(address1);
            rideDataContext.Addresses.InsertOnSubmit(address1);
            var address2 = new Address { Street = "Koningin Wilhelminaplein 129", Name = "Mediacatalyst", PostalCode = "1062 HH", City = "Amsterdam" };
            addressList.Add(address2);
            rideDataContext.Addresses.InsertOnSubmit(address2);
            var address3 = new Address { Street = "3e Binnenvestgracht 23F", Name = "Mindbus", PostalCode = "1062 HH", City = "Leiden" };
            addressList.Add(address3);
            rideDataContext.Addresses.InsertOnSubmit(address3);
            var address4 = new Address { Street = "Paasheuvelweg 3", Name = "Virtual Affairs", PostalCode = "1105 BE", City = "Amsterdam" };
            addressList.Add(address4);
            rideDataContext.Addresses.InsertOnSubmit(address4);
            var address5 = new Address { Street = "Rivium Boulevard 301", Name = "BP Nederland B.V.", PostalCode = "2909 LK", City = "Capelle aan den ijssel" };
            addressList.Add(address5);
            rideDataContext.Addresses.InsertOnSubmit(address5);

            rideDataContext.SubmitChanges();
        }

        public void GenerateRides()
        {
            for (int rideCounter = 0; rideCounter < 25 * 5 * 2; rideCounter++)
            {
                Address startAddress = this.GetAddress();
                Address endAddress = this.GetAddress();
                int startMilage = mileGenerator.Next(150000);
                int endMilage = startMilage + mileGenerator.Next(19010);

                Ride ride = new Ride
                    {
                        StartAddress = startAddress,
                        EndAddress = endAddress,
                        Date = DateTime.Now.AddDays(-1 * rideCounter * 0.5),
                        StartMilage = startMilage,
                        EndMilage = endMilage,
                        RideType = (int)RideType.Business
                    };
                rideDataContext.Rides.InsertOnSubmit(ride);
            }
            rideDataContext.SubmitChanges();
        }

        private Address GetAddress()
        {
            int addressIndex = mileGenerator.Next(5);
            return addressList[addressIndex];
        }
    }
}
