using MediatR;
using MongoDB.Driver;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;

namespace PortfolioAnalyzer.Application.Queries.GetPortfolios
{
    public class GetPortfolioQueryHandler : IRequestHandler<GetPortfolioQuery, IEnumerable<PortfolioDto>>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        public GetPortfolioQueryHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<IEnumerable<PortfolioDto>> Handle(GetPortfolioQuery getPortfolio, CancellationToken cancellationToken)
        {
            return await _portfolioRepository.GetAllPortoliosAsync();

        }
    }
}
