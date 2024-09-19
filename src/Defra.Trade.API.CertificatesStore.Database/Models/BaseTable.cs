// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Database.Models;

public class BaseTable
{
    public DateTimeOffset CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? CreatedSystem { get; set; }

    public DateTimeOffset? LastUpdatedOn { get; set; }

    public string? LastUpdatedBy { get; set; }

    public string? LastUpdatedSystem { get; set; }

    public bool IsActive { get; set; }
}