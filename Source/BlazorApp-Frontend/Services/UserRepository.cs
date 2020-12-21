using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp_Frontend.Services
{
    public class UserRepository
    {
        public HttpClient http { get; }
        public UserRepository(HttpClient client)
        {
            client.BaseAddress = new Uri("https://nearbyproduceapiTest.azurewebsites.net");
            http = client;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await http.GetJsonAsync<List<User>>(http.BaseAddress + "/api/v1.0/User/GetUsers");
            return users;
        }
        public async Task<User> GetUserById(int id)
        {
            var user = await http.GetJsonAsync<User>(http.BaseAddress + $"/api/v1.0/User/GetUser/{id}");

            return user;
        }

        public async Task<User> PostUser(User userToCreate)
        {
            var data = new StringContent(JsonConvert.SerializeObject(userToCreate), Encoding.UTF8, "application/json");

            var user = await http.PostJsonAsync<User>(http.BaseAddress + "api/v1.0/User", data);
            return user;
        }
        public async Task<HttpResponseMessage> DeleteUser(User userToDelete)
        {
            var response = await http.DeleteAsync(http.BaseAddress + $"api/v1.0/User/{userToDelete.UserID}");
            return response;
        }
    }
}
