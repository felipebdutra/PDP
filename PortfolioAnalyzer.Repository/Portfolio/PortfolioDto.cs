using PortfolioAnalyzer.Core.PortfolioAggregate;

namespace PortfolioAnalyzer.Repository.Portfolio
{
    public class PortfolioDto
    {
        public string Broker { get; set; }
        public List<Position> Positions { get; set; }
    }
}
