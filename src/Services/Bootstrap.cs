namespace Services
{
    using Microsoft.Extensions.DependencyInjection;
    using Services.Contracts;
    using Services.Implementations;

    /// <summary>
    /// Defines the <see cref="Services.Bootstrap" />
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// The Initialize
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        /// <param name="options">The options<see cref="BootstrapOptions"/></param>
        public static void Initialize(IServiceCollection services, BootstrapOptions options)
        {
            services.AddTransient<ITicketService, TicketService>();
        }
    }
}
