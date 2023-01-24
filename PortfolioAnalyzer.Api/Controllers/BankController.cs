using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortfolioAnalyzer.Application.Commands.AddNewBank;
using PortfolioAnalyzer.Application.Queries.GetAllBanks;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using PortfolioAnalyzer.Repository.Bank;
using PortfolioAnalyzer.Services.Bank;

namespace PortfolioAnalyzer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        private readonly IMediator _mediator;
        public readonly IBankService _bankService;
        public readonly IPortfolioRepository _bankRepository;

        public BankController(IBankService bankService, IPortfolioRepository bankRepository, IMediator mediator)
        {
            _bankService = bankService;
            _bankRepository = bankRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Portfolio>> GetAllBanks(string except) 
        {
            var response = await _mediator.Send(new GetPortfolioQuery{ Name = except });

            return response;
        }


        [HttpPost]
        [Route("AddBank")]
        public async Task AddBank(string name)
        {
            await _mediator.Send(new AddBankCommand() { Name = name });
        }

        [HttpPost]
        [Route("AddBankAccount")]
        public async Task AddBankAccount(string bankName, string currency, decimal amount)
        {
            await _mediator.Send(new AddBankAccountCommand() { BankName = bankName, Currency = currency, Amount = amount });
        }

    }
}
