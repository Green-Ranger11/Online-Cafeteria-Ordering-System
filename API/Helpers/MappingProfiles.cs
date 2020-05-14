using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Meal, MealToReturnDto>()
                .ForMember(d => d.Menu, o => o.MapFrom(s => s.Menu.Name))
                .ForMember(d => d.MealType, o => o.MapFrom(s => s.MealType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<MealUrlResolver>());
        }
    }
}