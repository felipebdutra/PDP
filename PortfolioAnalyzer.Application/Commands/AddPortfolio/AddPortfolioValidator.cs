using MongoDB.Driver;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Commands.AddPortfolio
{
    public class AddPortfolioValidator
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public AddPortfolioValidator(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<bool> IsValid(AddPortfolioCommand command)
        {
            return true;
        }
    }
}
