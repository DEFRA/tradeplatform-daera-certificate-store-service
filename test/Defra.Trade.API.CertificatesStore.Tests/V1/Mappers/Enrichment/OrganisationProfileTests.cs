// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos.Enrichment;
using FluentAssertions;

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Mappers.Enrichment;

public class OrganisationProfileTests : EnrichmentMapperTests
{
    [Fact]
    public void EnrichedOrganisation_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<Organisation>();

        // Act
        var actualResult = Mapper.Map<Logic.Models.Enrichment.Organisation>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }
}