namespace Api.Http
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json.Converters;
    using Services;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Mime;
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    /// <summary>
    /// Defines the <see cref="Startup" />
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets the Configuration
        /// </summary>
        public IConfiguration Configuration { get; private set; }

        /// <summary>
        /// The ConfigureServices
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("ElasticConnect");

            Services.Bootstrap.Initialize(
                services,
                new BootstrapOptions
                {
                    ConnectionString = connectionString
                });

            var swaggerConfig = Configuration.GetSection("SwaggerConfiguration");

            services
                .AddHealthChecksUI()
                .AddHealthChecks()
                .AddElasticsearch(
                    setup => { setup.UseServer(connectionString); },
                    "ElasticSearch",
                    HealthStatus.Unhealthy);

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(
                    swaggerConfig["ApiName"],
                    new OpenApiInfo
                    {
                        Title = swaggerConfig["Title"],
                        Version = swaggerConfig["Version"],
                        Description =
                            swaggerConfig["Description"],
                        Contact = new OpenApiContact
                        {
                            Name = swaggerConfig["ContactName"],
                            Url = new Uri(swaggerConfig["ContactUrl"])
                        }
                    });

                Directory.GetFiles(
                        AppContext.BaseDirectory,
                        "*.xml",
                        SearchOption.TopDirectoryOnly)
                    .ToList()
                    .ForEach(x => { swagger.IncludeXmlComments(x); });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvc(options =>
                {
                    options.Filters.Clear();
                    options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
                    options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
        }

        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="app">The application builder<see cref="IApplicationBuilder"/></param>
        /// <param name="env">The environment<see cref="IWebHostEnvironment"/></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app
                .UseHealthChecks(
                    "/hc",
                    new HealthCheckOptions
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });

            app.UseHealthChecksUI(config => { config.UIPath = "/hc-ui"; });

            app.UseSwagger();
            var swaggerConfig = Configuration.GetSection("SwaggerConfiguration");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerConfig["ApiName"]}/swagger.json",
                    $"{swaggerConfig["Title"]} v{swaggerConfig["Version"]}");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthorization();

            app
                .UseEndpoints(config =>
                {
                    config.MapDefaultControllerRoute();
                    config.MapControllers();
                });
        }
    }
}
