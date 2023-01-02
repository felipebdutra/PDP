using System.Collections.Concurrent;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace PortfolioAnalyzer.Infrastructure.Integration.Api.CurrencyApi
{
    public class CurrencyApiClient : ICurrencyApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public CurrencyApiClient(HttpClient httpClient, IConfiguration configRoot)
        {
            _httpClient = httpClient;
            _apiKey = configRoot["CurrencyApiApiKey"];
        }

        public async Task<Dictionary<string, decimal>> LoadCurrenciesAsync()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"/v3/latest?apikey={_apiKey}"
            );

            var response = await _httpClient.SendAsync(request);

            var dictionary = new Dictionary<string, decimal>();

            var stringJson = await response.Content.ReadAsStringAsync();

            var val = JObject.Parse(stringJson);

            foreach (var currency in val["data"]!.ToList())
            {
                var currentCurrency = currency.First;

                if (decimal.TryParse(currentCurrency["value"]!.ToString(), out decimal value))
                {
                    dictionary.Add(currentCurrency["code"]!.ToString(), value);
                }
            }

            return dictionary;
        }
    }
}
