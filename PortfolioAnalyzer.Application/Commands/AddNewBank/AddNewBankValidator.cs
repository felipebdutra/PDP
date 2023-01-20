using MongoDB.Driver;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Commands.AddNewBank
{
    public class AddNewBankValidator
    {
        private readonly IBankRepository _bankRepository;

        public AddNewBankValidator(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<bool> IsValid(AddNewBankCommand command)
        {
            var filter = Builders<Bank>.Filter.Where(d => d.Name == command.Name);

            var result = await _bankRepository.FindAsync(filter);

            if (result != null && result.Any())
                return false;

            return true;
        }
    }
}
