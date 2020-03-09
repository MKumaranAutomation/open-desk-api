namespace Api.Http.Swagger.Examples
{
    using Domain;
    using Swashbuckle.AspNetCore.Filters;

    /// <summary>
    /// Defines the <see cref="ConversationExample" />
    /// </summary>
    public class ConversationExample : IExamplesProvider<Conversation>
    {
        /// <summary>
        /// Get Examples
        /// </summary>
        /// <returns>The <see cref="Conversation"/></returns>
        public Conversation GetExamples()
        {
            var conversation = new Conversation
            {
                Title = "Sample Title",
                Content = "Sample Content"
            };

            return conversation;
        }
    }
}
