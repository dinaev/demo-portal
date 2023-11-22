using AutoFixture;
using DemoPortal.Backend.Documents.Abstractions.Errors;
using DemoPortal.Backend.Documents.Abstractions.Models;
using DemoPortal.Backend.Documents.Abstractions.Repositories;
using DemoPortal.Backend.Shared.BusinessLogic;
using FluentAssertions;
using Moq;

namespace DemoPortal.Backend.Documents.Core.Tests.DocumentsService;

public class GetListOfDocumentsTests
{
    private readonly Fixture _fixture;
    private readonly Services.DocumentsService _service;
    private readonly Mock<IDocumentsRepository> _documentsRepository;
    
    public GetListOfDocumentsTests()
    {
        _fixture = new Fixture();
        _documentsRepository = new Mock<IDocumentsRepository>();
        _service = new Services.DocumentsService(_documentsRepository.Object);
    }
    
    [Fact]
    public async Task GetList_UserHasDocuments_ReturnsBusinessResultWithDocumentGetModel()
    {
        // Arrange.
        var filter = _fixture.Create<DocumentListFilter>();
        var models = _fixture.CreateMany<DocumentGetSimpleModel>().ToArray();
        _documentsRepository
            .Setup(x => x.GetList(filter))
            .ReturnsAsync(() => models);
        
        // Act.
        var result = await _service.GetList(filter);
        
        // Assert.
        result.Should().NotBeNull();
        result.Error.Should().BeNull();
        result.ResultData.Should().BeEquivalentTo(models);
        
        _documentsRepository.Verify(x => x.GetList(filter), Times.Once);
    }
    
    [Fact]
    public async Task GetList_NullFilter_ReturnsArgumentNullException()
    {
// Disable Warning CS8625 : Cannot convert null literal to non-nullable reference type.
#pragma warning disable 8625
        
        // Arrange.
        // Act.
        var result = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetList(null));
#pragma warning restore 8625
        
        // Assert.
        result.Should().NotBeNull();
        result.Should().Match<ArgumentNullException>(x => x.ParamName == "filter");
        _documentsRepository.Verify(x => x.GetList(It.IsAny<DocumentListFilter>()), Times.Never);
    }
    
    [Fact]
    public async Task GetList_UserIdEmpty_ReturnsErrorUserNotProvided()
    {
        // Arrange.
        var filter = new DocumentListFilter
        {
            UserId = Guid.Empty
        };
        
        // Act.
        var result = await _service.GetList(filter);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.UserNotProvided.Key &&
                x.Message == DocumentsErrorModels.UserNotProvided.Message);
        _documentsRepository.Verify(x => x.GetList(It.IsAny<DocumentListFilter>()), Times.Never);
    }
}