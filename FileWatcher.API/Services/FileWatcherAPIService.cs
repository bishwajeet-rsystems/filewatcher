using FileWatcher.API.Controllers;
using FileWatcher.API.Model;

namespace FileWatcher.API.Services
{
    /// <summary>
    /// This class is injected as Singleton to mimic a storage.
    /// </summary>
    public class FileWatcherAPIService : IFileWatcherAPIService
    {
        /// <summary>
        /// The log category.
        /// </summary>
        private const string LogCategory = nameof(FileWatcherAPIService);
        private readonly ILogger<FileWatcherAPIController> _logger;
        private const string FileFetchingError = "Error while fetching file details";
        private const string DirectoryNotFoundError = "Directory was not found";
        private const string DirectoryAccessibleInfo = "Directory accessible, fetching files.";

        /// <summary>
        /// Currently this variable holds files metadata of selected folder. This is reset on every process method called.
        /// </summary>
        List<FileInfoMetadata> lstFileInfoMetadata = new();

        /// <summary>
        /// FileWatcherAPIService
        /// </summary>
        public FileWatcherAPIService(ILogger<FileWatcherAPIController> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public List<FileDetails> GetFileCountByType()
        {
            try
            {
                List<FileDetails>? fileDetails = null;

                // Group by files with an extension
                var result = lstFileInfoMetadata.Select(f => f.Type.TrimStart('.').ToLower())
                                 .GroupBy(y => y, (ex, excnt) => new
                                 {
                                     Extension = ex,
                                     Count = excnt.Count()
                                 });

                // Prepare the final result 
                fileDetails = new(result.Count());
                foreach (var i in result)
                {
                    var fileByType = new FileDetails
                    {
                        FileType = i.Extension,
                        Count = i.Count
                    };

                    fileDetails.Add(fileByType);
                }
                return fileDetails;
            }
            catch (Exception)
            {
                _logger.LogError(LogCategory, FileFetchingError);
                throw new Exception(FileFetchingError);
            }
        }

        /// <inheritdoc/>
        public List<FileInfoMetadata> GetFilesByType(string fileType)
        {
            // Filter files by extension
            return lstFileInfoMetadata.Where(f => f.Type.TrimStart('.').ToLower() == fileType.ToLower()).ToList();
        }

        /// <inheritdoc/>
        public List<FileDetails> ProcessFiles(string folderPath)
        {
            try
            {
                // Read Files from Folder 
                if (!Directory.Exists(folderPath))
                {
                    _logger.LogError(LogCategory, DirectoryNotFoundError);
                    throw new ArgumentException(DirectoryNotFoundError);
                }

                _logger.LogInformation(DirectoryAccessibleInfo);


                // Save Files in Collection
                List<FileInfoMetadata> fileByTypes = new();
                DirectoryInfo dir = new(folderPath);

                // Filter files by extension
                var files = dir.GetFiles();

                // Prepare the final result 
                lstFileInfoMetadata = new List<FileInfoMetadata>();
                foreach (var i in files)
                {
                    var fileByType = new FileInfoMetadata
                    {
                        FileName = Path.GetFileName(i.FullName),
                        Size = $"{Math.Round(((i.Length / 1024f) / 1024f), 2)} MB",
                        CreatedDate = i.CreationTimeUtc.ToShortDateString(),
                        ModifiedDate = i.LastWriteTimeUtc.ToShortDateString(),
                        Type = Path.GetExtension(i.FullName)
                    };

                    lstFileInfoMetadata.Add(fileByType);
                }

                // Return Collection mof FileTypeByCount
                return GetFileCountByType();


            }
            catch (Exception)
            {
                _logger.LogError(LogCategory, FileFetchingError);
                throw new Exception(FileFetchingError);
            }
        }
    }
}
