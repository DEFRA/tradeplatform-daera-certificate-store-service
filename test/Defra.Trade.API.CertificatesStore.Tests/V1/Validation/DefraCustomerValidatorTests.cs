// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class DefraCustomerValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new DefraCustomerValidator();

        var fixture = new Fixture();

        var request = fixture.Create<DefraCustomer>();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new DefraCustomerValidator();

        var result = itemUnderTest.TestValidate(new DefraCustomer());

        result.ShouldHaveValidationErrorFor(dc => dc.OrgId)
            .WithErrorMessage("'Org Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.UserId)
            .WithErrorMessage("'User Id' must not be empty.");
    }
}