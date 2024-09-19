// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class DefraCustomerOrgInfoValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new DefraCustomerOrgInfoValidator();

        var fixture = new Fixture();

        var request = fixture.Create<DefraCustomerOrgInfo>();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new DefraCustomerOrgInfoValidator();

        var result = itemUnderTest.TestValidate(new DefraCustomerOrgInfo());

        result.ShouldHaveValidationErrorFor(dc => dc.OrgId)
            .WithErrorMessage("'Org Id' must not be empty.");
    }
}