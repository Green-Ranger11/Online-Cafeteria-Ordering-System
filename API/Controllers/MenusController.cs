using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MenusController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MenusController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MenuToReturnDto>> CreateMenu(MenuCreateDto menuToCreate)
        {
            var menu = _mapper.Map<MenuCreateDto, Menu>(menuToCreate);
            menu.Restaurant = await _unitOfWork.Repository<Restaurant>().GetByIdAsync(menuToCreate.RestaurantId);

            _unitOfWork.Repository<Menu>().Add(menu);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem creating menu"));

            return _mapper.Map<Menu, MenuToReturnDto>(menu);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MenuToReturnDto>> UpdateMenu(int id, MenuCreateDto menuToUpdate)
        {
            var menu = await _unitOfWork.Repository<Menu>().GetByIdAsync(id);
            menu.Restaurant = await _unitOfWork.Repository<Restaurant>().GetByIdAsync(menuToUpdate.RestaurantId);

            _mapper.Map(menuToUpdate, menu);

            _unitOfWork.Repository<Menu>().Update(menu);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem updating menu"));

            return _mapper.Map<Menu, MenuToReturnDto>(menu);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMenu(int id)
        {
            var menu = await _unitOfWork.Repository<Menu>().GetByIdAsync(id);

            _unitOfWork.Repository<Menu>().Delete(menu);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem deleting menu"));

            return Ok();
        }
    }
}