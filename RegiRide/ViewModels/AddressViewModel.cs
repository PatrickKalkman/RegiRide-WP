using System.Windows.Media;

namespace RegiRide.ViewModels
{
    using RegiRide.Model;

    public class AddressViewModel 
    {
        private readonly Address address;
        private readonly BackgroundImageBrush backgroundImageBrush;

        public AddressViewModel(Address address, WhichAddress whichAddress, BackgroundImageBrush backgroundImageBrush)
        {
            this.address = address;
            this.backgroundImageBrush = backgroundImageBrush;
            this.WhichAddress = whichAddress;
        }

        public ImageBrush BackgroundImageBrush
        {
            get { return backgroundImageBrush.GetBackground(); }
        }

        public WhichAddress WhichAddress { get; set; }

        public string StreetName
        {
            get
            {
                return address.Street;
            }

            set
            {
                address.Street = value;
            }
        }

        public string Name
        {
            get
            {
                return address.Name;
            }

            set
            {
                address.Name = value;
            }
        }

        public string PostalCode
        {
            get
            {
                return address.PostalCode;
            }

            set
            {
                address.PostalCode = value;
            }
        }

        public string City
        {
            get
            {
                return address.City;
            }

            set
            {
                address.City = value;
            }
        }

        public Address Model
        {
            get
            {
                return address;
            }
        }
    }
}
