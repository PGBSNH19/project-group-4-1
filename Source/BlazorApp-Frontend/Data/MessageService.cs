using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;


namespace BlazorApp_Frontend.Data
{
    public class MessageService
    {
        private readonly HttpClient Client;

        public MessageService(HttpClient client)
        {
            Client = client;
        }
        public async Task<Message> GetMessages()
        {

            var test = await Client.GetJsonAsync<Message>("http://localhost:5000/api/v1.0/GetMessages");
            return test;
        }
    }
}
