// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Logic.Helpers;

namespace Defra.Trade.API.CertificatesStore.Logic.Tests.Helpers;

public class DaeraPayloadConstantsTests
{
    [Fact]
    public void DaeraPayloadConstantsS_TypeCode_AsExpected()
    {
        // Arrange
        int expectedTypeCode = 271;

        // Act
        int actualTypeOCde = DaeraPayloadConstants.PackingListPdfTypeCode;

        // Assert
        Assert.Equal(actualTypeOCde, expectedTypeCode);

    }

    [Fact]
    public void DaeraPayloadConstantsS_ListAgencyId_AsExpected()
    {
        // Arrange
        int expectedListAgencyId = 6;

        // Act
        int actualListAgencyId = DaeraPayloadConstants.ListAgencyId;

        // Assert
        Assert.Equal(actualListAgencyId, expectedListAgencyId);

    }
}