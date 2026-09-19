using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace MultiVersity.Presentation.Controllers;
[Route("api/courses")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly IServiceManager _service;
    public CoursesController(IServiceManager service) => _service = service;

    [HttpGet("/api/faculties/{facultyId}/courses")]
    public async Task<IActionResult> GetCourses(Guid facultyId)
    {
        var courses = await
        _service.CourseService.GetAllCoursesAsync(facultyId, trackChanges: false);
        return Ok(courses);
    }

    [HttpGet("{id:guid}", Name = "CourseById")]

    public async Task<IActionResult> GetCourse(Guid id)
    {
        var course = await _service.CourseService.GetCourseAsync(id, trackChanges:
        false);
        return Ok(course);
    }
    [Authorize]
    [HttpPost("/api/faculties/{facultyId}/courses")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCourse(Guid facultyId, [FromBody] CourseForCreationDto
courseForCreationDto)
    {

        var createdCourse = await _service.CourseService.CreateCourseAsync(facultyId, courseForCreationDto);
        return CreatedAtRoute("CourseById", new { id = createdCourse.Id },
        createdCourse);
    }
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        await _service.CourseService.DeleteCourseAsync(id, trackChanges: false);
        return NoContent();
    }
    [Authorize]
    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] CourseForUpdateDto
courseForUpdateDto)
    {

        await _service.CourseService.UpdateCourseAsync(id, courseForUpdateDto, trackChanges:
        true);
        return NoContent();
    }
}
