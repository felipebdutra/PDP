using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortfolioAnalyzer.Core.PortfolioAggregate;

public class StockPricesHistory
{
    public ObjectId Id { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Date { get; set; }
    public List<FinancialInstrument> Instruments { get; set; }
}
