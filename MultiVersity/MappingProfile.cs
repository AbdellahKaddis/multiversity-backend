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
     .ForCtorParam("ProfessorName", o => o.MapFrom(s =>
         s.Professor.User.FirstName + " " + s.Professor.User.LastName))
     .ForCtorParam("CourseName", o => o.MapFrom(s => s.Course.Title))
     .ForCtorParam("CourseCode", o => o.MapFrom(s => s.Course.Code))
     .ForCtorParam("ProgramName", o => o.MapFrom(s =>
         s.Course.ProgramCourses.Select(pgc => pgc.AcademicProgram.Name).FirstOrDefault()))
     .ForCtorParam("Semester", o => o.MapFrom(s =>
         s.Course.ProgramCourses.Select(pgc => (uint?)pgc.Semester).FirstOrDefault()))
     .ForCtorParam("StudentCount", o => o.MapFrom(s =>
         s.Course.ProgramCourses
             .SelectMany(pgc => pgc.AcademicProgram.Enrollments)
             .Count(e => e.Status == "Active")))
     .ForCtorParam("Coefficient", o => o.MapFrom(s => s.Course.Coefficient))
     .ForCtorParam("Credits", o => o.MapFrom(s => s.Course.Credits))
     .ForCtorParam("HoursCM", o => o.MapFrom(s => s.Course.HoursCM))
     .ForCtorParam("HoursTD", o => o.MapFrom(s => s.Course.HoursTD))
     .ForCtorParam("HoursTP", o => o.MapFrom(s => s.Course.HoursTP));
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

            CreateMap<ApplicantForRegistrationDto, User>();
            CreateMap<ApplicantForRegistrationDto, Applicant>();
            CreateMap<ApplicantForUpdateDto, Applicant>();

            CreateMap<Applicant, ApplicantDto>()
                 .ForCtorParam("FullName",
        o => o.MapFrom(s => s.User.FirstName + " " + s.User.LastName))
                 .ForCtorParam("StudentNumber",
        o => o.MapFrom(s => s.Enrollments.FirstOrDefault() != null
            ? s.Enrollments.First().StudentNumber
            : null))
                     .ForCtorParam("EnrolledAt",
        o => o.MapFrom(s => (DateTime?)(s.Enrollments.FirstOrDefault() != null
            ? s.Enrollments.First().EnrolledAt
            : (DateTime?)null)));

            CreateMap<Application, ApplicationDto>()
                 .ForCtorParam("ApplicantFullName",
               opt => opt.MapFrom(src => src.Applicant.User.FirstName + " " + src.Applicant.User.LastName))

                   .ForCtorParam("ProgramName", opt => opt.MapFrom(src => src.Program.Name))
                    .ForCtorParam("FacultyName", opt => opt.MapFrom(src => src.Applicant.Faculty.Name))

                     .ForCtorParam("ReviewerName", opt => opt.MapFrom(src => src.Reviewer == null ? null : src.Reviewer.FirstName + " " + src.Reviewer.LastName));

       
            CreateMap<ApplicantForUpdateDto, Application>();
            CreateMap<ApplicationForCreationDto, Application>();

            CreateMap<Enrollment, EnrollmentDto>()
               .ForCtorParam("ApplicantFullName",
             opt => opt.MapFrom(src => src.Applicant.User.FirstName + " " + src.Applicant.User.LastName))

                 .ForCtorParam("ProgramName", opt => opt.MapFrom(src => src.Program.Name))
                  .ForCtorParam("FacultyName", opt => opt.MapFrom(src => src.Faculty.Name));

            CreateMap<EnrollmentForCreationDto, Enrollment>();
            CreateMap<EnrollmentForUpdateDto, Enrollment>();

            CreateMap<Grade, GradeDto>()
                .ForMember(d => d.StudentFullName, o => o.MapFrom(s =>
                    s.Enrollment.Applicant.User.FirstName + " " +
                    s.Enrollment.Applicant.User.LastName))
                .ForMember(d => d.CourseCode, o => o.MapFrom(s => s.Course.Code))
                .ForMember(d => d.CourseName, o => o.MapFrom(s => s.Course.Title))
                .ForMember(d => d.ProgramName, o => o.MapFrom(s => s.Enrollment.Program.Name))
                .ForMember(d => d.Coefficient, o => o.MapFrom(s => s.Course.Coefficient))
                .ForMember(d => d.Credits, o => o.MapFrom(s => s.Course.Credits))
                .ForMember(d => d.ProfessorFullName, o => o.MapFrom(s =>
                    s.Professor != null
                        ? s.Professor.User.FirstName + " " + s.Professor.User.LastName
                        : null));
            CreateMap<GradeForCreationDto, Grade>();
            CreateMap<GradeForUpdateDto, Grade>();

        }
    }

}
