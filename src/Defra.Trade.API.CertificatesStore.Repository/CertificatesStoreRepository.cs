// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Context;
using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Defra.Trade.API.CertificatesStore.Repository;

public class CertificatesStoreRepository(CertificatesStoreDbContext context) : ICertificatesStoreRepository
{
    private readonly CertificatesStoreDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<GeneralCertificate> CreateAsync(GeneralCertificate generalCertificate,
        CancellationToken cancellationToken = default)
    {
        _context.GeneralCertificate.Add(generalCertificate);
        await _context.SaveChangesAsync(cancellationToken);

        return generalCertificate;
    }

    public async Task<GeneralCertificate?> GetAsync(string gcId, CancellationToken cancellationToken = default)
    {
        return await _context.GeneralCertificate.FirstOrDefaultAsync(c =>
            c.GeneralCertificateId == gcId,
            cancellationToken);
    }
}
