namespace Core.Entities
{
    public class Photo : BaseEntity
    {
        public string PictureUrl { get; set; }
        public string FileName { get; set; }
        public bool IsMain { get; set; }
        public Meal Meal { get; set; }
        public int MealId { get; set; }
    }
}