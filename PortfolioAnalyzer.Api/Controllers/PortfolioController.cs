using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortfolioAnalyzer.Application.Commands.AddPortfolio;
using PortfolioAnalyzer.Application.Commands.DeletePortfolio;
using PortfolioAnalyzer.Application.Commands.UpdatePortfolio;
using PortfolioAnalyzer.Application.Queries.GetPortfolios;
using PortfolioAnalyzer.Repository.Portfolio;

namespace PortfolioAnalyzer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private IPortfolioService _PortfolioService { get; set; }
        private IPortfolioRepository _PortfolioRepository { get; set; }
        private readonly IMediator _mediator;


        public PortfolioController(IPortfolioService PortfolioService, IPortfolioRepository PortfolioRepository, IMediator mediator)
        {
            _PortfolioService = PortfolioService;
            _PortfolioRepository = PortfolioRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<PortfolioDto>> GetAll()
        {
            return await _mediator.Send(new GetPortfolioQuery());
        }

        [HttpPost]
        public async Task<Unit> AddPortfolio(AddPortfolioCommand addPortfolioDto)
        {
            return await _mediator.Send(addPortfolioDto);
        }

        [HttpDelete]
        public async Task<Unit> DeletePortfolio(string broker)
        {
            return await _mediator.Send(new DeletePortfolioCommand() { Broker = broker});
        }


        [HttpPatch]
        public async Task<Unit> UpdatePortfolio(UpdatePortfolioCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
