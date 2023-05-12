
using FruitServices.Application.Services.Contracts;
using FruitServices.Infrastructure.Adapters.Http.Clients.FruitServicesAPI.Contract;
using FruitServices.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FruitServices.Application.Services
{
    public class FruitService : IFruitService
    {

        private readonly IFruitAPI _fruitAPI;
        
        public FruitService(IFruitAPI fruitAPI, ILogger<FruitService> logger)
        {
            _fruitAPI = fruitAPI;            
        }
        public async Task<List<Fruit>> GetAllFruits()
        {
            return await _fruitAPI.GetAllFruits();
        }
        public async Task<List<Fruit>> GetAllFruitsByFamily(string fruitFamilyName)
        {
            return await _fruitAPI.GetAllFruitsByFamily(fruitFamilyName);
        }

    }
}
