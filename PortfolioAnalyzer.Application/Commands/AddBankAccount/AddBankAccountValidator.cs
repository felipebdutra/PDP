using MongoDB.Driver;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Commands.AddBankAccount
{
    public class AddBankAccountValidator
    {
        private readonly IBankRepository _bankRepository;

        public AddBankAccountValidator(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<bool> IsValid(AddBankAccountCommand command)
        {
            var filter = Builders<Bank>.Filter.Where(d => d.Name == command.BankName);

            var result = await _bankRepository.FindAsync(filter);

            if (result != null && !result.Any())
                throw new ArgumentException($"Bank {command.BankName} doesn't exist.");

            return true;
        }
    }
}
