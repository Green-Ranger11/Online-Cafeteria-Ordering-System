namespace Core.Entities
{
    public class MealPlanDay : BaseEntity
    {
        public Meal Meal { get; set; }
        public int MealId { get; set; }
        public string Day { get; set; }
    }
}