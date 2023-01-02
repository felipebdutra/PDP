using System.Collections.Concurrent;

namespace PortfolioAnalyzer.Infrastructure.Integration.Api.CurrencyApi
{
    public interface ICurrencyApiClient
    {
        Task<Dictionary<string, decimal>> LoadCurrenciesAsync();
    }
}