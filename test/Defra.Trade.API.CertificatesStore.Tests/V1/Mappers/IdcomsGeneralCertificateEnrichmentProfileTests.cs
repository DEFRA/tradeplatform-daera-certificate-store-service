// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.API.CertificatesStore.V2.Mappers;
using Defra.Trade.API.CertificatesStore.V2.Mappers.Enrichment;
using FluentAssertions;

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Mappers;

public class IdcomsGeneralCertificateEnrichmentProfileTests : EnrichmentMapperTests
{
    protected new IMapper Mapper { get; }

    public IdcomsGeneralCertificateEnrichmentProfileTests()
    {
        var profiles =
            new List<Profile>
            {
                new IdcomsGeneralCertificateEnrichmentProfile(),
                new CustomerContactProfile(),
                new AddressProfile(),
                new CountryProfile(),
                new EstablishmentProfile(),
                new OrganisationProfile()
            };
        var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
        Mapper = config.CreateMapper();
    }

    [Fact]
    public void IdcomsGeneralCertificateEnrichment_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<IdcomsGeneralCertificateEnrichment>();

        // Act
        var actualResult = Mapper.Map<Logic.Models.IdcomsGeneralCertificateEnrichment>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }
}