using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class MealsFromMenu : BaseSpecification<Meal>
    {
        public MealsFromMenu(int id) : base(m => m.MenuId == id)
        {
        }
    }
}