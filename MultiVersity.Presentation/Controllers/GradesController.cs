using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace MultiVersity.Presentation.Controllers;

[Route("api/grades")]
[ApiController]
public class GradesController : ControllerBase
{
    private readonly IServiceManager _service;

    public GradesController(IServiceManager service) => _service = service;
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetGrades([FromQuery] GradeParameters gradeParameters)
    {
        var grades = await _service.GradeService.GetGradesAsync(gradeParameters, trackChanges: false);
        return Ok(grades);
    }
    [Authorize]
    [HttpGet("{id:guid}", Name = "GetGradeById")]
    public async Task<IActionResult> GetGrade(Guid id)
    {
        var grade = await _service.GradeService.GetGradeAsync(id, trackChanges: false);
        return Ok(grade);
    }
    [Authorize]
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateGrade([FromBody] GradeForCreationDto dto)
    {
        var createdGrade = await _service.GradeService.CreateGrade(dto);
        return CreatedAtRoute("GetGradeById", new { id = createdGrade.Id }, createdGrade);
    }
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGrade(Guid id)
    {
        await _service.GradeService.DeleteGrade(id, trackChanges: false);
        return NoContent();
    }
    [Authorize]
    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateGrade(Guid id, [FromBody] GradeForUpdateDto dto)
    {
        await _service.GradeService.UpdateGrade(id, dto, trackChanges: true);
        return NoContent();
    }
    [Authorize]
    [HttpPost("publish")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> PublishGrades([FromBody] PublishGradesDto dto)
    {
        await _service.GradeService.PublishGradesAsync(dto, trackChanges: true);
        return NoContent();
    }
    [Authorize]
    [HttpGet("semester/{enrollmentId:guid}")]
    public async Task<IActionResult> GetSemesterResult(
    Guid enrollmentId,
    [FromQuery] string semester)
    {
        if (string.IsNullOrWhiteSpace(semester))
            return BadRequest("Semester is required.");

        var result = await _service.GradeService
            .GetSemesterResultAsync(enrollmentId, semester, trackChanges: false);

        return Ok(result);
    }
}