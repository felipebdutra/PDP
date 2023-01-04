using MediatR;
using MongoDB.Driver;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var filter = Builders<Bank>.Filter.Where(n => n.Name != getAllBanksNameQuery.Except);

            var filterDefinition = new FindOptions<Bank>()
            {
                Projection = Builders<Bank>.Projection.Include(s => s.Name)
                                                       // .Exclude(s => s.Accounts)
                                                        .Exclude("_id")

            };

            var banks = await _bankRepository.FindAsync(filter, filterDefinition);

            var response = new List<GetAllBanksDto>();

            foreach (var bank in banks)
            {
                response.Add(new() { Name = bank.Name });
            }

            return response;
        }
    }
}
