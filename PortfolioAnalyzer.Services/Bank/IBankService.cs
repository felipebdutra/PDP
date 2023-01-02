using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioAnalyzer.Core.BankAggregate;

namespace PortfolioAnalyzer.Services.Bank
{
    public interface IBankService
    {
        Task<IList<BankAccount>> GetTotalCashValueAsync();
    }
}