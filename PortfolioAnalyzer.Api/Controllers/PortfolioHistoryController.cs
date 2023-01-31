using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PortfolioAnalyzer.Infrastructure.Logging;
using PortfolioAnalyzer.Services;
using PortfolioAnalyzer.Services.Facade;

namespace PortfolioAnalyzer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioHistoryController : ControllerBase
    {
        private readonly IPortfolioFacade _facade;
        private readonly IMongoDatabase _database;
        private readonly ICurrencyConvertionService _currencyService;
        private readonly LogBuilder _log;

        public PortfolioHistoryController(IPortfolioFacade portfolioFacade, IMongoDatabase database, ICurrencyConvertionService currencyService, LogBuilder log)
        {
            _facade = portfolioFacade;
            _log = log;
            _database = database;
            _currencyService = currencyService;
        }

        [HttpGet]
        [Route("GetLatestInfo")]
        public async Task<ActionResult<PortfolioInfoDto>> GetLatestReport(string currency = "PLN")
        {
            try
            {
               await _facade.LoadStoredDataAsync();

                if (!_currencyService.IsCurrencySupported(currency))
                {
                    throw new ArgumentException($"Currency {currency} is invalid or not supported.");
                }

                var info = await _facade.ProcessDataAsync();

                if (currency != "USD")
                {
                    await _currencyService.LoadCurrenciesAsync();

                    info.Total = _currencyService.ConvertTo(info.Total, currency);
                    info.TotalCash = _currencyService.ConvertTo(info.TotalCash, currency);
                    info.TotalPortfolio = _currencyService.ConvertTo(info.TotalPortfolio, currency);
                }

                return Ok(info);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _log.WriteLog(new DatabaseLogStrategy(_database), $"{e.Message} , {e.InnerException?.Message}");
                throw;
            }
        }
    }
}
