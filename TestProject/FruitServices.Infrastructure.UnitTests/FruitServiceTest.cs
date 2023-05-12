using FruitServices.Application.Services.Contracts;
using FruitServices.Infrastructure.Adapters.Http.Clients;
using FruitServices.Infrastructure.Adapters.Http.Clients.FruitServicesAPI.Contract;
using FruitServices.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FruitServices.Application.Services.UnitTests
{
    public class FruitServiceTest
    {
        private Mock<IFruitAPI> _fruitAPI;

        public static Mock<IFruitAPI> GetFruitAPI(string familyName)
        {
            var mockFruityviceAPI = new Mock<IFruitAPI>();
            var fruits = new List<Fruit>() {
                                                new Fruit{
                                                            Name= "Tomato",
                                                            Id= 5,
                                                            Family= "Solanaceae",
                                                            Order= "Solanales",
                                                            Genus= "Solanum",
                                                            Nutritions= new Nutritions{
                                                                    Calories= 74,
                                                                Fat =0.2,
                                                                Sugar = 2.6,
                                                                Carbohydrates = 3.9,
                                                                Protein =0.9
                                                            }

                                                         }
                                                ,
                                                new Fruit{
                                                            Name= "Persimmon",
                                                            Id= 52,
                                                            Family= "Ebenaceae",
                                                            Order= "Rosales",
                                                            Genus= "Diospyros",
                                                            Nutritions= new Nutritions{
                                                                Calories= 81,
                                                                Fat =0,
                                                                Sugar = 18.0,
                                                                Carbohydrates = 0,
                                                                Protein =18.0
                                                            }

                                                         }


             };
            mockFruityviceAPI.Setup(r => r.GetAllFruits()).ReturnsAsync(fruits);
            mockFruityviceAPI.Setup(r => r.GetAllFruitsByFamily(familyName)).ReturnsAsync(fruits.Where(i => i.Family == familyName).ToList());
            return mockFruityviceAPI;
        }
        public FruitServiceTest()
        {
        }

        [Fact]
        public void Test01_GetAllFruits_SuccessTest()
        {

            _fruitAPI = GetFruitAPI("");
            var logger = Mock.Of<ILogger<FruitService>>();

            IFruitService fruitService = new FruitService(_fruitAPI.Object, logger);
            var result = fruitService.GetAllFruits().Result;

            result.ShouldBeOfType<List<Fruit>>();
            result.Count.ShouldBeGreaterThan(0);

        }
        
        [Fact]
        public void Test02_GetAllFruitsByFamily_SuccessTest()
        {

            _fruitAPI = GetFruitAPI("Ebenaceae");
            var logger = Mock.Of<ILogger<FruitService>>();

            IFruitService fruitService = new FruitService(_fruitAPI.Object, logger);
            var result = fruitService.GetAllFruitsByFamily("Ebenaceae").Result;

            result.ShouldBeOfType<List<Fruit>>();
            result.Count.ShouldBeGreaterThan(0);

        }

        [Fact]
        public void Test03_GetAllFruitsByFamily_UnSuccessTest()
        {

            _fruitAPI = GetFruitAPI("abcd");
            var logger = Mock.Of<ILogger<FruitService>>();

            IFruitService fruitService = new FruitService(_fruitAPI.Object, logger);
            var result = fruitService.GetAllFruitsByFamily("abcd").Result;

            result.ShouldBeOfType<List<Fruit>>();
            result.Count.ShouldBe(0);

        }
    }
}
