// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;

namespace Defra.Trade.API.CertificatesStore.Repository.Interfaces;

public interface ICertificatesStoreRepository
{
    Task<GeneralCertificate> CreateAsync(GeneralCertificate generalCertificate, CancellationToken cancellationToken = default);

    Task<GeneralCertificate?> GetAsync(string gcId, CancellationToken cancellationToken = default);
}