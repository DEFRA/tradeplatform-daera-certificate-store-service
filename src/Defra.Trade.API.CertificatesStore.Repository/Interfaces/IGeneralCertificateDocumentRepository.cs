// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;

namespace Defra.Trade.API.CertificatesStore.Repository.Interfaces;

/// <summary>
/// Contract for Gc attachment repository.
/// </summary>
public interface IGeneralCertificateDocumentRepository
{
    /// <summary>
    /// Method to save general certificate attachment.
    /// </summary>
    /// <param name="generalCertificateDocument"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<GeneralCertificateDocument> CreateAsync(GeneralCertificateDocument generalCertificateDocument, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to get a general certificate attachment.
    /// </summary>
    /// <param name="generalCertificateDocumentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<GeneralCertificateDocument?> GetAsync(Guid generalCertificateDocumentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Method to save a general certificate attachment retrieval.
    /// </summary>
    /// <param name="generalCertificateDocumentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<GeneralCertificateDocument> SaveRetrievalAsync(Guid generalCertificateDocumentId, CancellationToken cancellationToken = default);
}