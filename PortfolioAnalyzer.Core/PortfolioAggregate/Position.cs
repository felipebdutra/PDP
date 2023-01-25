using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortfolioAnalyzer.Core.PortfolioAggregate;

public class Position
{
    public string Ticker { get; set; }
    [BsonRepresentation(BsonType.Double)]
    public decimal Quantity { get; set; }
}
