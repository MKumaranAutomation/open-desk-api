namespace OpenDesk.UnitTests
{
    using Domain;
    using FluentAssertions;
    using System;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="TicketTests" />
    /// </summary>
    public class TicketTests
    {
        /// <summary>
        /// Defines the Note Content
        /// </summary>
        private const string NoteContent = "Note Content";

        /// <summary>
        /// Defines the Conversation Title
        /// </summary>
        private const string ConversationTitle = "Conversation Title";

        /// <summary>
        /// Defines the Conversation Content
        /// </summary>
        private const string ConversationContent = "ConversationContent";

        /// <summary>
        /// Ticket Creation
        /// </summary>
        [Fact]
        public void TicketCreation()
        {
            // Act
            var ticket = new Ticket();

            // Assert
            ticket.Id.Should().NotBeNullOrWhiteSpace();
            ticket.Created.Should().BeBefore(DateTime.UtcNow);
            ticket.Status.Should().Be(TicketStatus.Unassigned);
        }

        /// <summary>
        /// Add note to the ticket
        /// </summary>
        [Fact]
        public void AddNoteToTicket()
        {
            // Arrange
            var ticket = new Ticket();
            var note = new Note(NoteContent);

            // Act
            ticket.AddNote(note);

            // Assert
            ticket.Notes.Count.Should().Be(1);
            ticket.Notes.Single().Content.Should().Be(NoteContent);
        }

        /// <summary>
        /// The Add Conversation To Ticket
        /// </summary>
        [Fact]
        public void AddConversationToTicket()
        {
            // Arrange
            var ticket = new Ticket();
            var conversation = new Conversation
            {
                Title = ConversationTitle,
                Content = ConversationContent
            };

            // Act
            ticket.AddConversation(conversation);

            // Assert
            ticket.Conversations.Count.Should().Be(1);
            ticket.Conversations.Single().Title.Should().Be(ConversationTitle);
            ticket.Conversations.Single().Content.Should().Be(ConversationContent);
        }

        /// <summary>
        /// Update Ticket Status
        /// </summary>
        /// <param name="status">The status<see cref="TicketStatus"/></param>
        [Theory]
        [InlineData(TicketStatus.Unassigned)]
        [InlineData(TicketStatus.Assigned)]
        [InlineData(TicketStatus.Closed)]
        [InlineData(TicketStatus.Blocked)]
        [InlineData(TicketStatus.Reopened)]
        public void UpdateTicketStatus(TicketStatus status)
        {
            // Arrange
            var ticket = new Ticket();

            // Act
            ticket.UpdateStatus(status);

            // Assert
            ticket.Status.Should().Be(status);
        }

        /// <summary>
        /// Update Ticket Note
        /// </summary>
        [Fact]
        public void UpdateTicketNote()
        {
            // Arrange
            var ticket = new Ticket();
            var note = new Note(NoteContent);
            ticket.AddNote(note);

            // Act
            ticket.UpdateNote(note.Id);

            // Assert
            ticket.Notes.Single(n => n.Id == note.Id).Closed.Should().BeTrue();
        }
    }
}
