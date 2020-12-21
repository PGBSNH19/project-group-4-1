using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp_Frontend.Services
{
    public class SellerPageRepository
    {
        public HttpClient http { get; }
        public SellerPageRepository(IHttpClientFactory _clientFactory)
        {
            var client = _clientFactory.CreateClient("api");
            http = client;
        }

        public async Task<List<SellerPage>> GetProducts()
        {
            var sellerPages = await http.GetJsonAsync<List<SellerPage>>(http.BaseAddress + "/api/v1.0/SellerPage/GetSellerPages");
            return sellerPages;
        }

        public async Task<SellerPage> GetSellerPageById(int id)
        {
            var sellerPage = await http.GetJsonAsync<SellerPage>(http.BaseAddress + $"/api/v1.0/SellerPage/GetSellerPage/{id}");
            return sellerPage;
        }
        public async Task<SellerPage> PostSellerPage(SellerPage sellerPageToCreate)
        {
            var data = new StringContent(JsonConvert.SerializeObject(sellerPageToCreate), Encoding.UTF8, "application/json");

            var sellerPage = await http.PostJsonAsync<SellerPage>(http.BaseAddress + "api/v1.0/SellerPage", data);
            return sellerPage;
        }
    }
}
