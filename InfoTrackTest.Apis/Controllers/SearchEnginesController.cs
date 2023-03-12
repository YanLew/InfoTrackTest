using InfoTrackTest.Models.SearchEngines;
using InfoTrackTest.Services.Services.SearchEngineServices;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackTest.Apis.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchEnginesController : Controller
    {
        private readonly ISearchEngineService _searchEngineService;

        public SearchEnginesController(ISearchEngineService searchEngineService)
        {
            _searchEngineService = searchEngineService;
        }

        [HttpGet]
        [Route("Options")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<SearchEngineOptionDto>>> GetSearchResultAsync()
        {
            var rtn = await _searchEngineService.GetSearchEngineOptionsAsync();

            return Ok(rtn);
        }
    }
}
