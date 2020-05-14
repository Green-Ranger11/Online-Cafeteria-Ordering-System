using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly IMealRepository _repo;
        public MealsController(IMealRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<ActionResult<List<Meal>>> GetMeals()
        {
            var meals = await _repo.GetMealsAsync();

            return Ok(meals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            return await _repo.GetMealByIdAsync(id);
        }

        [HttpGet("menus")]
        public async Task<ActionResult<IReadOnlyList<Menu>>> GetMenus()
        {
            return Ok(await _repo.GetMenusAsync());
        }

        
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<MealType>>> GetMealTypes()
        {
            return Ok(await _repo.GetMealTypesAsync());
        }
    }
}