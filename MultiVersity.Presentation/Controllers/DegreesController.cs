using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace MultiVersity.Presentation.Controllers;

[Route("api/degrees")]
[ApiController]
public class DegreesController : ControllerBase
{
    public readonly IServiceManager _service;
    public DegreesController(IServiceManager service) => _service = service;

    [Route("/api/universities/{universityId}/degrees")]
    [HttpGet]
    public async Task<IActionResult> GetDegrees(Guid universityId)
    {
        var degrees = await _service.DegreeService.GetAllDegreesAsync(universityId, trackChanges: false);
        return Ok(degrees);
    }

    [HttpGet("{id:guid}", Name = "GetDegreeById")]

    public async Task<IActionResult> GetDegree(Guid id)
    {
        var degree = await _service.DegreeService.GetDegreeAsync(id, trackChanges: false);
        return Ok(degree);
    }

    [Route("/api/universities/{universityId}/degrees")]
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateDegree(Guid universityId, [FromBody] DegreeForCreationDto degree)
    {

        var createdDegree = await _service.DegreeService.CreateDegree(universityId, degree);
        return CreatedAtRoute("GetDegreeById", new { id = createdDegree.Id },
        createdDegree);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDegree(Guid id)
    {
        await _service.DegreeService.DeleteDegree(id, trackChanges: false);
        return NoContent();
    }
    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateDegree(Guid id, [FromBody] DegreeForUpdateDto degree)
    {

        await _service.DegreeService.UpdateDegree(id, degree, trackChanges: true);
        return NoContent();
    }
}
