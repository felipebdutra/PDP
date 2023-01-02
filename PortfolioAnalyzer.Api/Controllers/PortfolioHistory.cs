using Microsoft.AspNetCore.Mvc;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;
using PortfolioAnalyzer.Services.Facade;

namespace PortfolioAnalyzer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioHistoryController : ControllerBase
    {
        public IPortfolioFacade _facade { get; set; }

        public PortfolioHistoryController(IPortfolioFacade portfolioFacade)
        {
            _facade = portfolioFacade;
        }

        [HttpGet]
        public async Task<ActionResult<PortfolioInfo>> GetLatestReport()
        {
            await _facade.LoadStoredDataAsync();
            var info = await _facade.ProcessDataAsync();

            return Ok(info);
        }
    }
}
