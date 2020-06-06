using System.Linq;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class MealUrlResolver : IValueResolver<Meal, MealToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public MealUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Meal source, MealToReturnDto destination, string destMember, ResolutionContext context)
        {
            var photo = source.Photos.FirstOrDefault(x => x.IsMain);

            if(photo != null)
            {
                return _config["ApiUrl"] + photo.PictureUrl;
            }

            return _config["ApiUrl"] + "images/meals/placeholder.png";
        }
    }
}