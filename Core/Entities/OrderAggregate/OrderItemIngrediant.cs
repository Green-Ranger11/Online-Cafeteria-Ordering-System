namespace Core.Entities.OrderAggregate
{
    public class OrderItemIngrediant : BaseEntity
    {
        public OrderItemIngrediant()
        {
        }

        public OrderItemIngrediant(int quantity, decimal price, string name)
        {
            this.Quantity = quantity;
            this.Price = price;
            this.Name = name;
        }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
    }
}