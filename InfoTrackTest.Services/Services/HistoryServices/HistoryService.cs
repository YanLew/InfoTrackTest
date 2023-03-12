using AutoMapper;
using Core.Models;
using InfoTrackTest.Models.Histories;
using InfoTrackTest.Repositories.Repositories.HistoryRepositories;
using Microsoft.Extensions.Logging;

namespace InfoTrackTest.Services.Services.HistoryServices
{
    public class HistoryService : IHistoryService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<HistoryService> _logger;
        private readonly IHistoryRepository _historyRepository;
        public HistoryService(IMapper mapper, ILogger<HistoryService> logger, IHistoryRepository historyRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _historyRepository = historyRepository;
        }

        public async Task<PaginatedResult<HistoryDto>> GetPaginatedHistoriesAsync(int page, int pageSize)
        {
            (var histories, var totalCount) = await _historyRepository.GetPaginatedHistoriesAsync(page, pageSize);
            var items = _mapper.Map<List<HistoryDto>>(histories);

            return new PaginatedResult<HistoryDto>()
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task AddHistoryAsync(string keyword, string rank, int searchEngineId)
        {
            await _historyRepository.CreateHistoryAsync(keyword, rank, searchEngineId);
        }

        public async Task<List<HistoryDashboardDto>> GetHistoriesDashboardDataAsync()
        {
            return await _historyRepository.GetHistoriesDashboardDataAsync();
        }
    }
}
