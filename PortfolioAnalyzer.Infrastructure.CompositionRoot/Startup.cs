using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using PortfolioAnalyzer.Application.Commands.AddBank;
using PortfolioAnalyzer.Application.Commands.AddBankAccount;
using PortfolioAnalyzer.Application.Commands.AddPortfolio;
using PortfolioAnalyzer.Application.Commands.DeleteBank;
using PortfolioAnalyzer.Application.Commands.DeletePortfolio;
using PortfolioAnalyzer.Application.Commands.UpdateBankAccountAmount;
using PortfolioAnalyzer.Application.Commands.UpdatePortfolio;
using PortfolioAnalyzer.Application.Queries.GetPortfolios;
using PortfolioAnalyzer.Infrastructure.Database;
using PortfolioAnalyzer.Infrastructure.Integration.Api;
using PortfolioAnalyzer.Infrastructure.Integration.Api.AlphaVantage;
using PortfolioAnalyzer.Infrastructure.Integration.Api.CurrencyApi;
using PortfolioAnalyzer.Infrastructure.Logging;
using PortfolioAnalyzer.Infrastructure.Repository;
using PortfolioAnalyzer.Repository.Bank;
using PortfolioAnalyzer.Repository.Portfolio;
using PortfolioAnalyzer.Services;
using PortfolioAnalyzer.Services.Bank;
using PortfolioAnalyzer.Services.Facade;
using PortfolioAnalyzer.Services.History;
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
       .AddServices(services)
       .AddMediatRServices(services);
    }

    private Startup AddHttpClient(IServiceCollection service)
    {
        service.AddHttpClient<IStockMarketApiClient, AlphaVantageClient>(
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
        service.AddScoped<PortfolioAnalyzer.Repository.Portfolio.IPortfolioRepository, PortfolioRepository>();
        service.AddScoped<IStockPricesHistoryRepository, StockPricesHistoryRepository>();
        service.AddScoped<PortfolioAnalyzer.Repository.Bank.IBankRepository, BankRepository>();

        return this;
    }

    private Startup AddServices(IServiceCollection service)
    {
        service.AddScoped<IPortfolioService, PortfolioService>();
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
        services.AddMediatR(typeof(GetPortfolioQuery).Assembly);
        services.AddScoped<GetPortfolioQueryHandler>();

        services.AddScoped<AddBankCommandHandler>();
        services.AddScoped<AddBankValidator>();

        services.AddScoped<AddBankAccontCommandHandler>();
        services.AddScoped<AddBankAccountValidator>();

        services.AddScoped<DeleteBankCommandHandler>();
        services.AddScoped<DeleteBankValidator>();

        services.AddScoped<AddPortfolioCommandHandler>();
        services.AddScoped<AddPortfolioValidator>();

        services.AddScoped<DeletePortfolioCommandHandler>();
        services.AddScoped<DeletePortfolioValidator>();

        services.AddScoped<UpdatePortfolioCommandHandler>();
        services.AddScoped<UpdatePortfolioValidator>();  
        
        services.AddScoped<UpdateBankAccountAmountCommandHandler>();
        services.AddScoped<UpdateBankAccountAmountValidator>();

        return this;
    }
}
