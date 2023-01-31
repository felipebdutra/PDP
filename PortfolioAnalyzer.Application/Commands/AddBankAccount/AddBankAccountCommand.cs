using MediatR;
using MongoDB.Bson;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.BankAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Commands.AddBankAccount
{
    public class AddBankAccountCommand : ICommand, IRequest
    {
        [Required]
        public string BankName { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
