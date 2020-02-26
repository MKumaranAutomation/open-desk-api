namespace Api.Http.IntegrationTests
{
    using Domain;
    using FluentAssertions;
    using Newtonsoft.Json;
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
        private HttpResponseMessage _response;
        private Conversation _conversation;
        private Ticket _ticket;

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
        /// <returns>The <see cref="Task"/></returns>
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
        /// <returns>The <see cref="Task"/></returns>
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
        /// <returns>The <see cref="Task"/></returns>
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
