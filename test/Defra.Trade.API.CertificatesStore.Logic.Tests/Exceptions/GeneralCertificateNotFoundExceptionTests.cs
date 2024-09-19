// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Logic.Exceptions;

namespace Defra.Trade.API.CertificatesStore.Logic.Tests.Exceptions;

public class GeneralCertificateNotFoundExceptionTests
{
    [Fact]
    public void Constructor_NoArgs_Success()
    {
        Assert.NotNull(new GeneralCertificateNotFoundException());
    }

    [Fact]
    public void Constructor_Message_Success()
    {
        var ex = new GeneralCertificateNotFoundException("Test Exception Message.");

        Assert.NotNull(ex);
        Assert.Equal("Test Exception Message.", ex.Message);
    }

    [Fact]
    public void Constructor_MessageWithInnerException_Success()
    {
        var ex = new GeneralCertificateNotFoundException("Test Exception Message and Inner Exception.", new InvalidOperationException());

        Assert.NotNull(ex);
        Assert.Equal("Test Exception Message and Inner Exception.", ex.Message);
        Assert.IsType<InvalidOperationException>(ex.InnerException);
    }
}