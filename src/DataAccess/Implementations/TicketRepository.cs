namespace DataAccess.Implementations
{
    using DataAccess.Contracts;
    using Domain;
    using Nest;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="TicketRepository" />
    /// </summary>
    public class TicketRepository : ITicketRepository
    {
        /// <summary>
        /// Defines the Tickets Index
        /// </summary>
        private const string TicketsIndex = "ticket";

        /// <summary>
        /// Defines the _db
        /// </summary>
        private readonly ElasticClient _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketRepository"/> class.
        /// </summary>
        /// <param name="db">The db<see cref="ElasticClient"/></param>
        public TicketRepository(ElasticClient db)
        {
            _db = db;

            if (!_db.Indices.Exists(TicketsIndex).Exists)
            {
                _db.Indices.Create(TicketsIndex, index => index.Map<Ticket>(x => x.AutoMap()));
            }
        }

        /// <summary>
        /// Add Ticket
        /// </summary>
        /// <param name="ticket">The ticket<see cref="Ticket"/></param>
        /// <returns>The <see cref="Task{Ticket}"/></returns>
        public async Task<Ticket> Add(Ticket ticket)
        {
            var response = await _db.IndexAsync(ticket, i => i.Index(TicketsIndex).Id(ticket.Id));
            return response.Result == Result.Created ? ticket : null;
        }

        /// <summary>
        /// Read ticket by id
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{Ticket}"/></returns>
        public async Task<Ticket> Read(string id)
        {
            var response = await _db.GetAsync<Ticket>(id, i => i.Index(TicketsIndex));
            return response.IsValid ? response.Source : null;
        }
    }
}
