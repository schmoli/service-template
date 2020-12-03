using FluentAssertions;
using Microsoft.OpenApi.Models;
using Schmoli.Services.Core.Swagger;
using Xunit;

namespace Schmoli.Services.Core.Tests
{

    public class RemoveApiVersionParameterFilterTests
    {
        [Theory]
        [InlineData("apiVersion", 0)]
        [InlineData("NotApiVersion", 1)]
        public void Apply_WithApiVersion_RemovesApiVersion(string parameter, int expectedCount)
        {
            var operation = new OpenApiOperation();
            operation.Parameters.Add(new OpenApiParameter() { Name = parameter });

            var filter = new RemoveApiVersionParameterFilter();

            filter.Apply(operation, null);

            operation.Parameters.Count.Should().Be(expectedCount);
        }
    }
}
