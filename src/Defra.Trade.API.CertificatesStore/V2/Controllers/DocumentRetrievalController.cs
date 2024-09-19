// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.ComponentModel.DataAnnotations;
using Defra.Trade.API.CertificatesStore.Logic.Extensions;
using Defra.Trade.API.CertificatesStore.Repository.Interfaces;
using Defra.Trade.Common.Api.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Controllers;

/// <summary>
/// Saves retrieval records of documents requested by DAERA
/// </summary>
[ApiVersion("1")]
[ApiController]
[Route("document-retrieval/{documentId}")]
public sealed class DocumentRetrievalController(
    IGeneralCertificateDocumentRepository generalCertificateDocumentRepository,
    ILogger<DocumentRetrievalController> logger) : ControllerBase
{
    private readonly IGeneralCertificateDocumentRepository _generalCertificateDocumentRepository = generalCertificateDocumentRepository
        ?? throw new ArgumentNullException(nameof(generalCertificateDocumentRepository));

    private readonly ILogger<DocumentRetrievalController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    /// <summary>
    /// Saves that a general certificate document has been retrieved
    /// </summary>
    /// <param name="documentId">The general certificate ID</param>
    /// <param name="cancellationToken"></param>
    /// <response code="204">Successfully saved a document retrieval from DAERA to the Trade ReMoS Certificates Cache store</response>
    /// <response code="400">The parameters specified were invalid. Please correct before trying again</response>
    /// <response code="500">There was an internal server error</response>
    [HttpPut(Name = "SaveDocumentRetrieved")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DocumentRetrieved(
        [FromRoute, Required] Guid documentId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.DocumentRetrievalPutReceived(documentId);
            await _generalCertificateDocumentRepository.SaveRetrievalAsync(documentId, cancellationToken);
            _logger.DocumentRetrievalPutSaved(documentId);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.DocumentRetrievalPutFailure(ex, documentId);
            return Problem(ex.Message);
        }

        return NoContent();
    }

    /// <summary>
    /// Gets a retrieval status for a document
    /// </summary>
    /// <param name="documentId"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Successfully returned document retrieval status</response>
    /// <response code="400">The parameters specified were invalid. Please correct before trying again</response>
    /// <response code="404">The resource ID requested could not be found</response>
    [HttpGet(Name = "GetDocumentRetrieved")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CommonProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRetrieval(
        [FromRoute, Required] Guid documentId,
        CancellationToken cancellationToken = default)
    {
        _logger.DocumentRetrievalStatusRequestReceived(documentId);
        var result = await _generalCertificateDocumentRepository.GetAsync(documentId, cancellationToken);

        if (result is null)
        {
            _logger.DocumentRetrievalDocumentNotFound(documentId);
            return NotFound();
        }

        return result.Retrieved is null
            ? Ok(false)
            : Ok(true);
    }
}
