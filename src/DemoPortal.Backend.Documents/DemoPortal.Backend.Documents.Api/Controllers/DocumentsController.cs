using AutoMapper;
using DemoPortal.Backend.Documents.Abstractions.Models;
using DemoPortal.Backend.Documents.Abstractions.Services;
using DemoPortal.Backend.Documents.Api.Contract;
using DemoPortal.Backend.Shared.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace DemoPortal.Backend.Documents.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentsController : Controller
{
    private readonly IMapper _mapper;
    private readonly IDocumentsService _documentsService;

    public DocumentsController(IMapper mapper, IDocumentsService documentsService)
    {
        _mapper = mapper;
        _documentsService = documentsService;
    }

    [HttpPost]
    public async Task<BusinessResult<DocumentDto>> Create([FromBody]DocumentCreateRequest request)
    {
        var document = _mapper.Map<DocumentCreateModel>(request);
            
        var result = await _documentsService.Create(document);
        
        if (result.IsSuccessful == false)
            return result.Error;
        
        return _mapper.Map<DocumentDto>(result.ResultData);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<BusinessResult<DocumentDto>> Get(Guid id)
    {
        var result = await _documentsService.GetById(id);

        if (result.IsSuccessful == false)
            return result.Error;

        return _mapper.Map<DocumentDto>(result.ResultData);
    }
    
    [HttpGet]
    public async Task<BusinessResult<DocumentListGetResponse>> GetList(DocumentListGetRequest request)
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
    
    [HttpPut]
    public async Task<BusinessResult<DocumentDto>> Update([FromBody]DocumentCreateRequest request)
    {
        var model = _mapper.Map<DocumentUpdateModel>(request);
            
        var result = await _documentsService.Update(model);

        if (result.IsSuccessful == false)
            return result.Error;

        return _mapper.Map<DocumentDto>(result.ResultData);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<BusinessResult> Delete(Guid id)
    {
        return await _documentsService.Delete(id);
    }
}