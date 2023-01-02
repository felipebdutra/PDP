using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace PortfolioAnalyzer.Core.BankAggregate
{
    public class Bank
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<BankAccount> Accounts { get; set; }
    }
}
