using Xunit;
using FluentAssertions;
using Schmoli.Services.Core.Swagger;
using Microsoft.OpenApi.Models;

namespace Schmoli.Services.Core.Tests
{
    public class ReplaceApiVersionWithValueFilterTests
    {
        [Theory]
        [InlineData("v{apiVersion}", "v1", "v1")]
        [InlineData("v2", "v1", "v2")]
        public void Apply_WithApiVersion_UpdatesApiVersion(string path, string version, string expectedValue)
        {
            OpenApiDocument doc = new OpenApiDocument
            {
                Paths = new OpenApiPaths
                {
                    { path, null }
                },
                Info = new OpenApiInfo
                {
                    Version = version
                }
            };

            var filter = new ReplaceApiVersionWithValueFilter();

            filter.Apply(doc, null);

            doc.Paths.ContainsKey(expectedValue).Should().BeTrue();
        }
    }
}
