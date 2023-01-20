using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using PortfolioAnalyzer.Infrastructure.Database;
using PortfolioAnalyzer.Infrastructure.Integration.Api;
using PortfolioAnalyzer.Infrastructure.Integration.Api.Alphavantage;
using PortfolioAnalyzer.Infrastructure.Integration.Api.CurrencyApi;
using PortfolioAnalyzer.Infrastructure.Loggin;
using PortfolioAnalyzer.Infrastructure.Repository;
using PortfolioAnalyzer.Repository.Bank;
using PortfolioAnalyzer.Repository.Portfolio;
using PortfolioAnalyzer.Services;
using PortfolioAnalyzer.Services.Bank;
using PortfolioAnalyzer.Services.Facade;
using PortfolioAnalyzer.Services.History;
using PortfolioAnalyzer.Services.Portfolio;
using MediatR;
using Queries = PortfolioAnalyzer.Application.Queries;
using PortfolioAnalyzer.Application.Queries.GetAllBanks;
using PortfolioAnalyzer.Application.Commands.AddNewBank;

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
       .AddServices(services)
       .AddMediatRServices(services);
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

        service.AddScoped<IMongoDatabase>(d => portfolioDb.GetDatabase(_context.Configuration, 
                            RepositoryConstants.MongoDb.Database.PortfolioAnalyzer.Name)) ;

        return this;
    }

    private Startup AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IPortfolioRepository, PortfolioRepository>();
        service.AddScoped<IStockPricesHistoryRepository, StockPricesHistoryRepository>();
        service.AddScoped<IBankRepository, BankRepository>();

        return this;
    }

    private Startup AddServices(IServiceCollection service)
    {
        service.AddScoped<IPortfolioService, PortfolioService>();
        service.AddScoped<IPortfolioCalculatorService, PortfolioCalculatorService>();
        service.AddScoped<IStockPriceHistoryService, StockPriceHistoryService>();
        service.AddSingleton<ICurrencyConvertionService, CurrencyConvertionService>();
        service.AddScoped<IBankService, BankService>();
        service.AddTransient<IUpdateStockPriceHistoryService, UpdateStockPriceHistoryService>();

        service.AddTransient<IPortfolioFacade, PortfolioFacade>();

        service.AddTransient<LogBuilder>();
        return this;
    }

    private Startup AddMediatRServices(IServiceCollection services)
    {
        services.AddMediatR(typeof(Queries.GetAllBanks.GetAllBanksNameQuery).Assembly);
        services.AddScoped<GetAllBanksNameQueryHandler>();
        services.AddScoped<AddNewBankCommandHandler>();
        services.AddScoped<AddNewBankValidator>();
        //services.AddScoped(typeof(IBankRepository), typeof(BankRepository));



        return this;
    }
}
