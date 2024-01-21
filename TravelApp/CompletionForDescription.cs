using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace TravelApp
{
    public class CompletionForDescription
    {
        static async Task Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                var data = new { Description = "It is a great place" };
                var response = await client.PostAsJsonAsync("http://localhost:5000/get_chat_completion", data);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var result = await JsonSerializer.DeserializeAsync<ChatCompletionResponse>(stream);
                    Console.WriteLine($"Chat Completion: {result.Completion}");
                }
            }
        }

        public class ChatCompletionResponse
        {
            public string Completion { get; set; }
        }
    }
}
