using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IGradeRepository
{
    void CreateGrade(Grade grade);
    void DeleteGrade(Grade grade);

    Task<Grade> GetGradeAsync(Guid gradeId, bool trackChanges);
    Task<IEnumerable<Grade>> GetGradesAsync(GradeParameters p, bool trackChanges);
    Task<IEnumerable<Grade>> GetGradesForPublishAsync(
    Guid courseId, string academicYear, string semester, string session,
    bool trackChanges);
}