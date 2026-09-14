
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ProfessorCourseDto(
    Guid Id,
    string? ProfessorId,
    string? ProfessorName,
    Guid CourseId,
    string? CourseName,
    string? CourseCode,
    string? TeachingType,
    string? AcademicYear );
