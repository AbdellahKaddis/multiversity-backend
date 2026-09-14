using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using MultiVersity.Presentation.ActionFilters;

namespace MultiVersity.Presentation.Controllers;

[Route("api/universities")]
[ApiController]
public class UniversitiesController : ControllerBase
{
    private readonly IServiceManager _service;
    public UniversitiesController(IServiceManager service) => _service = service;

    [HttpGet]
    //[Authorize(Roles = "Manager")]
    public async Task<IActionResult> GetUniversities()
    {
        var universities = await
        _service.UniversityService.GetAllUniversitiesAsync(trackChanges: false);
        return Ok(universities);
    }

    [HttpGet("{id:guid}", Name = "UniversityById")]

    public async Task<IActionResult> GetUniversity(Guid id)
    {
        var university = await _service.UniversityService.GetUniversityAsync(id, trackChanges:
        false);
        return Ok(university);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateUniversity([FromBody] UniversityForCreationDto university)
    {
        var createdUniversity = await _service.UniversityService.CreateUniversityAsync(university);
        return CreatedAtRoute("UniversityById", new { id = createdUniversity.Id },
        createdUniversity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUniversity(Guid id)
    {
        await _service.UniversityService.DeleteUniversityAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateUniversity(Guid id, [FromBody] UniversityForUpdateDto university)
    {
        await _service.UniversityService.UpdateUniversityAsync(id, university, trackChanges:
        true);
        return NoContent();
    }
    [HttpGet("{email}/{name}")]
    public async Task<IActionResult> CheckForDuplicatesAsync(string email, string name)
    {
        await _service.UniversityService.CheckForDuplicatesAsync(email, name, null);
        return Ok();
    }


    [HttpGet("/api/admins/{adminId}/university")]

    public async Task<IActionResult> GetUniversityByAdminId(string adminId)
    {
        var university = await _service.UniversityService.GetUniversityByAdminIdAsync(adminId, trackChanges:
        false);
        return Ok(university);
    }

    [HttpGet("{universityId:guid}/statistics")] 
    public async Task<IActionResult> GetStatistics(Guid universityId) 
    { 
        var statistics = await _service.UniversityService.GetStatisticsAsync(universityId);
        return Ok(statistics);
    }
}
