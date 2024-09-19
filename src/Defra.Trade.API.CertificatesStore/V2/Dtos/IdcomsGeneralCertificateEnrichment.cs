// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos.Enrichment;

namespace Defra.Trade.API.CertificatesStore.V2.Dtos;

/// <summary>
/// The General Certificate Enrichment payload
/// </summary>
public class IdcomsGeneralCertificateEnrichment
{
    /// <summary>
    /// The General Certificate Id
    /// </summary>
    public string GcId { get; set; }

    /// <summary>
    /// The applicant details for the General Certificate.
    /// </summary>
    public CustomerContact Applicant { get; set; }

    /// <summary>
    /// Enriched organisation details for the General Certificate.
    /// </summary>
    public IReadOnlyCollection<Organisation> Organisations { get; set; }

    /// <summary>
    /// Enriched establishment details for the General Certificate.
    /// </summary>
    public IReadOnlyCollection<Establishment> Establishments { get; set; }
}