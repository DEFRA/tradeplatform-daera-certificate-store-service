// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Dtos;

/// <summary>
/// Details of the applicant
/// </summary>
public class Applicant
{
    /// <summary>
    /// The Defra customer details
    /// </summary>
    public DefraCustomer DefraCustomer { get; set; }
}