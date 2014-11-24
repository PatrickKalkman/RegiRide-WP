namespace RegiRide.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using RegiRide.Model;
    using RegiRide.ViewModels;

    internal class AddressListMapper
    {
        private static readonly BackgroundImageBrush backgroundImageBrush = new BackgroundImageBrush();

        public static ObservableCollection<AddressViewModel> Map(List<Address> addressList)
        {
            var addresses = new ObservableCollection<AddressViewModel>();
            foreach (Address address in addressList)
            {
                addresses.Add(new AddressViewModel(address, WhichAddress.None, backgroundImageBrush));
            }

            return addresses;
        }

        public static ObservableCollection<AddressViewModel> MapForListPicker(List<Address> addressList)
        {
            var addresses = new ObservableCollection<AddressViewModel>();
            foreach (Address address in addressList)
            {
                addresses.Add(new AddressViewModel(address, WhichAddress.None, backgroundImageBrush));
            }

            addresses.Insert(0, new AddressViewModel(new Address { Id = Guid.Empty, Name = "Select an address" }, WhichAddress.None, backgroundImageBrush));

            return addresses;
        }
    }
}