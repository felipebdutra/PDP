using Microsoft.AspNetCore.Mvc;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;

namespace PortfolioAnalyzer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        public IPortfolioService _PortfolioService { get; set; }
        public IPortfolioRepository _PortfolioRepository { get; set; }

        public PortfolioController(IPortfolioService PortfolioService, IPortfolioRepository PortfolioRepository)
        {
            _PortfolioService = PortfolioService;
            _PortfolioRepository = PortfolioRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Portfolio>> GetAll() => await _PortfolioRepository.GetAllPortoliosAsync();
    }
}
