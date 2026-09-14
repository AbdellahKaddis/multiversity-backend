using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using MultiVersity.Presentation.ModelBinders;

namespace MultiVersity.Presentation.Controllers;

[Route("api/facultyDeans")]
[ApiController]
public class FacultyDeansConroller : ControllerBase
{
    private readonly IServiceManager _service;
    public FacultyDeansConroller(IServiceManager service) => _service = service;

    [HttpGet()]
    public async Task<IActionResult> GetFacultyDeans([FromQuery] FacultyDeanParameters facultyDeanParameteres)
    {
        var facultyDeans = await
        _service.FacultyDeanService.GetAllFacultyDeansAsync(facultyDeanParameteres, false);
        return Ok(facultyDeans);
    }

    [HttpGet("{id:guid}", Name = "FacultyDeanById")]

    public async Task<IActionResult> GetFacultyDean(Guid id)
    {
        var facultyDean = await _service.FacultyDeanService.GetFacultyDeanAsync(id, trackChanges:
        false);
        return Ok(facultyDean);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateFacultyDean([FromBody] FacultyDeanForCreationDto
facultyDeanForCreationDto)
    {

        var createdFacultyDean = await _service.FacultyDeanService.CreateFacultyDeanAsync(facultyDeanForCreationDto);
        return CreatedAtRoute("FacultyDeanById", new { id = createdFacultyDean.Id },
        createdFacultyDean);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFacultyDean(Guid id)
    {
        await _service.FacultyDeanService.RemoveFacultyDeanAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateFacultyDean(Guid id, [FromBody] FacultyDeanForUpdateDto
facultyDeanForUpdateDto)
    {

        await _service.FacultyDeanService.UpdateFacultyDeanAsync(id, facultyDeanForUpdateDto, trackChanges:
        true);
        return NoContent();
    }

    [HttpGet("current/{facultyId:guid}")]
    public async Task<IActionResult> GetCurrentFacultyDean(Guid facultyId)
    {
        var facultyDean = await
        _service.FacultyDeanService.GetCurrentFacultyDeanAsync(facultyId, false);
        return Ok(facultyDean);
    }
}
