using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace MultiVersity.Presentation.Controllers;

[Route("api/applicants")]
[ApiController]
public class ApplicantsController : ControllerBase
{
    private readonly IServiceManager _service;
    public ApplicantsController(IServiceManager service) => _service = service;

    [HttpGet("/api/{universityId}/applicants")]

    public async Task<IActionResult> GetApplicantss(Guid universityId, [FromQuery] ApplicantParameters applicantParameters)
    {
        var professors = await
        _service.ApplicantService.GetApplicantsAsync(universityId, applicantParameters, false);
        return Ok(professors);
    }

    [HttpGet("{id}", Name = "GetApplicantById")]

    public async Task<IActionResult> GetApplicantById(string id)
    {
        var applicant = await _service.ApplicantService.GetApplicantAsync(id, trackChanges: false);
        return Ok(applicant);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateApplicant([FromBody] ApplicantForRegistrationDto applicantForRegistrationDto)
    {
        var createdApplicant = await _service.ApplicantService.CreateApplicantAsync(applicantForRegistrationDto);
        return CreatedAtRoute("GetApplicantById", new { id = createdApplicant.Id },
        createdApplicant);
    }
}

