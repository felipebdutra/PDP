using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortfolioAnalyzer.Infrastructure.Database;
using PortfolioAnalyzer.Infrastructure.Integration.Api;
using PortfolioAnalyzer.Infrastructure.Integration.Api.Alphavantage;
using PortfolioAnalyzer.Infrastructure.Integration.Api.CurrencyApi;
using PortfolioAnalyzer.Infrastructure.Loggin;
using PortfolioAnalyzer.Repository.Bank;
using PortfolioAnalyzer.Repository.Portfolio;
using PortfolioAnalyzer.Services;
using PortfolioAnalyzer.Services.Bank;
using PortfolioAnalyzer.Services.Portfolio;

public class Startup
{
    public HostBuilderContext _context { get; set; }

    public Startup() { }

    public Startup(HostBuilderContext context)
    {
        _context = context;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        AddHttpClient(services)
       .AddDatabase(services)
       .AddRepositories(services)
       .AddServices(services);
    }

    private Startup AddHttpClient(IServiceCollection service)
    {
        service.AddHttpClient<IStockMarketApiClient, AlphavantageClient>(
            c => c.BaseAddress = new Uri(_context.Configuration["AlphaVantageBaseUrl"])
        );

        service.AddHttpClient<ICurrencyApiClient, CurrencyApiClient>(
            c => c.BaseAddress = new Uri(_context.Configuration["CurrencyApiBaseUrl"])
        );

        return this;
    }

    private Startup AddDatabase(IServiceCollection service)
    {
        var portfolioDb = new PortfolioAnalyzerDb();

        service.AddScoped(d => portfolioDb.GetDatabase(_context.Configuration, 
                            RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Name)) ;

        return this;
    }

    private Startup AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IPortfolioRepository, DatabaseLOgRepository>();
        service.AddScoped<IStockPricesHistoryRepository, StockPricesHistoryRepository>();
        service.AddScoped<IBankRepository, BankRepository>();

        return this;
    }

    private Startup AddServices(IServiceCollection service)
    {
        service.AddScoped<IPortfolioService, PortfolioService>();
        service.AddScoped<IPortfolioCalculatorService, PortfolioCalculatorService>();
        service.AddScoped<IStockPriceHistoryService, StockPriceHistoryService>();
        service.AddScoped<ICurrencyConvertionService, CurrencyConvertionService>();
        service.AddScoped<IBankService, BankService>();

        service.AddTransient<LogBuilder>();
        return this;
    }
}
