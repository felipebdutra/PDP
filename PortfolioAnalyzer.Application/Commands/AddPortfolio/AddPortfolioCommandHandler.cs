using MediatR;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;

namespace PortfolioAnalyzer.Application.Commands.AddPortfolio
{
    public class AddPortfolioCommandHandler : IRequestHandler<AddPortfolioCommand>
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly AddPortfolioValidator _validator;

        public AddPortfolioCommandHandler(IPortfolioRepository PortfolioRepository, AddPortfolioValidator validator)
        {
            _portfolioRepository = PortfolioRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(AddPortfolioCommand command, CancellationToken cancellationToken)
        {
            bool isValid = await _validator.IsValid(command);

            if (!isValid) throw new ArgumentException();

            var Portfolio = new Portfolio()
            {
                Broker = command.Broker,
                Positions = command.Positions,
            };

            await _portfolioRepository.InsertAsync(Portfolio);
            return new Unit();
        }

    }
}
