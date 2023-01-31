using AutoFixture;
using global::PortfolioAnalyzer.Services;
using Moq;
using PortfolioAnalyzer.Core.PortfolioAggregate;

namespace PortfolioAnalyzer.UnitTest
{
    public class WalletServiceTests
    {
        private MockRepository mockRepository;

        private Mock<IPortfolioService> mockPortfolioService;
        private Mock<ICurrencyConvertionService> mockCurrencyConvertionService;

        public WalletServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockPortfolioService = this.mockRepository.Create<IPortfolioService>();
            this.mockCurrencyConvertionService = this.mockRepository.Create<ICurrencyConvertionService>();
        }

        private WalletService CreateService()
        {
            return new WalletService(
                this.mockPortfolioService.Object,
                this.mockCurrencyConvertionService.Object);
        }

        [Theory]
        [InlineData("MockStock1",52.4, "MockStock2", 143.60)]
        [InlineData("MockStock1",75.4, "MockStock2", 163)]
        [InlineData("MockStock1",5452.4, "MockStock2", 13)]
        [InlineData("MockStock1",98798.667, "MockStock2", 6454)]
        [InlineData("MockStock1",10000.555, "MockStock2", 15)]
        public async Task TotalPortfolioValue_WhenPortfolioHasMultipleStocks_ExpectedBehavior(string ticker1, decimal stock1Price, string ticker2, decimal stock2Price)
        {
            var service = this.CreateService();

            var fix = new Fixture();
            var position1 = fix.Create<Position>();
            var position2 = fix.Create<Position>();
            position1.Ticker = ticker1;
            position2.Ticker = ticker2;

            this.mockPortfolioService.Setup(s => s.GetPortfoliosAsync())
                .Returns(Task.FromResult(new List<Portfolio>() { 
                    new Portfolio()
                    {
                         Positions = new List<Position>() { position1 , position2 }
                    }
                }));

            List<FinancialInstrument> instrumentsPrice = new() {
                new() { Ticker = ticker1, Total = stock1Price } ,
                new() { Ticker = ticker2, Total = stock2Price } ,
            };
                      
            var result = await service.TotalPortfolioValue(instrumentsPrice);
                        
            Assert.Equal(position1.Quantity * stock1Price + position2.Quantity * stock2Price, result);

        }
    }
}
