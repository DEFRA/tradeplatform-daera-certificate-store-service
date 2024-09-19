// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Repository.Interfaces;

public enum EnrichmentCreateResult
{
    Success,
    UnknownCertificate,
    CertificateMismatch,
    AlreadyEnriched
}