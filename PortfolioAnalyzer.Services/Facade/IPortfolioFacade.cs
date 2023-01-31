namespace PortfolioAnalyzer.Services.Facade
{
    public interface IPortfolioFacade
    {
        Task LoadDataAsync();
        Task<PortfolioInfoDto> ProcessDataAsync();
        Task LoadStoredDataAsync();
    }
}
