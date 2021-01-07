using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp_Frontend.Services
{
    public class MarketplaceRepository
    {
        public HttpClient http { get; }
        public MarketplaceRepository(IHttpClientFactory _clientFactory)
        {
            var client = _clientFactory.CreateClient("api");
            http = client;
        }

        public async Task<List<Marketplace>> GetMarketplaces()
        {
            var marketplaces = await http.GetJsonAsync<List<Marketplace>>(http.BaseAddress + $"/api/v1.0/Marketplace/GetMarketplaces");
            return marketplaces;
        }

        public async Task<List<Marketplace>> GetMarketplaceById(int id)
        {
            var marketplace = await http.GetJsonAsync<List<Marketplace>>(http.BaseAddress + $"/api/v1.0/Marketplace/GetMarketplace/{id}");
            return marketplace;
        }
        public async Task<Marketplace> PostMarketplace(Marketplace marketplaceToCreate)
        {
            var data = new StringContent(JsonConvert.SerializeObject(marketplaceToCreate), Encoding.UTF8, "application/json");

            var marketplace = await http.PostJsonAsync<Marketplace>(http.BaseAddress + "/api/v1.0/Marketplace", data);
            return marketplace;
        }
    }
}

