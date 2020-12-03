using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Schmoli.Services.Core.Results;
using Xunit;

#pragma warning disable 1998
namespace Schmoli.Services.Core.Tests
{
    public class PagedResultSetTests
    {
        [Theory]
        [InlineData(1, 6)]
        [InlineData(2, 3)]
        [InlineData(3, 2)]
        [InlineData(4, 2)]
        [InlineData(5, 2)]
        [InlineData(6, 1)]
        [InlineData(7, 1)]
        public async Task PagedResultSet_TotalPageCount_ReturnsCorrectValue(int pageSize, int expectedResult)
        {
            IQueryable<string> values = new List<string>
            {
                "a","b","c","d","e","f"
            }.AsQueryable();


            var result = await PagedResultSet<string>.CreateAsync(values, 1, pageSize, async x => x.ToList());

            result.TotalPageCount.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(4, 4)]
        public async Task PagedResultSet_PageNumber_ReturnsCorrectValue(int pageNumber, int expectedResult)
        {
            IQueryable<string> values = new List<string>
            {
                "a","b","c","d","e","f"
            }.AsQueryable();

            var result = await PagedResultSet<string>.CreateAsync(values, pageNumber, 1, async x => x.ToList());

            result.PageNumber.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(3, 2, 2)]
        [InlineData(4, 2, 0)]
        [InlineData(1, 3, 3)]
        [InlineData(2, 3, 3)]
        [InlineData(3, 3, 0)]
        [InlineData(1, 4, 4)]
        [InlineData(2, 4, 2)]
        [InlineData(3, 4, 0)]
        [InlineData(1, 6, 6)]
        [InlineData(2, 6, 0)]
        [InlineData(1, 9, 6)]
        [InlineData(2, 9, 0)]
        public async Task PagedResultSet_ItemCount_ReturnsCorrectValue(int pageNumber, int pageSize, int itemCount)
        {
            IQueryable<string> values = new List<string>
            {
                "a","b","c","d","e","f"
            }.AsQueryable();

            var result = await PagedResultSet<string>.CreateAsync(values, pageNumber, pageSize, async x => x.ToList());

            result.Items.Count().Should().Be(itemCount);
        }
    }
}
