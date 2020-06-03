using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Meal, MealToReturnDto>()
                .ForMember(d => d.Menu, o => o.MapFrom(s => s.Menu.Name))
                .ForMember(d => d.MealType, o => o.MapFrom(s => s.MealType.Name))
                .ForMember(d => d.Restaurant, o => o.MapFrom(s => s.Restaurant.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<MealUrlResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.MealId, o => o.MapFrom(s => s.ItemOrdered.MealItemId))
                .ForMember(d => d.MealName, o => o.MapFrom(s => s.ItemOrdered.MealName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
        }
    }
}