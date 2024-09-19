// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers.Enrichment;

public class EstablishmentProfile : Profile
{
    public EstablishmentProfile()
    {
        CreateMap<Dtos.Enrichment.Establishment, Logic.Models.Enrichment.Establishment>()
            .ReverseMap();
    }
}