
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace MultiVersity.Presentation.Controllers;

[Route("api/professorCourses")]
[ApiController]
public class ProfessorCourseController : ControllerBase
{
    private readonly IServiceManager _service;
    public ProfessorCourseController(IServiceManager service) => _service = service;

    [HttpGet()]
    public async Task<IActionResult> GetProfessorCourses([FromQuery] ProfessorCourseParameters professorCourseParameters)
    {
        var professorCourses = await
        _service.ProfessorCourseService.GetAllProfessorCoursesAsync(professorCourseParameters, false);
        return Ok(professorCourses);
    }

    [HttpGet("{id:guid}", Name = "ProfessorCourseById")]

    public async Task<IActionResult> GetPrrofessorCourse(Guid id)
    {
        var professorCourse = await _service.ProfessorCourseService.GetProfessorCourseAsync(id, trackChanges:
        false);
        return Ok(professorCourse);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateProfessorCourse([FromBody] ProfessorCourseForCreationDto
professorCourseForCreationDto)
    {

        var createdProfessorCourse = await _service.ProfessorCourseService.CreateProfessorCourseAsync(professorCourseForCreationDto);
        return CreatedAtRoute("ProfessorCourseById", new { id = createdProfessorCourse.Id },
        createdProfessorCourse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProfessorCourse(Guid id)
    {
        await _service.ProfessorCourseService.RemoveProfessorCourseAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateProfessorCourse(Guid id, [FromBody] ProfessorCourseForUpdateDto
professorCourseCourseForUpdateDto)
    {

        await _service.ProfessorCourseService.UpdateProfessorCourseAsync(id, professorCourseCourseForUpdateDto, trackChanges:
        true);
        return NoContent();
    }
 
}
