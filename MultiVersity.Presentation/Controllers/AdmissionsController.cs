using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace MultiVersity.Presentation.Controllers;

[Route("api/admissions")]
[ApiController]
public class AdmissionsController : ControllerBase
{
    private readonly IServiceManager _service;
    public AdmissionsController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("/api/{universityId}/admissions")]
    public async Task<IActionResult> GetAdmissions(Guid universityId, [FromQuery] AdmissionParameters admissionParameters)
    {
        var admissions = await _service.AdmissionService.GetAdmissionsAsync(universityId, admissionParameters, trackChanges: false);
        return Ok(admissions);
    }

    [HttpGet("{id:guid}", Name = "GetAdmissionForProgram")]

    public async Task<IActionResult> GetAdmissionForProgram(Guid id)
    {
        var admission = await _service.AdmissionService.GetAdmissionAsync(id,trackChanges: false);
        return Ok(admission);
    }
    [Authorize]
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateAdmissionForProgram([FromBody]
    AdmissionForCreationDto admissionForCreationDto)
    {
        var admissionToReturn = await _service.AdmissionService
            .CreateAdmissionForProgramAsync(admissionForCreationDto, trackChanges: false);

        return CreatedAtRoute("GetAdmissionForProgram", new
        {
            id = admissionToReturn.Id
        },
        admissionToReturn);
    }
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAdmissionForProgram(Guid id)
    {
        await _service.AdmissionService.DeleteAdmissionForProgramAsync(id, trackChanges:false);
        return NoContent();
    }
    [Authorize]
    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateAdmissionForProgram(Guid id,
[FromBody] AdmissionForUpdateDto admissionForUpdateDto)
    {

        await _service.AdmissionService.UpdateAdmissionForProgramAsync(id, admissionForUpdateDto,
         progTrackChanges: false, admTrackChanges: true);
        return NoContent();
    }
}
