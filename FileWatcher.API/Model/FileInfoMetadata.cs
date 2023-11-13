namespace FileWatcher.API.Model
{
    /// <summary>
    /// FileInfoMetadata
    /// </summary>
    public class FileInfoMetadata
    {
        /// <summary>
        /// FileName
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public string? Size { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// CreatedDate
        /// </summary>
        public string? CreatedDate { get; set; }

        /// <summary>
        /// ModifiedDate
        /// </summary>
        public string? ModifiedDate { get; set; }
    }
}
