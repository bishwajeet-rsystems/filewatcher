using FileWatcher.API.Controllers;
using FileWatcher.API.Model;
using FileWatcher.API.Services;
using NSubstitute;

namespace FileWatcher.Test
{
    public class FileWatcherTests
    {
        private IFileWatcherAPIService fileWatcherAPIService;

        public FileWatcherTests()
        {
            fileWatcherAPIService = Substitute.For<IFileWatcherAPIService>();
        }

        [Fact]
        public void FileProcess_Should_Return_List()
        {
            // Arrange
            fileWatcherAPIService.ProcessFiles(string.Empty).Returns(FileDetails());
            var controller = new FileWatcherAPIController(fileWatcherAPIService);

            // Act
            var actionResult = controller.GetFileDetails(string.Empty);

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void FileDetails_Should_Return_List_By_Type()
        {
            // Arrange
            fileWatcherAPIService.GetFilesByType("docx").Returns(InfoMetadata());
            var controller = new FileWatcherAPIController(fileWatcherAPIService);

            // Act
            var actionResult = controller.GetFilesByType("docx");

            // Assert
            Assert.True(actionResult.Count() == 2);
        }

        public static List<FileDetails> FileDetails()
        {
            List<FileDetails> result = new List<FileDetails>();
            result.Add(new FileDetails { FileType = "docx", Count = 1 });
            return result;
        }

        public static List<FileInfoMetadata> InfoMetadata()
        {
            List<FileInfoMetadata> result = new List<FileInfoMetadata>();
            result.Add(new FileInfoMetadata { FileName = "MockTest1.docx", Size = "2000", CreatedDate = DateTime.UtcNow.ToShortDateString(), ModifiedDate = DateTime.UtcNow.ToShortDateString() });
            result.Add(new FileInfoMetadata { FileName = "MockTest2.docx", Size = "3000", CreatedDate = DateTime.UtcNow.ToShortDateString(), ModifiedDate = DateTime.UtcNow.ToShortDateString() });
            return result;
        }
    }
}