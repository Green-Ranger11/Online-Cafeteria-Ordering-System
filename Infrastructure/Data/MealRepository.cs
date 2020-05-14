using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MealRepository : IMealRepository
    {
        private readonly StoreContext _context;
        public MealRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Meal> GetMealByIdAsync(int id)
        {
            return await _context.Meals
                .Include(m => m.MealType)
                .Include(m => m.Menu)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Meal>> GetMealsAsync()
        {
            return await _context.Meals
                .Include(m => m.MealType)
                .Include(m => m.Menu)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<MealType>> GetMealTypesAsync()
        {
            return await _context.MealTypes.ToListAsync();
        }

        public async Task<IReadOnlyList<Menu>> GetMenusAsync()
        {
            return await _context.Menus.ToListAsync();
        }
    }
}