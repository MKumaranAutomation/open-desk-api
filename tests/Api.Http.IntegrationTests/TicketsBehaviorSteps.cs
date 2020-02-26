namespace Api.Http.IntegrationTests
{
    using FluentAssertions;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Defines the <see cref="TicketsBehaviorSteps" />
    /// </summary>
    [Binding]
    public class TicketsBehaviorSteps : BaseSteps
    {
        /// <summary>
        /// Defines the `Tickets Count Endpoint`
        /// </summary>
        private string _ticketsCountEndpoint;

        /// <summary>
        /// Defines the _response
        /// </summary>
        private HttpResponseMessage _response;

        /// <summary>
        /// The Gets the Tickets controller
        /// </summary>
        [Given(@"Tickets controller")]
        public void GivenTicketsController()
        {
            _ticketsCountEndpoint = "/api/Tickets/count";
        }

        /// <summary>
        /// When I get the number of tickets available
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [When(@"I get the number of tickets available")]
        public async Task WhenIGetTheNumberOfTicketsAvailable()
        {
            _response = await Client.GetAsync(_ticketsCountEndpoint);
        }

        /// <summary>
        /// Then It should return
        /// </summary>
        /// <param name="value">The value<see cref="int"/></param>
        [Then(@"It should return (.*)")]
        public async Task ThenItShouldReturn(int value)
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await _response.Content.ReadAsStringAsync();
            content.Should().Be(value.ToString());
        }
    }
}
