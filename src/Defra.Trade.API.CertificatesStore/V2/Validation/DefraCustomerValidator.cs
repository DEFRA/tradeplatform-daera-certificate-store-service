// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Validation;

public class DefraCustomerValidator : AbstractValidator<DefraCustomer>
{
    public DefraCustomerValidator()
    {
        RuleFor(x => x.OrgId)
            .NotNull();

        When(x => x.OrgId != null, () =>
        {
            RuleFor(x => x.OrgId)
                .NotEmpty();
        });

        RuleFor(x => x.UserId)
            .NotNull();

        When(x => x.UserId != null, () =>
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
        });
    }
}