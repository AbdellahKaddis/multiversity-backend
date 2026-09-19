using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiVersity.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace MultiVersity.Presentation.Controllers;

[Route("api/faculties/{facultyId}/departments")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IServiceManager _service;
    public DepartmentsController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartmentsForFaculty(Guid facultyId)
    {
        var departments = await _service.DepartmentService.GetDepartmentsAsync(facultyId, trackChanges: false);
        return Ok(departments);
    }

    [HttpGet("{id:guid}", Name = "GetDepartmentForFaculty")]

    public async Task<IActionResult> GetDepartmentForFaculty(Guid facultyId, Guid id)
    {
        var department = await _service.DepartmentService.GetDepartmentAsync(facultyId, id,
        trackChanges: false);
        return Ok(department);
    }
    [Authorize]
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateDepartmentForFaculty(Guid facultyId, [FromBody]
    DepartmentForCreationDto department)
    {
        var departmentToReturn = await _service.DepartmentService
            .CreateDepartmentForFacultyAsync(facultyId, department, trackChanges: false);

        return CreatedAtRoute("GetDepartmentForFaculty", new
        {
            facultyId,
            id = departmentToReturn.Id
        },
        departmentToReturn);
    }
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDepartmentForFaculty(Guid facultyId, Guid id)
    {
        await _service.DepartmentService.DeleteDepartmentForFacultyAsync(facultyId, id, trackChanges:
        false);
        return NoContent();
    }
    [Authorize]
    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateDepartmentForFaculty(Guid facultyId, Guid id,
[FromBody] DepartmentForUpdateDto department)
    {

        await _service.DepartmentService.UpdateDepartmentForFacultyAsync(facultyId, id, department,
         facTrackChanges: false, depTrackChanges: true);
        return NoContent();
    }
}
