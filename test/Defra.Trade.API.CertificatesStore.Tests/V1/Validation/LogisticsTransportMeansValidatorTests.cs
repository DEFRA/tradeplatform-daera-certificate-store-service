// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class LogisticsTransportMeansValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new LogisticsTransportMeansValidator();

        var fixture = new Fixture();

        var request = fixture.Create<LogisticsTransportMeans>();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new LogisticsTransportMeansValidator();

        var result = itemUnderTest.TestValidate(new LogisticsTransportMeans());

        result.ShouldHaveValidationErrorFor(a => a.Id)
           .WithErrorMessage("'Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.ModeCode)
           .WithErrorMessage("'Mode Code' must not be empty.");
    }

    [Fact]
    public void Validate_IsEmpty_Error()
    {
        var itemUnderTest = new LogisticsTransportMeansValidator();

        var fixture = new Fixture();

        var request = fixture.Build<LogisticsTransportMeans>()
            .With(lte => lte.Id, string.Empty)
            .With(lte => lte.ModeCode, string.Empty)
            .Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldHaveValidationErrorFor(a => a.Id)
           .WithErrorMessage("'Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.ModeCode)
           .WithErrorMessage("'Mode Code' must not be empty.");
    }
}