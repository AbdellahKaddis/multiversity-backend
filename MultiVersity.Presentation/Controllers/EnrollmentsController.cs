using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;


namespace MultiVersity.Presentation.Controllers;

[Route("api/enrollments")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    public readonly IServiceManager _service;
    public EnrollmentsController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetEnrollments([FromQuery] EnrollmentParameters enrollmentParameters)
    {
        var enrollments = await _service.EnrollmentService.GetEnrollmentsAsync(enrollmentParameters, trackChanges: false);
        return Ok(enrollments);
    }

    [HttpGet("{id:guid}", Name = "GetEnrollmentById")]

    public async Task<IActionResult> GetEnrollment(Guid id)
    {
        var enrollment = await _service.EnrollmentService.GetEnrollmentAsync(id, trackChanges: false);
        return Ok(enrollment);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateEnrollment([FromBody] EnrollmentForCreationDto dto)
    {

        var createdEnrollment = await _service.EnrollmentService.CreateEnrollment(dto);
        return CreatedAtRoute("GetEnrollmentById", new { id = createdEnrollment.Id },
        createdEnrollment);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEnrollment(Guid id)
    {
        await _service.EnrollmentService.DeleteEnrollment(id, trackChanges: false);
        return NoContent();
    }
    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateEnrollment(Guid id, [FromBody] EnrollmentForUpdateDto dto)
    {

        await _service.EnrollmentService.UpdateEnrollment(id, dto, trackChanges: true);
        return NoContent();
    }
}
