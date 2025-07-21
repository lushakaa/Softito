using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace YuvamHazir.API.Services
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey     = configuration["OpenAI:ApiKey"]!;
        }

        public async Task<string> EnhanceDescriptionAsync(string rawDescription)
        {
            Console.WriteLine("💬 EnhanceDescriptionAsync çağrıldı.");
            Console.WriteLine("✍️ Gelen açıklama: " + rawDescription);

            try
            {
                var payload = new
                {
                    // model adını "gpt-3.5-turbo" olarak bırakıyoruz
                    model      = "gpt-3.5-turbo",
                    messages   = new[]
                    {
                        new { role = "system", content = "Sen bir hayvan açıklama düzenleyicisisin. Kullanıcının yazdığı açıklamayı daha etkileyici ve açıklayıcı hale getir." },
                        new { role = "user",   content = rawDescription }
                    },
                    max_tokens = 300
                };

                var jsonBody = JsonSerializer.Serialize(payload);
                using var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    "https://openrouter.ai/v1/chat/completions"
                );

                // Authorization başlığını doğru şekilde ekliyoruz
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                Console.WriteLine("🚀 OpenRouter isteği gönderiliyor...");
                using var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                Console.WriteLine("✅ OpenRouter yanıtı alındı: " + responseJson);

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
                // Bir sorun olursa orijinal açıklamayı döner
                return rawDescription;
            }
        }
    }
}
