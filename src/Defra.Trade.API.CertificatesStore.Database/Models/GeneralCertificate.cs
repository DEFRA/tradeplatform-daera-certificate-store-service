// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Database.Models;

public class GeneralCertificate : BaseTable
{
    public Guid Id { get; set; }

    public string? GeneralCertificateId { get; set; }

    public int SchemaVersion { get; set; }

    public string? Data { get; set; }

    public virtual EnrichmentData? EnrichmentData { get; set; }
}