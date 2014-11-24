namespace RegiRide.Repository
{
    using System;
    using System.Collections.Generic;

    using RegiRide.Model;

    public interface IAddressRepository
    {
        List<Address> GetAll();

        List<Address> GetAllWithNumberOfRides();

        Address GetAddress(Guid addressId);

        void Save(Address address);

        Address CreateNewAddress();

        Address GetPreviousState(Address address);

        void Delete(Address address);
    }
}
