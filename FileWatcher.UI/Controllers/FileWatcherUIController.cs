using FileWatcher.UI.Model;
using FileWatcher.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileWatcher.UI.Controllers
{
    public class FileWatcherUIController : Controller
    {
        private readonly IFileWatcherUIServices _fileWatcherUIService;
        private readonly IConfiguration _configuration;

        public FileWatcherUIController(IFileWatcherUIServices fileWatcherUIService, IConfiguration configuration)
        {
            _fileWatcherUIService = fileWatcherUIService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.DefaultPath = _configuration.GetValue<string>("FolderSetting:FolderPath");
            return View();
        }
        
        public IActionResult ShowFiles()
        {
            return View();
        }

        public IActionResult Details(string fileType)
        {
            ViewBag.FileInfo = fileType;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public async Task<ActionResult> ProcessFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return Json("Error");
            }
            List<FileDetails> fileDetails = await _fileWatcherUIService.ProcessFolder(folderPath);
            return Json(fileDetails);
        }

        public async Task<ActionResult> GetFileTypeAndCount()
        {
            List<FileDetails> fileDetails = await _fileWatcherUIService.GetFileTypeAndCount();
            return Json(fileDetails);
        }

        public async Task<ActionResult> GetFileMetadata(string fileType)
        {
            List<FileInfoMetadata> fileInfo = await _fileWatcherUIService.GetFilesByType(fileType);
            return Json(fileInfo);
        }
    }
}
