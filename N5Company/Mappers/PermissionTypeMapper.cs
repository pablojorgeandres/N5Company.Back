using AutoMapper;
using N5Company.DTOs;
using N5Company.Entities;

namespace N5Company.Mappers
{
    public class PermissionTypeMapper : Profile
    {
        public PermissionTypeMapper() 
        {
            CreateMap<PermissionType, PermissionTypeDto>().ReverseMap();
        }
    }
}
