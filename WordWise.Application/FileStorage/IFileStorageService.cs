using Microsoft.AspNetCore.Http;

namespace WordWise.Application.FileStorage;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string subFolder = "products", CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string filePath, CancellationToken cancellationToken = default);
    Task<string> UpdateFileAsync(IFormFile newFile, string oldFilePath, string subFolder = "products", CancellationToken cancellationToken = default);
}
