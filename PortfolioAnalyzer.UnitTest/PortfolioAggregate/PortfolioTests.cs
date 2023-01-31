using Moq;
using PortfolioAnalyzer.Core.PortfolioAggregate;
using System;
using Xunit;

namespace PortfolioAnalyzer.UnitTest.PortfolioAggregate
{
    public class PortfolioTests
    {
        private MockRepository mockRepository;



        public PortfolioTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private Portfolio CreatePortfolio()
        {
            return new Portfolio();
        }


        [Theory]
        [InlineData("MockStock1", 52.4,4.1)]
        [InlineData("MockStock1", 75.4,56)]
        [InlineData("MockStock1", 5452.4,778.6)]
        public void GetTotal_SumStockValue_ExpectedBehavior(string ticker, decimal stockPrice, decimal qnt)
        {
            // Arrange
            var portfolio = this.CreatePortfolio();

            // Act
            var instruments = new List<FinancialInstrument>()
            {
                new ()
                {
                    Ticker= ticker,
                    Total = stockPrice
                }
            };

            portfolio.SetPrices(instruments);
            portfolio.Positions = new List<Position>()
            {
                new Position()
                {
                    Ticker= ticker,
                    Quantity = qnt
                }
            };

            var result = portfolio.GetTotal(
                ticker);

            // Assert
            Assert.Equal(stockPrice * qnt, result);
            this.mockRepository.VerifyAll();
        }
    }
}
