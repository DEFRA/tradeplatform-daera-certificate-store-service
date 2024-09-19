// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.API.CertificatesStore.V2.Mappers;
using Defra.Trade.API.CertificatesStore.V2.Mappers.Enrichment;
using FluentAssertions;
using Applicant = Defra.Trade.API.CertificatesStore.V2.Dtos.Applicant;
using Consignee = Defra.Trade.API.CertificatesStore.V2.Dtos.Consignee;
using Consignor = Defra.Trade.API.CertificatesStore.V2.Dtos.Consignor;
using DefraCustomer = Defra.Trade.API.CertificatesStore.V2.Dtos.DefraCustomer;
using DefraCustomerOrgInfo = Defra.Trade.API.CertificatesStore.V2.Dtos.DefraCustomerOrgInfo;
using EhcoGeneralCertificateApplication = Defra.Trade.API.CertificatesStore.V2.Dtos.EhcoGeneralCertificateApplication;
using ExchangedDocument = Defra.Trade.API.CertificatesStore.V2.Dtos.ExchangedDocument;
using SupplyChainConsignment = Defra.Trade.API.CertificatesStore.V2.Dtos.SupplyChainConsignment;

namespace Defra.Trade.API.CertificatesStore.Tests.V1.Mappers;

public class EhcoGeneralCertificateApplicationProfileTests
{
    private readonly IMapper _mapper;

    public EhcoGeneralCertificateApplicationProfileTests()
    {
        var profiles =
            new List<Profile>
            {
                new EhcoGeneralCertificateApplicationProfile(),
                new ExchangedDocumentProfile(),
                new ApplicantProfile(),
                new DefraCustomerProfile(),
                new SupplyChainConsignmentProfile(),
                new LocationInfoProfile(),
                new OperatorProfile(),
                new LogisticsTransportMeansProfile(),
                new IdcomsProfile(),
                new LogisticsTransportEquipmentProfile(),
                new ConsigneeProfile(),
                new ConsignorProfile(),
                new DefraCustomerOrgInfoProfile(),
                new CountryProfile(),
                new TracesProfile()
            };
        var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void EhcoGeneralCertificateApplicationProfile_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<EhcoGeneralCertificateApplication>();

        // Act
        var actualResult = _mapper.Map<Logic.Models.EhcoGeneralCertificateApplication>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }

    [Fact]
    public void ExchangedDocumentProfile_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<ExchangedDocument>();

        // Act
        var actualResult = _mapper.Map<Logic.Models.ExchangedDocument>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }

    [Fact]
    public void ApplicantProfile_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<Applicant>();

        // Act
        var actualResult = _mapper.Map<Logic.Models.Applicant>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }

    [Fact]
    public void DefraCustomerProfile_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<DefraCustomer>();

        // Act
        var actualResult = _mapper.Map<Logic.Models.DefraCustomer>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }

    [Fact]
    public void DefraCustomerOrgInfoProfile_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<DefraCustomerOrgInfo>();

        // Act
        var actualResult = _mapper.Map<Logic.Models.DefraCustomerOrgInfo>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }

    [Fact]
    public void SupplyChainConsignmentProfile_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<SupplyChainConsignment>();

        // Act
        var actualResult = _mapper.Map<Logic.Models.SupplyChainConsignment>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }

    [Fact]
    public void ConsigneeProfile_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<Consignee>();

        // Act
        var actualResult = _mapper.Map<Logic.Models.Consignee>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }

    [Fact]
    public void ConsignorProfile_ShouldMap_AsExpected()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<Consignor>();

        // Act
        var actualResult = _mapper.Map<Logic.Models.Consignor>(source);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(source);
    }
}
