using System.Text.Json.Serialization;

namespace BlazorApp_Frontend.Data
{
    public class Message
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
