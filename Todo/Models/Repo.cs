using System.Text.Json.Serialization;

namespace TodoApi.Models
{
    public class Repo
    {
        public Repo(string name)
        {
            Name = name;
        }
        [JsonPropertyName("name")] 
        public string Name { get; set; }
    }
}