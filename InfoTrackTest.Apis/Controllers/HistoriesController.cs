using Core.Models;
using InfoTrackTest.Models.Histories;
using InfoTrackTest.Models.SearchEngines;
using InfoTrackTest.Services.Services.HistoryServices;
using InfoTrackTest.Services.Services.SearchEngineServices;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackTest.Apis.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HistoriesController : Controller
    {
        private readonly IHistoryService _historyService;

        public HistoriesController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedResult<HistoryDto>>> GetPaginatedHistoriesAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            var rtn = await _historyService.GetPaginatedHistoriesAsync(page, pageSize);

            return Ok(rtn);
        }

        [HttpGet]
        [Route("Dashboard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<HistoryDashboardDto>>> GetHistoryDashboardAsync()
        {
            var rtn = await _historyService.GetHistoriesDashboardDataAsync();

            return Ok(rtn);
        }
    }
}
