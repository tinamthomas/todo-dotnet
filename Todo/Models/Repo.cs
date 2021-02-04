using System.Text.Json.Serialization;

namespace TodoApi.Models
{
    public class Repo
    {
        [JsonPropertyName("name")] 
        public string Name { get; set; }
    }
}