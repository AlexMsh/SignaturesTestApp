using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Signarutes.Domain.Contracts;
using Signarutes.Domain.Contracts.Configuration;

namespace Signatures.Domain.Dependencies
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection collection)
        {
            collection.AddScoped<IDocumentSignatureService, DocumentSignatureService>();
            return collection;
        }

        public static IServiceCollection AddDomainConfiguration(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.Configure<SignatureRequestConfiguration>(configuration.GetSection("SignatureRequestSettings"));
            return collection;
        }
    }
}
