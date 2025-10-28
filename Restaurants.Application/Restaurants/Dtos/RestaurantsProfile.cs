using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<UpdateRestaurantCommand, Restaurant>();

        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode,
                }));

        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(x => x.City, opt => opt.MapFrom(r => r.Address == null ? null : r.Address.City))
            .ForMember(x => x.Street, opt => opt.MapFrom(r => r.Address == null ? null : r.Address.Street))
            .ForMember(x => x.PostalCode, opt => opt.MapFrom(r => r.Address == null ? null : r.Address.PostalCode))
            .ForMember(x => x.Dishes, opt => opt.MapFrom(r => r.Dishes));
    }
}
