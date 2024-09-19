﻿// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class ConsigneeValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new ConsigneeValidator();

        var fixture = new Fixture();

        var request = fixture.Create<Consignee>();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new ConsigneeValidator();

        var result = itemUnderTest.TestValidate(new Consignee());

        result.ShouldHaveValidationErrorFor(a => a.DefraCustomer)
            .WithErrorMessage("'Defra Customer' must not be empty.");
    }
}