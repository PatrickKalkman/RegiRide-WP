namespace RegiRide.DesignTime
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using RegiRide.Model;
    using RegiRide.Repository;

    public class DesignRideRepository : IRideRepository
    {
        public List<Ride> GetAll()
        {
            var rideList = new List<Ride>();
            
            var startDateTime = new DateTime(2014, 1, 1);
            var endDatetime = new DateTime(2014, 6, 1);

            for (DateTime dateTimeCounter = startDateTime; dateTimeCounter < endDatetime; dateTimeCounter = dateTimeCounter.AddDays(0.5))
            {
                var ride = new Ride();
                ride.Id = Guid.NewGuid();
                ride.RideType = (int)RideType.Business;
                ride.Date = dateTimeCounter;
                ride.Remark = "Remark " + dateTimeCounter.Ticks;
                ride.StartMilage = 12001;
                ride.EndMilage = 14082;

                ride.AddressStartId = new Guid("432947F1-DD59-4E79-9359-D771FB5DE6EE");
                ride.AddressEndId = new Guid("432947F1-DD59-4E79-9359-D771FB5DE6EF");
                rideList.Add(ride);
                ride.StartAddress = new Address() {Name="fffffff"};
                ride.EndAddress = new Address() { Name = "aaaaaaaaa" };
            }

            return rideList;
        }

        public Ride GetPreviousState(Ride model)
        {
            return model;
        }

        public void Save(Ride ride)
        {
        }

        public Ride CreateNewRide()
        {
            return new Ride();
        }

        public void Delete(Ride model)
        {
        }

        public bool IsAddressUsed(Address model)
        {
            return false;
        }

        public void DeleteAll()
        {
            
        }

        public Ride GetRide(Guid addressId)
        {
            try
            {
                var ride = new Ride();
                ride.Id = Guid.NewGuid();
                ride.RideType = (int)RideType.Business;
                ride.Date = DateTime.Now;
                ride.Remark = "Remark 001";
                ride.StartMilage = 12001;
                ride.EndMilage = 12082;
                //ride.StartAddress = new DesignAddressRepository().GetAddress(Guid.NewGuid());
                //ride.EndAddress = new DesignAddressRepository().GetAddress(Guid.NewGuid());
                return ride;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                return null;
            }
        }
    }
}
