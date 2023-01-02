using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortfolioAnalyzer.Console;
using PortfolioAnalyzer.Services;
using Microsoft.Extensions.Configuration;
using PortfolioAnalyzer.Services.Bank;
using PortfolioAnalyzer.Infrastructure.Loggin;

var provider = Config();

var portfolioService = provider.GetRequiredService<IPortfolioService>();
var stockPriceHistoryService = provider.GetRequiredService<IStockPriceHistoryService>();
var currencyService = provider.GetRequiredService<ICurrencyConvertionService>();
var bankService = provider.GetRequiredService<IBankService>();


await GenerateReport(portfolioService, stockPriceHistoryService, currencyService, bankService);

IServiceProvider Config()
{
    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureHostConfiguration(x => x.AddJsonFile("appsettings.json"))
        .ConfigureHostConfiguration(x => x.AddJsonFile("appbasesettings.json"))
        .ConfigureServices(
            (context, services) =>
            {
                new Startup(context).ConfigureServices(services);
            }
        )
        .Build();

    IServiceScope serviceScope = host.Services.CreateScope();
    return serviceScope.ServiceProvider;
}

static async Task GenerateReport(IPortfolioService portfolioService, 
    IStockPriceHistoryService stockPriceHistoryService, 
    ICurrencyConvertionService currencyService, 
    IBankService bankService)
{
    var logBuilder = new LogBuilder(new ConsoleLogStrategy());
    logBuilder.WriteLog("Syncing latest stock close prices, it might take a few minutes");

    var stockHistory = new UpdateStockPriceHistory(portfolioService, stockPriceHistoryService);

    var getTotalCash = bankService.GetTotalCashValueAsync();
    var getLatestPrices = stockHistory.GetInstrumentCurrentPricesAsync();

    await Task.WhenAll(getTotalCash, getLatestPrices, currencyService.LoadCurrenciesAsync());

    logBuilder.WriteLog("Syncing latest stock close prices, it might take a few minutes");

    var wallet = new WalletService(portfolioService, currencyService);
    var total = await wallet.TotalPortfolioValue(getLatestPrices.Result.Instruments);
    var totalDollarCash = currencyService.ToTotalDollar(getTotalCash.Result);

    logBuilder.WriteLog($@"Total portfolio value : PLN : {currencyService["PLN"] * total} USD : {total} 
                     Total cash value : PLN : {currencyService["PLN"] * totalDollarCash} USD {totalDollarCash} 
                     Total Dollar: {string.Format("{0:N}", (total + totalDollarCash).ToString())}
                     Total PLN : {string.Format("{0:N}", (currencyService["PLN"] * (total + totalDollarCash)).ToString())}");

}