// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Dtos;

/// <summary>
/// Consignor details.
/// </summary>
public class Consignor
{
    /// <summary>
    /// Defra Consignor org details.
    /// </summary>
    public DefraCustomerOrgInfo DefraCustomer { get; set; }
}