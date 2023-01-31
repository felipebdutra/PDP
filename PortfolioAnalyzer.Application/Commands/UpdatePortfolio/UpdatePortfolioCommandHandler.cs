using MediatR;
using MongoDB.Driver;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;
using ZstdSharp;

namespace PortfolioAnalyzer.Application.Commands.UpdatePortfolio
{
    public class UpdatePortfolioCommandHandler : IRequestHandler<UpdatePortfolioCommand>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly UpdatePortfolioValidator _validator;

        public UpdatePortfolioCommandHandler(IPortfolioRepository PortfolioRepository, UpdatePortfolioValidator validator)
        {
            _portfolioRepository = PortfolioRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdatePortfolioCommand command, CancellationToken cancellationToken)
        {
            bool isValid = await _validator.IsValid(command);

            if (!isValid) throw new ArgumentException();

            var filter = Builders<Portfolio>.Filter.Where(w => w.Broker == command.Broker);

            var updateDefinition = Builders<Portfolio>
                        .Update
                        .AddToSetEach(d => d.Positions, command.Positions)
                        .Set(d => d.Broker, command.Broker);

            var updateOptions = new UpdateOptions();
            updateOptions.IsUpsert = true;

            await _portfolioRepository.UpdateAsync(filter, updateDefinition, updateOptions);
            return new Unit();
        }

    }
}
