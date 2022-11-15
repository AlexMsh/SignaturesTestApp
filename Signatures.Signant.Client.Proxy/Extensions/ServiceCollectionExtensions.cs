using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Signatures.Client.Proxy.Contracts;
using Signatures.Signant.Client.Proxy.Configuration;
using Signatures.Signant.Client.Proxy.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signatures.Signant.Client.Proxy.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection collection)
        {

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SignatureRequestSignantProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            collection.AddSingleton(mapper);

            return collection;
        }

        public static IServiceCollection AddSignantClient(this IServiceCollection collection)
        {
            collection.AddScoped<IDocumentSignatureProxy, SignantDocumentSignatureProxy>();
            return collection;
        }

        public static IServiceCollection AddSignantConfiguration(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.Configure<SignantConfiguration>(configuration.GetSection("SignantConfiguration"));
            return collection;
        }
    }
}
