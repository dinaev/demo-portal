using AutoMapper;
using ClientContract = DemoPortal.Backend.Documents.Api.Contract;
using DemoPortal.Backend.GateWay.Contract.Documents;

namespace DemoPortal.Backend.GateWay.Web
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<DocumentCreateRequest, ClientContract.DocumentCreateRequest>();
            CreateMap<DocumentUpdateRequest, ClientContract.DocumentUpdateRequest>();
            
            CreateMap<ClientContract.DocumentDto, DocumentDto>();
            CreateMap<ClientContract.DocumentSimpleDto, DocumentSimpleDto>();
            CreateMap<ClientContract.DocumentListGetResponse, DocumentListGetResponse>();
        }
    }
}