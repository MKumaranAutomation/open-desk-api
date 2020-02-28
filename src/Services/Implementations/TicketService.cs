namespace Services.Implementations
{
    using DataAccess.Contracts;
    using Domain;
    using Services.Contracts;
    using System.Threading.Tasks;

    /// <summary>
    /// Ticket service
    /// </summary>
    public class TicketService : ITicketService
    {
        /// <summary>
        /// Defines the ticket repository
        /// </summary>
        private readonly ITicketRepository _ticketRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketService"/> class.
        /// </summary>
        /// <param name="ticketRepository">The ticke tRepository<see cref="ITicketRepository"/></param>
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// Create a ticket
        /// </summary>
        /// <param name="conversation">The conversation<see cref="Conversation"/></param>
        /// <returns>The <see cref="Ticket"/></returns>
        public async Task<Ticket> Create(Conversation conversation)
        {
            var ticket = new Ticket();
            ticket.AddConversation(conversation);

            var response = await _ticketRepository.Add(ticket);
            return response;
        }

        /// <summary>
        /// Get ticket by Id
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Ticket"/></returns>
        public async Task<Ticket> Get(string id)
        {
            var ticket = await _ticketRepository.Read(id);
            return ticket;
        }

        /// <summary>
        /// Add conversation
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="conversation">The conversation<see cref="Conversation"/></param>
        /// <returns>The <see cref="Ticket"/></returns>
        public async Task<Ticket> AddConversation(string id, Conversation conversation)
        {
            var ticket = await Get(id);
            ticket.AddConversation(conversation);
            ticket = await _ticketRepository.Update(ticket);
            return ticket;
        }

        /// <summary>
        /// Add new note
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="note">Note</param>
        /// <returns>Ticket</returns>
        public async Task<Ticket> AddNote(string id, Note note)
        {
            var ticket = await Get(id);
            ticket.AddNote(note);
            ticket = await _ticketRepository.Update(ticket);
            return ticket;
        }

        /// <summary>
        /// Update note
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="noteId">Note id</param>
        /// <returns>Ticket</returns>
        public async Task<Ticket> Update(string id, string noteId)
        {
            var ticket = await Get(id);
            ticket.UpdateNote(noteId);
            ticket = await _ticketRepository.Update(ticket);
            return ticket;
        }

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="id">The ticket id</param>
        /// <param name="status">The status<see cref="TicketStatus"/></param>
        /// <returns>The <see cref="Ticket"/></returns>
        public async Task<Ticket> Update(string id, TicketStatus status)
        {
            var ticket = await Get(id);
            ticket.UpdateStatus(status);

            ticket = await _ticketRepository.Update(ticket);
            return ticket;
        }
    }
}
