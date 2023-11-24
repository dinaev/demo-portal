using System.Text;
using AutoFixture;
using AutoMapper;
using DemoPortal.Backend.Documents.Abstractions.Constants;
using DemoPortal.Backend.Documents.Abstractions.Errors;
using DemoPortal.Backend.Documents.Abstractions.Models;
using DemoPortal.Backend.Documents.Abstractions.Repositories;
using DemoPortal.Backend.Shared.BusinessLogic;
using FluentAssertions;
using Moq;

namespace DemoPortal.Backend.Documents.Core.Tests.DocumentsService;

public class UpdateDocumentTests
{
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;
    private readonly Services.DocumentsService _service;
    private readonly Mock<IDocumentsRepository> _documentsRepository;
    private readonly StringBuilder _stringBuilder;
    
    public UpdateDocumentTests()
    {
        var configuration = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<DocumentUpdateModel, DocumentGetModel>();
        });
        _mapper = new Mapper(configuration);
        _fixture = new Fixture();
        _documentsRepository = new Mock<IDocumentsRepository>();
        _service = new Services.DocumentsService(_documentsRepository.Object);
        _stringBuilder = new StringBuilder();

    }
    
    [Fact]
    public async Task Update_AllFieldsAreCorrect_ReturnsBusinessResultWithDocumentGetModel()
    {
        // Arrange.
        var model = _fixture.Create<DocumentUpdateModel>();
        var expectedResult = _fixture.Create<DocumentGetModel>();
        _mapper.Map(model, expectedResult);
        _documentsRepository
            .Setup(x => x.Update(model))
            .ReturnsAsync(() => expectedResult);
        
        // Act.
        var result = await _service.Update(model);
        
        // Assert.
        _documentsRepository.Verify(x => x.Update(model), Times.Once);
        
        result.Should().NotBeNull();
        result.Error.Should().BeNull();
        result.IsSuccessful.Should().BeTrue();
        result.ResultData.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task Update_NullModel_ReturnsArgumentNullException()
    {

// Disable Warning CS8625 : Cannot convert null literal to non-nullable reference type.
#pragma warning disable 8625
        
        // Arrange.
        // Act.
        var result = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Update(null));
#pragma warning restore 8625
        
        // Assert.
        result.Should().NotBeNull();
        result.Should().Match<ArgumentNullException>(x => x.ParamName == "model");
        _documentsRepository.Verify(x => x.Update(It.IsAny<DocumentUpdateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Update_DefaultDocumentId_ReturnsErrorDocumentIdNotProvided()
    {
        // Arrange.
        var model = _fixture.Create<DocumentUpdateModel>();
        model.Id = default;
        
        // Act.
        var result = await _service.Update(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.DocumentIdNotProvided.Key &&
                x.Message == DocumentsErrorModels.DocumentIdNotProvided.Message);
        _documentsRepository.Verify(x => x.Update(It.IsAny<DocumentUpdateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Update_EmptyTitle_ReturnsErrorTitleIsEmpty()
    {
        // Arrange.
        var model = _fixture.Create<DocumentUpdateModel>();
        model.Title = string.Empty;
        
        // Act.
        var result = await _service.Update(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.TitleIsEmpty.Key &&
                x.Message == DocumentsErrorModels.TitleIsEmpty.Message);
        _documentsRepository.Verify(x => x.Update(It.IsAny<DocumentUpdateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Update_LongTitle_ReturnsErrorTitleIsTooLong()
    {
        // Arrange.
        var model = _fixture.Create<DocumentUpdateModel>();
        model.Title = _stringBuilder
            .Append('a', DocumentsConstants.DocumentTitleMaxLenght + 1)
            .ToString();
        
        // Act.
        var result = await _service.Update(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.TitleIsTooLong.Key &&
                x.Message == DocumentsErrorModels.TitleIsTooLong.Message);
        _documentsRepository.Verify(x => x.Update(It.IsAny<DocumentUpdateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Update_EmptyText_ReturnsErrorTextIsEmpty()
    {
        // Arrange.
        var model = _fixture.Create<DocumentUpdateModel>();
        model.Text = string.Empty;
        
        // Act.
        var result = await _service.Update(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.TextIsEmpty.Key &&
                x.Message == DocumentsErrorModels.TextIsEmpty.Message);
        _documentsRepository.Verify(x => x.Update(It.IsAny<DocumentUpdateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Update_LongText_ReturnsErrorTextIsTooLong()
    {
        // Arrange.
        var model = _fixture.Create<DocumentUpdateModel>();
        model.Text = _stringBuilder
            .Append('a', DocumentsConstants.DocumentTextMaxLenght + 1)
            .ToString();
        
        // Act.
        var result = await _service.Update(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.TextIsTooLong.Key &&
                x.Message == DocumentsErrorModels.TextIsTooLong.Message);
        _documentsRepository.Verify(x => x.Update(It.IsAny<DocumentUpdateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Update_UpdateError_ReturnsErrorDocumentUpdatingError()
    {
        // Arrange.
        var model = _fixture.Create<DocumentUpdateModel>();
        _documentsRepository
            .Setup(x => x.Update(model))
            .ReturnsAsync(() => null);
        
        // Act.
        var result = await _service.Update(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.DocumentUpdatingError.Key &&
                x.Message == DocumentsErrorModels.DocumentUpdatingError.Message);
        _documentsRepository.Verify(x => x.Update(It.IsAny<DocumentUpdateModel>()), Times.Once);
    }
}