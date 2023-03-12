namespace InfoTrackTest.Services.Services.SearchServices
{
    public interface ISearchService
    {
        Task<string> GetTargetUrlRanksInSearchResultAsync(int searchEngineId, string keywords);
    }
}
