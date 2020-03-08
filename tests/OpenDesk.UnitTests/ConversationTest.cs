namespace OpenDesk.UnitTests
{
    using Domain;
    using FluentAssertions;
    using System;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="ConversationTest" />
    /// </summary>
    public class ConversationTest
    {
        /// <summary>
        /// The Conversation Creation
        /// </summary>
        [Fact]
        public void ConversationCreation()
        {
            // Act
            var conversation = new Conversation();

            // Assert
            conversation.Id.Should().NotBeNullOrWhiteSpace();
            conversation.Created.Should().BeBefore(DateTime.UtcNow);
            conversation.Title.Should().BeNull();
            conversation.Content.Should().BeNull();
        }
    }
}
