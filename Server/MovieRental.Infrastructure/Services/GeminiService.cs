using Microsoft.Extensions.Options;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using MovieRental.Infrastructure.Helpers;
using System.Text;
using System.Text.Json;

namespace MovieRental.Infrastructure.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiApiOptions _geminiApiOptions;
        public GeminiService(HttpClient httpClient, IOptions<GeminiApiOptions> geminiApiOptions)
        {
            _httpClient = httpClient;
            _geminiApiOptions = geminiApiOptions.Value;
        }

        public async Task<string> AskGemini(string prompt)
        {
            var body = new
                {
                    contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_geminiApiOptions.BaseUrl}?key={_geminiApiOptions.ApiKey}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini API error: {response.StatusCode}.");
            }

            var responseJson = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseJson);
            var answer = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();
            if(answer == null) 
                throw new Exception("No response");

            return CleanGeminiResponse(answer);
        }

        public async Task<IEnumerable<GeminiMovie>> GetMoviesFromGemini(string prompt)
        {
            var raw = await AskGemini(prompt);

            var movies = JsonSerializer.Deserialize<IEnumerable<GeminiMovie>>(raw);

            return movies ?? [];
        }
        private string CleanGeminiResponse(string raw)
        {
            if (raw.StartsWith("```"))
            {
                var firstLineBreak = raw.IndexOf('\n');
                var lastBackticks = raw.LastIndexOf("```");

                if (firstLineBreak != -1 && lastBackticks != -1 && lastBackticks > firstLineBreak)
                {
                    return raw.Substring(firstLineBreak + 1, lastBackticks - firstLineBreak - 1).Trim();
                }
            }

            return raw.Trim();
        }
    }
}
