

namespace Service.Contracts;

public interface IFileStorageService
{
    /// <summary>
    /// Moves a file from the temp folder to a permanent folder
    /// and returns the new URL. Returns the input unchanged if null/empty.
    /// </summary>
    string Promote(string tempUrl, string targetFolder);

    /// <summary>
    /// Deletes a file by its URL. Safe if the file doesn't exist.
    /// </summary>
    void Delete(string url);
}
