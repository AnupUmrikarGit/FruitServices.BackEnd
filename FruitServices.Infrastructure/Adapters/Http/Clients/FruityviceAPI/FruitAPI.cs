
using FruitServices.Infrastructure.Adapters.Http.Clients.FruitServicesAPI.Contract;
using FruitServices.Models; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Text.Json;

namespace FruitServices.Infrastructure.Adapters.Http.Clients
{
    public class FruitAPI : IFruitAPI
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<FruitAPI> _logger;
        private readonly IConfiguration _config; 
        private const string _fruitApiClientName = "Fruityvice";
        private string _getAllFruitApiEndpoint = "";
        private string _getAllFruitsByFamilyApiEndpoint = "";

        public FruitAPI(ILogger<FruitAPI> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _getAllFruitApiEndpoint = _config["HttpClients:Fruityvice:GetAllFruits"];
            _getAllFruitsByFamilyApiEndpoint = _config["HttpClients:Fruityvice:GetAllFruitsByFamily"];
        }


        public async Task<List<Fruit>> GetAllFruits()
        {
            var httpClient = _httpClientFactory.CreateClient(_fruitApiClientName);
            List<Fruit> fruits = null;
            try
            {
               
                var apiResponse = httpClient.GetAsync(_getAllFruitApiEndpoint).Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    var contentStream = apiResponse.Content.ReadAsStreamAsync().Result;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    fruits = System.Text.Json.JsonSerializer.Deserialize<List<Fruit>>(contentStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error in FruitAPI.GetAllFruits");
            }

            return fruits;
        }
        public async Task<List<Fruit>> GetAllFruitsByFamily(string fruitFamilyName)
        {
            var httpClient = _httpClientFactory.CreateClient(_fruitApiClientName);
            List<Fruit> fruits = null;
            try
            {

                var apiResponse = await httpClient.GetAsync(_getAllFruitsByFamilyApiEndpoint+ fruitFamilyName);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var contentStream = apiResponse.Content.ReadAsStreamAsync().Result;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    fruits = System.Text.Json.JsonSerializer.Deserialize<List<Fruit>>(contentStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error in FruitAPI.GetAllFruitsByFamily");
            }

            return fruits;
        }
        
            

    }

}
