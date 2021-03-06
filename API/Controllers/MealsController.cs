using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MealsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public MealsController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _photoService = photoService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<MealToReturnDto>>> GetMeals([FromQuery] MealSpecParams mealParams)
        {
            var spec = new MealsWithTypesAndMenusSpecification(mealParams);

            var countSpec = new MealWithFiltersForCountSpecification(mealParams);

            var totalItems = await _unitOfWork.Repository<Meal>().CountAsync(countSpec);

            var meals = await _unitOfWork.Repository<Meal>().ListAsync(spec);
            var mealsToReturn = new List<Meal>();

            foreach (var meal in meals)
            {
                if (meal.Stock > 0)
                {
                    mealsToReturn.Add(meal);
                }
                if (meal.Stock < 1)
                {
                    totalItems --;
                }
            }

            var data = _mapper.Map<IReadOnlyList<Meal>, IReadOnlyList<MealToReturnDto>>(mealsToReturn);

            return Ok(new Pagination<MealToReturnDto>(mealParams.PageIndex, mealParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MealToReturnDto>> GetMeal(int id)
        {
            var spec = new MealsWithTypesAndMenusSpecification(id);

            var meal = await _unitOfWork.Repository<Meal>().GetEnitityWithSpec(spec);

            if (meal == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Meal, MealToReturnDto>(meal);
        }

        [HttpGet("menus")]
        public async Task<ActionResult<IReadOnlyList<Menu>>> GetMenus()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            return Ok(await _unitOfWork.Repository<Menu>().ListAllAsync());
        }

        [HttpGet("menus/manager")]
        public async Task<ActionResult<IReadOnlyList<Menu>>> GetMenusForManager()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var restaurants = await _unitOfWork.Repository<Restaurant>().ListAllAsync();

            foreach (var restaurant in restaurants)
            {
                if (restaurant.Name == user.DisplayName)
                {
                    var spec = new MenuByRestaurant(user.DisplayName);
                    return Ok(await _unitOfWork.Repository<Menu>().ListAsync(spec));
                }
            };

            return Ok(await _unitOfWork.Repository<Menu>().ListAllAsync());
        }

        [HttpGet("manager")]
        public async Task<ActionResult<Pagination<MealToReturnDto>>> GetMealsForManager([FromQuery] MealSpecParams mealParams)
        {
            var spec = new MealsWithTypesAndMenusSpecification(mealParams);
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var user = await _userManager.FindByEmailAsync(email);
            var restaurants = await _unitOfWork.Repository<Restaurant>().ListAllAsync();

            foreach (var restaurant in restaurants)
            {
                if (restaurant.Name == user.DisplayName)
                {
                    mealParams.RestaurantId = restaurant.Id;
                }
            };

            var countSpec = new MealWithFiltersForCountSpecification(mealParams);

            var totalItems = await _unitOfWork.Repository<Meal>().CountAsync(countSpec);

            var meals = await _unitOfWork.Repository<Meal>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Meal>, IReadOnlyList<MealToReturnDto>>(meals);

            return Ok(new Pagination<MealToReturnDto>(mealParams.PageIndex, mealParams.PageSize, totalItems, data));
        }

        [HttpGet("menus/{id}")]
        public async Task<ActionResult<IReadOnlyList<Menu>>> GetMenusForRestaurant(int id)
        {
            var spec = new MenuByRestaurant(id);
            var menus = await _unitOfWork.Repository<Menu>().GetEnititiesWithSpec(spec);
            return Ok(menus);
        }

        [HttpGet("restaurants")]
        public async Task<ActionResult<IReadOnlyList<Restaurant>>> GetRestaurants()
        {
            return Ok(await _unitOfWork.Repository<Restaurant>().ListAllAsync());
        }


        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<MealType>>> GetMealTypes()
        {
            return Ok(await _unitOfWork.Repository<MealType>().ListAllAsync());
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<MealToReturnDto>> CreateMeal(MealCreateDto mealToCreate)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var user = await _userManager.FindByEmailAsync(email);
            var restaurants = await _unitOfWork.Repository<Restaurant>().ListAllAsync();

            foreach (var restaurant in restaurants)
            {
                if (restaurant.Name == user.DisplayName)
                {
                    mealToCreate.RestaurantId = restaurant.Id;
                }
            };

            var meal = _mapper.Map<MealCreateDto, Meal>(mealToCreate);

            _unitOfWork.Repository<Meal>().Add(meal);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem creating meal"));

            return _mapper.Map<Meal, MealToReturnDto>(meal);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MealToReturnDto>> UpdateMeal(int id, MealCreateDto mealToUpdate)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var user = await _userManager.FindByEmailAsync(email);
            var restaurants = await _unitOfWork.Repository<Restaurant>().ListAllAsync();

            foreach (var restaurant in restaurants)
            {
                if (restaurant.Name == user.DisplayName)
                {
                    if (mealToUpdate.RestaurantId != restaurant.Id)
                    {
                        return BadRequest(403);
                    }
                }
            };

            if (mealToUpdate.Stock < 0)
            {
                mealToUpdate.Stock = 100;
            }

            var meal = await _unitOfWork.Repository<Meal>().GetByIdAsync(id);

            _mapper.Map(mealToUpdate, meal);

            _unitOfWork.Repository<Meal>().Update(meal);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem updating meal"));

            return _mapper.Map<Meal, MealToReturnDto>(meal);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeal(int id)
        {
            var spec = new MealsWithTypesAndMenusSpecification(id);
            var meal = await _unitOfWork.Repository<Meal>().GetEnitityWithSpec(spec);

            foreach (var photo in meal.Photos)
            {
                if (meal.Id > 18)
                {
                    _photoService.DeleteFromDisk(photo);
                }
            }

            _unitOfWork.Repository<Meal>().Delete(meal);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem deleting meal"));

            return Ok();
        }

        [HttpPut("{id}/photo")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<MealToReturnDto>> AddMealPhoto(int id, [FromForm] MealPhotoDto photoDto)
        {
            var spec = new MealsWithTypesAndMenusSpecification(id);
            var meal = await _unitOfWork.Repository<Meal>().GetEnitityWithSpec(spec);

            if (photoDto.Photo.Length > 0)
            {
                var photo = await _photoService.SaveToDiskAsync(photoDto.Photo);

                if (photo != null)
                {
                    meal.AddPhoto(photo.PictureUrl, photo.FileName);

                    _unitOfWork.Repository<Meal>().Update(meal);

                    var result = await _unitOfWork.Complete();

                    if (result <= 0) return BadRequest(new ApiResponse(400, "Problem adding photo meal"));
                }
                else
                {
                    return BadRequest(new ApiResponse(400, "problem saving photo to disk"));
                }
            }

            return _mapper.Map<Meal, MealToReturnDto>(meal);
        }

        [HttpDelete("{id}/photo/{photoId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteMealPhoto(int id, int photoId)
        {
            var spec = new MealsWithTypesAndMenusSpecification(id);
            var meal = await _unitOfWork.Repository<Meal>().GetEnitityWithSpec(spec);

            var photo = meal.Photos.SingleOrDefault(x => x.Id == photoId);

            if (photo != null)
            {
                if (photo.IsMain)
                    return BadRequest(new ApiResponse(400,
                        "You cannot delete the main photo"));

                _photoService.DeleteFromDisk(photo);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Photo does not exist"));
            }

            meal.RemovePhoto(photoId);

            _unitOfWork.Repository<Meal>().Update(meal);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem adding meal product"));

            return Ok();
        }

        [HttpPost("{id}/ingrediant")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<MealToReturnDto>> AddMealIngrediant(int id, IngrediantToReturnDto ingrediantDto)
        {
            var spec = new MealsWithTypesAndMenusSpecification(id);
            var meal = await _unitOfWork.Repository<Meal>().GetEnitityWithSpec(spec);

            meal.AddIngrediant(ingrediantDto.Name, ingrediantDto.Price, ingrediantDto.Quantity);

            _unitOfWork.Repository<Meal>().Update(meal);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem adding ingrediant"));


            return _mapper.Map<Meal, MealToReturnDto>(meal);
        }

        [HttpPut("{id}/ingrediant/{ingrediantId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<MealToReturnDto>> UpdateMealIngrediant(int id, IngrediantToReturnDto ingrediantDto, int ingrediantId)
        {
            var spec = new MealsWithTypesAndMenusSpecification(id);
            var meal = await _unitOfWork.Repository<Meal>().GetEnitityWithSpec(spec);

            meal.UpdateIngrediant(ingrediantDto.Name, ingrediantDto.Price, ingrediantDto.Quantity, ingrediantId);

            _unitOfWork.Repository<Meal>().Update(meal);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem updating ingrediant"));


            return _mapper.Map<Meal, MealToReturnDto>(meal);
        }

        [HttpDelete("{id}/ingrediant/{ingrediantId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteMealIngrediant(int id, int ingrediantId)
        {
            var spec = new MealsWithTypesAndMenusSpecification(id);
            var meal = await _unitOfWork.Repository<Meal>().GetEnitityWithSpec(spec);

            meal.RemoveIngrediant(ingrediantId);

            _unitOfWork.Repository<Meal>().Update(meal);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem adding meal product"));

            return Ok();
        }

        [HttpPost("{id}/photo/{photoId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<MealToReturnDto>> SetMainPhoto(int id, int photoId)
        {
            var spec = new MealsWithTypesAndMenusSpecification(id);
            var meal = await _unitOfWork.Repository<Meal>().GetEnitityWithSpec(spec);

            if (meal.Photos.All(x => x.Id != photoId)) return NotFound();

            meal.SetMainPhoto(photoId);

            _unitOfWork.Repository<Meal>().Update(meal);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem adding meal product"));

            return _mapper.Map<Meal, MealToReturnDto>(meal);
        }
    }

}