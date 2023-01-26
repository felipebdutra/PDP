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
            return await _mediator.Send(new GetAllBanksNameQuery());
        }


        [HttpPost]
        [Route("AddBank")]
        public async Task AddBank(AddBankCommand bankCommand)
        {            
            await _mediator.Send(bankCommand);
        }


        [HttpDelete]
        [Route("DeleteBank")]
        public async Task DeleteBank(DeleteBankCommand command)
        {
            await _mediator.Send(command);
        }

        [HttpPost]
        [Route("AddBankAccount")]
        public async Task AddBankAccount(AddBankAccountCommand command)
        {
            await _mediator.Send(command);
        }

        [HttpPut]
        [Route("UpdateBankAccountAmount")]
        public async Task UpdateBankAccountAmount(UpdateBankAccountAmountCommand command)
        {
            await _mediator.Send(command);        
        }
    }
}
