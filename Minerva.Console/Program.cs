using System;
using System.Text.Json;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    static async Task QueryOllamaStreamingAsync(string model, string prompt)
    {
        using HttpClient client = new HttpClient();
        var requestBody = new
        {
            model = model,
            prompt = prompt,
            stream = true // Wichtig für Streaming
        };

        string json = JsonSerializer.Serialize(requestBody);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        using HttpResponseMessage response = await client.PostAsync("http://localhost:11434/api/generate", content);
        response.EnsureSuccessStatusCode();

        using Stream stream = await response.Content.ReadAsStreamAsync();
        using StreamReader reader = new StreamReader(stream);

        while (!reader.EndOfStream)
        {
            string? line = await reader.ReadLineAsync();
            if (!string.IsNullOrEmpty(line))
            {
                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(line);
                Console.Write(jsonResponse.GetProperty("response").GetString()); // Direkt ausgeben
            }
        }
        Console.WriteLine("\n\nFertig!");
    }

    static async Task<string> RefineEntitiesWithMistral(string text, string entities)
    {
        using HttpClient client = new HttpClient();
        string prompt = $"Hier ist eine Liste erkannter Entitäten:\n\n{entities}\n\nFalls nötig, ergänze oder korrigiere sie.";

        var requestBody = new
        {
            model = "mistral",
            prompt = prompt
        };

        string json = JsonSerializer.Serialize(requestBody);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:11434/api/generate")
        {
            Content = content
        };

        HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);

        return jsonResponse.GetProperty("response").GetString();
    }
}