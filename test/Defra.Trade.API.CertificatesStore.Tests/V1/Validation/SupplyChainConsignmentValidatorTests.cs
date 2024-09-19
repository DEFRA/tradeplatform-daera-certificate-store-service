// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class SupplyChainConsignmentValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new SupplyChainConsignmentValidator();

        var fixture = new Fixture();
        var operatorResponsibleFor = fixture.Build<Operator>().Without(x => x.Traces).Create();
        var request = fixture
            .Build<SupplyChainConsignment>()
            .With(scc => scc.OperatorResponsibleForConsignment, operatorResponsibleFor)
            .Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new SupplyChainConsignmentValidator();

        var result = itemUnderTest.TestValidate(new SupplyChainConsignment());

        result.ShouldHaveValidationErrorFor(a => a.ExportExitDateTime)
            .WithErrorMessage("'Export Exit Date Time' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.Consignor)
            .WithErrorMessage("'Consignor' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.Consignee)
            .WithErrorMessage("'Consignee' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.DispatchLocation)
            .WithErrorMessage("'Dispatch Location' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.DestinationLocation)
            .WithErrorMessage("'Destination Location' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.OperatorResponsibleForConsignment)
            .WithErrorMessage("'Operator Responsible For Consignment' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.UsedTransportMeans)
            .WithErrorMessage("'Used Transport Means' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.BorderControlPostLocation)
            .WithErrorMessage("'Border Control Post Location' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.UsedTransportEquipment)
            .WithErrorMessage("'Used Transport Equipment' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.ImportCountry)
            .WithErrorMessage("'Import Country' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.ExportCountry)
            .WithErrorMessage("'Export Country' must not be empty.");
    }

    [Fact]
    public void Validate_IsEmpty_Error()
    {
        var itemUnderTest = new SupplyChainConsignmentValidator();

        var fixture = new Fixture();

        var request = fixture
            .Build<SupplyChainConsignment>()
            .With(scc => scc.ExportExitDateTime, string.Empty)
            .With(scc => scc.BorderControlPostLocation, string.Empty)
            .With(scc => scc.ImportCountry, string.Empty)
            .With(scc => scc.ExportCountry, string.Empty)
            .Without(scc => scc.OperatorResponsibleForConsignment)
            .Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldHaveValidationErrorFor(a => a.ExportExitDateTime)
            .WithErrorMessage("'Export Exit Date Time' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.BorderControlPostLocation)
            .WithErrorMessage("'Border Control Post Location' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.ImportCountry)
            .WithErrorMessage("'Import Country' must not be empty.");

        result.ShouldHaveValidationErrorFor(a => a.ExportCountry)
            .WithErrorMessage("'Export Country' must not be empty.");
    }
}