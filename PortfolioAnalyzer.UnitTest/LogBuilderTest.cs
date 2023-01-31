using Moq;
using PortfolioAnalyzer.Infrastructure.Logging;
using System.Text;

namespace PortfolioAnalyzer.UnitTest
{
    public class LogBuilderTest
    {
        private Mock<ILogStrategy> _logMock { get; set; }

        public LogBuilderTest()
        {
            _logMock = new Mock<ILogStrategy>();
        }

        [Fact]
        public void WriteLog_WhenStrategyIsInjectedInTheConstructor_WriteMethodWasCalledOnce()
        {
            var logBuilder = new LogBuilder(_logMock.Object);

            logBuilder.WriteLog("mockMessage");

            _logMock.Verify(d => d.WriteLog(It.IsAny<StringBuilder>()), Times.Once);            
        }

        [Fact]
        public void WriteLog_WhenStrategyIsInjectedInTheMethod_WriteMethodWasCalledOnce()
        {
            var logBuilder = new LogBuilder();

            logBuilder.WriteLog(_logMock.Object, "mockMessage");

            _logMock.Verify(d => d.WriteLog(It.IsAny<StringBuilder>()), Times.Once);
        }

        [Theory]
        [InlineData("LogMessage")]
        public void WriteLog_WhenLogIsAppended_HasExpectedSting(string message)
        {
            var logBuilder = new LogBuilder(_logMock.Object);
            logBuilder.Append(message);
            logBuilder.WriteLog();

            _logMock.Verify(d => d.WriteLog(It.IsAny<StringBuilder>()), Times.Once);
        }
    }
}