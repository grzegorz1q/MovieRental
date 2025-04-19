using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
