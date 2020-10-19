using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Schmoli.Services.Core.Swagger
{
    /// <summary>
    /// Removes the "apiVersion" parameter from the swagger page.
    /// </summary>
    public class RemoveApiVersionParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "apiVersion");
            operation.Parameters.Remove(versionParameter);
        }
    }
}
