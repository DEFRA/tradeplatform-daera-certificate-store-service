// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos.Enrichment;
using FluentAssertions;

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Mappers.Enrichment;

public class EstablishmentProfileTests : EnrichmentMapperTests
{
    [Fact]
    public void EnrichedEstablishment_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<Establishment>();

        // Act
        var actualResult = Mapper.Map<Logic.Models.Enrichment.Establishment>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }
}