
using FruitServices.Infrastructure.Adapters.Http.Clients;
using FruitServices.Infrastructure.Adapters.Http.Clients.FruitServicesAPI.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 


namespace FruitServices.Infrastructure.DependencyResolver
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IFruitAPI, FruitAPI>();
        }
    }
}
