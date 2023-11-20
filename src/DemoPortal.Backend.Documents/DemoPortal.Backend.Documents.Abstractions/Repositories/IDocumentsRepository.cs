using DemoPortal.Backend.Documents.Abstractions.Models;

namespace DemoPortal.Backend.Documents.Abstractions.Repositories;

public interface IDocumentsRepository
{
    Task<DocumentGetModel> Create(DocumentCreateModel model);
    Task<DocumentGetModel> GetById(Guid id);
    Task<DocumentGetSimpleModel[]> GetList(DocumentListFilter filter);
    Task<DocumentGetModel> Update(DocumentUpdateModel model);
    Task<bool> Delete(Guid id);
}