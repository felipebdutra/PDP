using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using PortfolioAnalyzer.Infrastructure.Logging;
using PortfolioAnalyzer.Services.Facade;

var provider = Config();

var database = provider.GetRequiredService<IMongoDatabase>();
var portfolioFacade = provider.GetRequiredService<IPortfolioFacade>();

await GenerateReport(portfolioFacade, database);

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

static async Task GenerateReport(IPortfolioFacade portfolioFacade, IMongoDatabase database)
{
    var consoleLogBuilder = new LogBuilder(new ConsoleLogStrategy());
    var dbLogBuilder = new LogBuilder(new DatabaseLogStrategy(database));

    try
    {
        string logStartExecution = "Syncing latest stock close prices, it might take a few minutes";
        consoleLogBuilder.WriteLog(logStartExecution);
        dbLogBuilder.WriteLog(logStartExecution);

        await portfolioFacade.LoadDataAsync();

        string logLoadData = "Syncronization data loaded successfuly";
        consoleLogBuilder.WriteLog(logLoadData);
        dbLogBuilder.WriteLog(logLoadData);

        var info = await portfolioFacade.ProcessDataAsync();

        string logFinishExecution = $@"Sync finished  
        USD Cash : {info.TotalCash}, Stock : {info.TotalPortfolio},  Total : {info.Total} .";

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