using AutoMapper;
using InfoTrackTest.Models.Entities;
using InfoTrackTest.Models.SearchEngines;
using InfoTrackTest.Repositories.Repositories.SearchEngineRepositories;
using Microsoft.Extensions.Logging;

namespace InfoTrackTest.Services.Services.SearchEngineServices
{
    public class SearchEngineService : ISearchEngineService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SearchEngineService> _logger;
        private readonly ISearchEngineRepository _searchEngineRepository;
        public SearchEngineService(IMapper mapper, ILogger<SearchEngineService> logger, ISearchEngineRepository searchEngineRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _searchEngineRepository = searchEngineRepository;
        }

        public async Task<List<SearchEngineOptionDto>> GetSearchEngineOptionsAsync()
        {
            var searchEngines = await _searchEngineRepository.GetAllSearchEnginesAsync();
            return _mapper.Map<List<SearchEngineOptionDto>>(searchEngines);
        }

        public async Task<SearchEngine> GetSingleSearchEngineByIdAsync(int id)
        {
            return await _searchEngineRepository.GetSingleSearchEngineByIdAsync(id);
        }
    }
}
