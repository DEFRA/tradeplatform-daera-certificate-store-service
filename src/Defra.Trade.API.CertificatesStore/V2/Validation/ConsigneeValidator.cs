// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Validation;

public class ConsigneeValidator : AbstractValidator<Consignee>
{
    public ConsigneeValidator()
    {
        RuleFor(x => x.DefraCustomer)
            .NotNull()
            .SetValidator(new DefraCustomerOrgInfoValidator());
    }
}