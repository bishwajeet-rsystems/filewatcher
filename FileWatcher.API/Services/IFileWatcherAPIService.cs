using FileWatcher.API.Model;

namespace FileWatcher.API.Services
{
    public interface IFileWatcherAPIService
    {
        /// <summary>
        /// ProcessFiles is used to get all files from specified folder.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public List<FileDetails> ProcessFiles(string folderPath);

        /// <summary>
        /// GetFileCountByType filters and group by files by type and count.
        /// </summary>
        /// <returns></returns>
        public List<FileDetails> GetFileCountByType();

        /// <summary>
        /// GetFilesByType takes file type i.e. extension and filters file list.
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public List<FileInfoMetadata> GetFilesByType(string fileType);
    }
}
