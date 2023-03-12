using InfoTrackTest.Models.Entities;
using InfoTrackTest.Models.Histories;
using InfoTrackTest.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InfoTrackTest.Repositories.Repositories.HistoryRepositories
{
    public class HistoryRepository : BaseRepository<History>, IHistoryRepository
    {
        public HistoryRepository(ILogger<HistoryRepository> logger, InfoTrackTestContext dbContext) : base(dbContext, true)
        {
        }

        private IQueryable<History> GetHistoriesWithPaginagtion(int? page = null, int? pageSize = null)
        {
            var query = _dbContext.Set<History>()
                                  .Include(h => h.SearchEngine)
                                  .OrderByDescending(h => h.Id)
                                  .AsQueryable();

            if (page.HasValue && pageSize.HasValue)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return query;
        }

        public async Task<List<History>> GetAllHistoriesAsync()
        {
            return await GetHistoriesWithPaginagtion().ToListAsync();
        }

        public async Task<(List<History>, int)> GetPaginatedHistoriesAsync(int page, int pageSize)
        {
            return (await GetHistoriesWithPaginagtion(page, pageSize).ToListAsync(), await GetHistoriesWithPaginagtion().CountAsync());
        }

        public async Task<History> CreateHistoryAsync(string keyword, string rank, int searchEngineId)
        {
            var result = await CreateSingleAsync(
                new History()
                {
                    Keyword = keyword.Trim(),
                    Rank = rank,
                    SearchEngineId = searchEngineId
                });

            return result;
        }

        public async Task<List<HistoryDashboardDto>> GetHistoriesDashboardDataAsync()
        {
            var query = _dbContext.Set<History>()
                                  .Include(h => h.SearchEngine)
                                  .OrderByDescending(h => h.Id)
                                  .AsQueryable();

            var groupByDate = await query.GroupBy(h => new { h.CreatedDateTime.Date, h.Keyword, h.SearchEngine.Name }).ToListAsync();

            List<HistoryDashboardDto> result = new List<HistoryDashboardDto>();
            foreach (var group in groupByDate)
            {
                var date = group.Key.Date;
                var keywords = group.Key.Keyword;
                var searchEngineName = group.Key.Name;
                var highestRanks = group.Where(h => h.Rank != "0").Select(h => h.Rank.Split(", ").First()).Select(r => Convert.ToInt32(r.Trim()));
                var noRankCount = highestRanks == null || !highestRanks.Any();
                var totalCount = group.Count();

                result.Add(new HistoryDashboardDto()
                {
                    Keyword = keywords,
                    SearchEngineName = searchEngineName,
                    Date = date,
                    TotalSearchCount = totalCount,
                    NotFoundCount = totalCount - (noRankCount ? 0 : highestRanks?.Count() ?? 0),
                    HighestRank = noRankCount ? 0 : (highestRanks?.Min() ?? 0),
                    AverageHighestRank = noRankCount ? 0 : (highestRanks?.Average() ?? 0)
                });
            }

            return result.OrderByDescending(h => h.Date).OrderBy(h => h.Keyword).ToList();
        }
    }
}
