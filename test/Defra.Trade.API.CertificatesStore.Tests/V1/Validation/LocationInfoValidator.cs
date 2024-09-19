// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class LocationInfoValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new LocationInfoValidator();

        var fixture = new Fixture();

        var request = fixture.Create<LocationInfo>();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new LocationInfoValidator();

        var result = itemUnderTest.TestValidate(new LocationInfo());

        result.ShouldHaveValidationErrorFor(a => a.IDCOMS)
            .WithErrorMessage("'IDCOMS' must not be empty.");
    }
}