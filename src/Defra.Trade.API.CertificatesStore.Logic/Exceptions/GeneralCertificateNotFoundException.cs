// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Logic.Exceptions;

public class GeneralCertificateNotFoundException : Exception
{
    public GeneralCertificateNotFoundException() : base()
    {
    }

    public GeneralCertificateNotFoundException(string message) : base(message)
    {
    }

    public GeneralCertificateNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}