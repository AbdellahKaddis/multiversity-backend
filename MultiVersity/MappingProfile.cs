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
            CreateMap<Faculty, FacultyDto>().ForCtorParam("DeanName",
            opt => opt.MapFrom(x => string.Join(' ', x.Dean.FirstName, x.Dean.LastName)));
            CreateMap<FacultyForUpdateDto, Faculty>();

            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeForCreationDto, Employee>();
         
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<UniversityAdminForRegistrationDto, User>();

            CreateMap<FacultyDeanForRegistrationDto, User>();

            CreateMap<DepartmentForCreationDto, Department>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentForUpdateDto, Department>();

            CreateMap<DegreeForCreationDto, Degree>();
            CreateMap<Degree, DegreeDto>();
            CreateMap<DegreeForUpdateDto, Degree>();

            CreateMap<AcademicProgramForCreationDto, Entities.Models.AcademicProgram>();
            CreateMap<Entities.Models.AcademicProgram, AcademicProgramDto>();
            CreateMap<AcademicProgramForUpdateDto, Entities.Models.AcademicProgram>();

            CreateMap<CourseForCreationDto, Course>();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseForUpdateDto, Course>();

            CreateMap<ProgramCourseForCreationDto, ProgramCourse>();
            CreateMap<ProgramCourse, ProgramCourseDto>();
            CreateMap<ProgramCourseForUpdateDto, ProgramCourse>();

        }
    }

}
