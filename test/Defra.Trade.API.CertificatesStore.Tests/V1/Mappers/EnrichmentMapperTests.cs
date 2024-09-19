// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.API.CertificatesStore.V2.Mappers;
using Defra.Trade.API.CertificatesStore.V2.Mappers.Enrichment;

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Mappers;

public class EnrichmentMapperTests
{
    protected IMapper Mapper { get; }

    public EnrichmentMapperTests()
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

}