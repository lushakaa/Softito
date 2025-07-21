using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace YuvamHazir.API.Services
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenAI:ApiKey"]!;
        }

        public async Task<string> EnhanceDescriptionAsync(string rawDescription)
{
    Console.WriteLine("💬 EnhanceDescriptionAsync çağrıldı.");
    Console.WriteLine("✍️ Gelen açıklama: " + rawDescription);

    try
    {
        var requestBody = new
        {
            model = "gpt-3.5-turbo-1106",
            messages = new[]
            {
                new { role = "system", content = "Sen bir hayvan açıklama düzenleyicisisin. Kullanıcıdan gelen açıklamayı daha etkileyici ve açıklayıcı hale getir." },
                new { role = "user", content = rawDescription }
            },
            max_tokens = 300
        };

        var requestJson = JsonSerializer.Serialize(requestBody);
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
        request.Headers.Add("Authorization", $"Bearer {_apiKey}");
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        Console.WriteLine("🚀 OpenAI isteği gönderiliyor...");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        Console.WriteLine("✅ OpenAI yanıtı alındı: " + responseJson);

        using var doc = JsonDocument.Parse(responseJson);
        var content = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        Console.WriteLine("🎯 Düzenlenmiş açıklama: " + content);

        return content ?? rawDescription;
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Hata oluştu: " + ex.Message);
        return rawDescription;
    }
}

    }
}
