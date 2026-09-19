using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public class GradeService : IGradeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public GradeService(
        IRepositoryManager repository,
        ILoggerManager logger,
        IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GradeDto> CreateGrade(GradeForCreationDto gradeForCreationDto)
    {
        await CheckIfEnrollmentExists(gradeForCreationDto.EnrollmentId, false);
        await CheckIfCourseExists(gradeForCreationDto.CourseId, false);

        var gradeEntity = _mapper.Map<Grade>(gradeForCreationDto);

        gradeEntity.Validated = gradeForCreationDto.Score >= 10;
        gradeEntity.IsPublished = false;

        _repository.Grade.CreateGrade(gradeEntity);

        await _repository.SaveAsync();

        return _mapper.Map<GradeDto>(gradeEntity);

    }

    public async Task DeleteGrade(Guid gradeId, bool trackChanges)
    {
        var gradeEntity = await GetGradeAndCheckIfItExistsAsync(gradeId, trackChanges);

        _repository.Grade.DeleteGrade(gradeEntity);
        await _repository.SaveAsync();
    }

    public async Task<GradeDto> GetGradeAsync(Guid gradeId, bool trackChanges)
    {
        var gradeEntity = await GetGradeAndCheckIfItExistsAsync(gradeId, trackChanges);
        return _mapper.Map<GradeDto>(gradeEntity);
    }

    public async Task<IEnumerable<GradeDto>> GetGradesAsync(
        GradeParameters gradeParameters, bool trackChanges)
    {
        var grades = await _repository.Grade.GetGradesAsync(gradeParameters, trackChanges);
        return _mapper.Map<IEnumerable<GradeDto>>(grades);
    }

    public async Task UpdateGrade(
        Guid gradeId,
        GradeForUpdateDto gradeForUpdateDto,
        bool trackChanges)
    {
        var gradeEntity = await GetGradeAndCheckIfItExistsAsync(gradeId, trackChanges);

        _mapper.Map(gradeForUpdateDto, gradeEntity);

        gradeEntity.Validated = gradeForUpdateDto.Score >= 10;

        await _repository.SaveAsync();
    }

    public async Task PublishGradesAsync(PublishGradesDto dto, bool trackChanges)
    {
        await CheckIfCourseExists(dto.CourseId, false);

        var grades = await _repository.Grade.GetGradesForPublishAsync(
            dto.CourseId.Value, dto.AcademicYear!, dto.Semester!, dto.Session!,
            trackChanges);

        if (!grades.Any())
            throw new NoGradesToPublishException(
                dto.CourseId.Value, dto.AcademicYear!, dto.Semester!, dto.Session!);

        foreach (var grade in grades)
        {
            grade.IsPublished = true;
        }

        await _repository.SaveAsync();
    }

    public async Task<SemesterResultDto> GetSemesterResultAsync(
    Guid enrollmentId, string semester, bool trackChanges)
    {
        // Load all published grades for this enrollment + semester
        var parameters = new GradeParameters
        {
            EnrollmentId = enrollmentId,
            Semester = semester,
            IsPublished = true,
        };

        var grades = (await _repository.Grade
            .GetGradesAsync(parameters, trackChanges)).ToList();

        // ── 1. Merge Normale + Rattrapage per course ──
        var courseMap = new Dictionary<Guid, CourseGradeDto>();

        foreach (var g in grades)
        {
            if (!courseMap.TryGetValue((Guid)g.CourseId, out var c))
            {
                c = new CourseGradeDto
                {
                    CourseId = (Guid)g.CourseId,
                    CourseCode = g.Course.Code,
                    CourseName = g.Course.Title,
                    Coefficient = g.Course.Coefficient,
                    Credits = g.Course.Credits,
                };
                courseMap[(Guid)g.CourseId] = c;
            }

            if (g.Session == "Rattrapage")
                c.RattrapageScore = g.Score;
            else
                c.NormalScore = g.Score;
        }

        // ── 2. Per-course computation ──
        foreach (var c in courseMap.Values)
        {
            c.HasNormal = c.NormalScore.HasValue;
            c.HasRattrapage = c.RattrapageScore.HasValue;

            c.NeedsRattrapage = c.HasNormal && c.NormalScore < 10;

            bool validatedByNormal = c.HasNormal && c.NormalScore >= 10;
            bool validatedByRattrapage = c.NeedsRattrapage
                                         && c.HasRattrapage
                                         && c.RattrapageScore >= 10;

            if (validatedByNormal)
                c.EffectiveScore = c.NormalScore;
            else if (c.HasNormal && c.HasRattrapage)
                c.EffectiveScore = Math.Max(c.NormalScore!.Value, c.RattrapageScore!.Value);
            else
                c.EffectiveScore = null;

            c.HasFinal = c.EffectiveScore.HasValue;

            if (validatedByNormal) c.ValidatedBy = "Normale";
            else if (validatedByRattrapage) c.ValidatedBy = "Rattrapage";
            else c.ValidatedBy = null;

            c.Validated = validatedByNormal || validatedByRattrapage;
        }

        var courses = courseMap.Values
            .OrderBy(c => c.CourseCode)
            .ToList();

        // ── 3. Semester average (only courses with a resolved final) ──
        var scored = courses.Where(c => c.HasFinal).ToList();

        decimal? average = null;
        if (scored.Count > 0)
        {
            var totalPoints = scored.Sum(c => c.EffectiveScore!.Value * c.Coefficient);
            var totalCoef = scored.Sum(c => c.Coefficient);
            average = totalCoef > 0 ? totalPoints / totalCoef : 0;
        }

        // ── 4. Compensation ──
        bool compensationApplies = average.HasValue && average >= 10;

        if (compensationApplies)
        {
            foreach (var c in courses)
            {
                if (c.HasFinal && c.ValidatedBy == null)
                {
                    c.ValidatedBy = "Compensation";
                    c.Validated = true;
                }
            }
        }

        // ── 5. Summary ──
        int creditsEarned = courses
            .Where(c => c.Validated)
            .Sum(c => (int)c.Credits);

        int coursesPassed = courses.Count(c => c.Validated);

        string decision = !average.HasValue ? "—"
                        : average >= 10 ? "Passed"
                        : "Failed";

        return new SemesterResultDto
        {
            Semester = semester,
            Average = average,
            CreditsEarned = creditsEarned,
            CoursesPassed = coursesPassed,
            TotalCourses = courses.Count,
            Decision = decision,
            Courses = courses,
        };
    }

    private async Task CheckIfEnrollmentExists(Guid? enrollmentId, bool trackChanges)
    {
        var enrollment = await _repository.Enrollment
            .GetEnrollmentAsync(enrollmentId.Value, trackChanges);

        if (enrollment is null)
            throw new EnrollmentNotFoundException(enrollmentId.Value);
    }

    private async Task CheckIfCourseExists(Guid? courseId, bool trackChanges)
    {
        var course = await _repository.Course.GetCourseAsync(courseId.Value, trackChanges);

        if (course is null)
            throw new CourseNotFoundException(courseId.Value);
    }

    private async Task<Grade> GetGradeAndCheckIfItExistsAsync(Guid gradeId, bool trackChanges)
    {
        var gradeDb = await _repository.Grade.GetGradeAsync(gradeId, trackChanges);

        if (gradeDb is null)
            throw new GradeNotFoundException(gradeId);

        return gradeDb;
    }
}