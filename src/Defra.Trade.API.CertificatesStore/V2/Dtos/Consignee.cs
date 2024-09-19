// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Dtos;

/// <summary>
/// Consignee model.
/// </summary>
public class Consignee
{
    /// <summary>
    /// Defra Consignee org details.
    /// </summary>
    public DefraCustomerOrgInfo DefraCustomer { get; set; }
}