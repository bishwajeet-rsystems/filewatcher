using FileWatcher.API.Model;
using FileWatcher.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileWatcher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileWatcherAPIController : ControllerBase
    {
        private readonly IFileWatcherAPIService _fileWatcherAPIService;
        public FileWatcherAPIController(IFileWatcherAPIService fileWatcherAPIService)
        {
            _fileWatcherAPIService = fileWatcherAPIService;
        }

        [HttpGet]
        [Route("ProcessFiles/{folderPath}")]
        public IEnumerable<FileDetails> GetFileDetails(string folderPath)
        {
            return _fileWatcherAPIService.ProcessFiles(folderPath);
        }

        [HttpGet]
        [Route("GetFileCountByType")]
        public IEnumerable<FileDetails> GetFileCountByType()
        {
            return _fileWatcherAPIService.GetFileCountByType();
        }

        [HttpGet]
        [Route("GetFilesByType/{fileExtension}")]
        public IEnumerable<FileInfoMetadata> GetFilesByType(string fileExtension)
        {
            return _fileWatcherAPIService.GetFilesByType(fileExtension);
        }
    }
}
