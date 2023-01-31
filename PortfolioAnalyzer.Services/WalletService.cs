using System.Runtime.ConstrainedExecution;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Services;

namespace PortfolioAnalyzer.Services
{
    public class WalletService
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ICurrencyConvertionService _currencyService;

        public WalletService(
            IPortfolioService portfolioService,
            ICurrencyConvertionService currencyService
        )
        {
            _portfolioService = portfolioService;
            _currencyService = currencyService;
        }

        public List<Core.PortfolioAggregate.Portfolio> Portfolios { get; set; }

        public async Task<decimal> TotalPortfolioValue(List<FinancialInstrument> instrumentsPrice)
        {
            var portfolios = await _portfolioService.GetPortfoliosAsync();

            portfolios.ForEach(f => f.SetPrices(instrumentsPrice));

            return portfolios.Sum(s => s.GetTotal());
        }
    }
}