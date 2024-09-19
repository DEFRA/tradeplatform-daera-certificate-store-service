// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Validation;

public class LogisticsTransportMeansValidator : AbstractValidator<LogisticsTransportMeans>
{
    public LogisticsTransportMeansValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();

        When(x => x.Id != null, () =>
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        });

        RuleFor(x => x.ModeCode)
            .NotNull();

        When(x => x.ModeCode != null, () =>
        {
            RuleFor(x => x.ModeCode)
                .NotEmpty();
        });
    }
}