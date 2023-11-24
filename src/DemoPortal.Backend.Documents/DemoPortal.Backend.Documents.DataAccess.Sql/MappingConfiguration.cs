using AutoMapper;
using DemoPortal.Backend.Documents.Abstractions.Models;

namespace DemoPortal.Backend.Documents.DataAccess.Sql;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<Entities.Document, DocumentGetModel>();
        CreateMap<Entities.Document, DocumentGetSimpleModel>();
        CreateMap<DocumentCreateModel, Entities.Document>();
        CreateMap<DocumentUpdateModel, Entities.Document>();
    }
}