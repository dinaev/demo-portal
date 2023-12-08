using DemoPortal.Backend.Documents.Abstractions.Errors;
using DemoPortal.Backend.Documents.Abstractions.Repositories;
using DemoPortal.Backend.Shared.BusinessLogic;
using FluentAssertions;
using Moq;

namespace DemoPortal.Backend.Documents.Core.Tests.DocumentsService;

public class DeleteDocumentTests
{
    private readonly Services.DocumentsService _service;
    private readonly Mock<IDocumentsRepository> _documentsRepository;
    
    public DeleteDocumentTests()
    {
        _documentsRepository = new Mock<IDocumentsRepository>();
        _service = new Services.DocumentsService(_documentsRepository.Object);
    }
    
    [Fact]
    public async Task Delete_DocumentFound_ReturnsSuccessfulBusiness()
    {
        // Arrange.
        var id = Guid.NewGuid();
        _documentsRepository
            .Setup(x => x.Delete(id))
            .ReturnsAsync(() => true);
        
        // Act.
        var result = await _service.Delete(id);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeTrue();
        result.Error.Should().BeNull();
        _documentsRepository.Verify(x => x.Delete(id), Times.Once);
    }
    
    [Fact]
    public async Task Delete_DefaultId_ReturnsErrorDocumentIdNotProvided()
    {
        // Arrange.
        var id = Guid.Empty;
        
        // Act.
        var result = await _service.Delete(id);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.DocumentIdNotProvided.Key &&
                x.Message == DocumentsErrorModels.DocumentIdNotProvided.Message);
        _documentsRepository.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Never);
    }
    
    [Fact]
    public async Task Delete_DeletionError_ReturnsErrorDocumentDeletionError()
    {
        // Arrange.
        var id = Guid.NewGuid();
        _documentsRepository
            .Setup(x => x.Delete(id))
            .ReturnsAsync(() => false);
        
        // Act.
        var result = await _service.Delete(id);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.DocumentDeletionError.Key &&
                x.Message == DocumentsErrorModels.DocumentDeletionError.Message);
        _documentsRepository.Verify(x => x.Delete(id), Times.Once);
    }
}