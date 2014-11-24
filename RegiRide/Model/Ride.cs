namespace RegiRide.Model
{
    using System;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;

    using RegiRide.Utils;

    [Table]
    public class Ride : PropertyChangeAndChangingEventHandlerBase
    {
        private Guid id;
        private int startMilage;
        private int endMilage;
        private int rideType;
        private string remark;
        private DateTime date;

        // Speeds up updates.
        [Column(IsVersion = true)]
        private Binary version;

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
        public int StartMilage
        {
            get
            {
                return startMilage;
            }

            set
            {
                this.OnPropertyChanging("StartMilage");
                this.startMilage = value;
                this.OnPropertyChanged("StartMilage");
            }
        }


        [Column]
        public int EndMilage
        {
            get
            {
                return endMilage;
            }

            set
            {
                this.OnPropertyChanging("EndMilage");
                this.endMilage = value;
                this.OnPropertyChanged("EndMilage");
            }
        }

        [Column]
        public int RideType
        {
            get
            {
                return rideType;
            }

            set
            {
                this.OnPropertyChanging("RideType");
                this.rideType = value;
                this.OnPropertyChanged("RideType");
            }
        }

        [Column]
        public string Remark
        {
            get
            {
                return remark;
            }

            set
            {
                this.OnPropertyChanging("Remark");
                remark = value;
                this.OnPropertyChanged("Remark");
            }
        }

        [Column]
        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                this.OnPropertyChanging("Date");
                date = value;
                this.OnPropertyChanged("Date");                
            }
        }

        private Guid addressStartId;

        [Column]
        public Guid AddressStartId
        {
            get
            {
                return this.addressStartId;
            }

            set
            {
                if (this.addressStartId != value)
                {
                    this.OnPropertyChanging("AddressStartId");
                    this.addressStartId = value;
                    this.OnPropertyChanged("AddressStartId");
                }
            }
        }

        private EntityRef<Address> startAddress;

        [Association(Name = "Ride_StartAddress", Storage = "startAddress", ThisKey = "AddressStartId", OtherKey = "Id", IsForeignKey = false)]
        public Address StartAddress
        {
            get
            {
                return this.startAddress.Entity;
            }

            set
            {
                Address previousValue = this.startAddress.Entity;
                if ((previousValue != value) || (this.startAddress.HasLoadedOrAssignedValue == false))
                {
                    this.OnPropertyChanging("StartAddress");
                    if (previousValue != null)
                    {
                        this.startAddress.Entity = null;
                        previousValue.StartRide.Remove(this);
                    }

                    this.startAddress.Entity = value;
                    if (value != null)
                    {
                        value.StartRide.Add(this);
                        this.addressStartId = value.Id;
                    }
                    else
                    {
                        this.addressStartId = default(Guid);
                    }
                    this.OnPropertyChanged("StartAddress");
                }
            }
        }

        private Guid addressEndId;

        [Column]
        public Guid AddressEndId
        {
            get
            {
                return this.addressEndId;
            }

            set
            {
                if (this.addressEndId != value)
                {
                    this.OnPropertyChanging("AddressEndId");
                    this.addressEndId = value;
                    this.OnPropertyChanged("AddressEndId");
                }
            }
        }

        private EntityRef<Address> endAddress;

        [Association(Name = "Ride_EndAddress", Storage = "endAddress", ThisKey = "AddressEndId", OtherKey = "Id", IsForeignKey = false)]
        public Address EndAddress
        {
            get
            {
                return this.endAddress.Entity;
            }

            set
            {
                Address previousValue = this.endAddress.Entity;
                if ((previousValue != value) || (this.endAddress.HasLoadedOrAssignedValue == false))
                {
                    this.OnPropertyChanging("EndAddress");
                    if (previousValue != null)
                    {
                        this.endAddress.Entity = null;
                        previousValue.EndRide.Remove(this);
                    }

                    this.endAddress.Entity = value;
                    if (value != null)
                    {
                        value.EndRide.Add(this);
                        this.AddressEndId = value.Id;
                    }
                    else
                    {
                        this.addressEndId = default(Guid);
                    }

                    this.OnPropertyChanged("EndAddress");
                }
            }
        }
    }
}