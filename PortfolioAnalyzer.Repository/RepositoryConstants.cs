using System.Collections.Specialized;
using System.Diagnostics;

public static class RepositoryConstants
{
    public struct MongoDb
    {
        public struct Database
        {
            public struct PortfolioAnalyzer
            {
                public const string Name = "PortfolioAnalyzer";

                public struct Collections
                {
                    public const string Portfolio = "portfolio";
                    public const string Position = "position";
                    public const string StockPricesHistory = "stockPricesHistory";
                    public const string Bank = "bank";
                    public const string ExecutionLog = "executionLog";
                }
            }
        }
    }
}
