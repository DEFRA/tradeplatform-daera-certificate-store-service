// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers.Enrichment;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Dtos.Enrichment.Address, Logic.Models.Enrichment.Address>()
            .ReverseMap();
    }
}