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

public class CreateDocumentTests
{
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;
    private readonly Services.DocumentsService _service;
    private readonly Mock<IDocumentsRepository> _documentsRepository;
    private readonly StringBuilder _stringBuilder;
    
    public CreateDocumentTests()
    {
        var configuration = new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<DocumentCreateModel, DocumentGetModel>();
        });
        _mapper = new Mapper(configuration);
        _fixture = new Fixture();
        _documentsRepository = new Mock<IDocumentsRepository>();
        _service = new Services.DocumentsService(_documentsRepository.Object);
        _stringBuilder = new StringBuilder();

    }
    
    [Fact]
    public async Task Create_AllFieldsAreCorrect_ReturnsBusinessResultWithDocumentGetModel()
    {
        // Arrange.
        var model = _fixture.Create<DocumentCreateModel>();
        var expectedResult = _fixture.Create<DocumentGetModel>();
        _mapper.Map(model, expectedResult);
        _documentsRepository
            .Setup(x => x.Create(model))
            .ReturnsAsync(() => expectedResult);
        
        // Act.
        var result = await _service.Create(model);
        
        // Assert.
        _documentsRepository.Verify(x => x.Create(model), Times.Once);
        
        result.Should().NotBeNull();
        result.Error.Should().BeNull();
        result.IsSuccessful.Should().BeTrue();
        result.ResultData.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public async Task Create_NullModel_ReturnsArgumentNullException()
    {

// Disable Warning CS8625 : Cannot convert null literal to non-nullable reference type.
#pragma warning disable 8625
        
        // Arrange.
        // Act.
        var result = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.Create(null));
#pragma warning restore 8625
        
        // Assert.
        result.Should().NotBeNull();
        result.Should().Match<ArgumentNullException>(x => x.ParamName == "model");
        _documentsRepository.Verify(x => x.Create(It.IsAny<DocumentCreateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Create_DefaultUserId_ReturnsErrorUserNotProvided()
    {
        // Arrange.
        var model = _fixture.Create<DocumentCreateModel>();
        model.UserId = default;
        
        // Act.
        var result = await _service.Create(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.UserNotProvided.Key &&
                x.Message == DocumentsErrorModels.UserNotProvided.Message);
        _documentsRepository.Verify(x => x.Create(It.IsAny<DocumentCreateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Create_EmptyTitle_ReturnsErrorTitleIsEmpty()
    {
        // Arrange.
        var model = _fixture.Create<DocumentCreateModel>();
        model.Title = string.Empty;
        
        // Act.
        var result = await _service.Create(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.TitleIsEmpty.Key &&
                x.Message == DocumentsErrorModels.TitleIsEmpty.Message);
        _documentsRepository.Verify(x => x.Create(It.IsAny<DocumentCreateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Create_LongTitle_ReturnsErrorTitleIsTooLong()
    {
        // Arrange.
        var model = _fixture.Create<DocumentCreateModel>();
        model.Title = _stringBuilder
            .Append('a', DocumentsConstants.DocumentTitleMaxLenght + 1)
            .ToString();
        
        // Act.
        var result = await _service.Create(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.TitleIsTooLong.Key &&
                x.Message == DocumentsErrorModels.TitleIsTooLong.Message);
        _documentsRepository.Verify(x => x.Create(It.IsAny<DocumentCreateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Create_EmptyText_ReturnsErrorTextIsEmpty()
    {
        // Arrange.
        var model = _fixture.Create<DocumentCreateModel>();
        model.Text = string.Empty;
        
        // Act.
        var result = await _service.Create(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.TextIsEmpty.Key &&
                x.Message == DocumentsErrorModels.TextIsEmpty.Message);
        _documentsRepository.Verify(x => x.Create(It.IsAny<DocumentCreateModel>()), Times.Never);
    }
    
    [Fact]
    public async Task Create_LongText_ReturnsErrorTextIsTooLong()
    {
        // Arrange.
        var model = _fixture.Create<DocumentCreateModel>();
        model.Text = _stringBuilder
            .Append('a', DocumentsConstants.DocumentTextMaxLenght + 1)
            .ToString();
        
        // Act.
        var result = await _service.Create(model);
        
        // Assert.
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().NotBeNull().And
            .Match<ErrorModel>(x =>
                x.Key == DocumentsErrorModels.TextIsTooLong.Key &&
                x.Message == DocumentsErrorModels.TextIsTooLong.Message);
        _documentsRepository.Verify(x => x.Create(It.IsAny<DocumentCreateModel>()), Times.Never);
    }
}