// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Mappers;

public class DefraCustomerOrgInfoProfile : Profile
{
    public DefraCustomerOrgInfoProfile()
    {
        CreateMap<Dtos.DefraCustomerOrgInfo, Logic.Models.DefraCustomerOrgInfo>()
            .ReverseMap();
    }
}