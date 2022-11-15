using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Signarutes.Domain.Contracts.models.Response;
using Signatures.Repositories.Contracts;
using Signatures.Repositories.Contracts.Configuration;

namespace Signatures.Repositories.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IRepository<SignedDocData>, MongoRepository<SignedDocData>>();
            return collection;
        }

        public static IServiceCollection RegisterRepositoryConfigurations(this IServiceCollection collection, IConfiguration config)
        {
            collection.Configure<DbConnectionConfiguration>(config.GetSection("DbConnectionConfiguration"));
            return collection;
        }
    }
}
