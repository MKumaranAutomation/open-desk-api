﻿namespace Api.Http.IntegrationTests
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

        private HttpResponseMessage _response;
        private Conversation _conversation;
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
    }
}
