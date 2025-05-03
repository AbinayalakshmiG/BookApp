using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookApp.Services;

//public class GoogleBooksService : IGoogleBooksService
//{
//    private readonly HttpClient _http;
//    private readonly string? _apiKey;

//    public GoogleBooksService(HttpClient http, IConfiguration cfg)
//    {
//        _http = http;
//        _apiKey = cfg.GetSection("GoogleBooks")["ApiKey"];
//    }

//    public async Task<GoogleBooksResponse?> SearchAsync(string query)
//    {
//        if (string.IsNullOrWhiteSpace(query))
//            throw new ArgumentException("Query must not be empty", nameof(query));
//        var relative = $"volumes?q={Uri.EscapeDataString(query)}";
//        if (!string.IsNullOrEmpty(_apiKey))
//            relative += $"&key={_apiKey}";

//        return await _http.GetFromJsonAsync<GoogleBooksResponse>(relative);
//    }
//}
public class GoogleBooksService : IGoogleBooksService
{
    private readonly HttpClient _http;
    private readonly string? _apiKey;
    private readonly ILogger<GoogleBooksService> _log;

    public GoogleBooksService(
        HttpClient http,
        IConfiguration cfg,
        ILogger<GoogleBooksService> log)
    {
        _http = http;
        _apiKey = cfg["GoogleBooks:ApiKey"];
        _log = log;
    }

    public async Task<GoogleBooksResponse?> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return null;

        var relative = $"volumes?q={Uri.EscapeDataString(query)}" +
                       (!string.IsNullOrEmpty(_apiKey) ? $"&key={_apiKey}" : string.Empty);

        _log.LogInformation("Google Books API call: {Url}",
            new Uri(_http.BaseAddress!, relative));

        var resp = await _http.GetAsync(relative);
        if (!resp.IsSuccessStatusCode) return null;

        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var json = await resp.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GoogleBooksResponse>(json, opts);
    }
}