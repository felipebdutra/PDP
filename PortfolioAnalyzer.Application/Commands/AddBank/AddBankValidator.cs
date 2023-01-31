using MongoDB.Driver;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp;

namespace PortfolioAnalyzer.Application.Commands.AddBank
{
    public class AddBankValidator
    {
        private readonly IBankRepository _bankRepository;

        public AddBankValidator(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<bool> IsValid(AddBankCommand command)
        {
            var filter = Builders<Bank>.Filter.Where(d => d.Name == command.Name);

            var result = await _bankRepository.FindAsync(filter);

            if (result != null && result.Any())
                throw new ArgumentException($"Bank {command.Name} already registered.");

            return true;
        }
    }
}
