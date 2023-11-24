using AutoMapper;
using DemoPortal.Backend.Documents.Abstractions.Models;
using DemoPortal.Backend.Documents.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoPortal.Backend.Documents.DataAccess.Sql.Repositories;

public class DocumentsRepository : IDocumentsRepository
{
    private readonly IMapper _mapper;
    private readonly DocumentsContext _context;

    public DocumentsRepository(IMapper mapper, DocumentsContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<DocumentGetModel> Create(DocumentCreateModel model)
    {
        var document = _mapper.Map<Entities.Document>(model);
        _context.Documents.Add(document);
        await _context.SaveChangesAsync();
            
        _context.Entry(document).State = EntityState.Detached;
        
        return _mapper.Map<DocumentGetModel>(document);
    }

    public async Task<DocumentGetModel> GetById(Guid id)
    {
        var entity = await _context.Documents.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == default)
            return default;
        
        return _mapper.Map<DocumentGetModel>(entity);
    }

    public async Task<DocumentGetSimpleModel[]> GetList(DocumentListFilter filter)
    {
        var entity = await _context.Documents
            .Where(x => x.UserId == filter.UserId)
            .ToListAsync();
        
        return _mapper.Map<DocumentGetSimpleModel[]>(entity);
    }

    public async Task<DocumentGetModel> Update(DocumentUpdateModel model)
    {
        var entity = await _context.Documents.SingleOrDefaultAsync(x => x.Id == model.Id);
        if (entity == null)
            return null;
        
        _mapper.Map(model, entity);
        _context.Documents.Update(entity);
        await _context.SaveChangesAsync();
            
        _context.Entry(entity).State = EntityState.Detached;
        return _mapper.Map<DocumentGetModel>(entity);
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = new Entities.Document
        {
            Id = id
        };
        
        _context.Documents.Remove(entity);

        await _context.SaveChangesAsync();
        _context.Entry(entity).State = EntityState.Detached;
        return true;
    }

    public async Task<bool> Exists(Guid userId, Guid documentId)
    {
        return await _context.Documents.AnyAsync(x => x.UserId == userId && x.Id == documentId);
    }
}