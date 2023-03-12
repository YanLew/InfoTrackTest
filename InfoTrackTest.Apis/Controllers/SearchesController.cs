using InfoTrackTest.Services.Services.SearchServices;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackTest.Apis.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchesController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchesController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GetSearchResultAsync([FromQuery] int searchEngineId, [FromQuery] string keywords)
        {
            var str = await _searchService.GetTargetUrlRanksInSearchResultAsync(searchEngineId, keywords);

            return Ok(str);
        }
    }
}
