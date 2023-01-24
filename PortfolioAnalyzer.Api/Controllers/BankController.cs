using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortfolioAnalyzer.Application.Commands.AddNewBank;
using PortfolioAnalyzer.Application.Queries.GetAllBanks;
using PortfolioAnalyzer.Core.BankAggregate;
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
        public readonly IBankRepository _bankRepository;

        public BankController(IBankService bankService, IBankRepository bankRepository, IMediator mediator)
        {
            _bankService = bankService;
            _bankRepository = bankRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllBanksDto>> GetAllBanks(string except) 
        {
            var response = await _mediator.Send(new GetAllBanksNameQuery{ Except = except });

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
