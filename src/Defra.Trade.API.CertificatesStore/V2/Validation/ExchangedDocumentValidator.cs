// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Validation;

public class ExchangedDocumentValidator : AbstractValidator<ExchangedDocument>
{
    public ExchangedDocumentValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();

        When(x => x.Id != null, () =>
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        });

        RuleFor(x => x.TypeCode)
            .NotNull();

        When(x => x.TypeCode != null, () =>
        {
            RuleFor(x => x.TypeCode)
                .NotEmpty();
        });

        RuleFor(x => x.ApplicationSubmissionID)
            .NotNull();

        When(x => x.ApplicationSubmissionID != null, () =>
        {
            RuleFor(x => x.ApplicationSubmissionID)
                .NotEmpty();
        });

        RuleFor(x => x.Applicant)
            .NotNull()
            .SetValidator(new ApplicantValidator());

        When(x => !string.IsNullOrWhiteSpace(x.PackingListFileLocation), () =>
        {
            RuleFor(x => x.PackingListFileLocation)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.PackingListFileLocation))
                .WithMessage("{PropertyName} must be a valid URI.");
        });

        When(x => !string.IsNullOrWhiteSpace(x.CertificatePDFLocation), () =>
            {
                RuleFor(x => x.CertificatePDFLocation)
                    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.PackingListFileLocation))
                    .WithMessage("{PropertyName} must be a valid URI.");
            });

        RuleFor(x => x.ApplicationSubmissionDateTime)
            .NotNull();

        RuleFor(x => x.CertificateIssueDateTime)
            .NotNull();
    }
}