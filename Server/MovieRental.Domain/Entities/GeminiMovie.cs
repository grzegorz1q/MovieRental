using System.Text.Json.Serialization;

namespace MovieRental.Domain.Entities
{
    public class GeminiMovie
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("year")]
        public int Year { get; set; }
        [JsonPropertyName("rating")]
        public double Rating { get; set; }
    }
}
