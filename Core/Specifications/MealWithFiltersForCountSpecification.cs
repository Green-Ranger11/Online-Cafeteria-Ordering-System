using Core.Entities;

namespace Core.Specifications
{
    public class MealWithFiltersForCountSpecification : BaseSpecification<Meal>
    {
        public MealWithFiltersForCountSpecification(MealSpecParams mealParams)
        : base(x => 
                (string.IsNullOrEmpty(mealParams.Search) || x.Name.ToLower().Contains(mealParams.Search)) &&
                (!mealParams.MenuId.HasValue || x.MenuId == mealParams.MenuId) &&
                (!mealParams.TypeId.HasValue || x.MealTypeId == mealParams.TypeId) &&
                (!mealParams.RestaurantId.HasValue || x.RestaurantId == mealParams.RestaurantId)
            )
        {
        }
    }
}