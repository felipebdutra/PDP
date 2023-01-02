using MongoDB.Bson;

namespace PortfolioAnalyzer.Core.PortfolioAggregate;

public class Position
{
    public ObjectId Id { get; set; }
    public string Ticker { get; set; }
    public decimal Quantity { get; set; }
}
