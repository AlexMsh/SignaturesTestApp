using FluentValidation;
using Signarutes.Domain.Contracts.models.Request;
using Sigtatures.Web.Validation;

namespace Sigtatures.Web.Dependencies
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection collection)
        {
            collection.AddScoped<IValidator<SignatureRequest>, SignatureRequestValidator>();
            return collection;
        }
    }
}
