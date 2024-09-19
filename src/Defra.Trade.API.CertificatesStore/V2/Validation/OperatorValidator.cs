// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Validation;

public class OperatorValidator : AbstractValidator<Operator>
{
    public OperatorValidator()
    {
        RuleFor(x => x.Name)
            .NotNull();

        When(x => x.Name != null, () =>
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        });

        RuleFor(x => x.Postcode)
            .NotNull();

        When(x => x.Postcode != null, () =>
        {
            RuleFor(x => x.Postcode)
                .NotEmpty();
        });

        RuleFor(x => x.LineOne)
            .NotNull();

        When(x => x.LineOne != null, () =>
        {
            RuleFor(x => x.LineOne)
                .NotEmpty();
        });

        RuleFor(x => x.CityName)
            .NotNull();

        When(x => x.CityName != null, () =>
        {
            RuleFor(x => x.CityName)
                .NotEmpty();
        });
    }
}