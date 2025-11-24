using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record CourseDto(
     Guid Id,
     string? Code,
     string? Title,
     string? Description,
     uint Coefficient,
     uint Credits,
     uint? HoursCM, 
     uint? HoursTD,
     uint? HoursTP,
     Guid FacultyId);
