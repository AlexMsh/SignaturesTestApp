// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Signatures.Domain.Dependencies;
using Signatures.Repositories.Extensions;
using Signatures.Signant.Client.Proxy.Extensions;
using Signatures.StatusUpdateJob;

await Host.CreateDefaultBuilder(args)
                .UseConsoleLifetime()
                .ConfigureServices((hostContext, services) =>
                {

                    services.RegisterRepositories();
                    services.RegisterRepositoryConfigurations(hostContext.Configuration);
                    services.AddDomainServices();
                    services.AddDomainConfiguration(hostContext.Configuration);
                    services.AddAutoMapper();
                    services.AddSignantClient();
                    services.AddHelpersConfiguration(hostContext.Configuration);
                    services.AddHelpers();
                    services.AddSignantConfiguration(hostContext.Configuration);

                    services.AddHostedService<StatusUpdatorJob>();
                    services.AddSingleton<IStatusUpdateService, StatusUpdateService>();
                    services.AddSingleton(Console.Out);
                }).RunConsoleAsync();
Console.WriteLine("Hello, World!");
