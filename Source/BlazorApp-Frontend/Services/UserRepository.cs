using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp_Frontend.Services
{
    public class UserRepository
    {
        public HttpClient http { get; }
        public UserRepository(IHttpClientFactory _clientFactory)
        {
            var client = _clientFactory.CreateClient("api");
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

        public async Task<HttpResponseMessage> PostUser(User userToCreate)
        {

            var data = new StringContent(JsonConvert.SerializeObject(userToCreate), Encoding.UTF8, "application/json");

            userToCreate.UserID = 100;
            userToCreate.Email = "hej";
            userToCreate.Password = "123";
            userToCreate.Username = "test";

            var response = await http.PostAsync($"http://localhost:5000/api/v1.0/User", data);

            return null;
        }
        public async Task<HttpResponseMessage> DeleteUser(User userToDelete)
        {
            var response = await http.DeleteAsync(http.BaseAddress + $"api/v1.0/User/{userToDelete.UserID}");
            return response;
        }
    }
}
