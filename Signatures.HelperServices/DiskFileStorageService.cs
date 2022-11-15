using Microsoft.Extensions.Options;
using Signarutes.Domain.Contracts.Configuration;

namespace Signatures.HelperServices
{
    public class DiskFileStorageService : IFileStorageService
    {
        private readonly FileStorageConfiguration _fileStorageConfiguration;

        public DiskFileStorageService(
            IOptions<FileStorageConfiguration> fileStorageConfiguration)
        {
            _fileStorageConfiguration = fileStorageConfiguration.Value;
        }

        public async Task<Stream> GetFileContent(string fileName)
        {
            string filePath = Path.Combine(_fileStorageConfiguration.Path, fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            var pdfBytes = await File.ReadAllBytesAsync(filePath);
            var stream = new MemoryStream(pdfBytes);
            return stream;
        }
    }
}
