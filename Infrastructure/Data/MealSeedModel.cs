namespace Infrastructure.Data
{
    public class MealSeedModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int MealTypeId { get; set; }
        public int RestaurantId { get; set; }  
        public int MenuId { get; set; }  
    }
}