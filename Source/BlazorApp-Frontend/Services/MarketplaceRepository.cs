using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorApp_Frontend.Services
{
    public class MarketplaceRepository
    {
        public HttpClient http { get; }
        public MarketplaceRepository(HttpClient client)
        {
            // client.BaseAddress = new Uri("https://nearbyproduceapiTest.azurewebsites.net");
            http = client;
        }

        public async Task<List<Marketplace>> GetAllMarketplaces()
        {
            var marketplaces = await http.GetJsonAsync<List<Marketplace>>("https://nearbyproduceapitest.azurewebsites.net/api/v1.0/Marketplace/GetMarketplaces");
            return marketplaces;
        }
    }
}
