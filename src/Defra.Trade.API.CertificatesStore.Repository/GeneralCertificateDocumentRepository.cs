// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Context;
using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Defra.Trade.API.CertificatesStore.Repository;

/// <inheritdoc />
/// <summary>
/// Initializes class object.
/// </summary>
/// <param name="context"></param>
/// <exception cref="ArgumentNullException"></exception>
public class GeneralCertificateDocumentRepository(CertificatesStoreDbContext context) : IGeneralCertificateDocumentRepository
{
    private readonly CertificatesStoreDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<GeneralCertificateDocument> CreateAsync(GeneralCertificateDocument generalCertificateDocument, CancellationToken cancellationToken = default)
    {
        _context.GeneralCertificateDocument.Add(generalCertificateDocument);
        await _context.SaveChangesAsync(cancellationToken);

        return generalCertificateDocument;
    }

    /// <inheritdoc/>
    public async Task<GeneralCertificateDocument> SaveRetrievalAsync(Guid generalCertificateDocumentId, CancellationToken cancellationToken = default)
    {
        var existingGcd = await GetAsync(generalCertificateDocumentId, cancellationToken)
            ?? throw new KeyNotFoundException($"document with ID {generalCertificateDocumentId} not found");

        existingGcd!.Retrieved = DateTime.UtcNow;
        existingGcd!.LastUpdatedOn = DateTime.UtcNow;
        existingGcd!.LastUpdatedSystem = "DaeraCerts";

        return await UpdateAsync(existingGcd, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<GeneralCertificateDocument?> GetAsync(Guid generalCertificateDocumentId, CancellationToken cancellationToken = default)
    {
        return await _context.GeneralCertificateDocument.FirstOrDefaultAsync(gcd => gcd.Id == generalCertificateDocumentId, cancellationToken);
    }

    /// <summary>
    /// Method to update a general certificate attachment.
    /// </summary>
    /// <param name="generalCertificateDocument"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<GeneralCertificateDocument> UpdateAsync(GeneralCertificateDocument generalCertificateDocument, CancellationToken cancellationToken = default)
    {
        var result = _context.Update(generalCertificateDocument);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}
