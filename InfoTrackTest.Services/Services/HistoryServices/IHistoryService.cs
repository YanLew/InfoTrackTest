using Core.Models;
using InfoTrackTest.Models.Histories;

namespace InfoTrackTest.Services.Services.HistoryServices
{
    public interface IHistoryService : IInfoTrackTestService
    {
        Task<PaginatedResult<HistoryDto>> GetPaginatedHistoriesAsync(int page, int pageSize);
        Task AddHistoryAsync(string keyword, string rank, int searchEngineId);
        Task<List<HistoryDashboardDto>> GetHistoriesDashboardDataAsync();
    }
}
