// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Microsoft.Extensions.Logging;

namespace Defra.Trade.API.CertificatesStore.Logic.Extensions;

public static partial class ILoggerExtensions
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Information, Message = "Starting {EnvironmentName} {ApplicationName} from {ContentRootPath}")]
    public static partial void LogStartup(this ILogger logger, string environmentName, string applicationName, string contentRootPath);

    [LoggerMessage(EventId = 10, Level = LogLevel.Information, Message = "Received EHCO GC Application {GcId} to be saved to cache store")]
    public static partial void GcReceived(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 11, Level = LogLevel.Information, Message = "Detected duplicate EHCO GC Application {GcId} so decided not to save to cache store")]
    public static partial void GcDuplicate(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 12, Level = LogLevel.Information, Message = "Successfully saved EHCO GC Application {GcId} to cache store")]
    public static partial void GcSaved(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 13, Level = LogLevel.Information, Message = "Missing PackingListFileLocation for GC Application {GcId} to cache store")]
    public static partial void GcMissingPackingListFileLocation(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 20, Level = LogLevel.Information, Message = "Received Enrichment for GC Application {GcId} to be saved to cache store")]
    public static partial void EnrichmentReceived(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 21, Level = LogLevel.Information, Message = "No EHCO GC Application {GcId} found in cache store")]
    public static partial void GcMissing(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 22, Level = LogLevel.Information, Message = "Detected duplicate IDCOMS GC Enrichment {GcId} so decided not to save to cache store")]
    public static partial void EnrichmentDuplicate(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 23, Level = LogLevel.Information, Message = "Successfully saved General Certificate Enrichment for {GcId} to cache store")]
    public static partial void EnrichmentSaved(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 30, Level = LogLevel.Information, Message = "Received retrieval PUT request for document ID {DocumentId}")]
    public static partial void DocumentRetrievalPutReceived(this ILogger logger, Guid documentId);

    [LoggerMessage(EventId = 31, Level = LogLevel.Information, Message = "Saved retrieval PUT request for document ID {DocumentId}")]
    public static partial void DocumentRetrievalPutSaved(this ILogger logger, Guid documentId);

    [LoggerMessage(EventId = 32, Level = LogLevel.Error, Message = "Failed to save retrieval PUT request for document ID {DocumentId}")]
    public static partial void DocumentRetrievalPutFailure(this ILogger logger, Exception ex, Guid documentId);

    [LoggerMessage(EventId = 33, Level = LogLevel.Information, Message = "Received retrieval status request for document ID {DocumentId}")]
    public static partial void DocumentRetrievalStatusRequestReceived(this ILogger logger, Guid documentId);

    [LoggerMessage(EventId = 34, Level = LogLevel.Information, Message = "Document ID {DocumentId} not found")]
    public static partial void DocumentRetrievalDocumentNotFound(this ILogger logger, Guid documentId);
}