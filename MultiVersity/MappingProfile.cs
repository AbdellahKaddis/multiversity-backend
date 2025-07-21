using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace SouqJemla
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<University, UniversityDto>();
            CreateMap<UniversityForUpdateDto, University>();
            CreateMap<UniversityForCreationDto, University>();

            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeForCreationDto, Employee>();
         
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<UserForRegistrationDto, User>();
        }
    }

}
