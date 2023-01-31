using MediatR;
using MongoDB.Driver;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;

namespace PortfolioAnalyzer.Application.Commands.DeleteBank
{
    public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand>
    {
        private readonly IBankRepository _bankRepository;
        private readonly DeleteBankValidator _validator;

        public DeleteBankCommandHandler(IBankRepository bankRepository, DeleteBankValidator validator)
        {
            _bankRepository = bankRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(DeleteBankCommand command, CancellationToken cancellationToken)
        {
            bool isValid = await _validator.IsValid(command);

            if (!isValid) return new Unit();


            await _bankRepository.DeleteAsync(Builders<Bank>.Filter.Where(w => w.Name == command.Name));
            return new Unit();
        }

    }
}
