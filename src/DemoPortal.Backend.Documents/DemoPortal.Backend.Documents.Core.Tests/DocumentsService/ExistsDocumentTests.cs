using DemoPortal.Backend.Documents.Abstractions.Errors;
using DemoPortal.Backend.Documents.Abstractions.Repositories;
using DemoPortal.Backend.Shared.BusinessLogic;
using FluentAssertions;
using Moq;

namespace DemoPortal.Backend.Documents.Core.Tests.DocumentsService;

public class ExistsDocumentTests
{
    private readonly Services.DocumentsService _service;
    private readonly Mock<IDocumentsRepository> _documentsRepository;
    
    public ExistsDocumentTests()
    {
        _documentsRepository = new Mock<IDocumentsRepository>();
        _service = new Services.DocumentsService(_documentsRepository.Object);
    }
    
    [Fact]
    public async Task Exists_DocumentFound_ReturnsSuccessfulBusiness()
    {
        // Arrange.
        var userId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        _documentsRepository
            .Setup(x => x.Exists(userId, documentId))
            .ReturnsAsync(() => true);
        
        // Act.
        var result = await _service.Exists(userId, documentId);
        
        // Assert.
        result.Should().NotBeNull();
        result.Error.Should().BeNull();
        result.IsSuccessful.Should().BeTrue();
        _documentsRepository.Verify(x => x.Exists(userId, documentId), Times.Once);
    }
    
    [Fact]
    public async Task Exists_DocumentNotFound_ReturnsErrorDocumentNotFound()
    {
        // Arrange.
        var userId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        _documentsRepository
            .Setup(x => x.Exists(userId, documentId))
            .ReturnsAsync(() => false);
        
        // Act.
        var result = await _service.Exists(userId, documentId);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.DocumentNotFound.Key &&
                x.Message == DocumentsErrorModels.DocumentNotFound.Message);
        _documentsRepository.Verify(x => x.Exists(userId, documentId), Times.Once);
    }
    
    [Fact]
    public async Task Exists_DefaultUserId_ReturnsErrorUserNotProvided()
    {
        // Arrange.
        var userId = Guid.Empty;
        var documentId = Guid.NewGuid();
        
        // Act.
        var result = await _service.Exists(userId, documentId);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.UserNotProvided.Key &&
                x.Message == DocumentsErrorModels.UserNotProvided.Message);
        _documentsRepository.Verify(x => x.Exists(userId, documentId), Times.Never);
    }
    
    [Fact]
    public async Task Exists_DefaultUserId_ReturnsErrorDocumentIdNotProvided()
    {
        // Arrange.
        var userId = Guid.NewGuid();
        var documentId = Guid.Empty;
        
        // Act.
        var result = await _service.Exists(userId, documentId);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.DocumentIdNotProvided.Key &&
                x.Message == DocumentsErrorModels.DocumentIdNotProvided.Message);
        _documentsRepository.Verify(x => x.Exists(userId, documentId), Times.Never);
    }
}