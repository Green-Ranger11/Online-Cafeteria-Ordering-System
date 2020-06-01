namespace Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string Restaurant { get; set; }
        public string Menu { get; set; }
        public string Type { get; set; }
    }
}