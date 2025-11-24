using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace MultiVersity.Presentation.Controllers;

[Route("api/programs")]
[ApiController]
public class AcademicProgramsController : ControllerBase
{
    private readonly IServiceManager _service;
    public AcademicProgramsController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetPrograms([FromQuery]ProgramParameters programParameters)
    {
        var programs = await _service.ProgramService.GetProgramsAsync(programParameters, trackChanges: false);
        return Ok(programs);
    }

    [HttpGet("{id:guid}", Name = "GetProgramById")]

    public async Task<IActionResult> GetProgramById(Guid id)
    {
        var program = await _service.ProgramService.GetProgramAsync(id,
        trackChanges: false);
        return Ok(program);
    }

    [HttpPost("/api/departments/{departmentId}/programs")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateProgramForDepartment(Guid departmentId, [FromBody]
    AcademicProgramForCreationDto programForCreationDto)
    {
        var programToReturn = await _service.ProgramService
            .CreateProgramForDepatmentAsync(departmentId, programForCreationDto, trackChanges: false);

        return CreatedAtRoute("GetProgramById", new
        {
            id = programToReturn.Id
        },
        programToReturn);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProgram(Guid id)
    {
        await _service.ProgramService.DeleteProgramAsync(id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateProgram(Guid id, [FromBody] AcademicProgramForUpdateDto programForUpdateDto)
    {

        await _service.ProgramService.UpdateProgramAsync(id, programForUpdateDto, trackChanges: true);
        return NoContent();
    }
}
