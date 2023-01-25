using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortfolioAnalyzer.Application.Commands.AddBank;
using PortfolioAnalyzer.Application.Commands.AddBankAccount;
using PortfolioAnalyzer.Application.Commands.DeleteBank;
using PortfolioAnalyzer.Application.Commands.UpdateBankAccountAmount;
using PortfolioAnalyzer.Application.Queries.GetAllBanks;
using PortfolioAnalyzer.Application.Queries.GetPortfolios;
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
        public readonly IBankRepository _bankRepository;

        public BankController(IBankService bankService, IBankRepository bankRepository, IMediator mediator)
        {
            _bankService = bankService;
            _bankRepository = bankRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllBanksDto>> GetAllBanks() 
        {
            var response = await _mediator.Send(new GetAllBanksNameQuery());

            return response;
        }


        [HttpPost]
        [Route("AddBank")]
        public async Task AddBank(string name)
        {
            await _mediator.Send(new AddBankCommand() { Name = name });
        }


        [HttpDelete]
        [Route("DeleteBank")]
        public async Task DeleteBank(string name)
        {
            await _mediator.Send(new DeleteBankCommand() { Name = name });
        }

        [HttpPost]
        [Route("AddBankAccount")]
        public async Task AddBankAccount(string bankName, string currency, decimal amount)
        {
            await _mediator.Send(new AddBankAccountCommand() { BankName = bankName, Currency = currency, Amount = amount });
        }

        [HttpPut]
        [Route("UpdateBankAccountAmount")]
        public async Task UpdateBankAccountAmount(string bankName, string currency, decimal amount)
        {
            await _mediator.Send(new UpdateBankAccountAmountCommand() { BankName = bankName, Currency = currency, Amount = amount });
        }
    }
}
