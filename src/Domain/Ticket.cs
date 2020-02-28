namespace Domain
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Ticket statuses
    /// </summary>
    public enum TicketStatus
    {
        /// <summary>
        /// Unassigned
        /// </summary>
        Unassigned = 0,

        /// <summary>
        /// Assigned
        /// </summary>
        Assigned = 1,

        /// <summary>
        /// Closed
        /// </summary>
        Closed = 2,

        /// <summary>
        /// Re-opened
        /// </summary>
        Reopened = 3,

        /// <summary>
        /// Blocked
        /// </summary>
        Blocked = 4
    }

    /// <summary>
    /// Defines the <see cref="Ticket" />
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ticket"/> class.
        /// </summary>
        public Ticket()
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.UtcNow;
            Status = TicketStatus.Unassigned;
            Conversations = new List<Conversation>();
            Notes = new List<Note>();
        }

        /// <summary>
        /// Gets or sets the `Id`
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the `Created`
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Gets the `Status`
        /// </summary>
        public TicketStatus Status { get; set; }

        /// <summary>
        /// Gets the `Conversations`
        /// </summary>
        public ICollection<Conversation> Conversations { get; set; }

        /// <summary>
        /// Gets the `Notes`
        /// </summary>
        public ICollection<Note> Notes { get; set; }

        /// <summary>
        /// Add a Conversation
        /// </summary>
        /// <param name="conversation">The conversation<see cref="Conversation"/></param>
        public void AddConversation(Conversation conversation)
        {
            Conversations.Add(conversation);
        }

        /// <summary>
        /// Add `Note`
        /// </summary>
        /// <param name="note">Note</param>
        public void AddNote(Note note)
        {
            Notes.Add(note);
        }

        /// <summary>
        /// Update status
        /// </summary>
        /// <param name="status">Ticket status</param>
        public void UpdateStatus(TicketStatus status)
        {
            Status = status;
        }

        /// <summary>
        /// Update note status closed / opened
        /// </summary>
        /// <param name="id">Note id</param>
        /// <returns>Note</returns>
        public Note UpdateNote(string id)
        {
            var note = Notes.FirstOrDefault(n => n.Id == id);
            note?.Flip();

            return note;
        }
    }
}
