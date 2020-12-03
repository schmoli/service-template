using System;
using System.Collections.Generic;
using FluentAssertions;
using Schmoli.Services.Core.Data.Postgres;
using Schmoli.Services.Core.Extensions;
using Xunit;

namespace Schmoli.Services.Core.Tests
{
    public class TypeExtensionTests
    {
        [Fact]
        public void GetFriendlyName_Object_ReturnsObject()
        {
            var result = typeof(TypeExtensionTests).GetFriendlyName();
            result.Should().Be("TypeExtensionTests");
        }

        [Fact]
        public void GetFriendlyName_ObjectArray_ReturnsObjectArray()
        {
            var result = typeof(TypeExtensionTests[]).GetFriendlyName();
            result.Should().Be("TypeExtensionTests[]");
        }

        [Fact]
        public void GetFriendlyName_ObjectList_ReturnsObjectList()
        {
            var result = typeof(List<TypeExtensionTests>).GetFriendlyName();
            result.Should().Be("List<TypeExtensionTests>");
        }

        /// <summary>
        /// This test is here to see if .NET ever fixes the need.
        /// Type.Name returns a weird string for generics.
        /// </summary>
        [Fact]
        public void Name_ObjectList_ReturnsBadString()
        {
            var result = typeof(List<TypeExtensionTests>).Name;
            result.Should().Be("List`1");
        }
    }
}
