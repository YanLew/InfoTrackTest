using InfoTrackTest.Models.Entities;
using InfoTrackTest.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InfoTrackTest.Repositories.Repositories.SearchEngineRepositories
{
    public class SearchEngineRepository : BaseRepository<SearchEngine>, ISearchEngineRepository
    {
        public SearchEngineRepository(ILogger<SearchEngineRepository> logger, InfoTrackTestContext dbContext) : base(dbContext, true)
        {
        }

        private IQueryable<SearchEngine> GetSearchEnginesWithPaginagtion(int? page = null, int? pageSize = null)
        {
            var query = _dbContext.Set<SearchEngine>().AsQueryable();

            if (page.HasValue && pageSize.HasValue)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return query;
        }

        public async Task<List<SearchEngine>> GetAllSearchEnginesAsync()
        {
            return await GetSearchEnginesWithPaginagtion().ToListAsync();
        }

        public async Task<(List<SearchEngine>, int)> GetPaginatedSearchEnginesAsync(int page, int pageSize)
        {
            return (await GetSearchEnginesWithPaginagtion(page, pageSize).ToListAsync(), await GetSearchEnginesWithPaginagtion().CountAsync());
        }

        public async Task<SearchEngine?> GetSingleSearchEngineByIdAsync(int id)
        {
            if (id <= 0) throw new Exception($"Invalid id({id}) to get search engine");

            var query = _dbContext.Set<SearchEngine>()
                                  .Where(se => se.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
