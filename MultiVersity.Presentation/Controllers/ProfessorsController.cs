using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace MultiVersity.Presentation.Controllers;

[Route("api/professors")]
[ApiController]
public class ProfessorsController : ControllerBase
{
    private readonly IServiceManager _service;
    public ProfessorsController(IServiceManager service) => _service = service;

    [HttpGet]

    public async Task<IActionResult> GetProfessors([FromQuery]ProfessorParameteres professorParameteres)
    {
        var professors = await
        _service.ProfessorService.GetAllProfessorsAsync(professorParameteres, false);
        return Ok(professors);
    }

    [HttpGet("{id}", Name = "GetProfessorById")]

    public async Task<IActionResult> GetProfessorById(string id)
    {
        var professor = await _service.ProfessorService.GetProfessorAsync(id, trackChanges:
        false);
        return Ok(professor);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateProfessor([FromBody] ProfessorForCreationDto professorForcreationDto)
    {
        var createdProfessor = await _service.ProfessorService.CreateProfessorAsync(professorForcreationDto);
        return CreatedAtRoute("GetProfessorById", new { id = createdProfessor.Id },
        createdProfessor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfessor(string id)
    {
        await _service.ProfessorService.DeleteProfessorAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateProfessor(string id, [FromBody] ProfessorForUpdateDto professorForUpdateDto)
    {
        await _service.ProfessorService.UpdateProfessorAsync(id, professorForUpdateDto, trackChanges: true);
        return NoContent();
    }
  
}
