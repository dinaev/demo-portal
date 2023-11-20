using DemoPortal.Backend.Documents.Abstractions.Models;
using DemoPortal.Backend.Shared.BusinessLogic;

namespace DemoPortal.Backend.Documents.Abstractions.Services;

public interface IDocumentsService
{
    /// <summary>
    /// Create a document
    /// </summary>
    /// <remarks>Only 5 documents can be created per a user</remarks>
    /// <param name="model">Model to create a document</param>
    /// <returns></returns>
    Task<BusinessResult<DocumentGetModel>> Create(DocumentCreateModel model);
    
    /// <summary>
    /// Get a document
    /// </summary>
    /// <param name="id">Document ID</param>
    /// <returns></returns>
    Task<BusinessResult<DocumentGetModel>> GetById(Guid id);
    
    /// <summary>
    /// Get a list of documents
    /// </summary>
    /// <param name="filter">List filter</param>
    /// <returns></returns>
    Task<BusinessResult<DocumentGetSimpleModel[]>> GetList(DocumentListFilter filter);
    
    /// <summary>
    /// Update a document
    /// </summary>
    /// <param name="model">Model to update a document</param>
    /// <returns></returns>
    Task<BusinessResult<DocumentGetModel>> Update(DocumentUpdateModel model);
    
    /// <summary>
    /// Delete a document
    /// </summary>
    /// <param name="id">Document ID</param>
    /// <returns></returns>
    Task<BusinessResult> Delete(Guid id);
    
}