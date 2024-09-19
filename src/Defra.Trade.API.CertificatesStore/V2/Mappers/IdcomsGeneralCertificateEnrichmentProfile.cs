// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers;

public class IdcomsGeneralCertificateEnrichmentProfile : Profile
{
    public IdcomsGeneralCertificateEnrichmentProfile()
    {
        CreateMap<Logic.Models.IdcomsGeneralCertificateEnrichment, Dtos.IdcomsGeneralCertificateEnrichment>()
            .ReverseMap();
    }
}