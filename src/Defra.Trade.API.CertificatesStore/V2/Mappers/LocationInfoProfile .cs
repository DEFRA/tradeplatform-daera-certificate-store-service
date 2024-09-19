// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers;

public class LocationInfoProfile : Profile
{
    public LocationInfoProfile()
    {
        CreateMap<Dtos.LocationInfo, Logic.Models.LocationInfo>()
            .ReverseMap();
    }
}