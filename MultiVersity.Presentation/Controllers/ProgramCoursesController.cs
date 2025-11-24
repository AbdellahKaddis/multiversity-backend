using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using MultiVersity.Presentation.ModelBinders;

namespace MultiVersity.Presentation.Controllers;

[Route("api/programCourses")]
[ApiController]
public class ProgramCoursesController : ControllerBase
{
    private readonly IServiceManager _service;
    public ProgramCoursesController(IServiceManager service) => _service = service;
    
    [HttpGet()]
    public async Task<IActionResult> GetProgramCourses([FromQuery] ProgramCourseParameteres programCourseParameteres)
    {
        var programCourses = await
        _service.ProgramCourseService.GetAllProgramCoursesAsync(programCourseParameteres, false);
        return Ok(programCourses);
    }

    [HttpGet("{id:guid}", Name = "ProgramCourseById")]

    public async Task<IActionResult> GetProgramCourse(Guid id)
    {
        var programCourse = await _service.ProgramCourseService.GetProgramCourseAsync(id, trackChanges:
        false);
        return Ok(programCourse);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateProgramCourse([FromBody] ProgramCourseForCreationDto
programCorseForCreationDto)
    {

        var createdProgramCourse = await _service.ProgramCourseService.CreateProgramCourseAsync(programCorseForCreationDto);
        return CreatedAtRoute("ProgramCourseById", new { id = createdProgramCourse.Id },
        createdProgramCourse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProgramCourse(Guid id)
    {
        await _service.ProgramCourseService.RemoveProgramCourseAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateProgramCourse(Guid id, [FromBody] ProgramCourseForUpdateDto
programCourseForUpdateDto)
    {

        await _service.ProgramCourseService.UpdateProgramCourseAsync(id, programCourseForUpdateDto, trackChanges:
        true);
        return NoContent();
    }
    [HttpGet("collection/({ids})", Name = "ProgramCourseCollection")]
    public async Task<IActionResult> GetProgramCourseCollection
        ([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var programCourses = await _service.ProgramCourseService.GetByIdsAsync(ids, trackChanges:
        false);
        return Ok(programCourses);
    }

    [HttpPost("collection")]
    public async Task<IActionResult> CreateProgramCourseCollection
        ([FromBody] IEnumerable<ProgramCourseForCreationDto> programCourseCollection)
    {
        var result = await
        _service.ProgramCourseService.CreateProgramCourseCollectionAsync(programCourseCollection);
        return CreatedAtRoute("ProgramCourseCollection", new { result.ids },
        result.programCourses);
    }

}
