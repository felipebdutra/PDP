using MediatR;
using MongoDB.Driver;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;

namespace PortfolioAnalyzer.Application.Commands.AddBankAccount
{
    public class AddBankAccontCommandHandler : IRequestHandler<AddBankAccountCommand>
    {
        private readonly IBankRepository _bankRepository;
        private readonly AddBankAccountValidator _validator;

        public AddBankAccontCommandHandler(IBankRepository bankRepository, AddBankAccountValidator validator)
        {
            _bankRepository = bankRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(AddBankAccountCommand command, CancellationToken cancellationToken)
        {
            bool isValid = await _validator.IsValid(command);

            if (!isValid) throw new ArgumentException($"Bank {command.BankName} is invalid");

            var bankAccount = new BankAccount()
            {
                Currency = command.Currency,
                Amount = command.Amount,
            };

            await _bankRepository.AddBankAccount(bankAccount, command.BankName);
            return new Unit();
        }

    }
}
