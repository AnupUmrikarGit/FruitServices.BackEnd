
using FruitServices.Models;

namespace FruitServices.Infrastructure.Adapters.Http.Clients.FruitServicesAPI.Contract
{
    public interface IFruitAPI
    {
        public Task<List<Fruit>> GetAllFruits();
        public Task<List<Fruit>> GetAllFruitsByFamily(string FruitFamilyName);

    }
}