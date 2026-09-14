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
            CreateMap<Faculty, FacultyDto>()
     .ForCtorParam(
         "DeanName",
         opt => opt.MapFrom(src =>
             src.FacultyDeans
                 .Where(fd => fd.EndDate == null)
                 .Select(fd => fd.Dean.FirstName + " " + fd.Dean.LastName)
                 .FirstOrDefault()
         ))
     .ForCtorParam(
         "DepartmentCount",
         opt => opt.MapFrom(src => src.Departments.Count)
     );
            CreateMap<FacultyForUpdateDto, Faculty>();


            CreateMap<UniversityAdminForRegistrationDto, User>();

            CreateMap<FacultyDeanForRegistrationDto, User>();

            CreateMap<DepartmentForCreationDto, Department>();
            CreateMap<Department, DepartmentDto>()
     .ForCtorParam(
         "DepartmentHead",
         opt => opt.MapFrom(src =>
             src.Professors!
                 .Where(p =>(bool) p.IsDepartmentHead)
                 .Select(p => p.User.FirstName + " " + p.User.LastName)
                 .FirstOrDefault()
         ));
            CreateMap<DepartmentForUpdateDto, Department>();

            CreateMap<DegreeForCreationDto, Degree>();
            CreateMap<Degree, DegreeDto>();
            CreateMap<DegreeForUpdateDto, Degree>();

            CreateMap<AcademicProgramForCreationDto, Entities.Models.AcademicProgram>();
            CreateMap<Entities.Models.AcademicProgram, AcademicProgramDto>()
                 .ForCtorParam(
        "FacultyName",
        opt => opt.MapFrom(src => src.Department.Faculty.Name))
         .ForCtorParam(
        "FacultyId",
        opt => opt.MapFrom(src => src.Department.Faculty.Id));
            CreateMap<AcademicProgramForUpdateDto, Entities.Models.AcademicProgram>();

            CreateMap<CourseForCreationDto, Course>();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseForUpdateDto, Course>();

            CreateMap<ProgramCourseForCreationDto, ProgramCourse>();
            CreateMap<ProgramCourse, ProgramCourseDto>();
            CreateMap<ProgramCourseForUpdateDto, ProgramCourse>();

            CreateMap<FacultyDeanForCreationDto, FacultyDean>();
            CreateMap<FacultyDean, FacultyDeanDto>().ForCtorParam("DeanName",
            opt => opt.MapFrom(x => string.Join(' ', x.Dean.FirstName, x.Dean.LastName)));;
            CreateMap<FacultyDeanForUpdateDto, FacultyDean>();

            CreateMap<ProfessorForCreationDto, Professor>();

            CreateMap<Professor, ProfessorDto>()
                .ForCtorParam("FirstName",
                opt => opt.MapFrom(src => src.User.FirstName))

            .ForCtorParam("LastName",
                opt => opt.MapFrom(src => src.User.LastName))

            .ForCtorParam("Email",
                opt => opt.MapFrom(src => src.User.Email))

            .ForCtorParam("Cin",
                opt => opt.MapFrom(src => src.User.Cin))

             .ForCtorParam("DepartmentName",
                opt => opt.MapFrom(src => src.Department.Name));
            CreateMap<ProfessorForUpdateDto, Professor>();
            CreateMap<ProfessorForUpdateDto, User>();
            CreateMap<ProfessorForCreationDto, User>();



            CreateMap<ProfessorCourseForCreationDto, ProfessorCourse>();
            CreateMap<ProfessorCourse, ProfessorCourseDto>()
                  .ForCtorParam("ProfessorName",
                opt => opt.MapFrom(src => src.Professor.User.FirstName + " " + src.Professor.User.FirstName))

                    .ForCtorParam("CourseName",opt => opt.MapFrom(src => src.Course.Title))

                      .ForCtorParam("CourseCode", opt => opt.MapFrom(src => src.Course.Code));
            CreateMap<ProfessorCourseForUpdateDto, ProfessorCourse>();


            CreateMap<Admission, AdmissionDto>()
            .ForCtorParam("ProgramName", o => o.MapFrom(s => s.Program.Name))
    .ForCtorParam("FacultyId", o => o.MapFrom(s => s.Program.Department.FacultyId));
            CreateMap<AdmissionForCreationDto, Admission>();
            CreateMap<AdmissionForUpdateDto, Admission>()
            .ForMember(d => d.Requirements, opt => opt.Ignore());
 

            CreateMap<AdmissionRequirementForUpdateDto, AdmissionRequirement>();
            CreateMap<AdmissionRequirementForCreationDto, AdmissionRequirement>();
            CreateMap<AdmissionRequirement, AdmissionRequirementDto>();

            
        }
    }

}
