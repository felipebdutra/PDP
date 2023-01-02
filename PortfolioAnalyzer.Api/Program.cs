using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

string prePathConfig = builder.Configuration["ASPNETCORE_ENVIRONMENT"].Contains("D") ?
                            $"{Directory.GetCurrentDirectory()}/../PortfolioAnalyzer.Infrastructure.CompositionRoot/" : String.Empty;

builder.Host.ConfigureHostConfiguration(x => x.AddJsonFile(prePathConfig + "appbasesettings.json"))

    .ConfigureServices((context, services) =>
            {
                new Startup(context).ConfigureServices(services);
            })
            ;

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
