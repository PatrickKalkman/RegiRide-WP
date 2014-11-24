namespace RegiRide.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    using RegiRide.Model;

    public class RideRepository : IRideRepository
    {
        private readonly RideDataContext rideDataContext;

        public RideRepository(RideDataContext rideDataContext)
        {
            this.rideDataContext = rideDataContext;
        }

        public List<Ride> GetAll()
        {
            return (from ride in rideDataContext.Rides select ride).ToList();
        }

        public Ride GetPreviousState(Ride ride)
        {
            return rideDataContext.Rides.GetOriginalEntityState(ride);    
        }

        public void Save(Ride ride)
        {
			  if (ride.Id == Guid.Empty )
			  {
					rideDataContext.Rides.InsertOnSubmit(ride);
			  }
			  rideDataContext.SubmitChanges();
        }

        public Ride CreateNewRide()
        {
            Ride latestRide = this.GetLatestRide();

            Ride ride;

            if (latestRide != null)
            {
                ride = new Ride
                    {
                        Date = DateTime.Now, 
                        StartMilage = latestRide.EndMilage,
                        EndMilage = latestRide.EndMilage + (latestRide.EndMilage - latestRide.StartMilage), 
                        StartAddress = latestRide.EndAddress,
                        EndAddress = latestRide.StartAddress,
                        RideType = (int)RideType.Business
                    };
            }
            else
            {
                ride = new Ride
                {
                    Date = DateTime.Now,
                    RideType = (int)RideType.Business
                };
            }

            return ride;
        }

        public void Delete(Ride model)
        {
            if (model != null)
            {
                if (model.Id != Guid.Empty)
                {
                    var rideToDelete = this.rideDataContext.Rides.SingleOrDefault(r => r.Id == model.Id);
                    if (rideToDelete != null)
                    {
                        rideDataContext.Rides.DeleteOnSubmit(rideToDelete);
                        rideDataContext.SubmitChanges();
                    }
                }
            }
        }

        public bool IsAddressUsed(Address model)
        {
            if (model != null)
            {
                if (model.Id != Guid.Empty)
                {
                    int numberOfRidesUsingAddress = this.rideDataContext.Rides.Count(r => r.AddressStartId == model.Id || r.AddressEndId == model.Id);
                    if (numberOfRidesUsingAddress > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void DeleteAll()
        {
            rideDataContext.Rides.DeleteAllOnSubmit(rideDataContext.Rides);
            rideDataContext.SubmitChanges();
        }

        public Ride GetLatestRide()
        {
            return (from ride in rideDataContext.Rides orderby ride.Date descending select ride).Take(1).SingleOrDefault();
        }
    }
}