// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Validation;

public class LogisticsTransportEquipmentValidator : AbstractValidator<LogisticsTransportEquipment>
{
    public LogisticsTransportEquipmentValidator()
    {
        RuleFor(x => x.AffixedSeal)
            .NotNull();

        When(x => x.AffixedSeal != null, () =>
        {
            RuleFor(x => x.AffixedSeal)
                .NotEmpty();
        });

        RuleFor(x => x.TemperatureSetting)
            .NotNull();

        When(x => x.TemperatureSetting != null, () =>
        {
            RuleFor(x => x.TemperatureSetting)
                .NotEmpty();
        });
    }
}