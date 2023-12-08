using DemoPortal.Backend.Documents.Api.Contract;
using DemoPortal.Backend.Shared.BusinessLogic;
using Refit;

namespace DemoPortal.Backend.Documents.Api.Client
{
    /// <summary>
    /// API to manage documents
    /// </summary>
    public interface IDocumentsApi
    {
        /// <summary>
        /// Create a document
        /// </summary>
        [Post("/documents")]
        Task<BusinessResult<DocumentDto>> Create([Body] DocumentCreateRequest request);

        /// <summary>
        /// Get a document list for a specifier user
        /// </summary>
        /// <param name="request">user identifier</param>
        [Get("/documents")]
        Task<BusinessResult<DocumentListGetResponse>> GetList(DocumentListGetRequest request);

        /// <summary>
        /// Update a document
        /// </summary>
        /// <param name="id">Document identifier</param>
        /// <param name="request">Fields to update</param>
        [Put("/documents/{id}")]
        Task<BusinessResult<DocumentDto>> Update(Guid id, [Body] DocumentUpdateRequest request);

        /// <summary>
        /// Get a document by ID
        /// </summary>
        /// <param name="id">Document identifier</param>
        [Get("/documents/{id}")]
        Task<BusinessResult<DocumentDto>> Get(Guid id);

        /// <summary>
        /// Delete a document
        /// </summary>
        /// <param name="id">Document identifier</param>
        [Delete("/documents/{id}")]
        Task<BusinessResult> Delete(Guid id);
        
        /// <summary>
        /// Check if a document exists with specified user ID and document ID
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="documentId">Document identifier</param>
        [Get("/documents/exists")]
        Task<BusinessResult> Exists(Guid userId, Guid documentId);
    }
}