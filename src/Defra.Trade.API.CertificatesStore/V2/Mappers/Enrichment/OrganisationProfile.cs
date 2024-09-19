// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers.Enrichment;

public class OrganisationProfile : Profile
{
    public OrganisationProfile()
    {
        CreateMap<Dtos.Enrichment.Organisation, Logic.Models.Enrichment.Organisation>()
            .ReverseMap();
    }
}