using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortfolioAnalyzer.Core.BankAggregate
{
    public class BankAccount
    {
        public string Currency { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Amount { get; set; }
    }
}
