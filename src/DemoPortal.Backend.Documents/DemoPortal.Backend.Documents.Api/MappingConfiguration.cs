using AutoMapper;
using DemoPortal.Backend.Documents.Abstractions.Models;
using DemoPortal.Backend.Documents.Api.Contract;

namespace DemoPortal.Backend.Documents.Api
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<DocumentCreateRequest, DocumentCreateModel>();
            CreateMap<DocumentListGetRequest, DocumentListFilter>();
            CreateMap<DocumentUpdateRequest, DocumentUpdateModel>();
            
            CreateMap<DocumentGetModel, DocumentDto>();
            CreateMap<DocumentGetSimpleModel, DocumentSimpleDto>();
        }
    }
}