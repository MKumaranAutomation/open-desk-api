namespace Api.Http.IntegrationTests
{
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
        private const string AddConversation = "/api/Tickets/add-conversation";

        private HttpResponseMessage _response;
        private Conversation _conversation;
        private string _id;
        private Ticket _ticket;

        /// <summary>
        /// Given A ticket id
        /// </summary>
        [Given(@"A ticket id")]
        public void GivenATicketId()
        {
            _id = "random";
        }

        /// <summary>
        /// When A conversation is added
        /// </summary>
        /// <returns></returns>
        [When(@"A conversation is added")]
        public async Task WhenAConversationIsAdded()
        {
            var json = JsonConvert.SerializeObject(_conversation);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            _response = await Client.PostAsync($"{AddConversation}?id={_id}", content);
        }

        /// <summary>
        /// Then It is available in the ticket
        /// </summary>
        /// <returns></returns>
        [Then(@"It is available in the ticket")]
        public async Task ThenItIsAvailableInTheTicket()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            _ticket = JsonConvert.DeserializeObject<Ticket>(await _response.Content.ReadAsStringAsync());
            _ticket.Should().NotBeNull();

            var conversation = _ticket.Conversations.LastOrDefault();
            conversation.Should().NotBeNull();
            conversation?.Title.Should().Be(_conversation.Title);
            conversation?.Content.Should().Be(_conversation.Content);
        }

        /// <summary>
        /// When Reading ticket by a valid id
        /// </summary>
        [When(@"Reading ticket by a valid id")]
        public async Task WhenReadingTicketByAValidId()
        {
            _response = await Client.GetAsync($"{ReadTicket}?id={_id}");
        }

        /// <summary>
        /// Then It should return a ticket
        /// </summary>
        /// <returns></returns>
        [Then(@"It should return a ticket")]
        public async Task ThenItShouldReturnATicket()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            _ticket = JsonConvert.DeserializeObject<Ticket>(await _response.Content.ReadAsStringAsync());
            _ticket.Should().NotBeNull();
        }

        /// <summary>
        /// Given A Conversation
        /// </summary>
        [Given(@"A Conversation")]
        public void GivenAConversation()
        {
            _conversation = new Conversation
            {
                Title = "Hello",
                Content = "World"
            };
        }

        /// <summary>
        /// When I create a new Ticket
        /// </summary>
        [When(@"I create a new Ticket")]
        public async Task WhenICreateANewTicket()
        {
            var json = JsonConvert.SerializeObject(_conversation);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            _response = await Client.PostAsync(CreateTicket, content);
        }

        /// <summary>
        /// Then A Ticket should have been created
        /// </summary>
        [Then(@"A Ticket should have been created")]
        public async Task ThenATicketShouldHaveBeenCreated()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            _ticket = JsonConvert.DeserializeObject<Ticket>(await _response.Content.ReadAsStringAsync());
            _ticket.Should().NotBeNull();
        }

        /// <summary>
        /// Then Its status should be Unassigned
        /// </summary>
        [Then(@"Its status should be Unassigned")]
        public void ThenItsStatusShouldBeUnassigned()
        {
            _ticket.Status.Should().Be(TicketStatus.Unassigned);
        }

        /// <summary>
        /// Then It should have [count]conversation
        /// </summary>
        /// <param name="count">The count<see cref="int"/></param>
        [Then(@"It should have (.*) conversation")]
        public void ThenItShouldHaveConversation(int count)
        {
            _ticket.Conversations.Count.Should().Be(count);
        }

        /// <summary>
        /// Then It should have only [count]notes
        /// </summary>
        /// <param name="count">The count<see cref="int"/></param>
        [Then(@"It should have (.*) notes")]
        public void ThenItShouldHaveNotes(int count)
        {
            _ticket.Notes.Count.Should().Be(count);
        }

    }
}
