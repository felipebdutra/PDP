using MongoDB.Driver;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Bank;
using PortfolioAnalyzer.Repository.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Commands.UpdateBankAccountAmount
{
    public class UpdateBankAccountAmountValidator
    {
        private readonly IBankRepository _bankRepository;

        public UpdateBankAccountAmountValidator(IBankRepository bankRepository)
        {
        }

        public async Task<bool> IsValid(UpdateBankAccountAmountCommand command)
        {

            var filter = Builders<Bank>.Filter.Where(d => d.Name == command.BankName);

            var result = await _bankRepository.FindAsync(filter);

            if (result == null || !result.Any())
                throw new ArgumentException($"Bank {command.BankName} is invalid.");

            return true;
        }
    }
}
