using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace BlazorApp_Frontend.Data
{
    public class MessageService
    {
        public HttpClient Client { get; }

        public MessageService(HttpClient client)
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            Client = client;
        }
        public async Task<List<Message>> GetMessages()
        {
            try
            {
                var response = await Client.GetAsync("api/v1.0/GetMessages");
                await using var responseString = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<Message>>(responseString);

            }
            catch (Exception e)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
