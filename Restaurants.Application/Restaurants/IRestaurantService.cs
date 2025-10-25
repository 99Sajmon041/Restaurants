using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
        Task<RestaurantDto?> GetById(int id);
        Task<int> Create(CreateRestaurantDto dto);
    }
} 