// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class EhcoGeneralCertificateApplicationValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new EhcoGeneralCertificateApplicationValidator();

        var fixture = new Fixture();

        var operatorResponsibleFor = fixture.Build<Operator>().Create();

        var request = fixture.Build<EhcoGeneralCertificateApplication>()
            .With(x => x.ExchangedDocument,
            fixture.Build<ExchangedDocument>()
                .With(x => x.PackingListFileLocation, "https://www.mocked.org/filename-csv")
                .With(x => x.CertificatePDFLocation, "https://www.mocked.org/filename-csv")
                .Create())
            .With(x => x.SupplyChainConsignment,
                fixture.Build<SupplyChainConsignment>().With(x => x.OperatorResponsibleForConsignment, operatorResponsibleFor).Create())
            .Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new EhcoGeneralCertificateApplicationValidator();

        var result = itemUnderTest.TestValidate(new EhcoGeneralCertificateApplication());

        result.ShouldHaveValidationErrorFor(dc => dc.ExchangedDocument)
            .WithErrorMessage("'Exchanged Document' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.SupplyChainConsignment)
            .WithErrorMessage("'Supply Chain Consignment' must not be empty.");
    }
}