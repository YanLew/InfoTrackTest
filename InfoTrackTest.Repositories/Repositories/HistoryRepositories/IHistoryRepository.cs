using InfoTrackTest.Models.Entities;
using InfoTrackTest.Models.Histories;

namespace InfoTrackTest.Repositories.Repositories.HistoryRepositories
{
    public interface IHistoryRepository : IInfoTrackTestRepository
    {
        Task<List<History>> GetAllHistoriesAsync();
        Task<(List<History>, int)> GetPaginatedHistoriesAsync(int page, int pageSize);
        Task<History> CreateHistoryAsync(string keyword, string rank, int searchEngineId);
        Task<List<HistoryDashboardDto>> GetHistoriesDashboardDataAsync();
    }
}
