using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MealsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;

        public MealsController(IUnitOfWork unitOfWork,
        IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<MealToReturnDto>>> GetMeals([FromQuery] MealSpecParams mealParams)
        {
            var spec = new MealsWithTypesAndMenusSpecification(mealParams);

            var countSpec = new MealWithFiltersForCountSpecification(mealParams);

            var totalItems = await _unitOfWork.Repository<Meal>().CountAsync(countSpec);

            var meals = await _unitOfWork.Repository<Meal>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Meal>, IReadOnlyList<MealToReturnDto>>(meals);

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
            return Ok(await _unitOfWork.Repository<Menu>().ListAllAsync());
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MealToReturnDto>> CreateMeal(MealCreateDto mealToCreate)
        {
            var meal = _mapper.Map<MealCreateDto, Meal>(mealToCreate);

            _unitOfWork.Repository<Meal>().Add(meal);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem creating meal"));

            return _mapper.Map<Meal, MealToReturnDto>(meal);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MealToReturnDto>> UpdateMeal(int id, MealCreateDto mealToUpdate)
        {
            var meal = await _unitOfWork.Repository<Meal>().GetByIdAsync(id);

            _mapper.Map(mealToUpdate, meal);

            _unitOfWork.Repository<Meal>().Update(meal);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem updating meal"));

            return _mapper.Map<Meal, MealToReturnDto>(meal);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeal(int id)
        {
            var meal = await _unitOfWork.Repository<Meal>().GetByIdAsync(id);

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [HttpPost("{id}/photo/{photoId}")]
        [Authorize(Roles = "Admin")]
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