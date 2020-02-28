namespace Api.Http.IntegrationTests
{
    using System;
    using Domain;
    using FluentAssertions;
    using Newtonsoft.Json;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;
    using System.Threading.Tasks;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Defines the <see cref="TicketsBehaviorSteps" />
    /// </summary>
    [Binding]
    public class TicketsBehaviorSteps : BaseSteps
    {
        private const string CreateTicket = "/api/Tickets/create";
        private const string ReadTicket = "/api/Tickets/read";
        private const string UpdateStatus = "/api/Tickets/update-status";
        private const string AddConversation = "/api/Tickets/add-conversation";
        private const string AddNote = "/api/Tickets/add-note";
        private const string UpdateNote = "/api/Tickets/update-note";

        private HttpResponseMessage _response;
        private Conversation _conversation;
        private Conversation _newConversation;
        private Note _newNote;
        private Ticket _ticket;

        private string _id;

        /// <summary>
        /// Given A Conversation title and content
        /// </summary>
        [Given(@"A Conversation title and content")]
        public void GivenAConversationTitleAndContent()
        {
            _conversation = new Conversation
            {
                Title = "Hello",
                Content = "World"
            };
        }

        /// <summary>
        /// When Create a new ticket
        /// </summary>
        [When(@"Create a new ticket")]
        public async Task WhenCreateANewTicket()
        {
            var json = JsonConvert.SerializeObject(_conversation);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            _response = await Client.PostAsync(CreateTicket, content);
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// When Read the created ticket
        /// </summary>
        [When(@"Read the created ticket")]
        public async Task WhenReadTheCreatedTicket()
        {
            _ticket = JsonConvert.DeserializeObject<Ticket>(await _response.Content.ReadAsStringAsync());
            _response = await Client.GetAsync($"{ReadTicket}?id={_ticket.Id}");
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// The ticket should have (.*) conversation and (.*) notes and status should be Unassigned
        /// </summary>
        /// <param name="conversationCount">Conversation count</param>
        /// <param name="notesCount">Notes count</param>
        [Then(@"The ticket should have (.*) conversation and (.*) notes and status should be Unassigned")]
        public async Task ThenTheTicketShouldHaveConversationAndNotes(int conversationCount, int notesCount)
        {
            _ticket = JsonConvert.DeserializeObject<Ticket>(await _response.Content.ReadAsStringAsync());

            _ticket.Should().NotBeNull();
            _ticket.Notes.Count.Should().Be(notesCount);
            _ticket.Conversations.Count.Should().Be(conversationCount);
            _ticket.Conversations.FirstOrDefault().Should().NotBeNull();
            _ticket.Conversations.First().Title.Should().Be(_conversation.Title);
            _ticket.Conversations.First().Content.Should().Be(_conversation.Content);
            _ticket.Status.Should().Be(TicketStatus.Unassigned);
        }

        /// <summary>
        /// Given A random ticket id
        /// </summary>
        [Given(@"A random ticket id")]
        public void GivenARandomTicketId()
        {
            _id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// When Read the ticket
        /// </summary>
        [When(@"Read the ticket")]
        public async Task WhenReadTheTicket()
        {
            _response = await Client.GetAsync($"{ReadTicket}?id={_id}");
        }

        /// <summary>
        /// Then Should return NotFound
        /// </summary>
        [Then(@"Should return NotFound")]
        public void ThenShouldReturnNotFound()
        {

            _response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Update Ticket Status to [status]
        /// </summary>
        /// <param name="status"></param>
        [When(@"Update Ticket Status to (.*)")]
        public async Task WhenUpdateTicketStatusTo(TicketStatus status)
        {
            _response = await Client.PutAsync($"{UpdateStatus}?id={_ticket.Id}&status={status}", null);
        }

        /// <summary>
        /// Ticket status should be [status]
        /// </summary>
        [Then(@"Ticket status should be (.*)")]
        public void ThenTicketStatusShouldBe(TicketStatus status)
        {
            _ticket.Status.Should().Be(status);
        }

        /// <summary>
        /// When Add a new conversation
        /// </summary>
        [When(@"Add a new conversation")]
        public async Task WhenAddANewConversation()
        {
            _newConversation = new Conversation
            {
                Content = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString()
            };

            var json = JsonConvert.SerializeObject(_newConversation);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            _response = await Client.PostAsync($"{AddConversation}?id={_ticket.Id}", content);
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Then Ticket should contain added conversation
        /// </summary>
        [Then(@"Ticket should contain added conversation")]
        public void ThenTicketShouldContainAddedConversation()
        {
            _ticket.Conversations.Count.Should().Be(2);
            _ticket.Conversations
                .Any(c => c.Title == _newConversation.Title && c.Content == _newConversation.Content)
                .Should()
                .BeTrue();
        }

        /// <summary>
        /// When Add a new note
        /// </summary>
        [When(@"Add a new note")]
        public async Task WhenAddANewNote()
        {
            _newNote = new Note(Guid.NewGuid().ToString());

            var json = JsonConvert.SerializeObject(_newNote);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            _response = await Client.PostAsync($"{AddNote}?id={_ticket.Id}", content);
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Then Ticket should contain added note
        /// </summary>
        [Then(@"Ticket should contain added note and its status is not closed")]
        public void ThenTicketShouldContainAddedNote()
        {
            _ticket.Notes.Count.Should().Be(1);
            _ticket.Notes
                .Any(n => n.Content == _newNote.Content && !n.Closed)
                .Should()
                .BeTrue();
        }

        /// <summary>
        /// When Set note status to Closed / Open
        /// </summary>
        [When(@"Set note status to Closed")]
        public async Task WhenSetNoteStatusTo()
        {
            _response = await Client.PutAsync($"{UpdateNote}?id={_ticket.Id}&noteId={_newNote.Id}", null);
        }

        /// <summary>
        /// Then Note status is Closed
        /// </summary>
        [Then(@"Note status is Closed")]
        public void ThenNoteStatusIs()
        {
            _ticket.Notes.Count.Should().Be(1);
            _ticket.Notes.First(n => n.Id == _newNote.Id).Closed.Should().BeTrue();
        }
    }
}
