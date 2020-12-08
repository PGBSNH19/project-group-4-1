using System;
using System.Net.Http;
using System.Text.Json;
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
            var response = await Client.GetAsync("graphql?query={messages{id,text}}");
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var messageList = await JsonSerializer.DeserializeAsync<Message>(responseStream);

            string message = messageList.Text;


            return message;
        }


        public int id;

        public string text;
    }

    public class Message
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
