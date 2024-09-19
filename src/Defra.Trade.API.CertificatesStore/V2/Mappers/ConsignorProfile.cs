// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers;

public class ConsignorProfile : Profile
{
    public ConsignorProfile()
    {
        CreateMap<Dtos.Consignor, Logic.Models.Consignor>()
            .ForMember(
                dest => dest.DefraCustomer,
                opt => opt.MapFrom(src => src.DefraCustomer))
            .ReverseMap();
    }
}