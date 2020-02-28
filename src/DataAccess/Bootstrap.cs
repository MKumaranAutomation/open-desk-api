namespace DataAccess
{
    using DataAccess.Contracts;
    using DataAccess.Implementations;
    using Microsoft.Extensions.DependencyInjection;
    using Nest;
    using System;

    /// <summary>
    /// Defines the <see cref="Bootstrap" />
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// Initialize IOC
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        /// <param name="options">The options<see cref="BootstrapOptions"/></param>
        public static void Initialize(IServiceCollection services, BootstrapOptions options)
        {
            services.AddTransient<ITicketRepository, TicketRepository>();
            MapEasticClient(services, options.ConnectionString);
        }

        /// <summary>
        /// Map Eastic Client
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        /// <param name="connectionString">The connectionString<see cref="string"/></param>
        private static void MapEasticClient(IServiceCollection services, string connectionString)
        {
            var node = new Uri(connectionString);
            var settings = new ConnectionSettings(node);
            services.AddTransient(_ => new ElasticClient(settings));
        }
    }
}
