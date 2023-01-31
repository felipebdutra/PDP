using MediatR;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Portfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Queries.GetPortfolios
{
    public class GetPortfolioQuery : IRequest<IEnumerable<PortfolioDto>>
    {
    }
}
