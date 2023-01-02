using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Services;
using PortfolioAnalyzer.Services.Bank;
using PortfolioAnalyzer.Services.History;

namespace PortfolioAnalyzer.Services.Facade
{
    public class PortfolioFacade : IPortfolioFacade
    {
        private readonly IBankService _bankService;
        private readonly ICurrencyConvertionService _currencyService;
        private readonly IUpdateStockPriceHistoryService _updateStockPriceHistoryService;
        private readonly IPortfolioService _portfolioService;

        private List<FinancialInstrument> _instruments;
        private IList<BankAccount> _totalCash;

        public PortfolioFacade(IBankService bankService,
            ICurrencyConvertionService currencyService,
            IUpdateStockPriceHistoryService updateStockPriceHistoryService,
            IPortfolioService portfolioService)
        {
            _bankService = bankService;
            _currencyService = currencyService;
            _updateStockPriceHistoryService = updateStockPriceHistoryService;
            _portfolioService = portfolioService;
        }

        public async Task LoadDataAsync()
        {
            var getTotalCash = _bankService.GetTotalCashValueAsync();
            var getLatestPrices = _updateStockPriceHistoryService.GetInstrumentCurrentPricesAsync();

            await Task.WhenAll(getTotalCash, getLatestPrices, _currencyService.LoadCurrenciesAsync());

            var stockHistory = await getLatestPrices;
            var totalCash = await getTotalCash;

            _instruments = stockHistory.Instruments;
            _totalCash = totalCash;
        }

        public async Task LoadStoredDataAsync()
        {
            var getTotalCash = _bankService.GetTotalCashValueAsync();
            var getLatestPrices = _updateStockPriceHistoryService.GetInstrumentStoredPricesAsync();

            await Task.WhenAll(getTotalCash, getLatestPrices, _currencyService.LoadCurrenciesAsync());

            _instruments = getLatestPrices.Result.Instruments;
            _totalCash = getTotalCash.Result;
        }

        public async Task<PortfolioInfo> ProcessDataAsync()
        {
            var wallet = new WalletService(_portfolioService, _currencyService);
            var total = await wallet.TotalPortfolioValue(_instruments);
            var totalDollarCash = _currencyService.ToTotalDollar(_totalCash);

            return new PortfolioInfo()
            {
                Total = total + totalDollarCash,
                TotalCash = totalDollarCash,
                TotalPortfolio = total
            };
        }
    }
}
