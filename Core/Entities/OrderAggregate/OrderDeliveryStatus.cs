using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    public enum OrderDeliveryStatus
    {
        [EnumMember(Value = "Order Recieved")]
        OrderRecieved,

        [EnumMember(Value = "Preparing Order")]
        PreparingOrder,

        [EnumMember(Value = "Out For Delivery")]
        OutForDelivery,

        [EnumMember(Value = "Delivered")]
        Delivered
    }
}