namespace DataAccess.Contracts
{
    using Domain;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ITicketRepository" />
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        /// Add Ticket
        /// </summary>
        /// <param name="ticket">The ticket<see cref="Ticket"/></param>
        /// <returns>The <see cref="Task{Ticket}"/></returns>
        Task<Ticket> Add(Ticket ticket);

        /// <summary>
        /// Read ticket by id
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{Ticket}"/></returns>
        Task<Ticket> Read(string id);

        /// <summary>
        /// Update ticket
        /// </summary>
        /// <param name="ticket">The ticket<see cref="Ticket"/></param>
        /// <returns>The <see cref="Task{Ticket}"/></returns>
        Task<Ticket> Update(Ticket ticket);
    }
}
