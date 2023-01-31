using MongoDB.Driver;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Commands.DeletePortfolio
{
    public class DeletePortfolioValidator
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public DeletePortfolioValidator(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<bool> IsValid(DeletePortfolioCommand command)
        {
            var filter = Builders<Portfolio>.Filter.Where(d => d.Broker == command.Broker);

            var result = await _portfolioRepository.FindAsync(filter);

            if (result == null || !result.Any())
                throw new ArgumentException($"Broker {command.Broker} is invalid.");

            return true;
        }
    }
}
