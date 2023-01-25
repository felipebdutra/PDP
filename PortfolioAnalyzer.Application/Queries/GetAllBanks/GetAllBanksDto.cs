using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioAnalyzer.Core.BankAggregate;

namespace PortfolioAnalyzer.Application.Queries.GetAllBanks
{
    public class GetAllBanksDto
    {
        public string Name { get; set; }
        public List<BankAccount> Accounts { get; set; }
    }
}
