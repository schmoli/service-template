using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Schmoli.Services.Core.Data.Postgres;
using Schmoli.Services.Core.Health;
using Schmoli.Services.Core.Swagger;
using Schmoli.ServiceTemplate.Data;
using Schmoli.ServiceTemplate.Repositories;
using Schmoli.ServiceTemplate.Services;
using Serilog;

namespace Schmoli.ServiceTemplate
{
    public class Startup
    {
        public const int ApiVersionMajor = 1;
        public const int ApiVersionMinor = 0;
        public const string ApiVersionMajorString = "1";
        public const string ApiVersionDescription = "ServiceTemplate API v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceVersion = typeof(Startup)
                    .Assembly
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    .InformationalVersion;

            // Configure Databases
            services.Configure<PostgresOptions>(options =>
            {
                Configuration.GetSection(PostgresOptions.Postgres).Bind(options);
            });

            services.AddDbContext<ServiceDbContext>();

            // Configure Health Checks
            // Get the connection string here for health checks
            var connectionString = Configuration
                .GetSection(PostgresOptions.Postgres)
                .Get<PostgresOptions>()
                .ConnectionString;

            // Configure custom health check
            services
                .AddHealthChecks()
                .AddCheck<LiveCheck>("live", tags: new string[] { "service" })
                // TODO: pick one or the other, both throw during report generation
                .AddDbContextCheck<ServiceDbContext>(
                    "ServiceDbContext",
                    tags: new string[] { "dependencies", "database" })
                .AddNpgSql(
                    npgsqlConnectionString: connectionString,
                    name: "postgres",
                    tags: new string[] { "dependencies", "database" });

            // Example of additional checks you could do
            // .AddCheck("messaging",
            //     () => HealthCheckResult.Healthy("reason"),
            //     tags: new string[] { "dependencies", "messaging" })
            // .AddCheck("cache",
            //     () => HealthCheckResult.Healthy("reason"),
            //     tags: new string[] { "dependencies", "cache" })
            //     ;

            services
                .SwaggerConfiguration(Configuration, $"v{ApiVersionMajor}", serviceVersion)
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddSingleton<LiveCheck>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddTransient<IPrimaryItemService, PrimaryItemService>()
                .AddTransient<ISecondaryItemService, SecondaryItemService>()
                .AddAutoMapper(typeof(Startup))
                .AddRouting(o => o.LowercaseUrls = true)
                .AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddMvc(o =>
            {
                //o.EnableEndpointRouting = true;
            }).AddFluentValidation(c =>
            {
                // no need to call SetValidator for child properties
                c.ImplicitlyValidateChildProperties = true;
                c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly(), lifetime: ServiceLifetime.Singleton);
            });

            services.AddApiVersioning(c =>
            {
                c.AssumeDefaultVersionWhenUnspecified = true;
                c.DefaultApiVersion = new ApiVersion(ApiVersionMajor, ApiVersionMinor);
                c.ReportApiVersions = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("Using Developer Exception Page.");
                app.UseDeveloperExceptionPage();
            }

            // Set up Swagger location, description, and route
            app.SwaggerConfiguration(ApiVersionMajor, ApiVersionDescription);

            app.UseHealthChecks("/health/ready", new HealthCheckOptions
            {
                // Set up Ready check - verify all healthcecks
                Predicate = r => true,
            });

            app.UseHealthChecks("/health/live", new HealthCheckOptions
            {
                // Set up Live check - verify service is up and running
                Predicate = r => r.Name.Contains("live"),
            });

            app.UseSerilogRequestLogging()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
