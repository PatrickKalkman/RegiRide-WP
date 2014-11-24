namespace RegiRide.Repository.DesignTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RegiRide.Model;
    using RegiRide.Repository;

    public class DesignAddressRepository : IAddressRepository
    {
        public List<Address> GetAll()
        {
            var addressList = new List<Address>();
            var address1 = new Address { Id = new Guid("432947F1-DD59-4E79-9359-D771FB5DE6EE"), Street = "Delftechpark 37i", Name = "Hinttech", PostalCode = "2628 XJ", City = "Delft" };
            addressList.Add(address1);
            var address2 = new Address { Id = new Guid("432947F1-DD59-4E79-9359-D771FB5DE6EF"), Street = "Koningin Wilhelminaplein 129", Name = "Mediacatalyst", PostalCode = "1062 HH", City = "Amsterdam" };
            addressList.Add(address2);
            var address3 = new Address { Id = Guid.NewGuid(), Street = "3e Binnenvestgracht 23F", Name = "Mindbus", PostalCode = "1062 HH", City = "Leiden"};
            addressList.Add(address3);
            var address4 = new Address { Id = Guid.NewGuid(), Street = "Paasheuvelweg 3", Name = "Virtual Affairs", PostalCode = "1105 BE", City = "Amsterdam" };
            addressList.Add(address4);
            var address5 = new Address { Id = Guid.NewGuid(), Street = "Rivium Boulevard 301", Name = "BP Nederland B.V.", PostalCode = "2909 LK", City = "Capelle aan den ijssel" };
            addressList.Add(address5);
            return addressList.OrderBy(a => a.Name).ToList();
        }

        public List<Address> GetAllWithNumberOfRides()
        {
            return this.GetAll();
        }

        public Address GetAddress(Guid addressId)
        {
            return new Address { Id = Guid.NewGuid(), Street = "Delftechpark", Name = "Hinttech", PostalCode = "2628 XJ", City = "Delft" };
        }

        public void Save(Address address)
        {
        }

        public Address CreateNewAddress()
        {
            return new Address();
        }

        public Address GetPreviousState(Address address)
        {
            return address;
        }

        public void Delete(Address address)
        {
        }
    }
}