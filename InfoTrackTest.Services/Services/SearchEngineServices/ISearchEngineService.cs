using InfoTrackTest.Models.Entities;
using InfoTrackTest.Models.SearchEngines;

namespace InfoTrackTest.Services.Services.SearchEngineServices
{
    public interface ISearchEngineService : IInfoTrackTestService
    {
        Task<List<SearchEngineOptionDto>> GetSearchEngineOptionsAsync();
        Task<SearchEngine> GetSingleSearchEngineByIdAsync(int id);
    }
}
