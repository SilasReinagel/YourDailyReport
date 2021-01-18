using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public sealed class CoinApi 
{
    private static readonly string _baseUrl = "https://rest.coinapi.io/";
    private readonly string _apiKey;
    private readonly HttpClient _client;

    public CoinApi()
        : this(new EnvironmentVariable("CoinApiKey")) {}
    
    public CoinApi(string apiKey)
    {
        _apiKey = apiKey;
        _client = new HttpClient();
    }

    public async Task<decimal> GetSymbolUsdPrice(string symbol)
    {
        var url = Path.Combine(_baseUrl, $"v1/exchangerate/{symbol}/USD");
        var message = new HttpRequestMessage(HttpMethod.Get, url);
        message.Headers.Add("X-CoinAPI-Key", _apiKey);
        
        var resp = await _client.SendAsync(message);
        if (!resp.IsSuccessStatusCode)
            throw new Exception($"Request Failed: {resp.StatusCode}");
        var content = await resp.Content.ReadAsStringAsync();
        var body = JsonSerializer.Deserialize<SymbolPriceResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return body.Rate;
    }

    private class SymbolPriceResponse
    {
        public decimal Rate { get; set; }
    }
}