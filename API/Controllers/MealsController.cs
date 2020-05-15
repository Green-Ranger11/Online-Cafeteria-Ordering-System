using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MealsController : BaseApiController
    {
        private readonly IGenericRepository<Meal> _mealsRepo;
        private readonly IGenericRepository<Menu> _menusRepo;
        private readonly IGenericRepository<MealType> _mealTypesRepo;
        private readonly IMapper _mapper;

        public MealsController(IGenericRepository<Meal> mealsRepo, 
        IGenericRepository<Menu> menusRepo, 
        IGenericRepository<MealType> mealTypesRepo,
        IMapper mapper)
        {
            _mealTypesRepo = mealTypesRepo;
            _mapper = mapper;
            _menusRepo = menusRepo;
            _mealsRepo = mealsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MealToReturnDto>>> GetMeals()
        {
            var spec = new MealsWithTypesAndMenusSpecification();

            var meals = await _mealsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Meal>,IReadOnlyList<MealToReturnDto>>(meals));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MealToReturnDto>> GetMeal(int id)
        {
            var spec = new MealsWithTypesAndMenusSpecification(id);

            var meal = await _mealsRepo.GetEnitityWithSpec(spec);

            if(meal == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Meal, MealToReturnDto>(meal);
        }

        [HttpGet("menus")]
        public async Task<ActionResult<IReadOnlyList<Menu>>> GetMenus()
        {
            return Ok(await _menusRepo.ListAllAsync());
        }


        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<MealType>>> GetMealTypes()
        {
            return Ok(await _mealTypesRepo.ListAllAsync());
        }
    }
}