using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects;

namespace Repository;
public class UniversityRepository : RepositoryBase<University>, IUniversityRepository
{
    public UniversityRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }
    public void CreateUniversity(University university)
    {
        Create(university);
    }

    public void DeleteUniversity(University university)
    {
        Delete(university);
    }

    public async Task<IEnumerable<University>> GetAllUniversitiesAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderByDescending(u => u.Id)
            .ToListAsync();
    }

    public async Task<University> GetUniversityAsync(Guid univeristyId, bool trackChanges)
    {
        return await FindByCondition(u => u.Id.Equals(univeristyId), trackChanges)
            .SingleOrDefaultAsync();
    }
    public async Task<University> GetUniversityByAdminIdAsync(string adminId, bool trackChanges)
    {
        return await FindByCondition(u => u.AdminId.Equals(adminId), trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await FindByCondition(u => u.Email == email, false)
            .AnyAsync();
    }

    public async Task<bool> NameExistsAsync(string name)
    {
        return await FindByCondition(u => u.Name == name, false)
            .AnyAsync();
    }

    public async Task<bool> PhoneNumberExistsAsync(string? phoneNumber)
    {
        return await FindByCondition(u => u.PhoneNumber != null && u.PhoneNumber == phoneNumber, false)
            .AnyAsync();
    }
    public async Task<List<string>> CheckForDuplicatesAsync(
       string email,
       string name,
       string? phoneNumber)
    {
        var duplicateFields = new List<string>();

        if (await EmailExistsAsync(email))
            duplicateFields.Add(nameof(University.Email));

        if (await NameExistsAsync(name))
            duplicateFields.Add(nameof(University.Name));

        if (await PhoneNumberExistsAsync(phoneNumber))
            duplicateFields.Add(nameof(University.PhoneNumber));

        return duplicateFields;
    }
}

