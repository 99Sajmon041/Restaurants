using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantsRepository(RestauranstDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext
            .Restaurants
            .AsNoTracking()
            .Include(x => x.Dishes)
            .ToListAsync();

        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? SortBy, SortDirection sortDirection)
    {
        string? parameter = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Restaurants
            .AsNoTracking()
            .Include(x => x.Dishes)
            .Where(x => parameter == null || (x.Name.Contains(searchPhrase ?? "") || x.Description.Contains(searchPhrase ?? "")));

        var totalCount = await baseQuery.CountAsync();

        if(SortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Category), r => r.Category }
            };

            var selectedColumn = columnsSelector[SortBy];


            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery = baseQuery.OrderBy(selectedColumn)
                : baseQuery = baseQuery.OrderByDescending(selectedColumn);
        }

        var restaurant = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurant, totalCount);
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(x => x.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);

        return restaurant;
    }

    public async Task<int> Create(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Restaurant entity)
    {
        dbContext.Restaurants.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChanges()
        => await dbContext.SaveChangesAsync();
}
