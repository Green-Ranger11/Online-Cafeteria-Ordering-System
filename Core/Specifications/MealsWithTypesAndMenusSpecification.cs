using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class MealsWithTypesAndMenusSpecification : BaseSpecification<Meal>
    {
        public MealsWithTypesAndMenusSpecification()
        {
            AddInclude(m => m.MealType);
            AddInclude(m => m.Menu);
        }

        public MealsWithTypesAndMenusSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(m => m.MealType);
            AddInclude(m => m.Menu);
        }
    }
}