// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class OperatorValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new OperatorValidator();

        var fixture = new Fixture();

        var request = fixture.Build<Operator>().Without(x => x.Traces).Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new OperatorValidator();

        var result = itemUnderTest.TestValidate(new Operator());

        result.ShouldHaveValidationErrorFor(a => a.Name)
            .WithErrorMessage("'Name' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.Postcode)
            .WithErrorMessage("'Postcode' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.LineOne)
            .WithErrorMessage("'Line One' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.CityName)
            .WithErrorMessage("'City Name' must not be empty.");
    }

    [Fact]
    public void Validate_IsEmpty_Error()
    {
        var itemUnderTest = new OperatorValidator();

        var fixture = new Fixture();

        var request = fixture.Build<Operator>()
            .With(o => o.Name, string.Empty)
            .With(o => o.Postcode, string.Empty)
            .With(o => o.LineOne, string.Empty)
            .With(o => o.CityName, string.Empty)
            .With(o => o.CountryCode, string.Empty)
            .Without(o => o.Traces)
            .Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldHaveValidationErrorFor(a => a.Name)
            .WithErrorMessage("'Name' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.Postcode)
            .WithErrorMessage("'Postcode' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.LineOne)
            .WithErrorMessage("'Line One' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.CityName)
            .WithErrorMessage("'City Name' must not be empty.");
    }
}