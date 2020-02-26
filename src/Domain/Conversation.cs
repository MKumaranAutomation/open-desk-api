namespace Domain
{
    using System;

    /// <summary>
    /// Defines the <see cref="Conversation" />
    /// </summary>
    public class Conversation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Conversation"/> class.
        /// </summary>
        public Conversation()
        {
            Created = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the `Id`
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets the `Created`
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Gets or sets the `Title`
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the `Content`
        /// </summary>
        public string Content { get; set; }
    }
}
