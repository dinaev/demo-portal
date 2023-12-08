using AutoMapper;
using ClientContract = DemoPortal.Backend.Documents.Api.Contract;
using DocumentCreateRequest = DemoPortal.Backend.GateWay.Contract.Documents.DocumentCreateRequest;
using DocumentDto = DemoPortal.Backend.GateWay.Contract.Documents.DocumentDto;
using DocumentListGetResponse = DemoPortal.Backend.GateWay.Contract.Documents.DocumentListGetResponse;
using DocumentSimpleDto = DemoPortal.Backend.GateWay.Contract.Documents.DocumentSimpleDto;
using DocumentUpdateRequest = DemoPortal.Backend.GateWay.Contract.Documents.DocumentUpdateRequest;

namespace DemoPortal.Backend.GateWay.Api
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