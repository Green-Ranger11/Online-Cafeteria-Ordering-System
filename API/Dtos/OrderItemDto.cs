namespace API.Dtos
{
    public class OrderItemDto
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}