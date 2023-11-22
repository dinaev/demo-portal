using DemoPortal.Backend.Documents.Abstractions.Constants;
using DemoPortal.Backend.Documents.Abstractions.Errors;
using DemoPortal.Backend.Documents.Abstractions.Models;
using DemoPortal.Backend.Documents.Abstractions.Repositories;
using DemoPortal.Backend.Documents.Abstractions.Services;
using DemoPortal.Backend.Shared.BusinessLogic;

namespace DemoPortal.Backend.Documents.Core.Services;

public class DocumentsService : IDocumentsService
{
    private readonly IDocumentsRepository _documentsRepository;

    public DocumentsService(IDocumentsRepository documentsRepository)
    {
        _documentsRepository = documentsRepository;
    }

    public async Task<BusinessResult<DocumentGetModel>> Create(DocumentCreateModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        if (model.UserId == default)
            return DocumentsErrorModels.UserNotProvided;

        if (string.IsNullOrEmpty(model.Title))
            return DocumentsErrorModels.TitleIsEmpty;
        
        if (model.Title.Length > DocumentsConstants.DocumentTitleMaxLenght)
            return DocumentsErrorModels.TitleIsTooLong;
        
        if (string.IsNullOrEmpty(model.Text))
            return DocumentsErrorModels.TextIsEmpty;
        
        if (model.Text.Length > DocumentsConstants.DocumentTextMaxLenght)
            return DocumentsErrorModels.TextIsTooLong;

        return await _documentsRepository.Create(model);
    }

    public async Task<BusinessResult<DocumentGetModel>> GetById(Guid id)
    {
        if (id == default)
            return DocumentsErrorModels.DocumentIdNotProvided;
        
        var document = await _documentsRepository.GetById(id);
        if (document == default)
            return DocumentsErrorModels.DocumentNotFound;
        
        return document;
    }

    public async Task<BusinessResult<DocumentGetSimpleModel[]>> GetList(DocumentListFilter filter)
    {
        if (filter == null)
            throw new ArgumentNullException(nameof(filter));
        
        if (filter.UserId == default)
            return DocumentsErrorModels.UserNotProvided;

        return await _documentsRepository.GetList(filter);
    }

    public async Task<BusinessResult<DocumentGetModel>> Update(DocumentUpdateModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        
        if (model.Id == default)
            return DocumentsErrorModels.DocumentIdNotProvided;
        
        if (string.IsNullOrEmpty(model.Title))
            return DocumentsErrorModels.TitleIsEmpty;
        
        if (model.Title.Length > DocumentsConstants.DocumentTitleMaxLenght)
            return DocumentsErrorModels.TitleIsTooLong;
        
        if (string.IsNullOrEmpty(model.Text))
            return DocumentsErrorModels.TextIsEmpty;
        
        if (model.Text.Length > DocumentsConstants.DocumentTextMaxLenght)
            return DocumentsErrorModels.TextIsTooLong;
        
        var document = await _documentsRepository.Update(model);
        
        if (document == null)
            return DocumentsErrorModels.DocumentUpdatingError;

        return document;
    }

    public async Task<BusinessResult> Delete(Guid id)
    {
        if (id == default)
            return DocumentsErrorModels.DocumentIdNotProvided;
        
        if (await _documentsRepository.Delete(id))
            return BusinessResult.Successful;
        
        return DocumentsErrorModels.DocumentDeletionError;
    }
}