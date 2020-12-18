using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp_Frontend.Services
{
    public class UserRepository
    {
        public HttpClient http { get; }
        public UserRepository(HttpClient client)
        {
            // client.BaseAddress = new Uri("https://nearbyproduceapiTest.azurewebsites.net");
            http = client;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await http.GetJsonAsync<List<User>>("https://nearbyproduceapi.azurewebsites.net/api/v1.0/User/GetUsers");
            return users;
        }
    }
}
