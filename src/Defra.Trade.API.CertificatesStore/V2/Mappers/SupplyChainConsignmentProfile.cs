// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers;

public class SupplyChainConsignmentProfile : Profile
{
    public SupplyChainConsignmentProfile()
    {
        CreateMap<Dtos.SupplyChainConsignment, Logic.Models.SupplyChainConsignment>()
            .ForMember(dest => dest.UsedTransportEquipment, optns => optns.MapFrom(source => source.UsedTransportEquipment))
            .ReverseMap();
    }
}