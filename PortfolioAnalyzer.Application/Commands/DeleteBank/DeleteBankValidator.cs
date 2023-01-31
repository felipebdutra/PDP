using MongoDB.Driver;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;

namespace PortfolioAnalyzer.Application.Commands.DeleteBank
{
    public class DeleteBankValidator
    {
        private readonly IBankRepository _bankRepository;

        public DeleteBankValidator(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<bool> IsValid(DeleteBankCommand command)
        {
            var filter = Builders<Bank>.Filter.Where(d => d.Name == command.Name);

            var result = await _bankRepository.FindAsync(filter);

            if (result == null || !result.Any())
                throw new ArgumentException($"Bank {command.Name} is invalid.");

            return true;
        }
    }
}
