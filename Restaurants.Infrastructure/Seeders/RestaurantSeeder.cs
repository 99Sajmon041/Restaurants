using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Domain.Constants;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestauranstDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if(!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
        [
            new() { Name = UserRoles.Admin, NormalizedName = UserRoles.Admin.ToUpper() },
            new() { Name = UserRoles.Owner, NormalizedName = UserRoles.Owner.ToUpper() },
            new() { Name = UserRoles.User, NormalizedName = UserRoles.User.ToUpper() }
        ];

        return roles;
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants =
        [
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description = "Populární řetězec s kuřecími specialitami a rychlým občerstvením.",
                ContactEmail = "info@kfc.cz",
                ContactNumber = "+420 800 100 100",
                HasDelivery = true,
                Dishes =
                [
                    new()
                    {
                        Name = "Original Chicken Bucket",
                        Description = "Kbelík smaženého kuřete podle originální receptury s 11 bylinkami a kořením.",
                        Price = 349.90M,
                    },
                    new()
                    {
                        Name = "Twister Menu",
                        Description = "Kuřecí wrap s hranolkami a nápojem dle výběru.",
                        Price = 189.00M,
                    },
                    new()
                    {
                        Name = "Zinger Burger",
                        Description = "Pikantní kuřecí sendvič s majonézou, salátem a sezamovou houskou.",
                        Price = 165.00M,
                    },
                ],
                Address = new()
                {
                    City = "Praha",
                    Street = "Václavské náměstí 12",
                    PostalCode = "11000"
                }
            },

            new()
            {
                Name = "Golden Dragon",
                Category = "Čínská restaurace",
                Description = "Tradiční čínská restaurace s širokou nabídkou jídel a příjemnou atmosférou.",
                ContactEmail = "goldendragon@restaurace.cz",
                ContactNumber = "+420 222 555 888",
                HasDelivery = true,
                Dishes =
                [
                    new()
                    {
                        Name = "Kuře Kung Pao",
                        Description = "Kuřecí kousky s arašídy, zeleninou a pikantní omáčkou.",
                        Price = 179.00M,
                    },
                    new()
                    {
                        Name = "Smažené nudle s krevetami",
                        Description = "Vaječné nudle restované se zeleninou, vejcem a krevetami.",
                        Price = 195.00M,
                    },
                    new()
                    {
                        Name = "Pekingská kachna",
                        Description = "Klasická čínská kachna s palačinkami, jarní cibulkou a hoisin omáčkou.",
                        Price = 289.00M,
                    },
                ],
                Address = new()
                {
                    City = "Brno",
                    Street = "Čínská 88",
                    PostalCode = "60200"
                }
            },
        ];
        return restaurants;
    }
}
