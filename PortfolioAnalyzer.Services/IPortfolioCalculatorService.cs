using PortfolioAnalyzer.Core.PortfolioAggregate;

public interface IPortfolioCalculatorService
{
    void CalculatePortfolioValue(Portfolio portfolio, List<FinancialInstrument> instruments);
}