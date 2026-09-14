using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace MultiVersity.Presentation.Controllers;

[Route("api/universities/{universityId}/faculties")]
[ApiController]
public class FacultiesController : ControllerBase
{
    private readonly IServiceManager _service;
    public FacultiesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetFacultiesForUniversity(Guid universityId)
    {
        var faculties = await _service.FacultyService
            .GetFacultiesAsync(universityId, trackChanges: false);
        return Ok(faculties);
    }

    [HttpGet("{id:guid}", Name = "GetFacultyForUniversity")]

    public async Task<IActionResult> GetFacultyForUniversity(Guid universityId, Guid id)
    {
        var faculty = await _service.FacultyService.GetFacultyAsync(universityId, id,
        trackChanges: false);
        return Ok(faculty);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateFacultyForUniversity(Guid universityId, [FromBody]
    FacultyForCreationDto faculty)
    {
        var facultyToReturn = await _service.FacultyService
            .CreateFacultyForUniversityAsync(universityId, faculty, trackChanges:false);
        
        return CreatedAtRoute("GetFacultyForUniversity", new
        {
            universityId,
            id = facultyToReturn.Id
        },
        facultyToReturn);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFacultyForUniversity(Guid universityId, Guid id)
    {
        await _service.FacultyService.DeleteFacultyForUniversityAsync(universityId, id, trackChanges:
        false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateFacultyForUniversity(Guid universityId, Guid id,
[FromBody] FacultyForUpdateDto faculty)
    {

        await _service.FacultyService.UpdateFacultyForUniversityAsync(universityId, id, faculty,
         uniTrackChanges: false, facTrackChanges: true);
        return NoContent();
    }
    [HttpGet("/api/deans/{deanId}/faculty")]

    public async Task<IActionResult> GetFacultyByDeanId(string deanId)
    {
        var faculty = await _service.FacultyService.GetFacultyByDeanIdAsync(deanId, trackChanges:
        false);
        return Ok(faculty);
    }

    [HttpPatch("~/api/faculties/{facultyId}/dean/end")]
    public async Task<IActionResult> EndDeanAssignment(Guid facultyId)
    {
        await _service.FacultyService.EndDeanAssignmentAsync(facultyId);

        return NoContent();
    }

    [HttpPost("~/api/faculties/{facultyId}/dean")]
    public async Task<IActionResult> CreateAndAssignDean(Guid facultyId,
    [FromBody] FacultyDeanForRegistrationDto dto)
    {
        await _service.FacultyDeanService.CreateAndAssignDeanAsync(facultyId, dto);

        return StatusCode(StatusCodes.Status201Created);
    }
}
