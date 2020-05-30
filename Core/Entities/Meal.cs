namespace Core.Entities
{
    public class Meal : BaseEntity
    {
        public string Name { get; set; }

        public string Description {get; set;}

        public decimal Price {get; set;}

        public string PictureUrl { get; set; }

        public MealType MealType {get; set;}

        public int MealTypeId { get; set; }

        public Menu Menu {get; set;}

        public int MenuId { get; set; }

        public Restaurant Restaurant { get; set; }

        public int RestaurantId { get; set; }
    }
}