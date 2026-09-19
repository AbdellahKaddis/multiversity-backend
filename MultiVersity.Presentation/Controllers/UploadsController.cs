



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MultiVersity.Presentation.Controllers;


[ApiController]
[Route("api/uploads")]

[Authorize]
public class UploadsController : ControllerBase
{
    private readonly FileStorageService _files;
    public UploadsController(FileStorageService files) => _files = files;

    [HttpPost("photo")]
    [RequestSizeLimit(2 * 1024 * 1024)]
    public async Task<IActionResult> UploadPhoto(IFormFile file)
    {
        var url = await _files.SaveImageAsync(file, "photos");   
        return Ok(new { url });
    }

    [HttpPost("pdf")]
    [RequestSizeLimit(5 * 1024 * 1024)]
    public async Task<IActionResult> UploadPdf(IFormFile file)
    {
        var url = await _files.SavePdfAsync(file, "applications"); 
        return Ok(new { url });
    }

    [HttpPost("university-logo")]
    [RequestSizeLimit(1 * 1024 * 1024)]
    public async Task<IActionResult> UploadUniversityLogo(IFormFile file)
    {
        var url = await _files.SaveImageAsync(file, "university-logos");
        return Ok(new { url });
    }
}