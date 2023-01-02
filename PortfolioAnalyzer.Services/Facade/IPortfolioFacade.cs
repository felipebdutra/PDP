namespace PortfolioAnalyzer.Services.Facade
{
    public interface IPortfolioFacade
    {
        Task LoadDataAsync();
        Task<PortfolioInfo> ProcessDataAsync();
        Task LoadStoredDataAsync();
    }
}
