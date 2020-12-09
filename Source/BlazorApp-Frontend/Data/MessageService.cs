using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.Json.Serialization;
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
        public async Task<string> GetMessage()
        {
            var response = await Client.GetAsync("graphql?query={messages{text}}");
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonHelper = JsonConvert.DeserializeObject<string>(responseString);

            return "Hej";

        }
    }

    public class Messages
    {
        [JsonPropertyName("Text")]
        public string messageText { get; set; }
    }
}
