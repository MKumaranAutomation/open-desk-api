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

    /// <summary>
    /// Defines the <see cref="Startup" />
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The ConfigureServices
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            Services.Bootstrap.Initialize(services, new BootstrapOptions());

            var swaggerConfig = Configuration.GetSection("SwaggerConfiguration");

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
            app.UseSwagger();
            var swaggerConfig = Configuration.GetSection("SwaggerConfiguration");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerConfig["ApiName"]}/swagger.json", $"{swaggerConfig["Title"]} v{swaggerConfig["Version"]}");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
