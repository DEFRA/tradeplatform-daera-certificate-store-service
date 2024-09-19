// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.Logic.Helpers;

namespace Defra.Trade.API.CertificatesStore.Logic.Mappers;

public class GeneralCertificateEnrichmentSaveMapper : Profile
{
    public GeneralCertificateEnrichmentSaveMapper()
    {
        CreateMap<Models.IdcomsGeneralCertificateEnrichment, EnrichmentData>()
            .ForMember(d => d.GeneralCertificate,
                opt => opt.MapFrom(s => new GeneralCertificate { GeneralCertificateId = s.GcId }))
            .ForMember(d => d.SchemaVersion, opt => opt.MapFrom(s => 1))
            .ForMember(d => d.Data, opt => opt.MapFrom((s, d) => JsonSerializer.Serialize(s, SerializerOptions.GetSerializerOptions())))
            .ForMember(d => d.IsActive, opt => opt.MapFrom(s => true));
    }
}