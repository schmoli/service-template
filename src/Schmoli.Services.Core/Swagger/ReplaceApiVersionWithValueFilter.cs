using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Schmoli.Services.Core.Swagger
{
    /// <summary>
    /// Replaces the string 'apiVersion' with correct value
    /// </summary>
    public class ReplaceApiVersionWithValueFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                paths.Add(path.Key.Replace("v{apiVersion}", swaggerDoc.Info.Version), path.Value);
            }
            swaggerDoc.Paths = paths;
        }
    }
}
