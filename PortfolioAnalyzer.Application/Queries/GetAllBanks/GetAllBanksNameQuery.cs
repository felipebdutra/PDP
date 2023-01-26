using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Queries.GetAllBanks
{
    public class GetAllBanksNameQuery : IRequest<IEnumerable<GetAllBanksDto>>
    {
    }
}
