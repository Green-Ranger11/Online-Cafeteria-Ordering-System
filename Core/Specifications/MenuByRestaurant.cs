using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class MenuByRestaurant : BaseSpecification<Menu>
    {
        public MenuByRestaurant(int id) : base(m => m.RestaurantId == id)
        {
        }
    }
}