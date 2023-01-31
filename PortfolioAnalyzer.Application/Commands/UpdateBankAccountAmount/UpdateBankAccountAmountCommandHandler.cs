using MediatR;
using MongoDB.Driver;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;

namespace PortfolioAnalyzer.Application.Commands.UpdateBankAccountAmount
{
    public class UpdateBankAccountAmountCommandHandler : IRequestHandler<UpdateBankAccountAmountCommand>
    {
        private readonly IBankRepository _bankRepository;
        private readonly UpdateBankAccountAmountValidator _validator;

        public UpdateBankAccountAmountCommandHandler(IBankRepository bankRepository, UpdateBankAccountAmountValidator validator)
        {
            _bankRepository = bankRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateBankAccountAmountCommand command, CancellationToken cancellationToken)
        {
            bool isValid = await _validator.IsValid(command);

            if (!isValid) return new Unit();

            await _bankRepository.UpdateBankAccountAmount(command.BankName,command.Currency,command.Amount);
            return new Unit();
        }

    }
}
