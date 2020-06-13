namespace Core.Entities
{
    public class Ingrediant : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Meal Meal { get; set; }
        public int MealId { get; set; }
    }
}