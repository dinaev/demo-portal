using AutoMapper;
using DemoPortal.Backend.Documents.Abstractions.Models;
using DemoPortal.Backend.Documents.Abstractions.Services;
using DemoPortal.Backend.Documents.Api.Contract;
using DemoPortal.Backend.Shared.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace DemoPortal.Backend.Documents.Api.Controllers;

/// <summary>
/// API to manage documents
/// </summary>
[ApiController]
[Route("[controller]")]
public class DocumentsController : Controller
{
    private readonly IMapper _mapper;
    private readonly IDocumentsService _documentsService;

    /// <summary>
    /// Documents controller
    /// </summary>
    /// <param name="mapper">AutoMapper</param>
    /// <param name="documentsService">Documents service</param>
    public DocumentsController(IMapper mapper, IDocumentsService documentsService)
    {
        _mapper = mapper;
        _documentsService = documentsService;
    }

    /// <summary>
    /// Create a document
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<BusinessResult<DocumentDto>> Create([FromBody]DocumentCreateRequest request)
    {
        var document = _mapper.Map<DocumentCreateModel>(request);
            
        var result = await _documentsService.Create(document);
        
        if (result.IsSuccessful == false)
            return result.Error;
        
        return _mapper.Map<DocumentDto>(result.ResultData);
    }
    
    /// <summary>
    /// Get a document by ID
    /// </summary>
    /// <param name="id">Document identifier</param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<BusinessResult<DocumentDto>> Get(Guid id)
    {
        var result = await _documentsService.GetById(id);

        if (result.IsSuccessful == false)
            return result.Error;

        return _mapper.Map<DocumentDto>(result.ResultData);
    }
    
    /// <summary>
    /// Get a document list for a specifier user
    /// </summary>
    /// <param name="request">user identifier</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<BusinessResult<DocumentListGetResponse>> GetList([FromQuery]DocumentListGetRequest request)
    {
        var filter = _mapper.Map<DocumentListFilter>(request);
        
        var result = await _documentsService.GetList(filter);

        if (result.IsSuccessful == false)
            return result.Error;

        return new DocumentListGetResponse
            {
                Documents = _mapper.Map<DocumentSimpleDto[]>(result.ResultData)
            };
    }
    
    /// <summary>
    /// Update a document
    /// </summary>
    /// <param name="id">Document identifier</param>
    /// <param name="request">Fields to update</param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    public async Task<BusinessResult<DocumentDto>> Update(Guid id, [FromBody]DocumentUpdateRequest request)
    {
        var model = _mapper.Map<DocumentUpdateModel>(request);
        model.Id = id;
            
        var result = await _documentsService.Update(model);

        if (result.IsSuccessful == false)
            return result.Error;

        return _mapper.Map<DocumentDto>(result.ResultData);
    }
    
    /// <summary>
    /// Delete a document
    /// </summary>
    /// <param name="id">Document identifier</param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<BusinessResult> Delete(Guid id)
    {
        return await _documentsService.Delete(id);
    }
    
    /// <summary>
    /// Check if a document exists with specified user ID and document ID
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="documentId">Document identifier</param>
    /// <returns></returns>
    [HttpGet("exists")]
    public async Task<BusinessResult> Exists(Guid userId, Guid documentId)
    {
        return await _documentsService.Exists(userId, documentId);
    }
}