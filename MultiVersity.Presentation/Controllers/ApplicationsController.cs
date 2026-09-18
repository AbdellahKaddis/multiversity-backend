

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace MultiVersity.Presentation.Controllers;

[Route("api/applications")]
[ApiController]
public class ApplicationsController : ControllerBase
{
    private readonly IServiceManager _service;
    public ApplicationsController(IServiceManager service) => _service = service;

    [HttpGet("/api/applications")]

    public async Task<IActionResult> GetApplications([FromQuery] ApplicationParameters applicationParameters)
    {
        var applications = await
        _service.ApplicationService.GetApplicationsAsync(applicationParameters, false);
        return Ok(applications);
    }

    [HttpGet("{id}", Name = "GetApplicationById")]

    public async Task<IActionResult> GetApplicationById(Guid id)
    {
        var application = await _service.ApplicationService.GetApplicationAsync(id, trackChanges: false);
        return Ok(application);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateApplication([FromBody] ApplicationForCreationDto applicationForCreationDto)
    {
        var createdApplication = await _service.ApplicationService.CreateApplicationAsync(applicationForCreationDto, trackChanges: true);
        return CreatedAtRoute("GetApplicationById", new { id = createdApplication.Id },
        createdApplication);
    }

    [HttpPatch("{id:guid}/status")]
    //[Authorize(Roles = "FacultyAdmin,Dean")]
    public async Task<IActionResult> UpdateApplicationStatus(
    Guid id,
    [FromBody] UpdateApplicationStatusDto dto)
    {
        await _service.ApplicationService.UpdateApplicationStatusAsync(id, dto, trackChanges: true);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteApplication(Guid id)
    {
        await _service.ApplicationService.DeleteApplicationAsync(id, trackChanges: false);
        return NoContent();
    }

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteProfessor(string id)
    //{
    //    await _service.ProfessorService.DeleteProfessorAsync(id, trackChanges: false);
    //    return NoContent();
    //}

    //[HttpPut("{id}")]
    //[ServiceFilter(typeof(ValidationFilterAttribute))]
    //public async Task<IActionResult> UpdateProfessor(string id, [FromBody] ProfessorForUpdateDto professorForUpdateDto)
    //{
    //    await _service.ProfessorService.UpdateProfessorAsync(id, professorForUpdateDto, trackChanges: true);
    //    return NoContent();
    //}
}
