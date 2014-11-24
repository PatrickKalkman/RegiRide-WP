namespace RegiRide.Messages
{
    using System;

    using RegiRide.ViewModels;

    public class AddressAddedMessage
    {
        public Guid AddressId { get; set; }

        public WhichAddress WhichAddress { get; set; }
    }
}
