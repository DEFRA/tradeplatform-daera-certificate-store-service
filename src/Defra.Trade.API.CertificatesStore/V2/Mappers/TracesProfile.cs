// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers;

public class TracesProfile : Profile
{
    public TracesProfile()
    {
        CreateMap<Dtos.Traces, Logic.Models.Traces>()
            .ReverseMap();
    }
}