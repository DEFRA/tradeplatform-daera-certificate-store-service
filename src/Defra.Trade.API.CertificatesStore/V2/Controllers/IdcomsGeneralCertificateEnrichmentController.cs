// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Extensions;
using Defra.Trade.API.CertificatesStore.Logic.Exceptions;
using Defra.Trade.API.CertificatesStore.V2.Dtos;
using Defra.Trade.API.CertificatesStore.V2.Services.Interfaces;
using Defra.Trade.Common.Api.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Controllers;

/// <summary>
/// Saves enrichment data to the cache store for a General Certificate
/// </summary>
/// <remarks>
/// EhcoGeneralCertificateEnrichmentController Constructor
/// </remarks>
[ApiVersion("1")]
[ApiController]
[Route("idcoms-gc-enrichment")]
public class IdcomsGeneralCertificateEnrichmentController(
    IGeneralCertificateEnrichmentService generalCertificatesEnrichmentService,
    IValidator<IdcomsGeneralCertificateEnrichment> gcValidator) : ControllerBase
{
    private readonly IGeneralCertificateEnrichmentService _generalCertificatesEnrichmentService = generalCertificatesEnrichmentService
        ?? throw new ArgumentNullException(nameof(generalCertificatesEnrichmentService));

    private readonly IValidator<IdcomsGeneralCertificateEnrichment> _gcValidator = gcValidator ?? throw new ArgumentNullException(nameof(gcValidator));

    private const string ExceptionKey = "GeneralCertificateNotFoundException";

    /// <summary>
    /// Save an IDCOMS General Certificate Enrichment to the Trade ReMoS Certificates Cache store.
    /// </summary>
    /// <param name="generalCertificateEnrichment">General Certificate Enrichment payload</param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Successfully saved a General Certificate Enrichment to the Trade ReMoS Certificates Cache store.</response>
    /// <response code="400">The parameters specified were invalid. Please correct before trying again.</response>
    [HttpPost(Name = "SaveIDCOMSGeneralCertificateEnrichment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save([FromBody] IdcomsGeneralCertificateEnrichment generalCertificateEnrichment,
        CancellationToken cancellationToken)
    {
        var validation = await _gcValidator.ValidateAsync(generalCertificateEnrichment, cancellationToken);

        if (!validation.IsValid)
            return this.FluentValidationProblem(validation);

        try
        {
            _ = await _generalCertificatesEnrichmentService.SaveEnrichmentAsync(generalCertificateEnrichment, cancellationToken);

            return NoContent();
        }
        catch (GeneralCertificateNotFoundException ex)
        {
            ModelState.AddModelError(ExceptionKey, ex.Message);

            return ValidationProblem();
        }
    }
}
