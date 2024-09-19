// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.V2.Dtos.Enrichment;

/// <summary>
/// Address details for enriched General Certificate payload
/// </summary>
public class Address
{
    /// <summary>
    /// Address line 1 of the Address
    /// </summary>
    public string AddressLine1 { get; set; }

    /// <summary>
    /// Address line 2 of the Address
    /// </summary>
    public string AddressLine2 { get; set; }

    /// <summary>
    /// Address line 3  of the Address
    /// </summary>
    public string AddressLine3 { get; set; }

    /// <summary>
    /// Postcode of the Address
    /// </summary>
    public string PostCode { get; set; }

    /// <summary>
    /// County of the Address
    /// </summary>
    public string County { get; set; }

    /// <summary>
    /// The town  of the Address
    /// </summary>
    public string Town { get; set; }

    /// <summary>
    /// The country the address is located in
    /// </summary>
    public Country Country { get; set; }
}