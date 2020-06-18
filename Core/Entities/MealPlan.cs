using System.Collections.Generic;

namespace Core.Entities
{
    public class MealPlan : BaseEntity
    {
        public IReadOnlyList<MealPlanDay> Meals { get; set; }
        public decimal TotalPrice { get; set; }
    }
}