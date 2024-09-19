// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers.Enrichment;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Dtos.Enrichment.Country, Logic.Models.Enrichment.Country>()
            .ReverseMap();
    }
}