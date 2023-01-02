using PortfolioAnalyzer.Core.PortfolioAggregate;

public class PortfolioCalculatorService : IPortfolioCalculatorService
{
    public void CalculatePortfolioValue(Portfolio portfolio, List<FinancialInstrument> instruments)
    {
        var total = portfolio.Positions.Join(
            instruments,
            d => d.Ticker,
            d => d.Ticker,
            (port, prices) => new { Ticker = port.Ticker, Total = prices.Total * port.Quantity }
        );

        total = null;
    }
}
