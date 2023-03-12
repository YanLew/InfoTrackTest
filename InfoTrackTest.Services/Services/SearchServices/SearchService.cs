using AutoMapper;
using InfoTrackTest.Models.SearchEngines;
using InfoTrackTest.Services.Services.HistoryServices;
using InfoTrackTest.Services.Services.SearchEngineServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace InfoTrackTest.Services.Services.SearchServices
{
    public class SearchService : ISearchService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SearchService> _logger;
        private readonly IHistoryService _historyService;
        private readonly ISearchEngineService _searchEngineService;
        private readonly int _targetResultCount;
        private readonly string _targetMatchUrl;
        public SearchService(IMapper mapper, ILogger<SearchService> logger, IConfiguration configuration, IHistoryService historyService, ISearchEngineService searchEngineService)
        {
            _mapper = mapper;
            _logger = logger;
            _historyService = historyService;
            _searchEngineService = searchEngineService;
            
            _targetResultCount = configuration.GetValue<int>("TargetResultCount");
            _targetMatchUrl = configuration.GetValue<string>("TargetMatchUrl");
        }

        public async Task<string> GetTargetUrlRanksInSearchResultAsync(int searchEngineId, string keywords)
        {
            var urls = await FetchSearchResultsAsync(searchEngineId, keywords);
            var ranks = "";
            int rank = 1;
            foreach (var url in urls)
            {
                if (url.Contains(_targetMatchUrl))
                    ranks += $"{rank}, ";
                rank++;
                if (rank > 100)
                    break;
            }

            ranks = ranks.IsNullOrEmpty() ? "0" : ranks.Substring(0, ranks.Length - 2);
            await _historyService.AddHistoryAsync(keywords, ranks, searchEngineId);
            return ranks;
        }

        private async Task<List<string>> FetchSearchResultsAsync(int searchEngineId, string keywords)
        {
            var searchEngine = await _searchEngineService.GetSingleSearchEngineByIdAsync(searchEngineId);

            if (searchEngine == null) throw new Exception($"Failed to get search engine with id = {searchEngineId}");

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", searchEngine.UserAgent);

            int pageNumber = 1;
            int resultCount = 0;
            List<string> urls = new List<string>();

            while (resultCount < (_targetResultCount + 10))
            {
                var targetUrl = searchEngine.Url
                                .Replace(SearchEngineSeparatorConst.SEARCH_KEYWORDS, keywords.Replace(" ", "+"))
                                .Replace(SearchEngineSeparatorConst.RESULT_NUMBER, (_targetResultCount + 10).ToString())
                                .Replace(SearchEngineSeparatorConst.PAGE_NUMBER, pageNumber.ToString())
                                .Replace(SearchEngineSeparatorConst.OFFSET, (pageNumber == 1 ? 0 : ((pageNumber - 1) * searchEngine.DefaultPageSize) + 1).ToString())
                                .Replace(SearchEngineSeparatorConst.DEFAULT_OFFSET_SIZE, (searchEngine.DefaultPageSize ?? 0).ToString());
                HttpResponseMessage response = await httpClient.GetAsync(targetUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var tempUrls = GetAllUrlsFromSearchResult(responseBody, searchEngine.Name.ToLower());
                resultCount += tempUrls.Count();
                urls.AddRange(tempUrls);
                pageNumber++;
            }

            return urls;
        }

        public List<string> GetAllUrlsFromSearchResult(string searchResult, string host)
        {
            string pattern = @"<a\s+(?:[^>]*?\s+)?href=""(?<url>.*?)""";
            MatchCollection matches = Regex.Matches(searchResult, pattern);
            List<string> urls = new List<string>();

            foreach (Match match in matches)
            {
                string url = match.Groups["url"].Value;
                if (url.StartsWith("http") && !url.ToLower().Contains(host))
                {
                    urls.Add(url);
                }
            }
            return urls;
        }
    }
}
