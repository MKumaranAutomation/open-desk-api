namespace OpenDesk.UnitTests
{
    using Domain;
    using FluentAssertions;
    using System;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="NoteTest" />
    /// </summary>
    public class NoteTest
    {
        /// <summary>
        /// Defines the NoteContent
        /// </summary>
        private const string NoteContent = "Note Content";

        /// <summary>
        /// Note Creation
        /// </summary>
        [Fact]
        public void NoteCreation()
        {
            // Act
            var note = new Note(NoteContent);

            // Assert
            note.Content.Should().Be(NoteContent);
            note.Created.Should().BeBefore(DateTime.UtcNow);
            note.Id.Should().NotBeNullOrWhiteSpace();
            note.Closed.Should().BeFalse();
        }

        /// <summary>
        /// Note Flip
        /// </summary>
        [Fact]
        public void NoteFlip()
        {
            // Arrange
            var note = new Note(NoteContent);

            // Act
            note.Flip();

            // Assert
            note.Closed.Should().BeTrue();
        }
    }
}
