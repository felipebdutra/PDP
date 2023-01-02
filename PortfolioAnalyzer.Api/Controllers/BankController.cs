using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Repository.Bank;
using PortfolioAnalyzer.Services.Bank;

namespace PortfolioAnalyzer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        public IBankService _bankService { get; set; }
        public IBankRepository _bankRepository { get; set; }

        public BankController(IBankService bankService, IBankRepository bankRepository)
        {
            _bankService = bankService;
            _bankRepository = bankRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Bank>> GetAll() => await _bankRepository.FindAsync();
    }
}
