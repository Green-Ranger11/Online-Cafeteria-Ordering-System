using System.Collections.Generic;

namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(MealItemOrdered itemOrdered, decimal price, int quantity, List<BasketItemIngrediant> ingrediants)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
            Ingrediants = ingrediants;
        }

        public MealItemOrdered ItemOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<BasketItemIngrediant> Ingrediants { get; set; }
    }
}