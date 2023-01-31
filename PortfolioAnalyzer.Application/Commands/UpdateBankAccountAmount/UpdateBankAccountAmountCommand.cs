using MediatR;
using PortfolioAnalyzer.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PortfolioAnalyzer.Application.Commands.UpdateBankAccountAmount
{
    public class UpdateBankAccountAmountCommand : ICommand, IRequest
    {
        [Required]
        public string BankName { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
