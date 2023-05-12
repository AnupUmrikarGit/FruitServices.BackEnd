
using FruitServices.Models;

namespace FruitServices.Application.Services.Contracts
{
    public interface IFruitService
    {
        public Task<List<Fruit>> GetAllFruits();
        public Task<List<Fruit>> GetAllFruitsByFamily(string fruitFamilyName);
       
    }
}
