using Xunit;
using FluentAssertions;
using Schmoli.Services.Core.Requests;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Schmoli.Services.Core.Tests
{
    public class PagedRequestTests
    {
        [Fact]
        public void Default_PageNumber_ReturnsOne()
        {
            var request = PagedRequest.Default;
            request.PageNumber.Should().Be(1);
        }

        [Fact]
        public void Default_PageSize_ReturnsOneHundred()
        {
            var request = PagedRequest.Default;
            request.PageSize.Should().Be(100);
        }

        [Fact]
        public void PageNumber_Negative_IsInvalid()
        {
            var request = PagedRequest.Default;
            request.PageNumber = -1;
            IList<ValidationResult> results = ValidateModel(request);

            results.Any(
                v => v.MemberNames.Contains(nameof(PagedRequest.PageNumber)) &&
                v.ErrorMessage.Contains("must be between")).Should().BeTrue();
        }

        [Fact]
        public void PageNumber_One_IsValid()
        {
            var request = PagedRequest.Default;
            request.PageNumber = 1;
            IList<ValidationResult> results = ValidateModel(request);

            results.Count.Should().Be(0);
        }

        [Fact]
        public void PageNumber_Int32Max_IsValid()
        {
            var request = PagedRequest.Default;
            request.PageNumber = int.MaxValue;
            IList<ValidationResult> results = ValidateModel(request);

            results.Count.Should().Be(0);
        }

        [Fact]
        public void PageSize_Negative_IsInvalid()
        {
            var request = PagedRequest.Default;
            request.PageSize = -1;
            IList<ValidationResult> results = ValidateModel(request);

            results.Any(
                v => v.MemberNames.Contains(nameof(PagedRequest.PageSize)) &&
                v.ErrorMessage.Contains("must be between")).Should().BeTrue();
        }

        [Fact]
        public void PageSize_One_IsValid()
        {
            var request = PagedRequest.Default;
            request.PageSize = 1;
            IList<ValidationResult> results = ValidateModel(request);

            results.Count.Should().Be(0);
        }

        [Fact]
        public void PageSize_Int32Max_IsValid()
        {
            var request = PagedRequest.Default;
            request.PageSize = int.MaxValue;
            IList<ValidationResult> results = ValidateModel(request);

            results.Count.Should().Be(0);
        }


        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
