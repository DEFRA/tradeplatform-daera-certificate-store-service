// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Validation;

public class ExchangedDocumentValidatorTests
{
    [Fact]
    public void Validate_Valid_OK()
    {
        var itemUnderTest = new ExchangedDocumentValidator();

        var fixture = new Fixture();

        var request = fixture.Build<ExchangedDocument>()
            .With(x => x.PackingListFileLocation, "https://www.mocked.org/blobname-csv")
            .With(x => x.CertificatePDFLocation, "https://www.mocked.org/blobname-csv")
            .Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_Nulls_Error()
    {
        var itemUnderTest = new ExchangedDocumentValidator();

        var result = itemUnderTest.TestValidate(new ExchangedDocument
        {
            PackingListFileLocation = "invalid-mocked"
        });

        result.ShouldHaveValidationErrorFor(dc => dc.Id)
            .WithErrorMessage("'Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.TypeCode)
            .WithErrorMessage("'Type Code' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.ApplicationSubmissionID)
            .WithErrorMessage("'Application Submission ID' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.Applicant)
            .WithErrorMessage("'Applicant' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.PackingListFileLocation)
            .WithErrorMessage("Packing List File Location must be a valid URI.");

        result.ShouldHaveValidationErrorFor(dc => dc.ApplicationSubmissionDateTime)
            .WithErrorMessage("'Application Submission Date Time' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.CertificateIssueDateTime)
            .WithErrorMessage("'Certificate Issue Date Time' must not be empty.");
    }

    [Fact]
    public void Validate_IsEmpty_Error()
    {
        var itemUnderTest = new ExchangedDocumentValidator();

        var fixture = new Fixture();

        var request = fixture
            .Build<ExchangedDocument>()
                .With(ed => ed.Id, string.Empty)
                .With(ed => ed.TypeCode, string.Empty)
                .With(ed => ed.ApplicationSubmissionID, string.Empty)
                .With(ed => ed.PackingListFileLocation, string.Empty)
                .Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldHaveValidationErrorFor(dc => dc.Id)
            .WithErrorMessage("'Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.TypeCode)
            .WithErrorMessage("'Type Code' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.ApplicationSubmissionID)
            .WithErrorMessage("'Application Submission ID' must not be empty.");
    }


    [Fact]
    public void Validate_PackingListFileLocation_IsInvalid_Error()
    {
        var itemUnderTest = new ExchangedDocumentValidator();

        var fixture = new Fixture();

        var request = fixture
            .Build<ExchangedDocument>()
            .With(ed => ed.Id, string.Empty)
            .With(ed => ed.TypeCode, string.Empty)
            .With(ed => ed.ApplicationSubmissionID, string.Empty)
            .With(ed => ed.PackingListFileLocation, "mocked")
            .Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldHaveValidationErrorFor(dc => dc.Id)
            .WithErrorMessage("'Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.TypeCode)
            .WithErrorMessage("'Type Code' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.ApplicationSubmissionID)
            .WithErrorMessage("'Application Submission ID' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.PackingListFileLocation)
            .WithErrorMessage("Packing List File Location must be a valid URI.");
    }

    [Fact]
    public void Validate_GcPdfLocation_IsInvalid_Error()
    {
        var itemUnderTest = new ExchangedDocumentValidator();

        var fixture = new Fixture();

        var request = fixture
            .Build<ExchangedDocument>()
            .With(ed => ed.Id, string.Empty)
            .With(ed => ed.TypeCode, string.Empty)
            .With(ed => ed.ApplicationSubmissionID, string.Empty)
            .With(ed => ed.CertificatePDFLocation, "mocked")
            .Create();

        var result = itemUnderTest.TestValidate(request);

        result.ShouldHaveValidationErrorFor(dc => dc.Id)
            .WithErrorMessage("'Id' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.TypeCode)
            .WithErrorMessage("'Type Code' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.ApplicationSubmissionID)
            .WithErrorMessage("'Application Submission ID' must not be empty.");

        result.ShouldHaveValidationErrorFor(dc => dc.CertificatePDFLocation)
            .WithErrorMessage("Certificate PDF Location must be a valid URI.");
    }
}