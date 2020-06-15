namespace Core.Entities.OrderAggregate
{
    public class OrderItemIngrediant : BaseEntity
    {
        public OrderItemIngrediant()
        {
        }

        public OrderItemIngrediant(int quantity, decimal price)
        {
            this.Quantity = quantity;
            this.Price = price;

        }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}