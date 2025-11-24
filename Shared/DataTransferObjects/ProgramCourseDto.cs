using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public record ProgramCourseDto
(
     Guid Id,
     Guid CourseId,
     Guid ProgramId,
     uint Semester,
     CourseDto Course
);
