using FruitServices.Application.Services; 
using FruitServices.Application.Services.Contracts; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FruitServices.Application.DependencyResolver
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IFruitService, FruitService>();
           

            // Todo: Add Identity Server Code




        }
    }
}
