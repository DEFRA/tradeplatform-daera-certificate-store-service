// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Extensions;
using Defra.Trade.API.CertificatesStore.V2.Dtos;
using Defra.Trade.API.CertificatesStore.V2.Services.Interfaces;
using Defra.Trade.Common.Api.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Controllers;

/// <summary>
/// Processing General Certificates received from EHCO.
/// </summary>
/// <remarks>
/// EhcoGeneralCertificateApplicationController Constructor
/// </remarks>
[ApiVersion("1")]
[ApiController]
[Route("ehco-gc-application")]
public class EhcoGeneralCertificateApplicationController(
    IGeneralCertificatesService generalCertificatesService,
    IValidator<EhcoGeneralCertificateApplication> gcValidator) : ControllerBase
{
    private readonly IGeneralCertificatesService _generalCertificatesService = generalCertificatesService
        ?? throw new ArgumentNullException(nameof(generalCertificatesService));

    private readonly IValidator<EhcoGeneralCertificateApplication> _gcValidator = gcValidator
        ?? throw new ArgumentNullException(nameof(gcValidator));

    /// <summary>
    /// Save an EHCO General Certificate Application to the Trade ReMoS Certificates Cache store.
    /// </summary>
    /// <param name="generalCertificate">General Certificate payload</param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Successfully saved a General Certificate Application to the Trade ReMoS Certificates Cache store.</response>
    /// <response code="400">The parameters specified were invalid. Please correct before trying again.</response>
    [HttpPost(Name = "SaveEHCOGeneralCertificateApplication")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(
        [FromBody] EhcoGeneralCertificateApplication generalCertificate,
        CancellationToken cancellationToken = default)
    {
        var validation = await _gcValidator.ValidateAsync(generalCertificate, cancellationToken);

        if (!validation.IsValid)
            return this.FluentValidationProblem(validation);

        if (string.IsNullOrWhiteSpace(generalCertificate.ExchangedDocument.CertificatePDFLocation))
        {
            return BadRequest("Invalid GC PDF Location");
        }

        if (!Uri.TryCreate(generalCertificate.ExchangedDocument.CertificatePDFLocation, UriKind.Absolute, out var outUri)
            && outUri.Scheme != Uri.UriSchemeHttps)
        {
            return BadRequest("Invalid GC PDF Location");
        }

        await _generalCertificatesService.SaveGeneralCertificateAsync(generalCertificate, cancellationToken);

        return NoContent();
    }
}
