using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var appicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IRestaurantService, RestaurantService>();

        services.AddAutoMapper(appicationAssembly);

        services.AddValidatorsFromAssembly(appicationAssembly)
            .AddFluentValidationAutoValidation();
    }
}

 