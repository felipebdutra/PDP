using MediatR;
using MongoDB.Driver;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;

namespace PortfolioAnalyzer.Application.Commands.DeletePortfolio
{
    public class DeletePortfolioCommandHandler : IRequestHandler<DeletePortfolioCommand>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly DeletePortfolioValidator _validator;

        public DeletePortfolioCommandHandler(IPortfolioRepository PortfolioRepository, DeletePortfolioValidator validator)
        {
            _portfolioRepository = PortfolioRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(DeletePortfolioCommand command, CancellationToken cancellationToken)
        {
            bool isValid = await _validator.IsValid(command);

            if (!isValid) return new Unit();

            var filter = Builders<Portfolio>.Filter.Where(f => f.Broker == command.Broker);

            await _portfolioRepository.DeleteAsync(filter);
            return new Unit();
        }

    }
}
