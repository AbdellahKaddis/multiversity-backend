using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace MultiVersity
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<University, UniversityDto>();
            CreateMap<UniversityForUpdateDto, University>();
            CreateMap<UniversityForCreationDto, University>();

            CreateMap<FacultyForCreationDto, Faculty>();
            CreateMap<Faculty, FacultyDto>();
            CreateMap<FacultyForUpdateDto, Faculty>();

            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeForCreationDto, Employee>();
         
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<UniversityAdminForRegistrationDto, User>();
        }
    }

}
