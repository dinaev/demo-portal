using AutoFixture;
using DemoPortal.Backend.Documents.Abstractions.Errors;
using DemoPortal.Backend.Documents.Abstractions.Models;
using DemoPortal.Backend.Documents.Abstractions.Repositories;
using DemoPortal.Backend.Shared.BusinessLogic;
using FluentAssertions;
using Moq;

namespace DemoPortal.Backend.Documents.Core.Tests.DocumentsService;

public class GetByIdDocumentTests
{
    private readonly Fixture _fixture;
    private readonly Services.DocumentsService _service;
    private readonly Mock<IDocumentsRepository> _documentsRepository;
    
    public GetByIdDocumentTests()
    {
        _fixture = new Fixture();
        _documentsRepository = new Mock<IDocumentsRepository>();
        _service = new Services.DocumentsService(_documentsRepository.Object);
    }
    
    [Fact]
    public async Task GetById_DocumentFound_ReturnsBusinessResultWithDocumentGetModel()
    {
        // Arrange.
        var id = Guid.NewGuid();
        var model = _fixture.Create<DocumentGetModel>();
        _documentsRepository
            .Setup(x => x.GetById(id))
            .ReturnsAsync(() => model);
        
        // Act.
        var result = await _service.GetById(id);
        
        // Assert.
        result.Should().NotBeNull();
        result.Error.Should().BeNull();
        result.IsSuccessful.Should().BeTrue();
        result.ResultData.Should().BeEquivalentTo(model);
        _documentsRepository.Verify(x => x.GetById(id), Times.Once);
    }
    
    [Fact]
    public async Task GetById_DefaultId_ReturnsErrorDocumentIdNotProvided()
    {
        // Arrange.
        var id = Guid.Empty;
        
        // Act.
        var result = await _service.GetById(id);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.DocumentIdNotProvided.Key &&
                x.Message == DocumentsErrorModels.DocumentIdNotProvided.Message);
        _documentsRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Never);
    }
    
    [Fact]
    public async Task GetById_DocumentNotFound_ReturnsErrorDocumentNotFound()
    {
        // Arrange.
        var id = Guid.NewGuid();
        _documentsRepository
            .Setup(x => x.GetById(id))
            .ReturnsAsync(() => default);
        
        // Act.
        var result = await _service.GetById(id);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.DocumentNotFound.Key &&
                x.Message == DocumentsErrorModels.DocumentNotFound.Message);
        _documentsRepository.Verify(x => x.GetById(id), Times.Once);
    }
}