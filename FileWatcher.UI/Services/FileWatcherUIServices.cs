using FileWatcher.UI.Model;
using Newtonsoft.Json;
using System.Web;

namespace FileWatcher.UI.Services
{
    public class FileWatcherUIServices : IFileWatcherUIServices
    {
        private readonly IConfiguration _configuration;

        public FileWatcherUIServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<FileInfoMetadata>> GetFilesByType(string fileType)
        {
            List<FileInfoMetadata> fileInfo = new();
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("BaseURL:BaseURLPath"));
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync($"api/FileWatcherAPI/GetFilesByType/{fileType}");

                if (Res.IsSuccessStatusCode)
                {
                    var fileResponse = Res.Content.ReadAsStringAsync().Result;
                    if (fileResponse != null)
                    {
                        fileInfo = JsonConvert.DeserializeObject<List<FileInfoMetadata>>(fileResponse) ?? new List<FileInfoMetadata>();
                    }
                }
            }

            return fileInfo ??= new List<FileInfoMetadata>();
        }

        public async Task<List<FileDetails>> GetFileTypeAndCount()
        {
            List<FileDetails> fileDetails = new();
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("BaseURL:BaseURLPath"));
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync($"api/FileWatcherAPI/GetFileCountByType");

                if (Res.IsSuccessStatusCode)
                {
                    var fileResponse = Res.Content.ReadAsStringAsync().Result;
                    fileDetails = JsonConvert.DeserializeObject<List<FileDetails>>(fileResponse) ?? new List<FileDetails>();
                }
            }

            return fileDetails;
        }

        public async Task<List<FileDetails>> ProcessFolder(string folderPath)
        {
            List<FileDetails> fileDetails = new();
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri("https://localhost:7129");
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage Res = await client.GetAsync($"api/FileWatcherAPI/ProcessFiles/{HttpUtility.UrlEncode(folderPath)}");

                if (Res.IsSuccessStatusCode)
                {
                    var fileResponse = Res.Content.ReadAsStringAsync().Result;
                    fileDetails = JsonConvert.DeserializeObject<List<FileDetails>>(fileResponse) ?? new List<FileDetails>();
                }
            }

            return fileDetails;
        }
    }
}
