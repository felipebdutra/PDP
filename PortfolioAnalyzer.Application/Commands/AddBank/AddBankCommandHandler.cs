using MediatR;
using MongoDB.Driver;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;

namespace PortfolioAnalyzer.Application.Commands.AddBank
{
    public class AddBankCommandHandler : IRequestHandler<AddBankCommand>
    {
        private readonly IBankRepository _bankRepository;
        private readonly AddBankValidator _validator;

        public AddBankCommandHandler(IBankRepository bankRepository, AddBankValidator validator)
        {
            _bankRepository = bankRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(AddBankCommand command, CancellationToken cancellationToken)
        {
            bool isValid = await _validator.IsValid(command);

            if (!isValid) return new Unit();

            var bank = new Bank()
            {
                Name = command.Name,
                Accounts = new List<BankAccount>()
            };

            await _bankRepository.InsertAsync(bank);
            return new Unit();
        }

    }
}
