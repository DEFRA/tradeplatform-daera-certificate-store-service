// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Validation;

public class SupplyChainConsignmentValidator : AbstractValidator<SupplyChainConsignment>
{
    public SupplyChainConsignmentValidator()
    {
        RuleFor(x => x.ExportExitDateTime)
            .NotNull();

        When(x => x.ExportExitDateTime != null, () =>
        {
            RuleFor(x => x.ExportExitDateTime)
                .NotEmpty();
        });

        RuleFor(x => x.Consignor)
            .NotNull()
            .SetValidator(new ConsignorValidator());

        RuleFor(x => x.Consignee)
            .NotNull()
            .SetValidator(new ConsigneeValidator());

        RuleFor(x => x.DispatchLocation)
            .NotNull()
            .SetValidator(new LocationInfoValidator());

        RuleFor(x => x.DestinationLocation)
            .NotNull()
            .SetValidator(new LocationInfoValidator());

        RuleFor(x => x.OperatorResponsibleForConsignment)
            .NotNull()
            .SetValidator(new OperatorValidator());

        RuleFor(x => x.UsedTransportMeans)
            .NotNull()
            .SetValidator(new LogisticsTransportMeansValidator());

        RuleFor(x => x.BorderControlPostLocation)
            .NotNull();

        When(x => x.BorderControlPostLocation != null, () =>
        {
            RuleFor(x => x.BorderControlPostLocation)
                .NotEmpty();
        });

        RuleFor(x => x.UsedTransportEquipment)
            .NotNull()
            .SetValidator(new LogisticsTransportEquipmentValidator());

        RuleFor(x => x.ImportCountry)
            .NotNull();

        When(x => x.ImportCountry != null, () =>
        {
            RuleFor(x => x.ImportCountry)
                .NotEmpty();
        });

        RuleFor(x => x.ExportCountry)
            .NotNull();

        When(x => x.ExportCountry != null, () =>
        {
            RuleFor(x => x.ExportCountry)
                .NotEmpty();
        });
    }
}