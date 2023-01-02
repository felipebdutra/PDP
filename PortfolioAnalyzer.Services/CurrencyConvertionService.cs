using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Infrastructure.Integration.Api.CurrencyApi;

namespace PortfolioAnalyzer.Services
{
    public class CurrencyConvertionService : ICurrencyConvertionService
    {
        private readonly ICurrencyApiClient _currencyApiClient;

        private Dictionary<string, decimal> _currency { get; set; }

        public decimal this[string currencyName]
        {
            get { return _currency[currencyName]; }
        }

        public CurrencyConvertionService(ICurrencyApiClient currencyApiClient)
        {
            _currencyApiClient = currencyApiClient;
        }

        public async Task<Dictionary<string, decimal>> LoadCurrenciesAsync()
        {
            _currency ??= await _currencyApiClient.LoadCurrenciesAsync();

            return _currency;
        }

        public decimal ToTotalDollar(IEnumerable<BankAccount> accounts)
        {
            decimal totalDollar = 0;
            foreach (var account in accounts)
            {
                totalDollar += account.Amount / _currency[account.Currency];
            }

            return totalDollar;
        }
    }
}
