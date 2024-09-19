// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers;

public class ConsigneeProfile : Profile
{
    public ConsigneeProfile()
    {
        CreateMap<Dtos.Consignee, Logic.Models.Consignee>()
            .ReverseMap();
    }
}