using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace MultiVersity.Presentation.Controllers;

[Route("api/universities/{universityId}/faculties")]
[ApiController]
public class FacultyController : ControllerBase
{
    private readonly IServiceManager _service;
    public FacultyController(IServiceManager service)
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
        var employee = await _service.FacultyService.GetFacultyAsync(universityId, id,
        trackChanges: false);
        return Ok(employee);
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
}
