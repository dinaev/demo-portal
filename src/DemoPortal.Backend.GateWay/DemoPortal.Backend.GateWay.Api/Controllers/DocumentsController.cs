using AutoMapper;
using DemoPortal.Backend.Documents.Api.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClientContract = DemoPortal.Backend.Documents.Api.Contract;
using DocumentCreateRequest = DemoPortal.Backend.GateWay.Contract.Documents.DocumentCreateRequest;
using DocumentDto = DemoPortal.Backend.GateWay.Contract.Documents.DocumentDto;
using DocumentListGetResponse = DemoPortal.Backend.GateWay.Contract.Documents.DocumentListGetResponse;
using DocumentUpdateRequest = DemoPortal.Backend.GateWay.Contract.Documents.DocumentUpdateRequest;

namespace DemoPortal.Backend.GateWay.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DocumentsController : PortalController
{
    private readonly IDocumentsApi _documentsApi;

    public DocumentsController(
        IMapper mapper,
        ILogger<DocumentsController> logger, 
        IDocumentsApi documentsApi) : base(mapper, logger)
    {
        _documentsApi = documentsApi;
    }
    
    /// <summary>
    /// Create a document
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DocumentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody]DocumentCreateRequest request)
    {
        var clientRequest = Mapper.Map<ClientContract.DocumentCreateRequest>(request);
        clientRequest.UserId = UserId;
            
        var result = await _documentsApi.Create(clientRequest);
        
        return RenderResult(result);
    }

    /// <summary>
    /// Get a document by ID
    /// </summary>
    /// <param name="id">Document identifier</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DocumentDto),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(Guid id)
    {
        var check = await _documentsApi.Exists(UserId, id);
        if (!check.IsSuccessful)
            return RenderError(check.Error);
        
        var result = await _documentsApi.Get(id);
        return RenderResult(result);
    }
    
    /// <summary>
    /// Get a document list for a specifier user
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(DocumentListGetResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetList()
    {
        var clientRequest = new ClientContract.DocumentListGetRequest
        {
            UserId = UserId
        };
        
        var result = await _documentsApi.GetList(clientRequest);

        return RenderResult<ClientContract.DocumentListGetResponse, DocumentListGetResponse>(result);
    }
    
    /// <summary>
    /// Update a document
    /// </summary>
    /// <param name="id">Document identifier</param>
    /// <param name="request">Fields to update</param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DocumentDto),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody]DocumentUpdateRequest request)
    {
        var check = await _documentsApi.Exists(UserId, id);
        if (!check.IsSuccessful)
            return RenderError(check.Error);
        
        var clientRequest = Mapper.Map<ClientContract.DocumentUpdateRequest>(request);
        var result = await _documentsApi.Update(id, clientRequest);

        return RenderResult<ClientContract.DocumentDto, DocumentDto>(result);
    }
    
    /// <summary>
    /// Delete a document
    /// </summary>
    /// <param name="id">Document identifier</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(DocumentDto),StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var check = await _documentsApi.Exists(UserId, id);
        if (!check.IsSuccessful)
            return RenderError(check.Error);
        
        var result = await _documentsApi.Delete(id);
        
        return RenderResult(result);
    }
}