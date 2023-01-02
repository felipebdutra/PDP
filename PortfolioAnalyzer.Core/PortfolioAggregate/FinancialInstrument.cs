using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortfolioAnalyzer.Core.PortfolioAggregate;

public class FinancialInstrument
{
    public string Ticker { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Total { get; set; }
}
