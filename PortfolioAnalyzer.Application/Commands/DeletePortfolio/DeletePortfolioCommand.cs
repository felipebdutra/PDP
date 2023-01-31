using MediatR;
using PortfolioAnalyzer.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PortfolioAnalyzer.Application.Commands.DeletePortfolio
{
    public class DeletePortfolioCommand : ICommand, IRequest
    {
        [Required]
        public string Broker { get; set; }
    }
}
