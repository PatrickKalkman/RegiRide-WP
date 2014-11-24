namespace RegiRide.Repository
{
    using System.Collections.Generic;

    using RegiRide.Model;

    public interface IRideRepository
    {
        List<Ride> GetAll();

        Ride GetPreviousState(Ride model);

        void Save(Ride model);

        Ride CreateNewRide();

        void Delete(Ride model);

        bool IsAddressUsed(Address model);

        void DeleteAll();
    }
}