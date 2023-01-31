using MongoDB.Driver;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Commands.UpdatePortfolio
{
    public class UpdatePortfolioValidator
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public UpdatePortfolioValidator(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<bool> IsValid(UpdatePortfolioCommand command)
        {
            var filter = Builders<Portfolio>.Filter.Where(d => d.Broker == command.Broker);

            var result = await _portfolioRepository.FindAsync(filter);

            if (result == null || !result.Any())
                throw new ArgumentException($"Broker {command.Broker} is invalid.");

            return true;
        }
    }
}
