namespace Domain
{
    using System;

    /// <summary>
    /// Defines the <see cref="Note" />
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        /// <param name="content">The content<see cref="string"/></param>
        public Note(string content)
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.UtcNow;
            Content = content;
            Closed = false;
        }

        /// <summary>
        /// Gets or sets the `Id`
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the `Created Date`
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Gets the `Content`
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Gets a value indicating whether `Closed`
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// `Close` the note
        /// </summary>
        public void Flip()
        {
            Closed = !Closed;
        }
    }
}
