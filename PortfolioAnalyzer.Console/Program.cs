using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using PortfolioAnalyzer.Console;
using PortfolioAnalyzer.Infrastructure.Loggin;
using PortfolioAnalyzer.Services;
using PortfolioAnalyzer.Services.Bank;
using PortfolioAnalyzer.Services.History;

var provider = Config();

var portfolioService = provider.GetRequiredService<IPortfolioService>();
var stockPriceHistoryService = provider.GetRequiredService<IStockPriceHistoryService>();
var currencyService = provider.GetRequiredService<ICurrencyConvertionService>();
var bankService = provider.GetRequiredService<IBankService>();
var updateStockPriceHistoryService = provider.GetRequiredService<IUpdateStockPriceHistoryService>();

var database = provider.GetRequiredService<IMongoDatabase>();

await GenerateReport(portfolioService, stockPriceHistoryService, currencyService, bankService, database, updateStockPriceHistoryService);

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
    IBankService bankService,
    IMongoDatabase database,
    IUpdateStockPriceHistoryService updateStockPriceHistoryService)
{

    var facade = new PortfolioFacade(bankService,
        currencyService,
        updateStockPriceHistoryService,
        portfolioService);

    var consoleLogBuilder = new LogBuilder(new ConsoleLogStrategy());
    var dbLogBuilder = new LogBuilder(new DatabaseLogStrategy(database));

    try
    {
        string logStartExecution = "Syncing latest stock close prices, it might take a few minutes";
        consoleLogBuilder.WriteLog(logStartExecution);
        dbLogBuilder.WriteLog(logStartExecution);

        await facade.LoadDataAsync();

        string logLoadData = "Syncronization data loaded successfuly";
        consoleLogBuilder.WriteLog(logLoadData);
        dbLogBuilder.WriteLog(logLoadData);

        var info = await facade.ProcessDataAsync();

        string logFinishExecution = $@"Sync finished  
        USD Cash : {info.TotalCashUSD}, Stock : {info.TotalPortfolioUSD},  Total : {info.TotalUSD} .
        PLN Cash : {currencyService["PLN"] *  info.TotalCashUSD}, Stock : {currencyService["PLN"] * info.TotalPortfolioUSD}, Total : {currencyService["PLN"] * info.TotalUSD}";

        consoleLogBuilder.WriteLog(logFinishExecution);
        dbLogBuilder.WriteLog(logFinishExecution);
    }
    catch (Exception e)
    {
        string applicationFailedLog = $"Error during syncronization, application failed with the message {e.Message} - {e.InnerException?.Message}";

        consoleLogBuilder.WriteLog(applicationFailedLog);
        dbLogBuilder.WriteLog(applicationFailedLog);
    }
}