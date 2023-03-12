using InfoTrackTest.Models.Entities;

namespace InfoTrackTest.Repositories.Repositories.SearchEngineRepositories
{
    public interface ISearchEngineRepository : IInfoTrackTestRepository
    {
        Task<List<SearchEngine>> GetAllSearchEnginesAsync();
        Task<(List<SearchEngine>, int)> GetPaginatedSearchEnginesAsync(int page, int pageSize);
        Task<SearchEngine?> GetSingleSearchEngineByIdAsync(int id);
    }
}
