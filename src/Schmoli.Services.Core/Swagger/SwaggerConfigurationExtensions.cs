using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Schmoli.Services.Core.Extensions;
using Schmoli.Services.Core.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Schmoli.Services.Core.Swagger
{
    public static class SwaggerConfigurationExtensions
    {
        public static IApplicationBuilder SwaggerConfiguration(this IApplicationBuilder builder, int apiVersion, string apiVersionDescription, string swaggerRoutePrefix = null)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v{apiVersion}/swagger.json", apiVersionDescription);

                // host swagger at app root
                c.RoutePrefix = swaggerRoutePrefix ?? string.Empty;
            });
            return builder;
        }

        /// <summary>
        /// Configure Swagger for this service
        /// </summary>
        public static IServiceCollection SwaggerConfiguration(this IServiceCollection services, IConfiguration configuration, string apiVersion, string serviceVersion)
        {
            var serviceOptions = configuration.GetSection(ServiceOptions.Service)
                                              .Get<ServiceOptions>();

            // Register Swagger Generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(apiVersion, new OpenApiInfo
                {
                    Version = apiVersion,
                    Title = serviceOptions.Name,
                    Description = serviceOptions.Description + $"<p><strong>Service Version: </strong> {serviceVersion}</p>",
                    Contact = new OpenApiContact
                    {
                        Name = serviceOptions.Maintainer.Name,
                        Email = serviceOptions.Maintainer.EmailAddress,
                        Url = !string.IsNullOrWhiteSpace(serviceOptions.Maintainer.Website) ? new Uri(serviceOptions.Maintainer.Website) : null,
                    },
                });
                c.OperationFilter<RemoveApiVersionParameterFilter>();
                c.DocumentFilter<ReplaceApiVersionWithValueFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.CustomSchemaIds(RenameClassForSwagger);
                c.AddFluentValidationRules();

            });

            return services;
        }

        /// <summary>
        /// Provide a way to rename class for display by swagger
        /// 1 : first character of class to be lower case
        /// 2 : remove "Resource" from end of class name
        /// </summary>
        /// <param name="currentClass"></param>
        private static string RenameClassForSwagger(Type currentClass)
        {
            // Gives proper name for generics
            string returnedValue = currentClass.GetFriendlyName();

            // TODO: if i do this here, i need to do it on the $1 lower
            //returnedValue = char.ToLowerInvariant(returnedValue[0]) + returnedValue[1..];

            // remove "Resource" from the end of resource objects or generic types
            returnedValue = Regex.Replace(returnedValue, @"<(.+)Resource>$", "<$1>");
            returnedValue = Regex.Replace(returnedValue, @"Resource$", "");

            return returnedValue;
        }
    }
}
