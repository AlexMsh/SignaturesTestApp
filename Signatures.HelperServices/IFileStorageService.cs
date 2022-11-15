namespace Signatures.HelperServices
{
    public interface IFileStorageService
    {
        Task<Stream> GetFileContent(string fileName);
    }
}