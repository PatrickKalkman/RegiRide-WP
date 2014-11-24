namespace RegiRide.Tests
{
    using System;
    using System.Collections.Generic;

    using RegiRide.Model;

    public class RideGeneratorMother
    {
        public static IEnumerable<Ride> CreateRideList(int numberOfRidesToGenerate)
        {
            var rides = new List<Ride>();
            for (int rideCounter = 0; rideCounter < numberOfRidesToGenerate; rideCounter++)
            {
                rides.Add(CreateFullDummyRide());
            }

            return rides;
        }

        public static Ride CreateFullDummyRide()
        {
            return new Ride
            {
                StartAddress = new Address { Name = "Hinttech", Street = "Delftechpark 37i", PostalCode = "2628 XJ", City = "Delft" },
                EndAddress = new Address { Name = "Mediacatalyst", Street = "Herengracht 182", PostalCode = "1016 BR", City = "Amsterdam" },
                Date = new DateTime(2010, 12, 11),
                StartMilage = 153800,
                EndMilage = 153875,
                RideType = (int)RideType.Business,
                Remark = "Via schiphol to prevent getting stuck in traffic jam"
            };
        }

        public static Ride CreateDummyRideWithEmptyFields()
        {
            return new Ride
            {
                StartAddress = new Address { Name = "Hinttech", Street = "Delftechpark 37i" },
                EndAddress = new Address { PostalCode = "1016 BR", City = "Amsterdam" },
                Date = new DateTime(2010, 12, 11),
                StartMilage = 153800,
                EndMilage = 153875,
                RideType = (int)RideType.Business
            };
        }
    }
}
