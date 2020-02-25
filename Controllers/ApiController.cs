using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSSSender.Models;
using RSSSender.Services;

namespace RSSSender.Controllers
{
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly IRssStoreService rssStoreService;

        public ApiController(IRssStoreService rssStoreService)
        {
            this.rssStoreService = rssStoreService;
        }

        [HttpPost("sendrssdata")]
        public async Task<IActionResult> SendRssData([FromBody] RssData rssData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await rssStoreService.SetRssDataAsync(rssData);

            return Ok(true);
        }
    }
}