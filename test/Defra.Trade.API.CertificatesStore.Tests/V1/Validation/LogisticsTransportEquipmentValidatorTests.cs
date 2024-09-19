// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class LogisticsTransportEquipmentValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new LogisticsTransportEquipmentValidator();

        var fixture = new Fixture();

        var request = fixture.Create<LogisticsTransportEquipment>();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new LogisticsTransportEquipmentValidator();

        var result = itemUnderTest.TestValidate(new LogisticsTransportEquipment());

        result.ShouldHaveValidationErrorFor(a => a.AffixedSeal)
           .WithErrorMessage("'Affixed Seal' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.TemperatureSetting)
           .WithErrorMessage("'Temperature Setting' must not be empty.");
    }

    [Fact]
    public void Validate_IsEmpty_Error()
    {
        var itemUnderTest = new LogisticsTransportEquipmentValidator();

        var fixture = new Fixture();

        var request = fixture.Build<LogisticsTransportEquipment>()
            .With(lte => lte.AffixedSeal, string.Empty)
            .With(lte => lte.TemperatureSetting, string.Empty)
            .Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldHaveValidationErrorFor(a => a.AffixedSeal)
           .WithErrorMessage("'Affixed Seal' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.TemperatureSetting)
           .WithErrorMessage("'Temperature Setting' must not be empty.");
    }
}