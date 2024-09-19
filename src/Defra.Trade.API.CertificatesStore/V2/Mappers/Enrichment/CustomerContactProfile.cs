// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers.Enrichment;

public class CustomerContactProfile : Profile
{
    public CustomerContactProfile()
    {
        CreateMap<Dtos.Enrichment.CustomerContact, Logic.Models.Enrichment.CustomerContact>()
            .ReverseMap();
    }
}