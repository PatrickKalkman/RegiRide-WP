namespace RegiRide.Model
{
    using System.Data.Linq;

    using RegiRide.Repository;

    public class RideDataContext : DataContext
    {
        public Table<Ride> Rides;

        public Table<Address> Addresses; 
        
        public RideDataContext(string fileOrConnection) : base(fileOrConnection)
        {
        }

        public void Initialize()
        {
            if (!this.DatabaseExists())
            {
                this.CreateDatabase();
            }
        }
    }
}
