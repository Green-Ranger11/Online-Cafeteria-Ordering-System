namespace Core.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }

        public Restaurant Restaurant { get; set; }

        public int RestaurantId { get; set; }
    }
}