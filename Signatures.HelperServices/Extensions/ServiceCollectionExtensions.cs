using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Signarutes.Domain.Contracts.Configuration;
using Signatures.HelperServices;

namespace Signatures.Domain.Dependencies
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHelpersConfiguration(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.Configure<FileStorageConfiguration>(configuration.GetSection("FileStorage"));
            return collection;
        }

        public static IServiceCollection AddHelpers(this IServiceCollection collection)
        {
            collection.AddScoped<IFileStorageService, DiskFileStorageService>();
            return collection;
        }
    }
}
