using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class MealsWithTypesAndMenusSpecification : BaseSpecification<Meal>
    {
        public MealsWithTypesAndMenusSpecification(MealSpecParams mealParams) 
            : base(x => 
                (string.IsNullOrEmpty(mealParams.Search) || x.Name.ToLower().Contains(mealParams.Search)) &&
                (!mealParams.MenuId.HasValue || x.MenuId == mealParams.MenuId) &&
                (!mealParams.TypeId.HasValue || x.MealTypeId == mealParams.TypeId) &&
                (!mealParams.RestaurantId.HasValue || x.RestaurantId == mealParams.RestaurantId)
            )
        {
            AddInclude(m => m.MealType);
            AddInclude(m => m.Menu);
            AddOrderBy(m => m.Name);
            ApplyPaging(mealParams.PageSize * (mealParams.PageIndex -1), mealParams.PageSize);

            if (!string.IsNullOrEmpty(mealParams.Sort))
            {
                switch(mealParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public MealsWithTypesAndMenusSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(m => m.MealType);
            AddInclude(m => m.Menu);
        }
    }
}