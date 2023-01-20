using MediatR;
using MongoDB.Driver;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;

namespace PortfolioAnalyzer.Application.Commands.AddNewBank
{
    public class AddNewBankCommandHandler : IRequestHandler<AddNewBankCommand>//, ICommandHandler<AddNewBankCommand>
    {
        private readonly IBankRepository _bankRepository;
        private readonly AddNewBankValidator _validator;

        public AddNewBankCommandHandler(IBankRepository bankRepository, AddNewBankValidator validator)
        {
            _bankRepository = bankRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(AddNewBankCommand command, CancellationToken cancellationToken)
        {
            bool isValid = await _validator.IsValid(command);

            if (!isValid) return new Unit();

            var bank = new Bank()
            {
                Name = command.Name,
                Accounts = command.Accounts
            };

            await _bankRepository.InsertAsync(bank);
            return new Unit();
        }

    }
}
