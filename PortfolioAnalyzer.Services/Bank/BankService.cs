using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;

namespace PortfolioAnalyzer.Services.Bank
{
    public class BankService : IBankService
    {
        private IPortfolioRepository _bankRepository;
        private ICurrencyConvertionService _currencyConvertion;

        public BankService(
            IPortfolioRepository bankRepository,
            ICurrencyConvertionService currencyConvertion
        )
        {
            _bankRepository = bankRepository;
            _currencyConvertion = currencyConvertion;
        }

        public async Task<IList<BankAccount>> GetTotalCashValueAsync()
        {
            return await _bankRepository.GetAllAccountsAsync();
        }
    }
}
