using AutoMapper;
using EmployeeProfile.Data.Entity;
using EmployeeProfile.DTO;
using EmployeeProfile.DTO.Response;
using EmployeeProfile.DTO.Request;

namespace EmployeeProfile.Infrastructure.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<Person, CreatePersonRequest>().ReverseMap();
            CreateMap<Person, UpdatePersonRequest>().ReverseMap();
            CreateMap<Person, PersonQueryResponse>().ReverseMap();
        }
    }
}
