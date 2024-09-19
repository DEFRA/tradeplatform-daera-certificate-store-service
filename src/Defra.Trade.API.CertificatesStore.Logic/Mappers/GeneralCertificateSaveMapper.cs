// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.API.CertificatesStore.Logic.Helpers;

namespace Defra.Trade.API.CertificatesStore.Logic.Mappers;

public class GeneralCertificateSaveMapper : Profile
{
    public GeneralCertificateSaveMapper()
    {
        CreateMap<Models.EhcoGeneralCertificateApplication, Database.Models.GeneralCertificate>()
            .ForMember(d => d.GeneralCertificateId, opt => opt.MapFrom(s => s.ExchangedDocument.Id))
            .ForMember(d => d.IsActive, opt => opt.MapFrom(s => true))
            .ForMember(d => d.SchemaVersion, opt => opt.MapFrom(s => 2))
            .ForMember(d => d.Data,
                opt => opt.MapFrom(
                    (s, d) => JsonSerializer.Serialize(s, SerializerOptions.GetSerializerOptions())));
    }
}