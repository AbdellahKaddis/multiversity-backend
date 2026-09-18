using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiVersity.Presentation;


public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;

    private static readonly string[] AllowedImageTypes = { "image/jpeg", "image/png" };
    private const long MaxImageSize = 2 * 1024 * 1024;   // 2 MB
    private const long MaxPdfSize = 5 * 1024 * 1024;   // 5 MB

    public FileStorageService(IWebHostEnvironment env) => _env = env;

    // ---------- Upload (used only by UploadsController) ----------

    public Task<string> SaveImageAsync(IFormFile file, string folder)
        => SaveAsync(file, folder, AllowedImageTypes, MaxImageSize);

    public Task<string> SavePdfAsync(IFormFile file, string folder)
        => SaveAsync(file, folder, new[] { "application/pdf" }, MaxPdfSize);

    private async Task<string> SaveAsync(
        IFormFile file, string folder, string[] allowedTypes, long maxSize)
    {
        if (file is null || file.Length == 0)
            throw new InvalidOperationException("File is empty.");

        if (!allowedTypes.Contains(file.ContentType))
            throw new InvalidOperationException(
                $"Only {string.Join(", ", allowedTypes)} allowed.");

        if (file.Length > maxSize)
            throw new InvalidOperationException(
                $"Max size is {maxSize / 1024 / 1024} MB.");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid()}{ext}";

        var root = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var dir = Path.Combine(root, "uploads", folder);
        Directory.CreateDirectory(dir);

        var fullPath = Path.Combine(dir, fileName);
        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/uploads/{folder}/{fileName}";
    }

    // ---------- IFileStorageService (used by Service layer) ----------

    public string Promote(string tempUrl, string targetFolder)
    {
        if (string.IsNullOrWhiteSpace(tempUrl)) return tempUrl;

        var root = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var fileName = Path.GetFileName(tempUrl);
        var srcPath = Path.Combine(root, tempUrl.TrimStart('/'));
        var dstDir = Path.Combine(root, "uploads", targetFolder);
        var dstPath = Path.Combine(dstDir, fileName);

        Directory.CreateDirectory(dstDir);
        if (File.Exists(srcPath)) File.Move(srcPath, dstPath);

        return $"/uploads/{targetFolder}/{fileName}";
    }

    public void Delete(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return;
        var root = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var fullPath = Path.Combine(root, url.TrimStart('/'));
        if (File.Exists(fullPath)) File.Delete(fullPath);
    }
}
