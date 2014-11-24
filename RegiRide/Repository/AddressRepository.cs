namespace RegiRide.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RegiRide.Model;

    public class AddressRepository : IAddressRepository
    {
        private readonly RideDataContext rideDataContext;

        public AddressRepository(RideDataContext rideDataContext)
        {
            this.rideDataContext = rideDataContext;
        }

        public List<Address> GetAll()
        {
            return (from address in rideDataContext.Addresses orderby address.Name select address).ToList();
        }

        public List<Address> GetAllWithNumberOfRides()
        {
            var addressList = (from address in rideDataContext.Addresses orderby address.Name select address).ToList();
            foreach (var address in addressList)
            {
                int usedInNumberStart = this.rideDataContext.Rides.Count(r => r.StartAddress.Id == address.Id);
                int usedInNumberEnd = this.rideDataContext.Rides.Count(r => r.EndAddress.Id == address.Id);
                address.NumberOfRides = usedInNumberStart + usedInNumberEnd;
            }

            return addressList;
        }

        public Address GetAddress(Guid addressId)
        {
            return this.rideDataContext.Addresses.Single(a => a.Id == addressId);
        }

        public void Save(Address address)
        {
            if (rideDataContext.Addresses.GetOriginalEntityState(address) == null)
            {
                rideDataContext.Addresses.InsertOnSubmit(address);
            }

            rideDataContext.SubmitChanges();
        }

        public Address GetPreviousState(Address address)
        {
            return rideDataContext.Addresses.GetOriginalEntityState(address);
        }

        public void Delete(Address model)
        {
            if (model != null)
            {
                if (model.Id != Guid.Empty)
                {
                    rideDataContext.Addresses.DeleteOnSubmit(model);
                    rideDataContext.SubmitChanges();
                }
            }
        }

        public Address CreateNewAddress()
        {
            return new Address();
        }

        public void Add(Address address)
        {
            this.rideDataContext.Addresses.InsertOnSubmit(address);
            this.rideDataContext.SubmitChanges();
        }
    }
}