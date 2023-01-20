using MediatR;
using MongoDB.Bson;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.BankAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Commands.AddNewBank
{
    public class AddNewBankCommand : ICommand, IRequest
    {
        public string Name { get; set; }
        public List<BankAccount> Accounts { get; set; }
    }
}
