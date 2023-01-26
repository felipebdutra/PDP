using MediatR;
using MongoDB.Driver;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;

namespace PortfolioAnalyzer.Application.Queries.GetAllBanks
{
    public class GetAllBanksNameQueryHandler : IRequestHandler<GetAllBanksNameQuery, IEnumerable<GetAllBanksDto>>
    {
        private readonly IBankRepository _bankRepository;
        public GetAllBanksNameQueryHandler(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public async Task<IEnumerable<GetAllBanksDto>> Handle(GetAllBanksNameQuery getAllBanksNameQuery, CancellationToken cancellationToken)
        {
            var filterDefinition = new FindOptions<Bank>()
            {
                Projection = Builders<Bank>.Projection.Include(s => s.Name).Include(s => s.Accounts).Exclude("_id")
            };

            var banks = await _bankRepository.FindAsync(Builders<Bank>.Filter.Empty, filterDefinition);

            var response = new List<GetAllBanksDto>();

            foreach (var bank in banks)
            {
                response.Add(new() { 
                    Name = bank.Name,
                    Accounts = bank.Accounts
                });
            }

            return response;
        }
    }
}
