
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace MultiVersity;

public class FileReconciliationService : BackgroundService
{
    private readonly IWebHostEnvironment _env;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<FileReconciliationService> _logger;

    private static readonly TimeSpan GracePeriod = TimeSpan.FromHours(24);
    private static readonly TimeSpan Interval = TimeSpan.FromHours(6);

    public FileReconciliationService(
        IWebHostEnvironment env,
        IServiceScopeFactory scopeFactory,
        ILogger<FileReconciliationService> logger)
    {
        _env = env;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        // initial delay so we don't run during startup
        await Task.Delay(TimeSpan.FromMinutes(2), ct);

        while (!ct.IsCancellationRequested)
        {
            try
            {
                await ReconcileAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File reconciliation failed.");
            }

            await Task.Delay(Interval, ct);
        }
    }

    private async Task ReconcileAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<RepositoryContext>();

        // 1. Collect all referenced URLs from the DB
        var photoUrls = await db.Applicants
            .Where(a => a.PhotoUrl != null)
            .Select(a => a.PhotoUrl!)
            .ToListAsync(ct);

        var fileUrls = await db.Applications
            .Where(a => a.FileUrl != null)
            .Select(a => a.FileUrl!)
            .ToListAsync(ct);

        var referenced = new HashSet<string>(
            photoUrls.Concat(fileUrls).Select(Normalize),
            StringComparer.OrdinalIgnoreCase);

        // 2. Scan the two folders and delete unreferenced files older than 24h
        var root = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var cutoff = DateTime.UtcNow - GracePeriod;

        foreach (var folder in new[] { "photos", "applications" })
        {
            var dir = Path.Combine(root, "uploads", folder);
            if (!Directory.Exists(dir)) continue;

            foreach (var path in Directory.GetFiles(dir))
            {
                if (File.GetLastWriteTimeUtc(path) > cutoff) continue;  // too new — skip
                var url = $"/uploads/{folder}/{Path.GetFileName(path)}";
                if (!referenced.Contains(Normalize(url)))
                {
                    _logger.LogInformation("Deleting orphan file {Url}", url);
                    File.Delete(path);
                }
            }
        }
    }

    private static string Normalize(string url) =>
        url.Replace('\\', '/').TrimEnd('/').ToLowerInvariant();
}

