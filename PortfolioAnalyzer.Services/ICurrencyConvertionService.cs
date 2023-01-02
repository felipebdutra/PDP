using PortfolioAnalyzer.Core.BankAggregate;

namespace PortfolioAnalyzer.Services
{
    public interface ICurrencyConvertionService
    {
        decimal this[string currencyName] { get; }
        Task<Dictionary<string, decimal>> LoadCurrenciesAsync();
        decimal ToTotalDollar(IEnumerable<BankAccount> accounts);
        bool IsCurrencySupported(string currency);
        decimal ConvertTo(decimal value, string currency);
    }
}