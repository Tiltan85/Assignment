using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class FileService(IFileRepository fileRepository) : IFileService
{
    private readonly IFileRepository _fileRepository = fileRepository;
 
    public FileResult GetContentFromFile(string path)
    {
        try
        {
            var exists = _fileRepository.FileExists(path);
            if (exists)
            {
                var content = _fileRepository.GetFileContent(path);
                return new FileResult { Succeeded = true, Content = content };
            }
            
            return new FileResult { Succeeded = false, Error = "File not found." };

        }
        catch (Exception ex)
        {
            return new FileResult { Succeeded = false, Error = ex.Message };
        }
    }

    public FileResult SaveContentToFile(string path, string content)
    {
        try
        {
            if (_fileRepository.SaveFileContent(path, content))
                return new FileResult { Succeeded = true };

            return new FileResult { Succeeded = false, Content = content, Error = "Failed to save content." };
        }
        catch (Exception ex)
        {
            return new FileResult { Succeeded = false, Error = ex.Message };
        }
    }
}
