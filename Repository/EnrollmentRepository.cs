
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
{
    private readonly RepositoryContext _context;
    public EnrollmentRepository(RepositoryContext repositoryContext) : base(repositoryContext) { _context = repositoryContext; }
    public void CreateEnrollment(Enrollment enrollment)
    {
        Create(enrollment);
    }

    public void DeleteEnrollment(Enrollment enrollment)
    {
        Delete(enrollment);
    }

    public async Task<Enrollment> GetEnrollmentAsync(Guid enrollmentId, bool trackChanges)
    {
        return await FindByCondition(e => e.Id.Equals(enrollmentId), trackChanges)
            .Include(e => e.Program)
               .Include(e => e.Applicant.User)
                  .Include(e => e.Faculty)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsAsync(
    EnrollmentParameters p, bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Include(e => e.Program)
            .Include(e => e.Applicant.User)
                  .Include(e => e.Faculty)
            .FilterEnrollments(
                p.ApplicantId, p.ProgramId, p.UniversityId, p.FacultyId,
                p.AcademicYear, p.StudentNumber, p.Status, p.YearLevel)
            .ToListAsync();
    }
    public async Task<string> GenerateStudentNumberAsync()
    {
        var year = DateTime.UtcNow.Year;

        await _context.Database.OpenConnectionAsync();
        try
        {
            var connection = _context.Database.GetDbConnection();

            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT NEXT VALUE FOR StudentNumberSeq";

            var currentTx = _context.Database.CurrentTransaction;
            if (currentTx is not null)
                command.Transaction = currentTx.GetDbTransaction();

            var result = await command.ExecuteScalarAsync();
            var next = Convert.ToInt32(result);

            return $"STU-{year}-{next:D5}";
        }
        finally
        {
            await _context.Database.CloseConnectionAsync();
        }
    }
    public async Task<IEnumerable<Enrollment>> GetEnrollmentsForCourseAsync(
    Guid courseId, bool trackChanges)
    {
        return await FindByCondition(e =>
                e.Status == "Active" &&
                e.Program.ProgramCourses.Any(pc => pc.CourseId == courseId),
                trackChanges)
            .Include(e => e.Applicant.User)
            .Include(e => e.Program)
            .ToListAsync();
    }
}
