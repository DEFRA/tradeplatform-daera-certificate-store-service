// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Validation;

public class EhcoGeneralCertificateApplicationValidator : AbstractValidator<EhcoGeneralCertificateApplication>
{
    public EhcoGeneralCertificateApplicationValidator()
    {
        RuleFor(x => x.ExchangedDocument)
            .NotNull()
            .SetValidator(new ExchangedDocumentValidator());

        RuleFor(x => x.SupplyChainConsignment)
            .NotNull()
            .SetValidator(new SupplyChainConsignmentValidator());
    }
}