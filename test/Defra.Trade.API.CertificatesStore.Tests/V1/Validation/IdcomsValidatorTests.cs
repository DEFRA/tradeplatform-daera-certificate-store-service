// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class IdcomsValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new IdcomsValidator();

        var fixture = new Fixture();

        var request = fixture.Create<Idcoms>();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new IdcomsValidator();

        var result = itemUnderTest.TestValidate(new Idcoms());

        result.ShouldHaveValidationErrorFor(a => a.EstablishmentId)
            .WithErrorMessage("'Establishment Id' must not be empty.");
    }
}