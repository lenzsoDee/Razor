// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor.Test.Internal;
using Newtonsoft.Json;
using Xunit;

namespace Microsoft.AspNetCore.Razor.Compilation.TagHelpers
{
    public class TagHelperDescriptorTest
    {
        [Fact]
        public void TagHelperDescriptor_CanBeSerialized()
        {
            // Arrange
            var descriptor = new TagHelperDescriptor
            {
                Prefix = "prefix:",
                TagName = "tag name",
                TypeName = "type name",
                AssemblyName = "assembly name",
                RequiredAttributes = new[]
                {
                    new TagHelperRequiredAttributeDescriptor
                    {
                        Name = "required attribute one",
                        NameComparison = TagHelperRequiredAttributeNameComparison.PrefixMatch
                    },
                    new TagHelperRequiredAttributeDescriptor
                    {
                        Name = "required attribute two",
                        NameComparison = TagHelperRequiredAttributeNameComparison.FullMatch,
                        Value = "something",
                        ValueComparison = TagHelperRequiredAttributeValueComparison.PrefixMatch,
                    }
                },
                AllowedChildren = new[] { "allowed child one" },
                RequiredParent = "parent name",
                DesignTimeDescriptor = new TagHelperDesignTimeDescriptor
                {
                    Summary = "usage summary",
                    Remarks = "usage remarks",
                    OutputElementHint = "some-tag"
                },
            };

            var expectedSerializedDescriptor =
                $"{{\"{ nameof(TagHelperDescriptor.Prefix) }\":\"prefix:\"," +
                $"\"{ nameof(TagHelperDescriptor.TagName) }\":\"tag name\"," +
                $"\"{ nameof(TagHelperDescriptor.FullTagName) }\":\"prefix:tag name\"," +
                $"\"{ nameof(TagHelperDescriptor.TypeName) }\":\"type name\"," +
                $"\"{ nameof(TagHelperDescriptor.AssemblyName) }\":\"assembly name\"," +
                $"\"{ nameof(TagHelperDescriptor.Attributes) }\":[]," +
                $"\"{ nameof(TagHelperDescriptor.RequiredAttributes) }\":" +
                $"[{{\"{ nameof(TagHelperRequiredAttributeDescriptor.Name)}\":\"required attribute one\"," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.NameComparison) }\":1," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.Value) }\":null," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.ValueComparison) }\":0}}," +
                $"{{\"{ nameof(TagHelperRequiredAttributeDescriptor.Name)}\":\"required attribute two\"," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.NameComparison) }\":0," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.Value) }\":\"something\"," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.ValueComparison) }\":2}}]," +
                $"\"{ nameof(TagHelperDescriptor.AllowedChildren) }\":[\"allowed child one\"]," +
                $"\"{ nameof(TagHelperDescriptor.RequiredParent) }\":\"parent name\"," +
                $"\"{ nameof(TagHelperDescriptor.TagStructure) }\":0," +
                $"\"{ nameof(TagHelperDescriptor.DesignTimeDescriptor) }\":{{" +
                $"\"{ nameof(TagHelperDesignTimeDescriptor.Summary) }\":\"usage summary\"," +
                $"\"{ nameof(TagHelperDesignTimeDescriptor.Remarks) }\":\"usage remarks\"," +
                $"\"{ nameof(TagHelperDesignTimeDescriptor.OutputElementHint) }\":\"some-tag\"}}}}";

            // Act
            var serializedDescriptor = JsonConvert.SerializeObject(descriptor);

            // Assert
            Assert.Equal(expectedSerializedDescriptor, serializedDescriptor, StringComparer.Ordinal);
        }

        [Fact]
        public void TagHelperDescriptor_WithAttributes_CanBeSerialized()
        {
            // Arrange
            var descriptor = new TagHelperDescriptor
            {
                Prefix = "prefix:",
                TagName = "tag name",
                TypeName = "type name",
                AssemblyName = "assembly name",
                Attributes = new[]
                {
                   new TagHelperAttributeDescriptor
                   {
                        Name = "attribute one",
                        PropertyName = "property name",
                        TypeName = "property type name",
                        IsEnum = true,
                   },
                    new TagHelperAttributeDescriptor
                   {
                        Name = "attribute two",
                        PropertyName = "property name",
                        TypeName = typeof(string).FullName,
                        IsStringProperty = true
                    },
                },
                TagStructure = TagStructure.NormalOrSelfClosing
            };

            var expectedSerializedDescriptor =
                $"{{\"{ nameof(TagHelperDescriptor.Prefix) }\":\"prefix:\"," +
                $"\"{ nameof(TagHelperDescriptor.TagName) }\":\"tag name\"," +
                $"\"{ nameof(TagHelperDescriptor.FullTagName) }\":\"prefix:tag name\"," +
                $"\"{ nameof(TagHelperDescriptor.TypeName) }\":\"type name\"," +
                $"\"{ nameof(TagHelperDescriptor.AssemblyName) }\":\"assembly name\"," +
                $"\"{ nameof(TagHelperDescriptor.Attributes) }\":[" +
                $"{{\"{ nameof(TagHelperAttributeDescriptor.IsIndexer) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsEnum) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsStringProperty) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.Name) }\":\"attribute one\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.PropertyName) }\":\"property name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.TypeName) }\":\"property type name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.DesignTimeDescriptor) }\":null}}," +
                $"{{\"{ nameof(TagHelperAttributeDescriptor.IsIndexer) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsEnum) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsStringProperty) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.Name) }\":\"attribute two\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.PropertyName) }\":\"property name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.TypeName) }\":\"{ typeof(string).FullName }\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.DesignTimeDescriptor) }\":null}}]," +
                $"\"{ nameof(TagHelperDescriptor.RequiredAttributes) }\":[]," +
                $"\"{ nameof(TagHelperDescriptor.AllowedChildren) }\":null," +
                $"\"{ nameof(TagHelperDescriptor.RequiredParent) }\":null," +
                $"\"{ nameof(TagHelperDescriptor.TagStructure) }\":1," +
                $"\"{ nameof(TagHelperDescriptor.DesignTimeDescriptor) }\":null}}";

            // Act
            var serializedDescriptor = JsonConvert.SerializeObject(descriptor);

            // Assert
            Assert.Equal(expectedSerializedDescriptor, serializedDescriptor, StringComparer.Ordinal);
        }

        [Fact]
        public void TagHelperDescriptor_WithIndexerAttributes_CanBeSerialized()
        {
            // Arrange
            var descriptor = new TagHelperDescriptor
            {
                Prefix = "prefix:",
                TagName = "tag name",
                TypeName = "type name",
                AssemblyName = "assembly name",
                Attributes = new[]
                {
                    new TagHelperAttributeDescriptor
                   {
                        Name = "attribute one",
                        PropertyName = "property name",
                        TypeName = "property type name",
                        IsIndexer = true,
                        IsEnum = true,
                    },
                    new TagHelperAttributeDescriptor
                   {
                        Name = "attribute two",
                        PropertyName = "property name",
                        TypeName = typeof(string).FullName,
                        IsIndexer = true,
                        IsEnum = false,
                        IsStringProperty = true
                    },
                },
                AllowedChildren = new[] { "allowed child one", "allowed child two" },
                RequiredParent = "parent name"
            };

            var expectedSerializedDescriptor =
                $"{{\"{ nameof(TagHelperDescriptor.Prefix) }\":\"prefix:\"," +
                $"\"{ nameof(TagHelperDescriptor.TagName) }\":\"tag name\"," +
                $"\"{ nameof(TagHelperDescriptor.FullTagName) }\":\"prefix:tag name\"," +
                $"\"{ nameof(TagHelperDescriptor.TypeName) }\":\"type name\"," +
                $"\"{ nameof(TagHelperDescriptor.AssemblyName) }\":\"assembly name\"," +
                $"\"{ nameof(TagHelperDescriptor.Attributes) }\":[" +
                $"{{\"{ nameof(TagHelperAttributeDescriptor.IsIndexer) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsEnum) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsStringProperty) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.Name) }\":\"attribute one\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.PropertyName) }\":\"property name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.TypeName) }\":\"property type name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.DesignTimeDescriptor) }\":null}}," +
                $"{{\"{ nameof(TagHelperAttributeDescriptor.IsIndexer) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsEnum) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsStringProperty) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.Name) }\":\"attribute two\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.PropertyName) }\":\"property name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.TypeName) }\":\"{ typeof(string).FullName }\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.DesignTimeDescriptor) }\":null}}]," +
                $"\"{ nameof(TagHelperDescriptor.RequiredAttributes) }\":[]," +
                $"\"{ nameof(TagHelperDescriptor.AllowedChildren) }\":[\"allowed child one\",\"allowed child two\"]," +
                $"\"{ nameof(TagHelperDescriptor.RequiredParent) }\":\"parent name\"," +
                $"\"{ nameof(TagHelperDescriptor.TagStructure) }\":0," +
                $"\"{ nameof(TagHelperDescriptor.DesignTimeDescriptor) }\":null}}";

            // Act
            var serializedDescriptor = JsonConvert.SerializeObject(descriptor);

            // Assert
            Assert.Equal(expectedSerializedDescriptor, serializedDescriptor, StringComparer.Ordinal);
        }

        [Fact]
        public void TagHelperDescriptor_CanBeDeserialized()
        {
            // Arrange
            var serializedDescriptor =
                $"{{\"{nameof(TagHelperDescriptor.Prefix)}\":\"prefix:\"," +
                $"\"{nameof(TagHelperDescriptor.TagName)}\":\"tag name\"," +
                $"\"{nameof(TagHelperDescriptor.FullTagName)}\":\"prefix:tag name\"," +
                $"\"{nameof(TagHelperDescriptor.TypeName)}\":\"type name\"," +
                $"\"{nameof(TagHelperDescriptor.AssemblyName)}\":\"assembly name\"," +
                $"\"{nameof(TagHelperDescriptor.Attributes)}\":[]," +
                $"\"{ nameof(TagHelperDescriptor.RequiredAttributes) }\":" +
                $"[{{\"{ nameof(TagHelperRequiredAttributeDescriptor.Name)}\":\"required attribute one\"," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.NameComparison) }\":1," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.Value) }\":null," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.ValueComparison) }\":0}}," +
                $"{{\"{ nameof(TagHelperRequiredAttributeDescriptor.Name)}\":\"required attribute two\"," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.NameComparison) }\":0," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.Value) }\":\"something\"," +
                $"\"{ nameof(TagHelperRequiredAttributeDescriptor.ValueComparison) }\":2}}]," +
                $"\"{ nameof(TagHelperDescriptor.AllowedChildren) }\":[\"allowed child one\",\"allowed child two\"]," +
                $"\"{ nameof(TagHelperDescriptor.RequiredParent) }\":\"parent name\"," +
                $"\"{nameof(TagHelperDescriptor.TagStructure)}\":2," +
                $"\"{ nameof(TagHelperDescriptor.DesignTimeDescriptor) }\":{{" +
                $"\"{ nameof(TagHelperDesignTimeDescriptor.Summary) }\":\"usage summary\"," +
                $"\"{ nameof(TagHelperDesignTimeDescriptor.Remarks) }\":\"usage remarks\"," +
                $"\"{ nameof(TagHelperDesignTimeDescriptor.OutputElementHint) }\":\"some-tag\"}}}}";
            var expectedDescriptor = new TagHelperDescriptor
            {
                Prefix = "prefix:",
                TagName = "tag name",
                TypeName = "type name",
                AssemblyName = "assembly name",
                RequiredAttributes = new[]
                {
                    new TagHelperRequiredAttributeDescriptor
                    {
                        Name = "required attribute one",
                        NameComparison = TagHelperRequiredAttributeNameComparison.PrefixMatch
                    },
                    new TagHelperRequiredAttributeDescriptor
                    {
                        Name = "required attribute two",
                        NameComparison = TagHelperRequiredAttributeNameComparison.FullMatch,
                        Value = "something",
                        ValueComparison = TagHelperRequiredAttributeValueComparison.PrefixMatch,
                    }
                },
                AllowedChildren = new[] { "allowed child one", "allowed child two" },
                RequiredParent = "parent name",
                DesignTimeDescriptor = new TagHelperDesignTimeDescriptor
                {
                    Summary = "usage summary",
                    Remarks = "usage remarks",
                    OutputElementHint = "some-tag"
                }
            };

            // Act
            var descriptor = JsonConvert.DeserializeObject<TagHelperDescriptor>(serializedDescriptor);

            // Assert
            Assert.NotNull(descriptor);
            Assert.Equal(expectedDescriptor.Prefix, descriptor.Prefix, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.TagName, descriptor.TagName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.FullTagName, descriptor.FullTagName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.TypeName, descriptor.TypeName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.AssemblyName, descriptor.AssemblyName, StringComparer.Ordinal);
            Assert.Empty(descriptor.Attributes);
            Assert.Equal(expectedDescriptor.RequiredAttributes, descriptor.RequiredAttributes, TagHelperRequiredAttributeDescriptorComparer.Default);
            Assert.Equal(
                expectedDescriptor.DesignTimeDescriptor,
                descriptor.DesignTimeDescriptor,
                TagHelperDesignTimeDescriptorComparer.Default);
        }

        [Fact]
        public void TagHelperDescriptor_WithAttributes_CanBeDeserialized()
        {
            // Arrange
            var serializedDescriptor =
                $"{{\"{ nameof(TagHelperDescriptor.Prefix) }\":\"prefix:\"," +
                $"\"{ nameof(TagHelperDescriptor.TagName) }\":\"tag name\"," +
                $"\"{ nameof(TagHelperDescriptor.FullTagName) }\":\"prefix:tag name\"," +
                $"\"{ nameof(TagHelperDescriptor.TypeName) }\":\"type name\"," +
                $"\"{ nameof(TagHelperDescriptor.AssemblyName) }\":\"assembly name\"," +
                $"\"{ nameof(TagHelperDescriptor.Attributes) }\":[" +
                $"{{\"{ nameof(TagHelperAttributeDescriptor.IsIndexer) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsEnum) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsStringProperty) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.Name) }\":\"attribute one\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.PropertyName) }\":\"property name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.TypeName) }\":\"property type name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.DesignTimeDescriptor) }\":null}}," +
                $"{{\"{ nameof(TagHelperAttributeDescriptor.IsIndexer) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsEnum) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsStringProperty) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.Name) }\":\"attribute two\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.PropertyName) }\":\"property name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.TypeName) }\":\"{ typeof(string).FullName }\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.DesignTimeDescriptor) }\":null}}]," +
                $"\"{ nameof(TagHelperDescriptor.RequiredAttributes) }\":[]," +
                $"\"{ nameof(TagHelperDescriptor.AllowedChildren) }\":null," +
                $"\"{ nameof(TagHelperDescriptor.RequiredParent) }\":null," +
                $"\"{nameof(TagHelperDescriptor.TagStructure)}\":0," +
                $"\"{ nameof(TagHelperDescriptor.DesignTimeDescriptor) }\":null}}";
            var expectedDescriptor = new TagHelperDescriptor
            {
                Prefix = "prefix:",
                TagName = "tag name",
                TypeName = "type name",
                AssemblyName = "assembly name",
                Attributes = new[]
                {
                    new TagHelperAttributeDescriptor
                   {
                        Name = "attribute one",
                        PropertyName = "property name",
                        TypeName = "property type name",
                        IsEnum = true,
                    },
                    new TagHelperAttributeDescriptor
                   {
                        Name = "attribute two",
                        PropertyName = "property name",
                        TypeName = typeof(string).FullName,
                        IsEnum = false,
                        IsStringProperty = true
                    },
                },
                AllowedChildren = new[] { "allowed child one", "allowed child two" }
            };

            // Act
            var descriptor = JsonConvert.DeserializeObject<TagHelperDescriptor>(serializedDescriptor);

            // Assert
            Assert.NotNull(descriptor);
            Assert.Equal(expectedDescriptor.Prefix, descriptor.Prefix, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.TagName, descriptor.TagName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.FullTagName, descriptor.FullTagName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.TypeName, descriptor.TypeName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.AssemblyName, descriptor.AssemblyName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.Attributes, descriptor.Attributes, TagHelperAttributeDescriptorComparer.Default);
            Assert.Empty(descriptor.RequiredAttributes);
        }

        [Fact]
        public void TagHelperDescriptor_WithIndexerAttributes_CanBeDeserialized()
        {
            // Arrange
            var serializedDescriptor =
                $"{{\"{ nameof(TagHelperDescriptor.Prefix) }\":\"prefix:\"," +
                $"\"{ nameof(TagHelperDescriptor.TagName) }\":\"tag name\"," +
                $"\"{ nameof(TagHelperDescriptor.FullTagName) }\":\"prefix:tag name\"," +
                $"\"{ nameof(TagHelperDescriptor.TypeName) }\":\"type name\"," +
                $"\"{ nameof(TagHelperDescriptor.AssemblyName) }\":\"assembly name\"," +
                $"\"{ nameof(TagHelperDescriptor.Attributes) }\":[" +
                $"{{\"{ nameof(TagHelperAttributeDescriptor.IsIndexer) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsEnum) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsStringProperty) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.Name) }\":\"attribute one\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.PropertyName) }\":\"property name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.TypeName) }\":\"property type name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.DesignTimeDescriptor) }\":null}}," +
                $"{{\"{ nameof(TagHelperAttributeDescriptor.IsIndexer) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsEnum) }\":false," +
                $"\"{ nameof(TagHelperAttributeDescriptor.IsStringProperty) }\":true," +
                $"\"{ nameof(TagHelperAttributeDescriptor.Name) }\":\"attribute two\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.PropertyName) }\":\"property name\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.TypeName) }\":\"{ typeof(string).FullName }\"," +
                $"\"{ nameof(TagHelperAttributeDescriptor.DesignTimeDescriptor) }\":null}}]," +
                $"\"{ nameof(TagHelperDescriptor.RequiredAttributes) }\":[]," +
                $"\"{ nameof(TagHelperDescriptor.AllowedChildren) }\":null," +
                $"\"{ nameof(TagHelperDescriptor.RequiredParent) }\":null," +
                $"\"{nameof(TagHelperDescriptor.TagStructure)}\":1," +
                $"\"{ nameof(TagHelperDescriptor.DesignTimeDescriptor) }\":null}}";
            var expectedDescriptor = new TagHelperDescriptor
            {
                Prefix = "prefix:",
                TagName = "tag name",
                TypeName = "type name",
                AssemblyName = "assembly name",
                Attributes = new[]
                {
                    new TagHelperAttributeDescriptor
                   {
                        Name = "attribute one",
                        PropertyName = "property name",
                        TypeName = "property type name",
                        IsIndexer = true,
                        IsEnum = true,
                    },
                    new TagHelperAttributeDescriptor
                   {
                        Name = "attribute two",
                        PropertyName = "property name",
                        TypeName = typeof(string).FullName,
                        IsIndexer = true,
                        IsEnum = false,
                        IsStringProperty = true
                    }
                },
                TagStructure = TagStructure.NormalOrSelfClosing
            };

            // Act
            var descriptor = JsonConvert.DeserializeObject<TagHelperDescriptor>(serializedDescriptor);

            // Assert
            Assert.NotNull(descriptor);
            Assert.Equal(expectedDescriptor.Prefix, descriptor.Prefix, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.TagName, descriptor.TagName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.FullTagName, descriptor.FullTagName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.TypeName, descriptor.TypeName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.AssemblyName, descriptor.AssemblyName, StringComparer.Ordinal);
            Assert.Equal(expectedDescriptor.Attributes, descriptor.Attributes, TagHelperAttributeDescriptorComparer.Default);
            Assert.Empty(descriptor.RequiredAttributes);
        }
    }
}
