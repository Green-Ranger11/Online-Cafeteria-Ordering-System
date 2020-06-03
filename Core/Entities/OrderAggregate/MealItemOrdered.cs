namespace Core.Entities.OrderAggregate
{
    public class MealItemOrdered
    {
        public MealItemOrdered()
        {
        }

        public MealItemOrdered(int mealItemId, string mealName, string pictureUrl)
        {
            MealItemId = mealItemId;
            MealName = mealName;
            PictureUrl = pictureUrl;
        }

        public int MealItemId { get; set; }
        public string MealName { get; set; }
        public string PictureUrl { get; set; }

    }
}