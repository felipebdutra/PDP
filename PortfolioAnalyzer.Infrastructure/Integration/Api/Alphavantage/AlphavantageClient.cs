using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace PortfolioAnalyzer.Infrastructure.Integration.Api.AlphaVantage;

public class AlphaVantageClient : IStockMarketApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    public int ChunkSize { get => AlphaVantageConsts.RequestChunkSize; }
    public int AuthorizationDelay { get => AlphaVantageConsts.AuthorizationDelay; }
    public bool RequiresAuthorization { get => true; }

    public AlphaVantageClient(HttpClient httpClient, IConfiguration configRoot)
    {
        _httpClient = httpClient;
        _apiKey = configRoot["AlphaVantageApiKey"];
    }

    public async Task<decimal> GetClosePriceAsync(string ticker)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={ticker}&apikey={_apiKey}"
        );

        var response = await _httpClient.SendAsync(request);

        var stringJson = await response.Content.ReadAsStringAsync();

        var val = JObject.Parse(stringJson);

        var date = DateTime.Now;

        while (val["Time Series (Daily)"][date.ToString("yyyy-MM-dd")] == null)
        {
            date = date.AddDays(-1);
        }

        decimal latestPrice = decimal.Parse(
            val["Time Series (Daily)"][date.ToString("yyyy-MM-dd")]["4. close"].ToString()
        );

        return latestPrice;
    }

}
