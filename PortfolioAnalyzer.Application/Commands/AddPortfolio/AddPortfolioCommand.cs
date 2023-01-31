using MediatR;
using MongoDB.Bson;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.BankAggregate;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Commands.AddPortfolio
{
    public class AddPortfolioCommand : ICommand, IRequest
    {
        [Required]
        public string Broker { get; set; }
        public IEnumerable<Position> Positions { get; set; }

    }
}
