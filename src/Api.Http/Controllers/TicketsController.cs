namespace Api.Http.Controllers
{
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Services.Contracts;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="TicketsController" />
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        /// <summary>
        /// Defines the Ticket Service
        /// </summary>
        private readonly ITicketService _ticketService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketsController"/> class.
        /// </summary>
        /// <param name="ticketService">The ticket service<see cref="ITicketService"/></param>
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Create a `Ticket`
        /// </summary>
        /// <param name="conversation">The conversation<see cref="Conversation"/></param>
        /// <returns>The `Ticket`</returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Ticket>> Create([FromBody] Conversation conversation)
        {
            var ticket = await _ticketService.Create(conversation);

            if (ticket == null)
            {
                return BadRequest();
            }

            return Ok(ticket);
        }

        /// <summary>
        /// Read a ticket by Id
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <returns>Ticket</returns>
        [HttpGet("read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Ticket>> Get(string id)
        {
            var ticket = await _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        /// <summary>
        /// Update `Ticket Status`
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="status">Ticket status</param>
        /// <returns></returns>
        [HttpPut("update-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Ticket>> UpdateStatus([FromQuery] string id, [FromQuery] TicketStatus status)
        {
            var ticket = await _ticketService.Update(id, status);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        /// <summary>
        /// Add `Conversation`
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="conversation">Conversation</param>
        /// <returns>Ticket</returns>
        [HttpPost("add-conversation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Ticket>> AddConversation([FromQuery] string id, [FromBody] Conversation conversation)
        {
            var ticket = await _ticketService.AddConversation(id, conversation);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        /// <summary>
        /// Add `Note`
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="note">Note</param>
        /// <returns>Ticket</returns>
        [HttpPost("add-note")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Ticket>> AddNote([FromQuery] string id, [FromBody] Note note)
        {
            var ticket = await _ticketService.AddNote(id, note);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }
    }
}
