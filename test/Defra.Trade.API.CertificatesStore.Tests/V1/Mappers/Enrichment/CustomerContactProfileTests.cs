// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos.Enrichment;
using FluentAssertions;

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Mappers.Enrichment;

public class CustomerContactProfileTests : EnrichmentMapperTests
{
    [Fact]
    public void CustomerContact_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<CustomerContact>();

        // Act
        var actualResult = Mapper.Map<Logic.Models.Enrichment.CustomerContact>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }
}