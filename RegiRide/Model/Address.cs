namespace RegiRide.Model
{
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;

    using RegiRide.Utils;

    [Table]
    public class Address : PropertyChangeAndChangingEventHandlerBase
    {
        private readonly EntitySet<Ride> startRide;
        private readonly EntitySet<Ride> endRide;
        private Guid id;
        private string name;
        private string street;
        private string postalCode;
        private string city;

        // Speeds up updates.
        [Column(IsVersion = true)]
        private Binary version;

        public Address()
        {
            this.startRide = new EntitySet<Ride>(
                    startRide =>
                    {
                        this.OnPropertyChanging("StartRide");
                        startRide.StartAddress = this;
                    },
                    startRide =>
                    {
                        this.OnPropertyChanging("StartRide");
                        startRide.StartAddress = null;
                    });
            this.endRide = new EntitySet<Ride>(
                    endRide =>
                    {
                        this.OnPropertyChanging("EndRide");
                        endRide.EndAddress = this;
                    },
                    endRide =>
                    {
                        this.OnPropertyChanging("EndRide");
                        endRide.EndAddress = null;
                    });
        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        public Guid Id
        {
            get
            {
                return id;
            }

            set
            {
                this.OnPropertyChanging("Id");
                this.id = value;
                this.OnPropertyChanged("Id");
            }
        }

        [Column]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                this.OnPropertyChanging("Name");
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        [Column]
        public string Street
        {
            get
            {
                return street;
            }

            set
            {
                this.OnPropertyChanging("Street");
                this.street = value;
                this.OnPropertyChanged("Street"); 
            }
        }

        [Column]
        public string City
        {
            get
            {
                return city;
            }

            set
            {
                this.OnPropertyChanging("City");
                this.city = value;
                this.OnPropertyChanged("City");
            }
        }

        [Column]
        public string PostalCode
        {
            get
            {
                return postalCode;
            }

            set
            {
                this.OnPropertyChanging("PostalCode");
                this.postalCode = value;
                this.OnPropertyChanged("PostalCode");
            }
        }

        [Association(Name = "StartAddress_Ride", Storage = "startRide", ThisKey = "Id", OtherKey = "AddressStartId")]
        public EntitySet<Ride> StartRide
        {
            get
            {
                return this.startRide;
            }

            set
            {
                if (value.HasLoadedOrAssignedValues)
                {
                    this.startRide.Assign(value);
                }
            }
        }

        [Association(Name = "EndAddress_Ride", Storage = "endRide", ThisKey = "Id", OtherKey = "AddressEndId")]
        public EntitySet<Ride> EndRide
        {
            get
            {
                return this.endRide;
            }

            set
            {
                if (value.HasLoadedOrAssignedValues)
                {
                    this.endRide.Assign(value);
                }
            }
        }

        public int NumberOfRides { get; set; }
    }
}