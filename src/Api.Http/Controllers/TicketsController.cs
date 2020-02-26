namespace Api.Http.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="TicketsController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        /// <summary>
        /// Get the `Ticket Count`
        /// </summary>
        /// <returns>The `Ticket Count`</returns>
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTicketsCount()
        {
            var count = await Task.FromResult(int.MaxValue);
            return Ok(count);
        }
    }
}
