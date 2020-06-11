using System;

namespace API.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
        public bool PaymentMethod {get; set;}
        public DateTimeOffset ShippingDate { get; set; }
    }
}