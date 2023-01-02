using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortfolioAnalyzer.Core.PortfolioAggregate;

public class Portfolio
{
    public ObjectId Id { get; set; }
    public string Broker { get; set; }
    public IEnumerable<Position> Positions { get; set; }

    public Dictionary<string, decimal> Instruments { get; set; }

    [BsonIgnore]
    private List<FinancialInstrument> _instrumentsPrice;

    public Portfolio() { }

    public Portfolio(List<FinancialInstrument> instruments)
    {
        SetPrices(instruments);
    }

    public void SetPrices(List<FinancialInstrument> instruments)
    {
        _instrumentsPrice = instruments;
    }

    public decimal GetTotal(string ticker)
    {
        return Positions
            .Where(w => w.Ticker == ticker)
            .Sum(s => s.Quantity * _instrumentsPrice.First(w => w.Ticker == ticker).Total);
    }

    public decimal GetTotal()
    {
        return Positions.Sum(
            s => s.Quantity * _instrumentsPrice.First(w => w.Ticker == s.Ticker).Total
        );
    }
}
