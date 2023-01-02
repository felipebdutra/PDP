using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace PortfolioAnalyzer.Core.BankAggregate
{
    public class BankAccount
    {
        public ObjectId Id { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
