namespace Services.Contracts
{
    using Domain;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ITicketService" />
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Create a ticket
        /// </summary>
        /// <param name="conversation">The conversation<see cref="Conversation"/></param>
        /// <returns>The <see cref="Ticket"/></returns>
        Task<Ticket> Create(Conversation conversation);

        /// <summary>
        /// Get ticket by Id
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Ticket"/></returns>
        Task<Ticket> Get(string id);

        /// <summary>
        /// Add conversation
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="conversation">The conversation<see cref="Conversation"/></param>
        /// <returns>The <see cref="Ticket"/></returns>
        Task<Ticket> AddConversation(string id, Conversation conversation);

        /// <summary>
        /// Add new note
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="note">Note</param>
        /// <returns>Ticket</returns>
        Task<Ticket> AddNote(string id, Note note);

        /// <summary>
        /// Update note
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <param name="noteId">Note id</param>
        /// <returns>Ticket</returns>
        Task<Ticket> Update(string id, string noteId);

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="id">The ticket id</param>
        /// <param name="status">The status<see cref="TicketStatus"/></param>
        /// <returns>The <see cref="Ticket"/></returns>
        Task<Ticket> Update(string id, TicketStatus status);
    }
}
