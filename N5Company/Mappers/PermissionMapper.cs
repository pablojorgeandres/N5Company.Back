using AutoMapper;
using N5Company.DTOs;
using N5Company.Entities;

namespace N5Company.Mappers
{
    public class PermissionMapper : Profile
    {
        public PermissionMapper()
        {
            CreateMap<Permission, PermissionDto>().ReverseMap();
        }
    }
}
