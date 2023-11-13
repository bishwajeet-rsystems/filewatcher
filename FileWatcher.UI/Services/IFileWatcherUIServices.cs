using FileWatcher.UI.Model;

namespace FileWatcher.UI.Services
{
    public interface IFileWatcherUIServices
    {
        /// <summary>
        /// ProcessFolder fetch the data from API based on specified folder.
        /// </summary>
        /// <param name="FolderName"></param>
        /// <returns></returns>
        public Task<List<FileDetails>> ProcessFolder(string FolderName);

        /// <summary>
        /// GetFileTypeAndCount fetch the data from API based on file type and calculates count.
        /// </summary>
        /// <returns></returns>
        public Task<List<FileDetails>> GetFileTypeAndCount();

        /// <summary>
        /// GetFilesByType fetch the data from API based on fileType i.e extension given by end user.
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public Task<List<FileInfoMetadata>> GetFilesByType(string fileType);
    }
}
